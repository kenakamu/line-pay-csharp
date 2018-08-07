using Line.Pay.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Line.Pay
{
    /// <summary>
    /// LINE Messaging API client, which handles request/response to LINE server.
    /// </summary>
    public class LinePayClient : IDisposable
    {
        private HttpClient client;
        static private Uri realUri = new Uri("https://api-pay.line.me");
        static private Uri sandboxUri = new Uri( "https://sandbox-api-pay.line.me");
        private string version;
       
        static private JsonSerializerSettings serializerSettings = new JsonSerializerSettings() {
            DefaultValueHandling = DefaultValueHandling.Ignore
        };
        /// <summary>
        /// Create LINE Pay client.
        /// </summary>
        /// <param name="channelId">Payment Integration Information - Channel ID</param>
        /// <param name="channelSecret">Payment Integration Information - ChannelSecret Key</param>
        /// <param name="isSandbox">Specify true if sandbox</param>
        /// <param name="version">Specify version</param>
        public LinePayClient(string channelId, string channelSecret, bool isSandbox = false, string version = "v2")
        {
            if (string.IsNullOrEmpty(channelId)) { throw new ArgumentNullException(nameof(channelId)); }
            if (string.IsNullOrEmpty(channelSecret)) { throw new ArgumentNullException(nameof(channelSecret)); }

            this.version = version;
            client = new HttpClient();
            client.BaseAddress = isSandbox ? sandboxUri : realUri;
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-LINE-ChannelId", channelId);
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-LINE-ChannelSecret", channelSecret);
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
        }


        #region Message 

        /// <summary>
        /// Get Payment Details AP
        /// Gets the details of payments made with LINE Pay. This API only gets the payments that have been captured.
        /// </summary>
        /// <param name="transactionId">A transaction ID issued by LINE Pay, for payment or refund.</param>
        /// <param name="orderId">Merchant Transaction Order ID</param>
        /// <returns>PaymentResponse</returns>
        public async Task<PaymentResponse> GetPaymentAsync(Int64? transactionId, string orderId = null)
        {
            if (transactionId == null && string.IsNullOrEmpty(orderId))
                throw new ArgumentNullException("Either transactionId or orderId has to be specified.");

            var uri = transactionId != null ? $"/{version}/payments?transactionId={transactionId}" : $"/{version}/payments?orderId={orderId}";
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<PaymentResponse>(await response.Content.ReadAsStringAsync());
            else
                throw new Exception(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Reserve Payment API
        /// Prior to processing payments with LINE Pay, the Merchant is evaluated if it is a normal Merchant store then the
        /// information is reserved for payment.When a payment is successfully reserved, the Merchant gets a "transactionId"
        /// that is a key value used until the payment is completed or refunded.
        /// </summary>
        /// <param name="reserve">Reserve Information</param>
        /// <returns>ReserveResponse</returns>
        public async Task<ReserveResponse> ReserveAsync(Reserve reserve)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"/{version}/payments/request");
            request.Content = new StringContent(JsonConvert.SerializeObject(reserve, serializerSettings), Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ReserveResponse>(await response.Content.ReadAsStringAsync());
            else
                throw new Exception(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Payment Confirm API
        /// This API is used for a Merchant to complete its payment. The Merchant must call Confirm Payment API to
        /// actually complete the payment.However, when "capture" parameter is "false" on payment reservation, the
        /// payment status becomes AUTHORIZATION, and the payment is completed only after "Capture API" is called.
        /// </summary>
        /// <param name="transactionId">Transaction Id</param>
        /// <param name="amount">Payment amount</param>
        /// <param name="currency">Payment currency (ISO 4217)</param>
        /// <returns>ConfirmResponse</returns>
        public async Task<ConfirmResponse> ConfirmAsync(Int64 transactionId, int amount, Currency currency)
        {
            var confirm = new Confirm() { Amount = amount, Currency = currency };
            return await ConfirmAsync(transactionId, confirm);
        }

        /// <summary>
        /// Payment Confirm API
        /// This API is used for a Merchant to complete its payment. The Merchant must call Confirm Payment API to
        /// actually complete the payment.However, when "capture" parameter is "false" on payment reservation, the
        /// payment status becomes AUTHORIZATION, and the payment is completed only after "Capture API" is called.
        /// </summary>
        /// <param name="transactionId">Transaction Id</param>
        /// <param name="confirm">Confirm</param>
        /// <returns>ConfirmResponse</returns>
        public async Task<ConfirmResponse> ConfirmAsync(Int64 transactionId, Confirm confirm)
        {
            var response = await client.PostAsync($"/{version}/payments/{transactionId}/confirm", new StringContent(JsonConvert.SerializeObject(confirm, serializerSettings), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ConfirmResponse>(await response.Content.ReadAsStringAsync());
            else
                throw new Exception(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Refund Payment API
        /// Requests refund of payments made with LINE Pay. To refund a payment, the LINE Pay user's payment transaction
        /// Id must be forwarded.A partial refund is also possible depending on the refund amount.
        /// </summary>
        /// <param name="transactionId">Transaction Id</param>
        /// <param name="refundAmount">Refund Amount</param>
        /// <returns>RefundResponse</returns>
        public async Task<RefundResponse> RefundAsync(Int64 transactionId, int refundAmount)
        {
            var refund = new Refund() { RefundAmount = refundAmount };
            return await RefundAsync(transactionId, refund);
        }
        /// <summary>
        /// Refund Payment API
        /// Requests refund of payments made with LINE Pay. To refund a payment, the LINE Pay user's payment transaction
        /// Id must be forwarded.A partial refund is also possible depending on the refund amount.
        /// </summary>
        /// <param name="transactionId">Transaction Id</param>
        /// <param name="refund">Refund</param>
        /// <returns>RefundResponse</returns>
        public async Task<RefundResponse> RefundAsync(Int64 transactionId, Refund refund)
        {           
            var response = await client.PostAsync($"/{version}/payments/{transactionId}/refund", new StringContent(JsonConvert.SerializeObject(refund, serializerSettings), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<RefundResponse>(await response.Content.ReadAsStringAsync());
            else
                throw new Exception(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Get Authorization Details API
        /// Gets the details authorized with LINE Pay. This API only gets data that is authorized or whose authorization is voided; 
        /// the one that is already captured can be viewed by using "Get Payment Details API”. 
        /// </summary>
        /// <param name="transactionId">Transaction number issued by LINE Pay</param>
        /// <param name="orderId">Order number of Merchant</param>
        /// <returns>AuthorizationResponse</returns>
        public async Task<AuthorizationResponse> GetAuthorizationAsync(Int64? transactionId, string orderId)
        {
            if (transactionId == null && string.IsNullOrEmpty(orderId))
                throw new ArgumentNullException("Either transactionId or orderId has to be specified.");

            var uri = transactionId != null ? $"/{version}/payments/authorizations?transactionId={transactionId}" : $"/{version}/payments/authorizations?orderId={orderId}";

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<AuthorizationResponse>(await response.Content.ReadAsStringAsync());
            else
                throw new Exception(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Capture API
        /// If "capture" is "false" when the Merchant calls the “Reserve Payment API” , the payment is completed only after the Capture API is called.
        /// </summary>
        /// <param name="transactionId">Transaction Id</param>
        /// <param name="amount">Payment amount</param>
        /// <param name="currency">Payment currency </param>
        /// <returns>CaptureResponse</returns>
        public async Task<CaptureResponse> CaptureAsync(Int64 transactionId, int amount, Currency currency)
        {
            var capture = new Capture() { Amount = amount, Currency = currency };
            return await CaptureAsync(transactionId, capture);
        }
        /// <summary>
        /// Capture API
        /// If "capture" is "false" when the Merchant calls the “Reserve Payment API” , the payment is completed only after the Capture API is called.
        /// </summary>
        /// <param name="transactionId">Transaction Id</param>
        /// <param name="capture">Capture</param>
        /// <returns>CaptureResponse</returns>
        public async Task<CaptureResponse> CaptureAsync(Int64 transactionId, Capture capture)
        {
            var response = await client.PostAsync($"/{version}/payments/authorizations/{transactionId}/capture", new StringContent(JsonConvert.SerializeObject(capture, serializerSettings), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<CaptureResponse>(await response.Content.ReadAsStringAsync());
            else
                throw new Exception(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Void Authorization API
        /// Voids a previously authorized payment. A payment that has been already captured can be refunded by using the "Refund Payment API”
        /// </summary>
        /// <param name="transactionId">Transaction Id</param>
        /// <returns></returns>
        public async Task<ResponseBase> VoidAuthorizationAsync(Int64 transactionId)
        {
            var response = await client.PostAsync($"/{version}/payments/authorizations/{transactionId}/void", null);
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseBase>(await response.Content.ReadAsStringAsync());
            else
                throw new Exception(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Preapproved Payment API
        /// When the payment type of the Reserve Payment API was set as PREAPPROVED, a regKey is returned with the
        /// payment result.Preapproved Payment API uses this regKey to directly complete a payment without using the LINE app.
        /// </summary>
        /// <param name="regKey">Registration Key</param>
        /// <param name="preApprovedPay">PreApprovedPay</param>
        /// <returns></returns>
        public async Task<PreApprovedPayResponse> PreApprovedPayAsync(string regKey, PreApprovedPay preApprovedPay)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"/{version}/payments/preapprovedPay/{regKey}/payment");
            request.Content = new StringContent(JsonConvert.SerializeObject(preApprovedPay, serializerSettings), Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<PreApprovedPayResponse>(await response.Content.ReadAsStringAsync());
            else
                throw new Exception(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Check regKey Status API
        /// Checks if regKey is available before using the preapproved payment API
        /// </summary>
        /// <param name="regKey">Registration Key</param>
        /// <param name="creditCardAuth">Check Authorization for Credit Card minimum amount saved in regKey</param>
        /// <returns></returns>
        public async Task<ResponseBase> RegKeyCheckAsync(string regKey, bool creditCardAuth = false)
        {
            var response = await client.GetAsync($"/{version}/payments/preapprovedPay/{regKey}/check?creditCardAuth={creditCardAuth}");
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseBase>(await response.Content.ReadAsStringAsync());
            else
                throw new Exception(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Expire regKey API
        /// Expires the regKey information registered for preapproved payment.Once the API is called, the regKey is no longer used for preapproved payments.
        /// </summary>
        /// <param name="regKey">Registration Key</param>
        /// <returns></returns>
        public async Task<ResponseBase> RegKeyExpireAsync(string regKey)
        {
            var response = await client.PostAsync($"/{version}/payments/preapprovedPay/{regKey}/expire", null);
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResponseBase>(await response.Content.ReadAsStringAsync());
            else
                throw new Exception(await response.Content.ReadAsStringAsync());
        }
        #endregion

        /// <summary>
        /// Dispose client.
        /// </summary>
        public void Dispose()
        {
            client?.Dispose();
        }
    }
}