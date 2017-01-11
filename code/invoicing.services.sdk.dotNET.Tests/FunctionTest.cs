using invoicing.services.sdk.dotNET.Model.APIResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace invoicing.services.sdk.dotNET.Tests
{
    public class FunctionTest
    {

        // Set your API KEY here for doing the tests.
        const string MY_API_KEY = "";

        [Fact(DisplayName = "Creates a simple invoice.")]
        public void Test1() {

            InvoicingAPI API = new InvoicingAPI(MY_API_KEY);

            long now = Utils.Timestamp.CurrentTimeMillis();
            AddInvoiceResponse response = API.AddInvoice(new Model.Invoice() {
                Dummy = true,
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
                         ItemTotalAmount = 40.02m
                     } },
                Totals = new Model.InvoiceTotals() {  Total = 40.02m }
            });
            Assert.Equal(now.ToString(), response.InvoiceDate);
        }

        [Fact(DisplayName = @"Sets your Seller ""Bill From"" data information.")]
        public void Test2() {

            InvoicingAPI API = new InvoicingAPI(MY_API_KEY);

            Model.Actor response = API.UpdateSeller(new Model.Actor() {
                    Id = "My Seller ID",
                    Name = "ACME Corporation",
                    Line1="This is my address line 1",
                    TaxIds = new List<Model.ActorTaxId>() {
                         new Model.ActorTaxId() {
                             Name = "VAT",
                             Value = "FE65774648"
                         }
                    }
                });

            Assert.Equal("My Seller ID", response.Id);
        }


        [Fact(DisplayName = @"Creates an invoice with your default Seller ""Bill From"" info.")]
        public void Test3() {

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

        [Fact(DisplayName = @"Creates a more extensive Invoice.")]
        public void Test4() {

            InvoicingAPI API = new InvoicingAPI(MY_API_KEY);

            long now = Utils.Timestamp.CurrentTimeMillis();
            AddInvoiceResponse response = API.AddInvoice(new Model.Invoice() {
                Dummy = true,
                Id="0000123",
                Date = now,
                CurrencyCode = "USD",
                CountryCode = "US",
                Seller = new Model.Actor() {
                    Id="My Seller ID",
                    Name="I am a Seller",
                    Line1="This is my address.",
                    TaxIds= new List<Model.ActorTaxId>() {
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
                    Id="INTERNAL 00212",
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
                     TaxTotals = new List<Model.InvoiceTaxTotal>() {
                         new Model.InvoiceTaxTotal() {
                             TaxName="VAT",
                             TaxRate=10,
                             TaxTotal=4.00m
                         }
                     },
                    Total = 44.02m
                },
                Notes = new Model.TextBlock() {
                    Line1="This is a note to include some text/conditions."
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

    }
}
