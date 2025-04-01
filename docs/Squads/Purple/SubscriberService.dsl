# Level 3 - Subscriber Service
subscriberController = component "Subscriber Controller" "Handles HTTP requests for subscription actions and getting subscriber data" ".NET Controller"
subscriberService = component "Subscriber Service" "Provides business logic and validation" ".NET Scoped Service"
subscriberRepository = component "Subscriber Repository" "Communicates with a relation database" ".NET Scoped Service"
rabbitMQClient = component "RabbitMQ Client" "Handles communication with RabbitMQ" ".NET Scoped Service"

# Internal relationships
subscriberController -> subscriberService "Subscribe actions and get subscriber data"
subscriberService -> subscriberRepository "Subscribe actions and get subscriber data"
subscriberService -> rabbitMQClient "Publish subscriber events"