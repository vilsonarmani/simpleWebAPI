# simpleWebAPI
## Criando uma API utilizando ASP.NET Core e Swagger
   Os valores são passados no Body


## URLS disponíveis 
  /v1/Categories: [GET/POST]  
  https://localhost:44313/v1/Categories

  
​  /v1​/Products: [GET] 
  https://localhost:44313/v1/Products

  

## Exemplos Simples

Para utilizar será necessário utilizar o Postman
https://localhost:5001/v1/categories
body:
{
     "title":"Categoria teste"
}



https://localhost:44313/v1/products
body:

{
	"title": "Produto 5",
	"description": "teste produto 55555555",
	"price": 1200,
	"categoryId": 1;
}

{
	"title": "Produto 10",
	"description": "",
	"price": 1680,
	"categoryId": 1
}
{
	"title": "Produto 20",
	"description": "produto de n° 2000000",
	"price": 130.15,
	"categoryId": 1
}


