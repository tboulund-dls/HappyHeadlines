<!-- Account for the REST-endpoints of your API here -->

# PublisherService (Orange Squad)

## Squad Members
- Doina Plesca,
- Ane Høimark Olesen,
- Daniels Kanepe,
- Klaus H.

---

## Goal
The `PublisherService` manages the publishing workflow for articles.
It receives finalized drafts, validates them, stores them as published articles, and notifies other services like `NewsletterService` via `ArticleQueue`.

---

## Responsibilities
- Accept finalized drafts from the publisher's web interface.
- Optionally send content to `ProfanityService` for moderation.
- Convert approved drafts into published articles.
- Save published articles to the `ArticleDatabase`.
- Send publish events to `ArticleQueue` for newsletter delivery.

---

## Technology Stack
- Language: .NetCore
- Database: PostgreSQL or MongoDB (connected via `ArticleDatabase`)
- Queue Integration: RabbitMQ / Kafka
- API Style: RESTful API

---
## API Endpoints

The `PublisherService` exposes the following RESTful API endpoints:

| Method | Endpoint                 | Description                                          |
|--------|--------------------------|------------------------------------------------------|
| `POST` | `/publish`               | Publishes a finalized draft article                  |
| `POST` | `/safedraft`             | Publishes a finalized draft article                  |
| `POST` | `/moderate` _(optional)_ | Sends the article content to `ProfanityService`      |
| `GET`  | `/status/:articleId`     | Returns the current publication status of an article |
| `GET`  | `/published`             | Retrieves a list of published articles (optional)    |
| `GET`  | `/drafts`                | Retrieves a list of drafts                           |

---

### Example Payload (for `/publish`)

json
{
"title": "Cities Go Green: Urban Forests Take Root",
"content": "Cities around the world are planting trees...",
"author": "Jordan Ellis",
"publishedAt": "2025-03-20T09:00:00Z"
}

---

## Data Flow

1. A publisher submits a finalized draft via the `Webapp`.
2. `PublisherService` optionally checks content with `ProfanityService`.
3. If approved, the article is saved to the `ArticleDatabase`.
4. A publish event is sent to `ArticleQueue`.
5. `NewsletterService` receives the event and notifies subscribers.

---

## Dependencies

-  `Webapp` – Frontend used by publishers to write and submit articles.
-  `ProfanityService` – (Optional) Used to moderate content.
-  `ArticleDatabase` – Where published articles are stored.
-  `ArticleQueue` – Used to notify `NewsletterService`.

---

## Error Handling & Edge Cases

-  If profanity check fails → article is rejected with a clear message.
-  If `ArticleDatabase` is down → retry logic or queue for retry.
-  If `ArticleQueue` is unavailable → log and retry publish event.
-  Response codes and validation errors are clearly communicated to the frontend.

---

## Observability

- Logging: For draft submissions, moderation status, and publish events.
- Monitoring: Publish success/failure, queue delivery status.


