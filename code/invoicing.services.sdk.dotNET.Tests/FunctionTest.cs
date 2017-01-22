using invoicing.services.sdk.dotNET.Model;
using invoicing.services.sdk.dotNET.Model.APIQueries;
using invoicing.services.sdk.dotNET.Model.APIResponses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace invoicing.services.sdk.dotNET.Tests {
    public class FunctionTest {

        // Set your API key here (get one at http://invoicing.services).
        const string MY_API_KEY = "";

        [Fact(DisplayName = "SDK: Creates a simple invoice.")]
        public void Test_SDK_INVOICE_ADD_1() {

            InvoicingAPI API = new InvoicingAPI(MY_API_KEY);

            long now = Utils.Timestamp.CurrentTimeMillis();
            AddInvoiceResponse response = API.AddInvoice(new Model.Invoice() {
                Dummy = false,
                Date = now,
                Seller = new Model.Actor() {
                    Name = "ACME Corporation",
                },
                Buyer = new Model.Actor() {
                    Name = "My First Invoice Buyer"
                },
                Items = new List<Model.InvoiceItem>() {
                     new Model.InvoiceItem() {
                         Name = "My Product Name",
                         UnitPrice = 20.01m,
                         Quantity = 2,
                         Taxes = new List<Tax>() {
                             new Tax() {
                                 TaxName = "VAT",
                                 TaxTotal = 1.02m
                             }
                         },
                         ItemTotalAmount = 41.04m
                     } },
                Totals = new Model.InvoiceTotals() { Total = 41.04m }
            });
            Assert.Equal(now.ToString(), response.InvoiceDate);
        }

        [Fact(DisplayName = @"SDK: Sets your Seller ""Bill From"" data information.")]
        public void Test_SDK_INVOICE_ADD_2() {

            InvoicingAPI API = new InvoicingAPI(MY_API_KEY);

            Model.Actor response = API.UpdateSeller(new Model.Actor() {
                Id = "My Seller ID",
                Name = "ACME Corporation",
                Line1 = "This is my address line 1",
                TaxIds = new List<Model.ActorTaxId>() {
                         new Model.ActorTaxId() {
                             Name = "VAT",
                             Value = "FE65774648"
                         }
                    }
            });

            Assert.Equal("My Seller ID", response.Id);
        }


        [Fact(DisplayName = @"SDK: Creates an invoice with your default Seller ""Bill From"" info.")]
        public void Test_SDK_INVOICE_ADD_3() {

            InvoicingAPI API = new InvoicingAPI(MY_API_KEY);

            long now = Utils.Timestamp.CurrentTimeMillis();
            AddInvoiceResponse response = API.AddInvoice(new Model.Invoice() {
                Dummy = true,
                Date = now,
                Buyer = new Model.Actor() {
                    Name = "My First Invoice Buyer"
                },
                Items = new List<Model.InvoiceItem>() {
                     new Model.InvoiceItem() {
                         Name = "My Product Name",
                         UnitPrice = 20.01m,
                         Quantity = 2,
                         ItemTotalAmount = 40.02m
                     } },
                Totals = new Model.InvoiceTotals() { Total = 40.02m }
            });
            Assert.Equal(now.ToString(), response.InvoiceDate);
        }

        [Fact(DisplayName = @"SDK: Creates a more extensive Invoice.")]
        public void Test_SDK_INVOICE_ADD_4() {

            InvoicingAPI API = new InvoicingAPI(MY_API_KEY);

            long now = Utils.Timestamp.CurrentTimeMillis();
            AddInvoiceResponse response = API.AddInvoice(new Model.Invoice() {
                Dummy = false,
                Id = "0000123",
                Date = now,
                CurrencyCode = "USD",
                CountryCode = "US",
                Seller = new Model.Actor() {
                    Id = "My Seller ID",
                    Name = "I am a Seller",
                    Line1 = "This is my address.",
                    TaxIds = new List<Model.ActorTaxId>() {
                        new Model.ActorTaxId() {
                            Name="FEV",
                            Value="774646/OL"
                        },
                        new Model.ActorTaxId() {
                            Name="FR",
                            Value="US7758847"
                        }
                    }

                },
                Buyer = new Model.Actor() {
                    Id = "INTERNAL 00212",
                    Name = "My First Invoice Buyer",
                    Line1 = "Company Address Line 1",
                    Line2 = "Address Line 2",
                    TaxIds = new List<Model.ActorTaxId>() {
                        new Model.ActorTaxId() {
                            Name = "FR",
                            Value="US9093930"
                        }
                    }
                },
                Items = new List<Model.InvoiceItem>() {
                     new Model.InvoiceItem() {
                         Name = "My Product Name",
                         Description = "Product description.",
                         UnitPrice = 20.01m,
                         Quantity = 2,
                         Taxes = new List<Model.Tax>() {
                             new Model.Tax() {
                                 TaxName = "VAT",
                                 TaxRate = 10
                             }
                         },
                         ItemSubTotalAmount = 40.02m,
                         ItemTotalAmount = 44.02m
                     } },
                Totals = new Model.InvoiceTotals() {
                    SubTotal = 40.02m,
                    TaxTotals = new List<Model.Tax>() {
                         new Model.Tax() {
                             TaxName="VAT",
                             TaxRate=10,
                             TaxTotal=4.00m
                         }
                     },
                    Total = 44.02m
                },
                Notes = new Model.TextBlock() {
                    Line1 = "This is a note to include some text/conditions."
                },
                Labels = new Model.Labels() {
                    Title = "QUOTATION",
                    Total = "Total Quotation"
                },
                Colors = new Model.Colors() {
                    Color1 = "#766755"
                }
            });
            Assert.Equal(now.ToString(), response.InvoiceDate);
        }

      
        [Fact(DisplayName = @"SDK:PAYPAL:PARSE: Parses an Invoice from PAYPAL IPN Message.")]
        public void Test_SDK_PAYPAL_PARSE_1() {

            string ipnMessage = File.ReadAllText(@"PayPalIPNMessage_3.txt");
            Invoice invoice = Parsers.PayPal.IPNParser.Parse(ipnMessage);

            Assert.Equal(1484472178000, invoice.Date);

        }

        [Fact(DisplayName = @"SDK:PAYPAL:PARSE: Parses a REFUND Invoice from PAYPAL IPN Message.")]
        public void Test_SDK_PAYPAL_PARSE_2() {

       
            string ipnMessage = File.ReadAllText(@"PayPalIPNMessage_4.txt");
            Invoice invoice = Parsers.PayPal.IPNParser.Parse(ipnMessage);

            Assert.Equal(-3.99m, invoice.Totals.TaxTotals[0].TaxTotal);

        }

        [Fact(DisplayName = @"SDK:PAYPAL:PARSE: Parses a Invoice from PAYPAL IPN Message WITHOUT Taxes.")]
        public void Test_SDK_PAYPAL_PARSE_4() {


            string ipnMessage = File.ReadAllText(@"PayPalIPNMessage_5.txt");
            Invoice invoice = Parsers.PayPal.IPNParser.Parse(ipnMessage);

            Assert.Equal(null, invoice.Totals.TaxTotals);
            Assert.Equal(null, invoice.Items[0].Taxes);

        }

        [Fact(DisplayName = @"SDK:PAYPAL:PARSE: Parses a Recurring Payment CREATION WITH INITIAL PAYMENT.")]
        public void Test_SDK_PAYPAL_PARSE_5() {

            string ipnMessage = File.ReadAllText(@"PayPalIPNMessage_6.txt");
            Invoice invoice = Parsers.PayPal.IPNParser.Parse(ipnMessage);

            Assert.Equal(29.00m, invoice.Totals.Total);
  
        }

        [Fact(DisplayName = @"SDK:PAYPAL:PARSE: Parses a Recurring REGULAR Payment.")]
        public void Test_SDK_PAYPAL_PARSE_6() {

            string ipnMessage = File.ReadAllText(@"PayPalIPNMessage_7.txt");
            Invoice invoice = Parsers.PayPal.IPNParser.Parse(ipnMessage);

            Assert.Equal(29.00m, invoice.Totals.Total);

        }

        [Fact(DisplayName = @"SDK:PAYPAL:INVOICE: Creates an Invoice from PAYPAL IPN Message.")]
        public void Test_SDK_PAYPAL_INVOICE_ADD_1() {

            InvoicingAPI API = new InvoicingAPI(MY_API_KEY);

            string ipnMessage = File.ReadAllText(@"PayPalIPNMessage_1.txt");

            Invoice invoice = Parsers.PayPal.IPNParser.Parse(ipnMessage);
            AddInvoiceResponse response = API.AddInvoice(invoice);
            Assert.Equal("1375-5556-7266-7753", response.InvoiceId);
        }

        [Fact(DisplayName = @"SDK:PAYPAL:INVOICE: Creates an Invoice from PAYPAL IPN Message. Including custom param ""item_price1"".")]
        public void Test_SDK_PAYPAL_INVOICE_ADD_2() {

            InvoicingAPI API = new InvoicingAPI(MY_API_KEY);

            string ipnMessage = File.ReadAllText(@"PayPalIPNMessage_2.txt");
            Invoice invoice = Parsers.PayPal.IPNParser.Parse(ipnMessage);
            AddInvoiceResponse response = API.AddInvoice(invoice);
            Assert.Equal("0642-9185-2343-3538", response.InvoiceId);

        }

        [Fact(DisplayName = @"SDK:PAYPAL:INVOICE: Creates an Invoice from PAYPAL IPN Message. WITHOUT TAXES.")]
        public void Test_SDK_PAYPAL_INVOICE_ADD_3() {

            InvoicingAPI API = new InvoicingAPI(MY_API_KEY);

            string ipnMessage = File.ReadAllText(@"PayPalIPNMessage_5.txt");

            Invoice invoice = Parsers.PayPal.IPNParser.Parse(ipnMessage);
            AddInvoiceResponse response = API.AddInvoice(invoice);
            Assert.Equal("4R478441Y3261273T", response.InvoiceId);
        }

        [Fact(DisplayName = @"SDK: List invoices for Year 2017.")]
        public void Test_SDK_INVOICE_LIST_1() {

            InvoicingAPI API = new InvoicingAPI(MY_API_KEY);

            ListInvoiceResponse response = API.ListInvoice(new Model.APIQueries.ListInvoiceQuery() { Year = 2017 });
            Assert.True(response.Count > 0);

        }

        [Fact(DisplayName = @"SDK: List invoices for Year 2017, Quarter 1.")]
        public void Test_SDK_INVOICE_LIST_2() {

            InvoicingAPI API = new InvoicingAPI(MY_API_KEY);

            ListInvoiceResponse response = API.ListInvoice(new Model.APIQueries.ListInvoiceQuery() { Year = 2017, Quarter = 1 });
            Assert.True(response.Count > 0);

        }

        [Fact(DisplayName = @"SDK: List invoices for Year 2017, Month 1.")]
        public void Test_SDK_INVOICE_LIST_3() {

            InvoicingAPI API = new InvoicingAPI(MY_API_KEY);

            ListInvoiceResponse response = API.ListInvoice(new Model.APIQueries.ListInvoiceQuery() { Year = 2017, Month = Months.January });
            Assert.True(response.Count > 0);

        }

        [Fact(DisplayName = @"SDK: List invoices for Year 2017, Month 1, Day 1.")]
        public void Test_SDK_INVOICE_LIST_4() {

            InvoicingAPI API = new InvoicingAPI(MY_API_KEY);

            ListInvoiceResponse response = API.ListInvoice(new Model.APIQueries.ListInvoiceQuery() { Year = 2017, Month = Months.January, Day = 1 });
            Assert.True(response.Count == 1);

        }

        [Fact(DisplayName = @"SDK: Get invoice by GUID.")]
        public void Test_SDK_INVOICE_GET_1() {

            InvoicingAPI API = new InvoicingAPI(MY_API_KEY);

            Invoice response = API.GetInvoice(new GetInvoiceQuery() { InvoiceGuid = "e234cd8f-4c66-4855-a5b4-23816fea6f23" });
            Assert.Equal("1375-5556-7266-7753", response.Id);

        }
    }
}
