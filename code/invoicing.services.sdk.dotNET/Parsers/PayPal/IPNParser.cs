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

            if (!
                (IPNValues.TRANSACTION.txn_type == IPNProperties.CONSTANTS.TransactionTypes.cart ||
                IPNValues.TRANSACTION.txn_type == IPNProperties.CONSTANTS.TransactionTypes.express_checkout ||
                IPNValues.PAYMENT.payment_status == IPNProperties.CONSTANTS.PaymentStatus.Refunded ||
                IPNValues.PAYMENT.payment_status == IPNProperties.CONSTANTS.PaymentStatus.Reversed)) {
                throw new Exception("Only 'cart/express_checkout' IPN messages are parsed for now!");
            }

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

            // items.
            List<InvoiceItem> items = new List<InvoiceItem>();
            foreach (var item in IPNValues.PAYMENT.item_namex) {
                InvoiceItem invoiceItem = new InvoiceItem() {
                    Name = item.Value,
                    UnitPrice = (IPNValues.PAYMENT.item_pricex != null ? (IPNValues.PAYMENT.item_pricex.ContainsKey(item.Key) ? IPNValues.PAYMENT.item_pricex[item.Key] : decimal.Zero) : decimal.Zero),
                    Quantity = IPNValues.PAYMENT.quantityx.ContainsKey(item.Key) ? IPNValues.PAYMENT.quantityx[item.Key] : 1,
                    ItemTotalAmount = IPNValues.PAYMENT.mc_gross_x[item.Key]
                };

                if (IPNValues.PAYMENT.taxx.ContainsKey(item.Key)) {
                    if (invoiceItem.Taxes == null)
                        invoiceItem.Taxes = new List<Tax>();
                    Tax newTax = new Tax() {
                        TaxName = "Tax",
                        TaxTotal = IPNValues.PAYMENT.taxx[item.Key]
                    };
                    invoiceItem.Taxes.Add(newTax);
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
                                TaxTotal = IPNValues.PAYMENT.tax
                            }
                        };
            }

            return invoice;
        }
    }
}
