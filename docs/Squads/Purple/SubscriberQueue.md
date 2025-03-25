<!-- Create a class diagram of the message this queue will contain -->

# Subscriber Queue
When a new subscriber is added to the database, a message is sent to the subscriber queue. The message contains the subscriber's email and the subscription type.
```json
{
  "subscriberMail": "string",
  "subscriptionType": "string"
}
```