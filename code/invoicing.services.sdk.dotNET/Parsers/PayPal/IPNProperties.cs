using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace invoicing.services.sdk.dotNET {

    /// <summary>
    /// PayPal IPN properties as described on https://developer.paypal.com/webapps/developer/docs/classic/ipn/integration-guide/IPNandPDTVariables
    /// </summary>
    public class IPNProperties {


        static CultureInfo _decimalsCulture = new CultureInfo("en-US");

        public static class CONSTANTS {

            public static class TransactionTypes {

                public const string adjustment = "adjustment";
                public const string cart = "cart";
                public const string express_checkout = "express_checkout";
                public const string masspay = "masspay";
                public const string merch_pmt = "merch_pmt";
                public const string mp_cancel = "mp_cancel";
                public const string mp_signup = "mp_signup";
                public const string new_case = "new_case";
                public const string payout = "payout";
                public const string pro_hosted = "pro_hosted";
                public const string recurring_payment = "recurring_payment";
                public const string recurring_payment_expired = "recurring_payment_expired";
                public const string recurring_payment_failed = "recurring_payment_failed";
                public const string recurring_payment_profile_cancel = "recurring_payment_profile_cancel";
                public const string recurring_payment_profile_created = "recurring_payment_profile_created";
                public const string recurring_payment_skipped = "recurring_payment_skipped";
                public const string recurring_payment_suspended = "recurring_payment_suspended";
                public const string recurring_payment_suspended_due_to_max_failed_payment = "recurring_payment_suspended_due_to_max_failed_payment";
                public const string send_money = "send_money";
                public const string subscr_cancel = "subscr_cancel";
                public const string subscr_eot = "subscr_eot";
                public const string subscr_failed = "subscr_failed";
                public const string subscr_modify = "subscr_modify";
                public const string subscr_payment = "subscr_payment";
                public const string subscr_signup = "subscr_signup";
                public const string virtual_terminal = "virtual_terminal";
                public const string web_accept = "web_accept";
            }
            public static class PaymentStatus {
                public const string Canceled_Reversal = "Canceled_Reversal";
                public const string Completed = "Completed";
                public const string Created = "Created";
                public const string Denied = "Denied";
                public const string Expired = "Expired";
                public const string Failed = "Failed";
                public const string Pending = "Pending";
                public const string Refunded = "Refunded";
                public const string Reversed = "Reversed";
                public const string Processed = "Processed";
                public const string Voided = "Voided";
            }
        }

        public IPNProperties_TRANSACTION TRANSACTION = new IPNProperties_TRANSACTION();
        public class IPNProperties_TRANSACTION : IPNProperties_X {

            /// <summary>
            /// CUSTOM. Indicates if transaction is for testing.
            /// </summary>
            public bool dummy { get; private set; } = false;
            /// <summary>
            /// The kind of transaction for which the IPN message was sent.
            /// </summary>
            public string txn_type { get; private set; } = string.Empty;
            /// <summary>
            /// The merchant's original transaction identification number for the payment from the buyer, against which the case was registered.
            /// </summary>
            public string txn_id { get; private set; } = string.Empty;
            /// <summary>
            /// Unique ID generated during guest checkout.
            /// </summary>
            public string receipt_id { get; private set; } = string.Empty;
            /// <summary>
            /// ISO 3166 country code associated with the country of residence.
            /// </summary>
            public string residence_country { get; private set; } = string.Empty;

            public string business { get; private set; } = string.Empty;
            public string charset { get; private set; } = string.Empty;
            public string custom { get; private set; } = string.Empty;
            public string ipn_track_id { get; private set; } = string.Empty;
            public string notify_version { get; private set; } = string.Empty;
            public string parent_txn_id { get; private set; } = string.Empty;
            public string receiver_email { get; private set; } = string.Empty;
            public string receiver_id { get; private set; } = string.Empty;
            public string resend { get; private set; } = string.Empty;
            public string test_ipn { get; private set; } = string.Empty;
            public string verify_sign { get; private set; } = string.Empty;

        }

        public IPNProperties_PAYREQUEST PAYREQUEST = new IPNProperties_PAYREQUEST();
        public class IPNProperties_PAYREQUEST : IPNProperties_X {
            public string transaction_type { get; private set; } = string.Empty;
            public string status { get; private set; } = string.Empty;
            public string sender_email { get; private set; } = string.Empty;
            public string action_type { get; private set; } = string.Empty;
            public string payment_request_date { get; private set; } = string.Empty;
            public string reverse_all_parallel_payments_on_error { get; private set; } = string.Empty;
            public string return_url { get; private set; } = string.Empty;
            public string cancel_url { get; private set; } = string.Empty;
            public string ipn_notification_url { get; private set; } = string.Empty;
            public string pay_key { get; private set; } = string.Empty;
            public string memo { get; private set; } = string.Empty;
            public string fees_payer { get; private set; } = string.Empty;
            public string trackingId { get; private set; } = string.Empty;
            public string preapproval_key { get; private set; } = string.Empty;
            public string reason_code { get; private set; } = string.Empty;
        }

        public IPNProperties_PAYER PAYER = new IPNProperties_PAYER();
        public class IPNProperties_PAYER : IPNProperties_X {
            public string address_country { get; private set; } = string.Empty;
            public string address_city { get; private set; } = string.Empty;
            public string address_country_code { get; private set; } = string.Empty;
            public string address_name { get; private set; } = string.Empty;
            public string address_state { get; private set; } = string.Empty;
            public string address_status { get; private set; } = string.Empty;
            public string address_street { get; private set; } = string.Empty;
            public string address_zip { get; private set; } = string.Empty;
            public string contact_phone { get; private set; } = string.Empty;
            public string first_name { get; private set; } = string.Empty;
            public string last_name { get; private set; } = string.Empty;
            public string payer_business_name { get; private set; } = string.Empty;
            public string payer_email { get; private set; } = string.Empty;
            public string payer_id { get; private set; } = string.Empty;

            // calculated.
            public string FullName { get { return first_name == string.Empty ? (payer_business_name == string.Empty ? (payer_email == string.Empty ? payer_id : payer_email) : payer_business_name) : (first_name + " " + last_name); } }
            public string Address { get { return string.Format("{0} {1} {2} {3}", address_street, address_zip, address_city, address_country); } }
        }

        public IPNProperties_PAYMENT PAYMENT = new IPNProperties_PAYMENT();
        public class IPNProperties_PAYMENT : IPNProperties_X {

            /// <summary>
            /// The status of the payment.
            /// </summary>
            public string payment_status { get; private set; } = string.Empty;
            /// <summary>
            /// echeck: This payment was funded with an eCheck. instant: This payment was funded with PayPal balance, credit card, or Instant Transfer.
            /// </summary>
            public string payment_type { get; private set; } = string.Empty;
            /// <summary>
            /// Time/Date stamp generated by PayPal, in the following format: HH:MM:SS Mmm DD, YYYY PDT 
            /// </summary>
            public string payment_date { get; private set; } = string.Empty;

            /// <summary>
            /// For payment IPN notifications, this is the currency of the payment. For non-payment subscription IPN notifications this is the currency of the subscription.
            /// </summary>
            public string mc_currency { get; private set; } = "USD";
            /// <summary>
            /// If this is a PayPal Shopping Cart transaction, number of items in cart.
            /// </summary>
            public int num_cart_items { get; private set; } = 1;
            /// <summary>
            /// Amount of tax charged on payment.
            /// </summary>
            public decimal tax { get; private set; } = 0m;
            /// <summary>
            /// CUSTOM. Tax rate (to use when only 1 tax rate type applies).
            /// </summary>
            public decimal tax_rate { get; private set; } = 0m;
            /// <summary>
            /// Full amount of the customer's payment, before transaction fee is subtracted.
            /// </summary>
            public decimal mc_gross { get; private set; } = 0m;
            /// <summary>
            /// Transaction fee associated with the payment. mc_gross minus mc_fee equals the amount deposited into the receiver_email account.
            /// </summary>
            public string mc_fee { get; private set; } = string.Empty;


            /// <summary>
            /// CUSTOM. Item product unit price.
            /// </summary>
            public SortedDictionary<int, decimal> item_pricex = new SortedDictionary<int, decimal>();
            /// <summary>
            /// Item name as passed by you, the merchant. Or, if not passed by you, as entered by your customer.
            /// </summary>
            public SortedDictionary<int, string> item_namex = new SortedDictionary<int, string>();
            /// <summary>
            /// Quantity as entered by your customer or as passed by you, the merchant.
            /// </summary>
            public SortedDictionary<int, int> quantityx = new SortedDictionary<int, int>();


            /// <summary>
            /// The taxx variable is included only if there was a specific tax amount applied to a particular shopping cart item.
            /// </summary>
            public SortedDictionary<int, decimal> taxx = new SortedDictionary<int, decimal>();

            /// <summary>
            /// The amount is in the currency of mc_currency, where x is the shopping cart detail item number. The sum of mc_gross_x should total mc_gross.
            /// </summary>
            public SortedDictionary<int, decimal> mc_gross_x = new SortedDictionary<int, decimal>();

            public string auth_amount { get; private set; } = string.Empty;
            public string auth_exp { get; private set; } = string.Empty;
            public string auth_id { get; private set; } = string.Empty;
            public string auth_status { get; private set; } = string.Empty;
            public string echeck_time_processed { get; private set; } = string.Empty;
            public string exchange_rate { get; private set; } = string.Empty;
            public string fraud_management_pending_filters_x { get; private set; } = string.Empty;
            public string invoice { get; private set; } = string.Empty;
            public string mc_handling { get; private set; } = string.Empty;
            public string mc_shipping { get; private set; } = string.Empty;
            public string memo { get; private set; } = string.Empty;
            public string payer_status { get; private set; } = string.Empty;

            public string pending_reason { get; private set; } = string.Empty;
            public string protection_eligibility { get; private set; } = string.Empty;

            public string reason_code { get; private set; } = string.Empty;
            public string remaining_settle { get; private set; } = string.Empty;
            public string settle_amount { get; private set; } = string.Empty;
            public string settle_currency { get; private set; } = string.Empty;
            public string shipping { get; private set; } = string.Empty;
            public string shipping_method { get; private set; } = string.Empty;

            public string transaction_entity { get; private set; } = string.Empty;

            public void InitializeItems(Dictionary<string, Microsoft.Extensions.Primitives.StringValues> Dictionary) {



                /// mc_gross_1,mc_gross_2,.. values
                foreach (var mc_gross_x in Dictionary.Where(k => k.Key.StartsWith("mc_gross_"))) {
                    this.mc_gross_x.Add(int.Parse(mc_gross_x.Key.Split('_')[2]), decimal.Parse(mc_gross_x.Value, _decimalsCulture));
                }
                /// quantity1, quantity2,.. values.
                foreach (var quantityx in Dictionary.Where(k => k.Key.StartsWith("quantity") && k.Key.Length > 8)) {
                    this.quantityx.Add(int.Parse(quantityx.Key.Replace("quantity", "")), int.Parse(quantityx.Value));
                }
                /// item_name1, item_name2,.. values.
                foreach (var item_namex in Dictionary.Where(k => k.Key.StartsWith("item_name") && k.Key.Length > 9)) {
                    this.item_namex.Add(int.Parse(item_namex.Key.Replace("item_name", "")), item_namex.Value);
                }
                /// item_price1, item_price2,.. values.
                foreach (var item_pricex in Dictionary.Where(k => k.Key.StartsWith("item_price") && k.Key.Length > 10)) {
                    this.item_pricex.Add(int.Parse(item_pricex.Key.Replace("item_price", "")), decimal.Parse(item_pricex.Value, _decimalsCulture));
                }
                /// tax1,tax2,.. values.
                foreach (var taxx in Dictionary.Where(k => k.Key.StartsWith("tax") && k.Key.Length > 3)) {
                    this.taxx.Add(int.Parse(taxx.Key.Replace("tax", "")), decimal.Parse(taxx.Value, _decimalsCulture));
                }


            }
        }

        public IPNProperties_RECURRING RECURRING = new IPNProperties_RECURRING();
        public class IPNProperties_RECURRING : IPNProperties_X {

            public string initial_payment_amount { get; private set; } = string.Empty;
            public string initial_payment_status { get; private set; } = string.Empty;
            public string initial_payment_txn_id { get; private set; } = string.Empty;
            public string currency_code { get; private set; } = "USD";
            public string residence_country { get; private set; } = string.Empty;

            public string amount { get; private set; } = string.Empty;
            public string amount_per_cycle { get; private set; } = string.Empty;

            public string next_payment_date { get; private set; } = "monthly";
            public string outstanding_balance { get; private set; } = string.Empty;
            public string payment_cycle { get; private set; } = string.Empty;
            public string period_type { get; private set; } = string.Empty;
            public string product_name { get; private set; } = string.Empty;
            public string product_type { get; private set; } = string.Empty;
            public string profile_status { get; private set; } = string.Empty;
            public string recurring_payment_id { get; private set; } = string.Empty;
            public string rp_invoice_id { get; private set; } = string.Empty;
            public string time_created { get; private set; } = string.Empty;

        }

        public IPNProperties_DISPUTE DISPUTE = new IPNProperties_DISPUTE();
        public class IPNProperties_DISPUTE : IPNProperties_X {

            public string buyer_additional_information { get; private set; } = string.Empty;
            public string case_creation_date { get; private set; } = string.Empty;
            public string case_id { get; private set; } = string.Empty;

            public string case_type { get; private set; } = string.Empty;
            public string reason_code { get; private set; } = string.Empty;
        }

        public IPNProperties(Dictionary<string, Microsoft.Extensions.Primitives.StringValues> Dictionary) {
            TRANSACTION.Initialize(Dictionary);
            PAYREQUEST.Initialize(Dictionary);
            PAYER.Initialize(Dictionary);
            PAYMENT.Initialize(Dictionary);
            PAYMENT.InitializeItems(Dictionary);

            RECURRING.Initialize(Dictionary);
        }

        public abstract class IPNProperties_X {
            public void Initialize(Dictionary<string, Microsoft.Extensions.Primitives.StringValues> Dictionary) {
                foreach (var classProperty in this.GetType()
                                .GetProperties()) {
                    if (Dictionary.ContainsKey(classProperty.Name)) {
                        if (classProperty.PropertyType == typeof(decimal)) {
                            classProperty.SetValue(this, decimal.Parse(Dictionary[classProperty.Name].FirstOrDefault(), new System.Globalization.CultureInfo("US")));
                        }
                        else
                            classProperty.SetValue(this, Convert.ChangeType(Dictionary[classProperty.Name].FirstOrDefault(), classProperty.PropertyType));
                    }
                }
            }
        }

        public override string ToString() {
            StringBuilder output = new StringBuilder();
            foreach (var classProperty in PAYER.GetType()
                               .GetProperties()) {
                output.AppendFormat("[{0},{1}]", classProperty.Name, classProperty.GetValue(PAYER));
            }
            return output.ToString();
        }




    }
}
