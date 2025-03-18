# Subscriber Service API Documentation

## Endpoints

### Get subscribers by type

```http
GET /api/v1/subscribe/{subscription-type}
```

#### Path Parameters

| Name              | Required | Type   | Description                                   |
|-------------------|----------|--------|-----------------------------------------------|
| subscription-type | Yes      | string | The type of subscription to query             |

**Allowed values for subscription-type:**
- `Basic`
- `Pro`

### Get subscriptions for an e-mail (subscriber)

```http
GET /api/v1/subscribe?email={email}
```

#### Query Parameters

| Name  | Required | Type   | Description                  |
|-------|----------|--------|------------------------------|
| email | Yes      | string | Email of the subscriber      |

**Example:**
```
GET /api/v1/subscribe?email=user@example.com
```

### Subscribe to newsletter

```http
POST /api/v1/subscribe
```

#### Request Body

| Name              | Required | Type   | Description                                   |
|-------------------|----------|--------|-----------------------------------------------|
| subscription-type | Yes      | string | The subscription tier                         |
| email             | Yes      | string | Email of the subscriber                       |

**Allowed values for subscription-type:**
- `Basic`
- `Pro`

### Unsubscribe from newsletter

```http
DELETE /api/v1/subscribe?email={email}
```

#### Query Parameters

| Name  | Required | Type   | Description                  |
|-------|----------|--------|------------------------------|
| email | Yes      | string | Email of the subscriber      |

**Example:**
```
DELETE /api/v1/subscribe?email=user@example.com
```