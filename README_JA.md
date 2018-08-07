# LINE Pay dotnet core C# SDK
非公式の LINE Pay API 用 C# SDK とサンプルです。<br>
API 詳細は [こちら](https://pay.line.me/jp/developers/documentation/download/tech?locale=ja_JP) のドキュメントより確認可能です。

# 初めに
このレポジトリは SDK とサンプルの Web アプリが含まれます。ChannelId, ChannelSecret および ServerUri など必要な情報を更新して使ってください。

## サンドボックス環境の作成

[LINE Pay Developers](https://pay.line.me/developers/techsupport/sandbox/creation) からサンドボックス環境が作成できます。[LINE Pay コンソール](https://pay.line.me/login) にログインすると、環境に Channel Id と Channel Secret が取得可能となります。LINE Pay にアクセスするサーバーの IP アドレスをホワイトリストに追加してください。

# SDK の使い方

SDK は NuGet より取得できます。

**LINE Pay クライアントの作成**
チャネルの情報およびサンドボックスを使うか指定して、作成します。

```csharp
var channelId = "channel id";
var channelSecret = "channel secret";
var isSandbox = true;
var client = new LinePayClient(
                channelId,
                channelSecret,
                isSandbox);
```

**決済の予約**
Reserve オブジェクトを作成して、ReserveAsync メソッドに渡します。

```csharp
var reserve = new Reserve()
{
    ProductName = "チョコレート",
    Amount = 108,
    Currency = Currency.JPY,
    OrderId =  Guid.NewGuid().ToString(), // 発注番号
    ConfirmUrl = $"{configuration["ServerUri"]}/api/pay/confirm", // キャンセル Url
    CancelUrl = $"{configuration["ServerUri"]}/api/pay/cancel", // 確認 Url
}

var response = await client.ReserveAsync(reserve);
```

**決済の確定**
Confirm オブジェクトを作成して ConfirmAsync メソッドに渡します。

```csharp
var transactionId = Int64.Parse(HttpContext.Request.Query["transactionId"]);

var reserve = //予約情報を DB などから取得

var confirm = new Confirm()
{
    Amount = reserve.Amount,
    Currency = reserve.Currency
}

var response = await client.ConfirmAsync(transactionId, confirm);
```

他のメソッドについてはサンプルの Web アプリを参照してください。

# ライセンス
[MIT](./LICENSE)