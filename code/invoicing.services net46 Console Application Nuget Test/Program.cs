using invoicing.services.sdk.dotNET;
using invoicing.services.sdk.dotNET.Model;
using invoicing.services.sdk.dotNET.Model.APIResponses;
using System;
using System.Collections.Generic;

namespace invoicing.services_net46_Console_Application_Nuget_Test {
    class Program {

        //  Set your API key here (get one at http://invoicing.services).
        const string MY_API_KEY = "";

        static void Main(string[] args) {

            Console.WriteLine("Hello World! This is a net46 Console app example.");
            Console.WriteLine("Creating a sample PDF invoice ... (first execution may take a while)");

            try {
                InvoicingAPI API = new InvoicingAPI(MY_API_KEY);

                long now = invoicing.services.sdk.dotNET.Utils.Timestamp.CurrentTimeMillis();
                AddInvoiceResponse response = API.AddInvoice(new Invoice() {
                    Dummy = false,
                    Date = now,
                    Seller = new Actor() {
                        Name = "ACME Corporation",
                    },
                    Buyer = new Actor() {
                        Name = "My First Invoice Buyer"
                    },
                    Items = new List<InvoiceItem>() {
                     new InvoiceItem() {
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
                    Totals = new InvoiceTotals() { Total = 41.04m }
                });
                Console.WriteLine("********************************");
                Console.WriteLine(string.Format("Invoice with Id {0} correctly created.", response.InvoiceId));
                Console.WriteLine(string.Format("Invoice can be found at {0}.", response.InvoiceFileURL));
                Console.WriteLine("********************************");
            }
            catch (Exception e) {
                Console.WriteLine("-------------------------------");
                Console.WriteLine(string.Format("An error has ocurried while creating Invoice: {0}", e.Message));
                Console.WriteLine("-------------------------------");
            }
            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
        }
    }
}
