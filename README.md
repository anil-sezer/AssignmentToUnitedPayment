# Services
###### API Endpoints that I will write

- [x] Buyer - POST (Save a customer card info)
1. Save Customer info to DB.
2. Check IdentityNo from https://tckimlik.nvi.gov.tr/Service/KPSPublic.asmx
3. Update check result to DB.

- [ ] Payment - POST (Process a payment)
1. Check cache for a token.
2. If no token, than login to ppgsec and get token and cache it. (Did it in Postman)
3. Sell product
4. Log the sell


### Checklist
- [x] Login to https://ppgsecurity-test.birlesikodeme.com
- [x] Check IdentityNo from https://tckimlik.nvi.gov.tr/Service/KPSPublic.asmx

# INVENTORY
###### What do I have?

Docs: https://dev.birlesikodeme.com/api-payzee/tr/test-cards

### Cards

#### Provision

| Bank   | Number              | Valid Date | CVV | 3DS Pass | Card Company | Card Type | Expected Response |
|--------|---------------------|:----------:|-----|:--------:|:-------------|-----------|-------------------|
| Akbank | 4355 0843 5508 4358 |  12/2026   | 000 |    a     | Visa         | Credit    | 00 - Başarılı     |
| Ziraat | 4546 7112 3456 7894 |     ""     | 000 |    a     | ""           | ""        | ""                |

todo: Add more

### DB

#### Customer
•	CustomerId
•	Name
•	Surname
•	BirthDate
•	IdentityNo
•	IdentityNoVerified
•	Status

#### Transaction
•	TransactionId
•	CustomerId
•	OrderId
•	TypeId (Sale, Void, Refund)
•	Amount
•	CardPan (Kart numarası)
•	ResponseCode
•	ResponseMessage
•	Status


## Payzee Services

#### Login - POST
https://ppgsecurity-test.birlesikodeme.com:55002/api/ppg/Securities/authenticationMerchant

Request:
```json
{
  "ApiKey": "****",
  "Email": "****",
  "Lang": "TR"
}
```

Response:
```json
{
  "fail": false,
  "statusCode": 200,
  "result": {
    "userId": 441,
    "token": "****"
  },
  "count": 0,
  "responseCode": "00",
  "responseMessage": "İşlem Başarılı"
}
```
When 'token' JWT is decoded:
```json
{
  "Lang": "TR",
  "Email": "murat.karayilan@dotto.com.tr",
  "UserId": "441",
  "MemberId": "1",
  "MemberCode": "BO",
  "MerchantId": "215",
  "MerchantNumber": "215",
  "UserStatus": "A",
  "ChangePasswordRequired": "0",
  "PasswordStatus": "U",
  "UserRoles": "1",
  "RoleScore": "900",
  "TicketType": "API",
  "UserType": "API_USER",
  "nbf": 1686328658,
  "exp": 1704328658,
  "iat": 1686328658
}
```


##### Payment - POST
https://ppgpayment-test.birlesikodeme.com:20000/api/ppg/Payment/NoneSecurePayment

(Sale, Void, Refund)

Request or response?
```json
{
    "Password": "****",
    "Email": "****",
    "Lang": "TR",
    "ApiKey" : "****",
    "MerchantId" : "1894",
    "MemberId"  : 1
}
```


#### Other


Hash: https://dev.birlesikodeme.com/api-payzee/tr/api/odeme-servisleri/payment-inquiry
```C#
public static string CreateHash(VposRequest request)
{

var hashPassword =""; // Bu bilgi size özel olup kayıtlı kullanıcınıza mail olarak gönderilmiştir.

var hashString = $"{hashPassword}{request.Rnd}{request.OrderNo}{request.TotalAmount}";

System.Security.Cryptography.SHA512 s512 = System.Security.Cryptography.SHA512.Create();

System.Text.UnicodeEncoding ByteConverter = new System.Text.UnicodeEncoding();

byte[] bytes = s512.ComputeHash(ByteConverter.GetBytes(hashString));

var hash = System.BitConverter.ToString(bytes).Replace("-","");

return hash;

}
```

# Structure
Project: UPay (United Payment)

**Used yaml template to show the structure clearly in both editor & display**
```yaml
Domain: 
  Entities:
    - Customer
    - Transaction

DataAccess:

Web.Api: 
  Controllers:
    - CustomerController
    - PaymentController

Application:
  Services:
    Buyer:
      - BuyerAppService + IBuyerAppService + Dtos
    IdentityVerificationService:
      - IdentityVerificationAppService + IIdentityVerificationService + Dtos
    PaymentService:
      - PaymentAppService + IPaymentAppService + Dtos
```
**Oh, and also the tests!**

3 Docker containers:
- Web.Api
- Postgres
- Adminer (For easy db management)

Rest of it will be designed on the fly

# Other others