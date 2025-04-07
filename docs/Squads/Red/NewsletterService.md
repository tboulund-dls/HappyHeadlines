Newsletter Service

The Newsletter Service is responsible for generating and sending newsletters to subscribers. It integrates with multiple services to fetch the latest articles, retrieve the subscriber list, and send emails.

Service Dependencies

Article Service: Provides the latest articles to include in the newsletter.

Subscriber Service: Supplies the list of subscribers who will receive the newsletter.

Mailpit: Handles the actual email delivery.

Endpoints

GET

/generate-newsletter

Fetches the latest articles from the Article Service and composes a newsletter.

/subscribers

Retrieves the list of subscribers from the Subscriber Service.

POST

/send-newsletter

Sends the generated newsletter to all subscribers via Mailpit.

Workflow

The service requests the latest articles from Article Service.

It fetches the list of subscribers from Subscriber Service.

A newsletter is generated with the latest articles.

The newsletter is sent to all subscribers using Mailpit.

Technologies Used

RESTful API

Mailpit for email delivery

Integration with Article Service and Subscriber Service

Configuration

Ensure the service has access to the following environment variables:

ARTICLE_SERVICE_URL=<URL to Article Service>
SUBSCRIBER_SERVICE_URL=<URL to Subscriber Service>
MAILPIT_URL=<URL to Mailpit>

Usage

This service runs on a scheduled basis or can be triggered manually via API calls. It ensures subscribers receive up-to-date content in their inboxes.

