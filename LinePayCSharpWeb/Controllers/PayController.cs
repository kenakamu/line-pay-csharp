using Line.Pay;
using Line.Pay.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LinePayCSharpWeb.Controllers
{
    [Route("api/[controller]")]
    public class PayController : Controller
    {
        private LinePayClient client;
        private IConfiguration configuration { get; set; }

        public PayController()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            configuration = builder.Build();

            client = new LinePayClient(
                configuration["LinePay:ChannelId"],
                configuration["LinePay:ChannelSecret"],
                bool.Parse(configuration["LinePay:IsSandbox"]));
        }

        [HttpGet]
        [Route("payment")]
        public async Task<IActionResult> GetPayment(Int64? transactionId, string orderId)
        {
            var response = await client.GetPaymentAsync(transactionId, orderId);
            return new OkObjectResult(JsonConvert.SerializeObject(response));
        }

        [HttpGet]
        [Route("authorize")]
        public async Task<IActionResult> GetAuthorize(Int64? transactionId, string orderId)
        {
            var response = await client.GetAuthorizationAsync(transactionId, orderId);
            return new OkObjectResult(JsonConvert.SerializeObject(response));
        }

        [HttpGet]
        [Route("authorize/void")]
        public async Task<IActionResult> VoidAuthorization(Int64 transactionId)
        {
            var response = await client.VoidAuthorizationAsync(transactionId);
            return new OkObjectResult(JsonConvert.SerializeObject(response));
        }

        [HttpGet]
        [Route("reserve")]
        public async Task<IActionResult> Reserve(bool capture, PayType payType = PayType.NORMAL)
        {
            var reserve = new Reserve()
            {
                ProductName = "チョコレート",
                Amount = 1,
                Currency = Currency.JPY,
                OrderId =  Guid.NewGuid().ToString(),
                ConfirmUrl = $"{configuration["ServerUri"]}/api/pay/confirm",
                CancelUrl = $"{configuration["ServerUri"]}/api/pay/cancel",
                Capture = capture,
                PayType = payType
            };

            var response = await client.ReserveAsync(reserve);
            CacheService.Cache.Add(response.Info.TransactionId, reserve);

            return Redirect(response.Info.PaymentUrl.Web);
        }

        [HttpGet]
        [Route("confirm")]
        public async Task<IActionResult> Confirm()
        {
            var transactionId = Int64.Parse(HttpContext.Request.Query["transactionId"]);
            var reserve = CacheService.Cache[transactionId] as Reserve;

            var confirm = new Confirm()
            {
                Amount = reserve.Amount,
                Currency = reserve.Currency
            };

            if ((bool)reserve.Capture)
            {
                var response = await client.ConfirmAsync(transactionId, confirm);
                return new OkObjectResult(JsonConvert.SerializeObject(response));
            }
            else
            {
                var authorizationRes = await client.GetAuthorizationAsync(transactionId, reserve.OrderId);
                var captureRes = await client.CaptureAsync(transactionId, reserve.Amount, reserve.Currency);
                return new OkObjectResult(JsonConvert.SerializeObject(captureRes));
            }
        }

        [HttpGet]
        [Route("refund")]
        public async Task<IActionResult> Refund(Int64 transactionId, int amount)
        {            
            var response = await client.RefundAsync(transactionId, amount);
            return new OkObjectResult(JsonConvert.SerializeObject(response));
        }

        [HttpGet]
        [Route("regkey/check")]
        public async Task<IActionResult> Check(string regkey)
        {
            var response = await client.RegKeyCheckAsync(regkey);
            return new OkObjectResult(JsonConvert.SerializeObject(response));
        }

        [HttpGet]
        [Route("regkey/expire")]
        public async Task<IActionResult> Expire(string regkey)
        {
            var response = await client.RegKeyExpireAsync(regkey);
            return new OkObjectResult(JsonConvert.SerializeObject(response));
        }

        [HttpGet]
        [Route("preapprovedpay")]
        public async Task<IActionResult> PreApprovedPay(string regkey)
        {
            var reserve = new PreApprovedPay()
            {
                Amount = 1,
                Capture = true,
                Currency = Currency.JPY,
                ProductName = "チョコレート",
                OrderId = Guid.NewGuid().ToString()
            };
            var response = await client.PreApprovedPayAsync(regkey, reserve);
            return new OkObjectResult(JsonConvert.SerializeObject(response));
        }
    }
}
