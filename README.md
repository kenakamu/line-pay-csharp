# LINE Pay dotnet core C# SDK
※日本語のドキュメントは[こちら](./README_JA.md)

This is unofficial SDK for LINE Pay for C# developers.<br>
You can find detail API document [here](https://pay.line.me/jp/developers/documentation/download/tech?locale=en_US)

# Getting Started
This repository contains sample web application to use the SDK.
Update ChannelId, ChannelSecret and ServerUri.

## Create sandbox

Go to [LINE Pay Developers](https://pay.line.me/developers/techsupport/sandbox/creation) and create your sandbox. You can retrieve Channel Id and Channel Secret Key after successful login to [LINE Pay Console](https://pay.line.me/login). You also need to configure white lists of server ip addresses which access to LINE Pay API.

# How to use the SDK

**Instantiate client**
Pass channel information and if you use sandbox or not.

```csharp
var channelId = "channel id";
var channelSecret = "channel secret";
var isSandbox = true;
var client = new LinePayClient(
                channelId,
                channelSecret,
                isSandbox);
```

**Reserve the order**
Instantiate reserve object and pass to ReserveAsync

```csharp
var reserve = new Reserve()
{
    ProductName = "Chocolate",
    Amount = 2.2,
    Currency = Currency.USD,
    OrderId =  Guid.NewGuid().ToString(), // Unique order id
    ConfirmUrl = $"{configuration["ServerUri"]}/api/pay/confirm", // Cancel Url
    CancelUrl = $"{configuration["ServerUri"]}/api/pay/cancel", // Confirm Url
}

var response = await client.ReserveAsync(reserve);
```

**Confirm the order**
Instantiate confirm object by using reserve you created, and call ConfirmAsync.

```csharp
var transactionId = Int64.Parse(HttpContext.Request.Query["transactionId"]);

var reserve = //get your reserve from db.

var confirm = new Confirm()
{
    Amount = reserve.Amount,
    Currency = reserve.Currency
}

var response = await client.ConfirmAsync(transactionId, confirm);
```

See the sample web app in the repository for more detail.

# License
[MIT](./LICENSE)