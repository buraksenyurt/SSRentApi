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
dotnet ef migrations add initial-create
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
