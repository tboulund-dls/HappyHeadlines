# DraftService.MD

## Endpoint: api/Draft


#### To create a draft
```http
POST api/Draft/
```
Request Body (JSON)
{
"title": "string",
"content": "string",
"author": "string"
}


### To update a draft
```http
PUT api/Draft/
```
Request Body (JSON)
{
"title": "string",
"content": "string",
"author": "string"
}


### To delete a draft
```http
DELETE api/Draft/{UserId}
```

### To get a draft
```http
GET api/Draft/{UserId}
```

### To get all drafts
```http
GET api/Draft
```