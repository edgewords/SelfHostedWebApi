Run the SelfHostedWebApi.exe as Administrator!!!

This holds a list of products with the following key/values
id: int
name: string
price: int

You can get, update, add and delete products.
Here are the requests and possible response codes:

GET - All products: http://127.0.0.1:2002/api/products/
	200 - OK

GET - A product by ID: http://127.0.0.1:2002/api/products/1
	200 - OK
	404 - NotFound

POST - Add a Product: http://127.0.0.1:2002/api/products/
POST /api/products/ HTTP/1.1
Host: 127.0.0.1:2002
Content-Type: application/json

  {
    "id": 5,
    "name": "PC",
    "price": 250
  }

	201 - Created
	400 - BadRequest


PUT - Update a product: http://127.0.0.1:2002/api/products/1
PUT /api/products/1 HTTP/1.1
Host: 127.0.0.1:2002
Content-Type: application/json

{
    "name": "HDD",
    "price": 125
}

	200 - Ok
	400 - BadRequest


DELETE - delete a product: http://127.0.0.1:2002/api/products/1
DELETE /api/products/1 HTTP/1.1
Host: 127.0.0.1:2002

	200 - Ok
	400 - BadRequest
