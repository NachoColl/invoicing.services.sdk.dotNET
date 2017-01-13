# invoicing.services .NET SDK

Use this SDK to call [invoicing.services](https://invoicing.services/) API.

You can check the API documentation at [docs.invoicing.services](http://docs.invoicing.services/).


## Install

To start using the SDK just install the nuget package.

`PM > Install-Package invoicing.services.sdk.dotNET`

## How to use the .NET SDK

To start calling API methods you first need to register at [invoicing.services](https://invoicing.services/) and get your **API Key**.

It's free.

### Example 1: Create a simple Invoice.

To create a new Invoice just call `AddInvoice` method passing the invoice related information.

```net
InvoicingAPI API = new InvoicingAPI(<YOUR_API_KEY>);

AddInvoiceResponse response = API.AddInvoice(new Model.Invoice() {
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

```

*AddInvoiceResponse* will include `InvoiceId`, `InvoiceDate` and `InvoiceFileURL`.


### Example 2: Update your Bill From (Seller) data.

By calling `UpdateSeller` API method you set `Bill From` default values so you don’t have to include that info each time you create a new Invoice.

```
InvoicingAPI API = new InvoicingAPI(<YOUR_API_KEY>);

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
```
### Example 3: List invoices.

Get a list of invoices.

```
InvoicingAPI API = new InvoicingAPI(MY_API_KEY);
ListInvoiceResponse response = API.ListInvoice(new Model.APIQueries.ListInvoiceQuery() { 
        Year = 2017, 
        Quarter = 1 
    });
```

### Example 4: Get invoice details.

Get invoice by GUID (returned on creation/list).

```
InvoicingAPI API = new InvoicingAPI(MY_API_KEY);
Invoice response = API.GetInvoice(new GetInvoiceQuery() { 
        InvoiceGuid = "e234cd8f-4c66-4855-a5b4-23816fea6f23"
    });
```

### Example 5: Extensive invoice.

This is an extensive invoice example.

```
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
```

