# SSRentApi (Space Shuttle Rent API)

This is a common use web api written by .Net and we call it "Space Shuttle Rent Service" :D

In general, I often needed to use a service in .net trainings. That's why I wanted to write a general-use service. JWT token support is also available.

Panacea :P

## Setup

```shell

# to use Postgresql
docker run --name postgresql -e POSTGRES_USER=scoth -e POSTGRES_PASSWORD=tiger -p 5432:5432 -v /data:/var/lib/postgresql/data -d postgres

# to use ef tool
dotnet tool install -g dotnet-ef
# or update the existing dotnet-ef tool
dotnet tool update -g dotnet-ef

# for ef migrations
dotnet ef migrations add InitialCreate
dotnet ef database update

# For Visual Studio Developers

# in package manager console
Install-Package Microsoft.EntityFrameworkCore.Tools
# to create migration
Add-Migration initial-create
# to create/update database
Update-Database

```

## Docker

```shell

# start existing container
docker start postgresql
# to working container list
docker ps -a
# to stop container
docker stop postgresql
# drop container
docker rm [container_id]

# if you want you can use docker-compose file
docker-compose up

```

## Sample Requests

We will need a token information to get a response from the service functions. In this symbolic example, we can use UsersController's Register and Login functions to add users and get JWT values. Below is how we can get token information with the Login method.

```text
HTTP Post
http://localhost:5206/api/Users/Login

{
  "email": "skati@email.com",
  "password": "skati@1234"
}

```

To call any service function, we just need to add the Bearer token information to the Request. It's easy to do this in the Authorization section in Postman. Or we can integrate it into curl commands like below. On the other hand, there is an additional development for swagger to be able to use JWT tokens in the application.

```text
# Registera a new user
curl -X 'POST' \
  'http://localhost:5206/api/Users/Register' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer token_value' \
  -H 'Content-Type: application/json' \
  -d '{
  "id": 4,
  "name": "Takaşi Kovaç",
  "email": "kovac@email.com",
  "password": "kovac@1234"
}'

# User Login and get Token
curl -X 'POST' \
  'http://localhost:5206/api/Users/Login' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "email": "skati@email.com",
  "password": "skati@1234"
}'

# Get all categories
curl -X 'GET' \
  'http://localhost:5206/api/Categories' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer token_value'

# Get all ports
curl -X 'GET' \
  'http://localhost:5206/api/Ports' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer token_value'

# Get bookmarks
curl -X 'GET' \
  'http://localhost:5206/api/Bookmarks' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer token_value'

# Add Vehicle
curl -X 'POST' \
  'https://localhost:5206/api/Vehicles' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer token_value' \
  -H 'Content-Type: application/json' \
  -d '{
  "id": 1234,
  "name": "Thunder Bird",
  "detail": "Interstellar cruise",
  "portId": 2,
  "imageUrl": "thunder_bird.png",
  "price": 45600,
  "range": 1000000,
  "isTrending": false,
  "categoryId": 2
}'

# Add bookmark
curl -X 'POST' \
  'https://localhost:5206/api/Bookmarks' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer token_value' \
  -H 'Content-Type: application/json' \
  -d '{
  "id": 3,
  "status": true,
  "userId": 1,
  "vehicleId": 1234
}'

# Delete bookmark
curl -X 'DELETE' \
  'https://localhost:5206/api/Bookmarks/3' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer token_value'
```
