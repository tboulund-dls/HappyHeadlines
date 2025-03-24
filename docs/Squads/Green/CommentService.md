# Comment Service API

The CommentService is responsible for handling article comments, including fetching, posting, and moderating comments. It integrates with the ProfanityService to filter inappropriate content.

## Models
### Shared Models
#### CommentDTO
```json
{
  "id": "string",
  "articleId": "string",
  "content": "string",
  "author": "string",
  "createdAt": "datetime",
  "updatedAt": "datetime"
}
```
Used for returning comment data to clients in API responses.

#### NewCommentDTO
```json
{
  "articleId": "string",
  "content": "string",
  "author": "string"
}
```
Used when submitting a new comment to be created.

#### UpdateCommentDTO
```json
{
  "content": "string",
}
```
Used when updating an existing comment.

### Internal Models
#### Comment
```json
{
  "id": "string",
  "articleId": "string",
  "content": "string",
  "authorId":"string",
  "author": "string",
  "createdAt": "datetime",
  "updatedAt": "datetime"
}

```

## Endpoints

### Get Comments

#### Get Comments for Article
```
GET /api/v1/comments/{articleId}?page=1&pageSize=1
```

**Description:** Retrieves all comments for a specific article.

**Parameters:**
- `articleId` (path): ID of the article to get comments for
- `page` (query, optional): Page number for pagination
- `pageSize` (query, optional): Number of comments per page

**Response:**
```json
{
  "comments": [
    {
      "id": "string",
      "articleId": "string",
      "content": "string",
      "author": "string",
      "createdAt": "datetime",
      "updatedAt": "datetime"
    }
  ],
  "totalComments": "number",
  "page": "number",
  "pageSize": "number"
}
```

### Post Comment

```
POST /api/v1/comments/{articleId}
```

**Description:** Posts a new comment on an article. The service will automatically filter profanity by calling the ProfanityService.

**Parameters:**
- `articleId` (path): ID of the article to comment on

**Request Body:**
```json
{
    "articleId": "string",
    "content": "string",
    "author": "string",
}
```

**Response:**
```json
{
  "id": "string",
  "articleId": "string",
  "content": "string",
  "author": "string",
  "createdAt": "datetime"
}
```

### Get Single Comment

```
GET /api/v1/comments/{commentId}
```

**Description:** Retrieves a specific comment by ID.

**Parameters:**
- `commentId` (path): ID of the comment to retrieve

**Response:**
```json
{
  "id": "string",
  "articleId": "string",
  "content": "string",
  "author": "string",
  "createdAt": "datetime",
  "updatedAt": "datetime"
}
```

### Update Comment

```
PUT /api/v1/comments/{commentId}
```

**Description:** Updates an existing comment.

**Parameters:**
- `commentId` (path): ID of the comment to update

**Request Body:**
```json
{
  "id": "string",
  "articleId": "string",
  "content": "string",
  "author": "string",
  "status": "pending|approved|rejected"
}
```

**Response:**
```json
{
  "id": "string",
  "articleId": "string",
  "content": "string",
  "author": "string",
  "status": "pending|approved|rejected",
  "createdAt": "datetime",
  "updatedAt": "datetime"
}
```

### Delete Comment

```
DELETE /api/v1/comments/{commentId}
```

**Description:** Deletes a specific comment by ID.

**Parameters:**
- `commentId` (path): ID of the comment to delete

**Response:**
```
204 No Content
```

## Error Responses

All endpoints may return the following error responses:

- `400 Bad Request`: Invalid input parameters
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server-side error