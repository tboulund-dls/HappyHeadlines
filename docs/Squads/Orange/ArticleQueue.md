<!-- Create a class diagram of the message this queue will contain -->

# ArticleQueue (Orange Squad)

## Squad Members
- Doina Plesca,
- Ane Høimark Olesen,
- Daniels Kanepe,
- Klaus H.


---

## Goal
`ArticleQueue` is responsible for delivering publish events from the `PublisherService` to the `NewsletterService`.
It acts as a message broker between the article publishing flow and the system that sends emails to subscribers.

---

## Responsibilities
- Receive published article events from `PublisherService`.
- Queue these events reliably using a message queue system.
- Deliver the messages to `NewsletterService`, which sends newsletters to subscribers.

---

## Technology Stack
- Queue System: RabbitMQ
- Language: .NetCore
- Message Format: JSON
- Communication Model: Publish/Subscribe (asynchronous)

---

## Example Message Payload

json
{
"articleId": "abc123",
"title": "Cities Go Green: Urban Forests Take Root",
"publishedAt": "2025-03-20T09:00:00Z"
}
---

## Data Flow
- When an article is published, the following sequence occurs:
- PublisherService publishes a new article.
- It creates a message with article metadata (ID, title, timestamp).
- The message is sent to ArticleQueue.
- NewsletterService listens to the queue and receives the message.
- NewsletterService then sends emails to all subscribers.

---
## Message Structure

The message sent to the queue has the following structure:

**Class: PublishedArticleMessage**

| Field         | Type      | Description                              |
|---------------|-----------|------------------------------------------|
| articleId     | String    | Unique ID of the published article       |
| title         | String    | The headline/title of the article        |
| publishedAt   | DateTime  | Timestamp of when the article was published |
| authorName    | String    | (Optional) Name of the author            |
| summary       | String    | (Optional) Short summary or preview text |

> This structure is serialized to JSON and placed in the `ArticleQueue`.


## Dependencies
- PublisherService – Acts as the message producer; sends data to the queue.
- NewsletterService – Acts as the message consumer; receives data from the queue and handles email delivery.

---

## Edge Cases & Error Handling
- If the queue is full or unavailable, messages are retried or stored in a fallback mechanism.
- If NewsletterService is down, messages stay in the queue and are delivered when it comes back online.
- All messages are logged for monitoring and debugging.

---

## Monitoring & Observability
- Dashboard for tracking message volume, failures, and retries.
- Logs stored for audit and debugging.