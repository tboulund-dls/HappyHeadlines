# DraftService.MD

## Endpoint: api/Draft


### To create a draft
```http
POST api/Draft/
```

#### Request Body (JSON)
```json
{
    "title": "string",
    "content": "string",
    "authorId": "string"
}
```


### To update a draft
```http
PUT api/Draft/
```
#### Request Body (JSON)
```json
{
    "id": "string",
    "title": "string",
    "content": "string",
    "authorId": "string"
}
```


### To delete a draft
```http
DELETE api/Draft/{Id}
```

### To get a draft
```http
GET api/Draft/{Id}
```

### To get all drafts from author
```http
GET api/Draft/{authorId}
```