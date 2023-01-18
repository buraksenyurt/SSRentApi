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

# get all categories
curl -X 'GET' \
  'http://localhost:5206/api/Categories' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer token_value'

# get all ports
curl -X 'GET' \
  'http://localhost:5206/api/Categories' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer token_value'
```
