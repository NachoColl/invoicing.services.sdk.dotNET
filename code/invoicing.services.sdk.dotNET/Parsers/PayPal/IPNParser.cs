using invoicing.services.sdk.dotNET.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace invoicing.services.sdk.dotNET.Parsers.PayPal {
    public class IPNParser {

        public static Invoice Parse(string IPNMessage) {

            IPNProperties IPNValues = new IPNProperties(Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(IPNMessage));

            if (!(
                IPNValues.TRANSACTION.txn_type == IPNProperties.CONSTANTS.TransactionTypes.cart ||
                IPNValues.TRANSACTION.txn_type == IPNProperties.CONSTANTS.TransactionTypes.express_checkout ||
                IPNValues.PAYMENT.payment_status == IPNProperties.CONSTANTS.PaymentStatus.Refunded ||
                IPNValues.PAYMENT.payment_status == IPNProperties.CONSTANTS.PaymentStatus.Reversed ||
                (IPNValues.TRANSACTION.txn_type == IPNProperties.CONSTANTS.TransactionTypes.recurring_payment_profile_created && IPNValues.RECURRING.initial_payment_status == IPNProperties.CONSTANTS.PaymentStatus.Completed)
                 )) {

            }

            switch (IPNValues.TRANSACTION.txn_type) {

                case IPNProperties.CONSTANTS.TransactionTypes.cart:
                case IPNProperties.CONSTANTS.TransactionTypes.express_checkout:
                case IPNProperties.CONSTANTS.PaymentStatus.Refunded:
                case IPNProperties.CONSTANTS.PaymentStatus.Reversed:
                    return Parse_cart(IPNValues);
                case IPNProperties.CONSTANTS.TransactionTypes.recurring_payment_profile_created:
                    if (IPNValues.RECURRING.initial_payment_status != IPNProperties.CONSTANTS.PaymentStatus.Completed)
                        throw new Exception("This recurring payment does not have any completed payment.");
                    return Parse_recurring_payment_profile_created(IPNValues);
                case IPNProperties.CONSTANTS.TransactionTypes.recurring_payment:
                    return Parse_recurring_payment(IPNValues);
                default:
                    throw new Exception("This TXN_TYPE IPN message is not parsed for now!");
            }

        }

        private static Invoice Parse_recurring_payment_profile_created(IPNProperties IPNValues) {

            DateTime invoiceDate = DateTime.Now;
            try {
                invoiceDate = DateTime.ParseExact(IPNValues.RECURRING.time_created, "HH:mm:ss MMM dd, yyyy PST", CultureInfo.InvariantCulture);
            }
            catch { }

            Invoice invoice = new Invoice() {
                Dummy = IPNValues.TRANSACTION.dummy,
                Id = IPNValues.RECURRING.initial_payment_txn_id,
                Date = Utils.Timestamp.TimeToMillis(invoiceDate),
                CurrencyCode = IPNValues.RECURRING.currency_code,
                CountryCode = IPNValues.RECURRING.residence_country,
                Buyer = new Actor() {
                    Name = IPNValues.PAYER.FullName,
                    Line1 = IPNValues.PAYER.Address,
                    Line2 = IPNValues.PAYER.payer_email,
                    Line3 = IPNValues.RECURRING.residence_country
                },
                Items = new List<InvoiceItem>(){
                new InvoiceItem() {
                Name = IPNValues.RECURRING.product_name,
                ItemTotalAmount = IPNValues.RECURRING.initial_payment_amount
            } },
                Notes = new TextBlock() {
                    Line3 = "PayPal Subscription: " + IPNValues.RECURRING.recurring_payment_id
                }
            };



            // totals.
            InvoiceTotals totals = new InvoiceTotals() {
                Total = IPNValues.RECURRING.amount
            };
            invoice.Totals = totals;

            // taxes.
            if (IPNValues.PAYMENT.tax > decimal.Zero) {
                totals.TaxTotals = new List<Tax>() {
                    new Tax() {
                        TaxName = "Total Tax",
                        TaxTotal = IPNValues.PAYMENT.tax
                    }
                };
            }

            return invoice;
        }

        private static Invoice Parse_recurring_payment(IPNProperties IPNValues) {

            DateTime invoiceDate = DateTime.Now;
            try {
                invoiceDate = DateTime.ParseExact(IPNValues.PAYMENT.payment_date, "HH:mm:ss MMM dd, yyyy PST", CultureInfo.InvariantCulture);
            }
            catch { }

            Invoice invoice = new Invoice() {
                Dummy = IPNValues.TRANSACTION.dummy,
                Id = IPNValues.TRANSACTION.txn_id,
                Date = Utils.Timestamp.TimeToMillis(invoiceDate),
                CurrencyCode = IPNValues.PAYMENT.mc_currency,
                CountryCode = IPNValues.TRANSACTION.residence_country,
                Buyer = new Actor() {
                    Name = IPNValues.PAYER.FullName,
                    Line1 = IPNValues.PAYER.Address,
                    Line2 = IPNValues.PAYER.payer_email,
                    Line3 = IPNValues.RECURRING.residence_country
                },
                Items = new List<InvoiceItem>(){
                    new InvoiceItem() {
                    Name = IPNValues.RECURRING.product_name,
                    ItemTotalAmount = IPNValues.RECURRING.initial_payment_amount
                } },
                Notes = new TextBlock() {
                    Line3 = "PayPal Subscription: " + IPNValues.RECURRING.recurring_payment_id
                }
            };



            // totals.
            InvoiceTotals totals = new InvoiceTotals() {
                Total = IPNValues.PAYMENT.mc_gross
            };
            invoice.Totals = totals;

            // taxes.
            if (IPNValues.PAYMENT.tax > decimal.Zero) {
                totals.TaxTotals = new List<Tax>() {
                    new Tax() {
                        TaxName = "Total Tax",
                        TaxTotal = IPNValues.PAYMENT.tax
                    }
                };
            }

            return invoice;
        }

        private static Invoice Parse_cart(IPNProperties IPNValues) {

            bool isRefund = (IPNValues.PAYMENT.payment_status == IPNProperties.CONSTANTS.PaymentStatus.Refunded || IPNValues.PAYMENT.payment_status == IPNProperties.CONSTANTS.PaymentStatus.Reversed);

            DateTime invoiceDate = DateTime.Now;
            try {
                invoiceDate = DateTime.ParseExact(IPNValues.PAYMENT.payment_date, "HH:mm:ss MMM dd, yyyy PST", CultureInfo.InvariantCulture);
            }
            catch { }


            Invoice invoice = new Invoice() {
                Dummy = IPNValues.TRANSACTION.dummy,
                Id = IPNValues.TRANSACTION.txn_id,
                Date = Utils.Timestamp.TimeToMillis(invoiceDate),
                CurrencyCode = IPNValues.PAYMENT.mc_currency,
                CountryCode = IPNValues.TRANSACTION.residence_country,
                Buyer = new Actor() {
                    Name = IPNValues.PAYER.FullName,
                    Line1 = IPNValues.PAYER.Address,
                    Line2 = IPNValues.PAYER.payer_email,
                    Line3 = IPNValues.TRANSACTION.residence_country
                },
                Notes = new TextBlock() {
                    Line3 = "PayPal status notes: " + IPNValues.PAYMENT.payment_status
                }
            };

            decimal totalItemsTaxes = decimal.Zero;

            // items.
            List<InvoiceItem> items = new List<InvoiceItem>();
            foreach (var item in IPNValues.PAYMENT.item_namex) {
                InvoiceItem invoiceItem = new InvoiceItem() {
                    Name = item.Value,
                    UnitPrice = (IPNValues.PAYMENT.item_pricex != null ? (IPNValues.PAYMENT.item_pricex.ContainsKey(item.Key) ? IPNValues.PAYMENT.item_pricex[item.Key] : decimal.Zero) : decimal.Zero),
                    Quantity = IPNValues.PAYMENT.quantityx.ContainsKey(item.Key) ? IPNValues.PAYMENT.quantityx[item.Key] : 1,
                    ItemTotalAmount = IPNValues.PAYMENT.mc_gross_x[item.Key]
                };

                if (IPNValues.PAYMENT.taxx.ContainsKey(item.Key) && IPNValues.PAYMENT.taxx[item.Key] > decimal.Zero) {
                    if (invoiceItem.Taxes == null)
                        invoiceItem.Taxes = new List<Tax>();
                    Tax newTax = new Tax() {
                        TaxName = "Tax",
                        TaxTotal = IPNValues.PAYMENT.taxx[item.Key]
                    };
                    invoiceItem.Taxes.Add(newTax);

                    totalItemsTaxes += IPNValues.PAYMENT.taxx[item.Key];
                }

                items.Add(invoiceItem);
            }
            invoice.Items = items;

            // totals.
            InvoiceTotals totals = new InvoiceTotals() {
                Total = IPNValues.PAYMENT.mc_gross
            };
            invoice.Totals = totals;

            // taxes.
            if (IPNValues.PAYMENT.tax > decimal.Zero) {
                totals.TaxTotals = new List<Tax>() {
                            new Tax() {
                                TaxName = "Total Tax",
                                TaxRate = IPNValues.PAYMENT.tax_rate,
                                TaxTotal = (isRefund && IPNValues.PAYMENT.tax> decimal.Zero) ? -IPNValues.PAYMENT.tax : IPNValues.PAYMENT.tax
                            }
                        };
            }
            else if (totalItemsTaxes > decimal.Zero) {
                totals.TaxTotals = new List<Tax>() {
                            new Tax() {
                                TaxName = "Total Tax",
                                TaxTotal = (isRefund && totalItemsTaxes > decimal.Zero) ? -totalItemsTaxes : totalItemsTaxes
                            }
                        };
            }

            return invoice;
        }
    }
}
