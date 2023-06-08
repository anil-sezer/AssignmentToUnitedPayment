# Services
###### API Endpoints that I will write

- [ ] Customer - POST (Save a customer card info)
1. Save Customer info to DB.
2. Check IdentityNo from https://tckimlik.nvi.gov.tr/Service/KPSPublic.asmx
3. Update check result to DB.

- [ ] Sell - POST (Sell a product)
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
  "ApiKey": "kU8@iP3@",
  "Email": "murat.karayilan@dotto.com.tr",
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
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJMYW5nIjoiVFIiLCJFbWFpbCI6Im11cmF0LmthcmF5aWxhbkBkb3R0by5jb20udHIiLCJVc2VySWQiOiI0NDEiLCJNZW1iZXJJZCI6IjEiLCJNZW1iZXJDb2RlIjoiQk8iLCJNZXJjaGFudElkIjoiMjE1IiwiTWVyY2hhbnROdW1iZXIiOiIyMTUiLCJVc2VyU3RhdHVzIjoiQSIsIkNoYW5nZVBhc3N3b3JkUmVxdWlyZWQiOiIwIiwiUGFzc3dvcmRTdGF0dXMiOiJVIiwiVXNlclJvbGVzIjoiMSIsIlJvbGVTY29yZSI6IjkwMCIsIlRpY2tldFR5cGUiOiJBUEkiLCJVc2VyVHlwZSI6IkFQSV9VU0VSIiwibmJmIjoxNjYyNDY2MTEwLCJleHAiOjE2ODA0NjYxMTAsImlhdCI6MTY2MjQ2NjExMH0.xhTJIWUJlEL3VZdmubXRn3MK_NHU56Qm9iMGVgLsjYo"
  },
  "count": 0,
  "responseCode": "00",
  "responseMessage": "İşlem Başarılı"
}
```

##### Payment - POST
https://ppgpayment-test.birlesikodeme.com:20000/api/ppg/Payment/NoneSecurePayment

(Sale, Void, Refund)

Request or response?
```json
{
    "Password": "kU8@iP3@",
    "Email": "murat.karayilan@dotto.com.tr",
    "Lang": "TR",
    "ApiKey" : "SKI0NDHEUP60J7QVCFATP9TJFT2OQFSO",
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

Web.Api: UPay.Web.Api
    |-> Controllers: UPay.Web.Api.Controllers
        |-> CustomerController
        |-> PaymentController

Domain: UPay.Domain
    |-> Entities: UPay.Domain.Entities
        |-> Customer
        |-> Transaction

DataAccess: UPay.DataAccess
    |-> Migrations: UPay.DataAccess.Migrations

Application: UPay.Application
    |
    |-> CustomerService
    |   |-> CustomerAppService + ICustomerAppService + Dtos
    |-> CustomerService
    |   |-> IdentityVerificationService + IIdentityVerificationService + Dtos
    |
    |-> PaymentService
        |-> PaymentAppService + IPaymentAppService + Dtos

3 Docker containers:
    - Web.Api
    - Postgres
    - Adminer (For easy db management)

Rest of it will be designed on the fly