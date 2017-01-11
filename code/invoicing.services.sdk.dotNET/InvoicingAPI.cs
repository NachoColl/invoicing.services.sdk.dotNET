using invoicing.services.sdk.dotNET.Model;
using invoicing.services.sdk.dotNET.Model.APIResponses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace invoicing.services.sdk.dotNET
{
    public class InvoicingAPI
    {
        public const string APIEndPoint = "https://api.invoicing.services/";
        public const string InvoicesBaseURL = "https://s3-us-west-2.amazonaws.com/files.invoicing.services/";

        static HttpClient _client = new HttpClient() { BaseAddress = new Uri(APIEndPoint) };
     
        public InvoicingAPI(string APIKey) {
            if (string.IsNullOrWhiteSpace(APIKey))
                throw new Exception("You must provide your invoicing.services API Key!");
      
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("X-Api-Key", APIKey);
        }


        /// <summary>
        /// Use this method to set your Bill From data so you don’t have to include that info each time you create a new Invoice.
        /// </summary>
        /// <param name="Seller">Your Bill From Seller Information</param>
        /// <returns>Returns the same Seller information</returns>
        public Actor UpdateSeller(Actor Seller) {

            Task<Task<string>> result = CallAPIAsync("seller/update", Seller);
            string content = result.Result.Result;

            Actor updateSellerResponse = JsonConvert.DeserializeObject<Actor>(content, new JsonSerializerSettings() {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            });

            return updateSellerResponse;
        }


        /// <summary>
        /// Use this method to create a new invoice.
        /// </summary>
        /// <param name="Invoice">Invoice data.</param>
        /// <returns>AddInvoiceResponse class including Invoice id, date and PDF file URL.</returns>
        public AddInvoiceResponse AddInvoice(Invoice Invoice) {

            Task<Task<string>> result = CallAPIAsync("invoice/add", Invoice);
            string content = result.Result.Result;

            AddInvoiceResponse addInvoiceResponse = JsonConvert.DeserializeObject<AddInvoiceResponse>(content, new JsonSerializerSettings() {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            });

            return addInvoiceResponse;
        }




        static async Task<Task<string>> CallAPIAsync(string Method, Object Object) {

            string invoiceJsonString = JsonConvert.SerializeObject(Object, new JsonSerializerSettings() {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            });

            HttpResponseMessage response = await _client.PostAsync(Method, 
                new StringContent(invoiceJsonString, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsStringAsync();
        }

    }
}
