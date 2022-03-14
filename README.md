# toDo_API
---
This is a proposal for my TODO API
--
## End Points

### Create TODO
`POST /add`
request body
```
{
    title: "String",
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
