# toDo_API
---
This is a proposal for my TODO API
--
## End Points

### Create TODO

Make a post request to the API to create a TODO.

`POST /add`

request body
```
{
    title: String,
    createdAt: Date,
    dueAt: Date,
    completed: Bool
}
```

response code:
    201 - Created

response body
```
{
    id: GUID,
    title: String,
    createdAt: Date,
    dueAt: Date,
    completed: Bool
}
```

response code:
    400 - Bad Request

```
{
    message: String
}
```

### Get a TODO
Get one TODO

`GET /fetch/{id}`

request body
```
none
```

response code:
    200 - Okay

response body
```
{
    id: GUID,
    title: String,
    createdAt: Date,
    dueAt: Date,
    completed: Bool
}
```

response code: 
    404 - Not Found

response body
```
{
    message: String
}
```