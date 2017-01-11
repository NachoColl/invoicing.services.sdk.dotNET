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
        [Fact(DisplayName = "Creates a simple invoice.")]
        public void Test1() {

            InvoicingAPI API = new InvoicingAPI("QpMIwFClmaa1BH5E3UhsJWFLwHW6jhr5ijkgEHqe");

            long now = Utils.Timestamp.CurrentTimeMillis();
            AddInvoiceResponse response = API.AddInvoice(new Model.Invoice() {
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

            InvoicingAPI API = new InvoicingAPI("QpMIwFClmaa1BH5E3UhsJWFLwHW6jhr5ijkgEHqe");

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

            InvoicingAPI API = new InvoicingAPI("QpMIwFClmaa1BH5E3UhsJWFLwHW6jhr5ijkgEHqe");

            long now = Utils.Timestamp.CurrentTimeMillis();
            AddInvoiceResponse response = API.AddInvoice(new Model.Invoice() {
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

    }
}
