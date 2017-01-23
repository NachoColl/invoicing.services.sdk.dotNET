using invoicing.services.sdk.dotNET.Model;
using invoicing.services.sdk.dotNET.Model.APIQueries;
using invoicing.services.sdk.dotNET.Model.APIResponses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace invoicing.services.sdk.dotNET
{
    public class InvoicingAPI
    {
        const string APIEndPoint = "https://api.invoicing.services/";
        const string InvoicesBaseURL = "https://s3-us-west-2.amazonaws.com/files.invoicing.services/";

        public delegate void LogHandler(string LogMessage);
      
        HttpClient _client = null;
     
        public InvoicingAPI(string APIKey, LogHandler LogHandler = null) {
            if (string.IsNullOrWhiteSpace(APIKey))
                throw new Exception("You must provide your invoicing.services API Key!");
       
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            _client = new HttpClient(new HttpClientLogHandler(new HttpClientHandler(), LogHandler)) { BaseAddress = new Uri(APIEndPoint) };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("X-Api-Key", APIKey);
        }


        /// <summary>
        /// Use this method to set your Bill From data so you don’t have to include that info each time you create a new Invoice.
        /// </summary>
        /// <param name="Seller">Your Bill From Seller Information</param>
        /// <returns>Returns the same Seller information</returns>
        public Actor UpdateSeller(Actor Seller) {

            string result = CallAPI("seller/update", Seller);
         
            Actor updateSellerResponse = JsonConvert.DeserializeObject<Actor>(result, new JsonSerializerSettings() {
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

            string result = CallAPI("invoice/add", Invoice);
           
            AddInvoiceResponse addInvoiceResponse = JsonConvert.DeserializeObject<AddInvoiceResponse>(result, new JsonSerializerSettings() {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            });

            return addInvoiceResponse;
        }

        /// <summary>
        /// Use this method to get Invoice List.
        /// </summary>
        /// <param name="Query">Query object</param>
        /// <returns>ListInvoiceResponse object.</returns>
        public ListInvoiceResponse ListInvoice(ListInvoiceQuery Query) {

            string result = CallAPI("invoice/list", Query);
         
            ListInvoiceResponse listInvoiceResponse = JsonConvert.DeserializeObject<ListInvoiceResponse>(result, new JsonSerializerSettings() {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            });

            return listInvoiceResponse;
        }

        /// <summary>
        /// Use this method to get one specific Invoice.
        /// </summary>
        /// <param name="Query">Query object</param>
        /// <returns>Invoice object</returns>
        public Invoice GetInvoice(GetInvoiceQuery Query) {

            string result = CallAPI("invoice/get", Query);
          
            Invoice invoice = JsonConvert.DeserializeObject<Invoice>(result, new JsonSerializerSettings() {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            });

            return invoice;
        }

        #region privates

        string CallAPI(string Method, Object Object) {

            string jsonString = JsonConvert.SerializeObject(Object, new JsonSerializerSettings() {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            });

            var result =  _client.PostAsync(Method, 
                new StringContent(jsonString, Encoding.UTF8, "application/json")).Result;
            var response = result.Content.ReadAsStringAsync().Result;

            if (!result.IsSuccessStatusCode) {
                var responseMessage = "Response status code does not indicate success: " + (int)result.StatusCode + ". ";
                throw new HttpRequestException(responseMessage + Environment.NewLine + response);
            }

            return response;
        }

        string CallAPI(string Method, string BodyText) {

            Encoding wind1252 = Encoding.GetEncoding("windows-1252");

            var result = _client.PostAsync(Method,
                new StringContent(BodyText, wind1252, "text/plain")).Result;
            var response = result.Content.ReadAsStringAsync().Result;

            if (!result.IsSuccessStatusCode) {
                var responseMessage = "Response status code does not indicate success: " + (int)result.StatusCode + ". ";
                throw new HttpRequestException(responseMessage + Environment.NewLine + response);
            }
       

            return response;
        }

        #endregion


        public class HttpClientLogHandler : DelegatingHandler {

            LogHandler _logHandler = null;

            public HttpClientLogHandler(HttpMessageHandler innerHandler, LogHandler LogHandler = null)
                : base(innerHandler) {

                _logHandler = LogHandler;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {

                if (_logHandler != null) {
                    _logHandler(request.ToString());
                }
                return base.SendAsync(request, cancellationToken);
            }

        }

    }
}
