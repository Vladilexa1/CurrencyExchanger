# CurrencyExchanger
Created according to the technical specifications presented in [this course](https://zhukovsd.github.io/java-backend-learning-course/Projects/CurrencyExchange/)
## Overview
REST API for describing currencies and exchange rates. Allows you to view and edit lists of currencies and exchange rates, and calculate the conversion of arbitrary amounts from one currency to another.
## API Reference
GET /currencies
  `[
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
  ]`
