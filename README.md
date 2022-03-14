# TODO API SPEC

This is a proposal for my TODO API Spec

## End Points

### Create TODO
`POST /add`

request body:
```
{
    title: String,
    created: Date,
    due: Date,
    completed: Bool
}
```

response code:

`    201 - Created`

response body:
```
{
    id: GUID,
    title: String,
    created: Date,
    due: Date,
    completed: Bool
}
```

response code:
    
`    400 - Bad Request`

response body:
```
{
    message: String
}
```

### Get a TODO
`GET /todos/{id}`

request body
```
none
```

response code:
    
`    200 - Okay`

response body
```
{
    id: GUID,
    title: String,
    created: Date,
    due: Date,
    completed: Bool
}
```

response code: 
    
`    404 - Not Found`

response body
```
{
    message: String
}
```

### Get all TODOs
`GET /todos/`

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
    
`    200 - Okay`

response body:
```
{
    [
        {
            id: GUID,
            title: String,
            createdAt: Date,
            dueAt: Date,
            completed: Bool
        },
        {
            id: GUID,
            title: String,
            createdAt: Date,
            dueAt: Date,
            completed: Bool
        },
        ...
    ]
}
```

response code:
    
`    400 - Bad request`

response body:
```
{
    message: String
}
```

### Update a TODO
`PUT /todos/{id}`

request body:
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
    
`    200 - Okay`

response body:
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
   
`   40X - Client error status codes`

response body:
```
{
    message: String
}
```

### Delete a TODO
`DELETE /todos/{id}`

request body:
```
none
```

response code:
    
`    200 - Okay`

response body:
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
    
`    404 - Not found`

response body:
```
{
    message: String
}
```