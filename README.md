# CurrencyExchanger
Created according to the technical specifications presented in [this course](https://zhukovsd.github.io/java-backend-learning-course/Projects/CurrencyExchange/)
## Overview
REST API for describing currencies and exchange rates. Allows you to view and edit lists of currencies and exchange rates, and calculate the conversion of arbitrary amounts from one currency to another.
## API Reference
**GET /currencies**
  ```[
    {
        "id": 0,
        "name": "United States dollar",
        "code": "USD",
        "sign": "$"
    },   
    {
        "id": 0,
        "name": "Euro",
        "code": "EUR",
        "sign": "€"
    }
  ]
```
**HTTP response codes:**
```
Success - 200
Error (the database is not available) - 500
```
**GET /currency/${CURRENCY_CODE}**
```{
    "id": 0,
    "name": "Euro",
    "code": "EUR",
    "sign": "€"
}
```
**HTTP response codes:**
```
Success - 200
The currency code is not in the address - 400
Currency not found - 404
Error (for example, the database is not available) - 500
```
|Parameter|Type|Description|
|---------|----|-----------|
|`CURRENCY_CODE`|`string`|**Required**. of item to fetch|
**POST /currencies**
```
{
    "id": 0,
    "name": "Euro",
    "code": "EUR",
    "sign": "€"
}
```
**HTTP response codes:**
```
Success - 200
Required form field is missing - 400
Currency with this code already exists - 409
Error (the database is not available) - 500
```
|Parameter|Type|Description|
|---------|----|-----------|
|`name`|`string`|**Required**. of item to fetch|
|`code`|`string`|**Required**. of item to fetch|
|`sign`|`string`|**Required**. of item to fetch|
