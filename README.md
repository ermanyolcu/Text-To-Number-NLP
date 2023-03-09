# TextToNumberNLP

Convert numbers written as Turkish text to numeric numbers. An NLP proof-of-concept made for an interview assessment.

Works as a MVC Web API, with Swagger UI to test the /api/nlp endpoint.

POST /api/nlp
```json
{
  "userText": "Elli 6 bin lira kredi alıp üç yılda geri ödeyeceğim."
}
```

Response
```json
{
  "success": true,
  "output": "56.000 lira kredi alıp 3 yılda geri ödeyeceğim.",
  "errorMessage": null
}
```
