# TODO API SPEC

This is a proposal for my TODO API Spec

# Resources
- User - A user represents the owner of a collection of Todos
- Todo - A task to completed by the user who owns it.
- Session - A session represents an authenticated user session, and only one session per user can be active at a time.

# Data Tables

## TODO
```
ID - Guid (PK)
userID - GUID (FK)
title - String
created - DateTime
due - DateTime
completed - Bool
```

## Users
```
ID - GUID (PK)
username - String
password - String (Hash)
```

# Data Models
## Users
```
ID - Guid
Username - String
Password - String (Hash)
Todos - Todo []
```

## Todo:
```
id - GUID
userID - GUID
title - String
created - DateTime
due - DateTime
completed - Bool
```

## Session:
```
ID - GUID
Token - String (JWT)
Expiration - DateTime
```

# TODO End Points

## Create TODO
`POST /todo`

headers:
```
"Authorization": Bearer <Token>
```

request body:
```
{
    title: "String",
    due: "DateTime",
    completed: Bool
}
```

response code:

`201 - Created`

response body:
```
{
    id: "GUID",
    title: "String",
    created: "DateTime",
    due: "DateTime",
    completed: Bool
}
```

response code:
    
`400 - Bad Request`

response body:
```
{
    message: "String"
}
```

## Get a TODO
`GET /todo/{id}`

headers:
```
"Authorization": Bearer <Token>
```

request body
```
none
```

response code:
    
`200 - Okay`

response body:
```
{
    id: "GUID",
    title: "String",
    created: "DateTime",
    due: "DateTime",
    completed: Bool
}
```

response code: 
    
`404 - Not Found`

response body:
```
{
    message: "String"
}
```

## Get all TODOs
`GET /todo/`

headers:
```
"Authorization": Bearer <Token>
```

URL Parameters
```
Name: completed
Valid values: true, false
Required: no
Description: If provided, the api will only return TODOs which have been completed if the given value is `true`. If the value is `false`, the API will only return TODOs which have not been completed.

Name: field
Valid values: created, due, title
Required: no
Description: If provided, the TODOs will be sorted by the URL parameters corresponding value for each TODO. The default order these will be filtered by is descending.

Name: order
Valid values: asc, desc
Required: no
Description: This parameter is used to determine the order in which to arrange the TODOs. This parameter must be used with the field URL parameter.
```

request body:
```
none
```

response code: 
    
`200 - Okay`

response body:
```
{
    [
        {
            id: "GUID",
            title: "String",
            created: "DateTime",
            due: "DateTime",
            completed: Bool
        },
        {
            id: "GUID",
            title: "String",
            created: "DateTime",
            due: "DateTime",
            completed: Bool
        },
        ...
    ]
}
```

response code:
    
`400 - Bad request`

response body:
```
{
    message: "String"
}
```

## Update a TODO
`PUT /todo/{id}`

headers:
```
"Authorization": Bearer <Token>
```

request body:
```
{
    id: "GUID",
    title: "String",
    created: "DateTime",
    due: "DateTime",
    completed: Bool
}
```

response code:
    
`200 - Okay`

response body:
```
{
    id: "GUID",
    title: "String",
    created: "DateTime",
    due: "DateTime",
    completed: Bool
}
```

response code:
   
`4XX - Client error status codes`

response body:
```
{
    message: "String"
}
```

## Delete a TODO
`DELETE /todo/{id}`

headers:
```
"Authorization": Bearer <Token>
```

request body:
```
none
```

response code:
    
`200 - Okay`

response body:
```
{
    id: "GUID",
    title: "String",
    created: "DateTime",
    due: "DateTime",
    completed: Bool
}
```

response code:
    
`404 - Not found`

response body:
```
{
    message: "String"
}
```

# User Endpoints
## Create a User
`POST /users`

request body:
```
{
    "data": {
        "username": "john_lewis",
        "password": "P@ssw0rd!"
    }
}
```

response code:
`201 - Created`

response body:
```
{
    "message": "Success"
}
```

headers:
```
"Location": "/users/{id}"
```

response code:
`400 Bad Request` - Request payload invalid

response code:
`409 Conflict` - User with username already exists

response body:
```
{
    "message": "Username already exists"
}
```

## Fetch users
`GET /users`

request body:
```
none
```

response code: 
    
`200 - Okay`

response body:
```
{
    [
        {
            id: "3fad06ad-88f5-432d-b91d-0415fdddf015",
            username: "johnny_boi"
        },
        {
            id: "3fad06ad-88f5-432d-b91d-0415fdddf016",
            username: "johnny_dog"
        },
        ...
    ]
}
```

## Update a user
`PUT /users/{id}`

headers:
```
"Authorization": Bearer <Token>
```
request body:
```
{
    "data": {
        "password": "passw3rd",
        "new_password": "P@ssw0rd"
    }
}
```

response code:

`204 - No content` - The password was successfully updated

response body:
```
none
```

response code:

`400 - Bad request` - request payload format incorrect or {id} does not exist

response code:

`401 - Unauthorized` - user hasn't been authenticated

response code:

`403 - Forbidden` - user doesn't have permission to update user with this {id}

response code:

`404 - Not found` - user with id {id} does not exist

## Delete a user
`DELETE /users/{id}`
headers:
```
"Authorization": Bearer <Token>
```

request body:
```
none
```

response code:

`204 - no content` - The user was successfully deleted.

response body:
```
none
```

response code:

`400 - Bad request` - request payload format incorrect or {id} does not exist

response code:

`401 - Unauthorized` - user hasn't been authenticated

response code:

`403 - Forbidded` - user doesn't have permission to delete user with this {id}

response code:

`404 - Not found` - user with id {id} does not exist

# Login
`POST /login`

request body:
```
{
    "data": {
        "username": "johnnyBoi",
        "password": "p@ssw0rd"
    }
}
```

response code:
`200 - Okay` - Successful login

response body:
```
{
    "message": "success"
    "data": {
        "token": "eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWV9",
        "id": "3fad06ad-88f5-432d-b91d-0415fdddf016"
    }
}
```

response code:
`400 - Bad request` - Request payload in wrong format

response code:
`401 - Conflict` - A user with the given username / password could not be found.

response body:
```
{
    "message": "Invalid username or password"
}
```