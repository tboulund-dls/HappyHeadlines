# Draw level 3 diagram of this service using DSL here.

workspace {

  model {
    # Define the person interacting with the system
    publisher = person "Publisher" {
      description "A user who submits articles for publication."
    }

    # Define the software system
    publisherSystem = softwareSystem "PublisherService" {
      description "Handles the publishing of articles from finalized drafts, content moderation, and notifies other systems."

      # Define the main container within the system
      publisherService = container "PublisherService" {
        description "Backend service that handles publishing workflows, including validation, database saving, and queue notification."
        technology "Node.js / Java / Python"

        # Define components inside the container
        publishingController = component "PublishingController" {
          description "REST controller that handles incoming publish requests."
          technology "REST Controller"
        }

        moderationClient = component "ModerationServiceClient" {
          description "Handles communication with the ProfanityService for content moderation."
          technology "HTTP Client"
        }

        articleRepository = component "ArticleRepository" {
          description "Stores published articles in the ArticleDatabase."
          technology "Database Access Layer"
        }

        queuePublisher = component "QueuePublisher" {
          description "Sends article data to ArticleQueue after publishing."
          technology "Message Publisher"
        }
      }

      # External containers
      profanityService = container "ProfanityService" {
        description "Checks for inappropriate content."
        technology "REST API"
      }

      articleDatabase = container "ArticleDatabase" {
        description "Stores all published articles."
        technology "Database"
      }

      articleQueue = container "ArticleQueue" {
        description "Queues published articles for newsletter delivery."
        technology "Message Queue"
      }
    }

    # ✅ Corrected Relationships using exact container/component names
    publisher -> publisherService "Submits articles"

    publishingController -> moderationClient "Sends content for moderation"
    publishingController -> articleRepository "Saves published articles"
    publishingController -> queuePublisher "Triggers message to ArticleQueue"

    # ✅ Added missing external dependencies
    moderationClient -> profanityService "Checks content"
    articleRepository -> articleDatabase "Stores articles"
    queuePublisher -> articleQueue "Queues published articles"
  }

  views {
    systemContext publisherSystem {
      include *
      autolayout lr
      title "PublisherService - System Context View"
    }

    container publisherSystem {
      include *
      autolayout lr
      title "PublisherService - Container View"
    }

    component publisherService {
      include *
      autolayout lr
      title "PublisherService - Component View (Level 3)"
    }

    theme default
  }
}
