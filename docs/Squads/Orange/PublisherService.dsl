# Draw level 3 diagram of this service using DSL here.

workspace {

  model {
    publisher = person "Publisher" {
      description "A user who submits articles for publication."
    }

    publisherSystem = softwareSystem "PublisherService" {
      description "Handles the publishing of articles from finalized drafts, content moderation, and notifies other systems."

      container publisherService "PublisherService" {
        description "Backend service that handles publishing workflows, including validation, database saving, and queue notification."
        technology "Node.js / Java / Python"

        component publishingController "PublishingController" {
          description "REST controller that handles incoming publish requests."
          technology "REST Controller"
        }

        component moderationClient "ModerationServiceClient" {
          description "Handles communication with the ProfanityService for content moderation."
          technology "HTTP Client"
        }

        component articleRepository "ArticleRepository" {
          description "Stores published articles in the ArticleDatabase."
          technology "Database Access Layer"
        }

        component queuePublisher "QueuePublisher" {
          description "Sends article data to ArticleQueue after publishing."
          technology "Message Publisher"
        }

        publisher -> publishingController "Submits articles"
        publishingController -> moderationClient "Sends content for moderation"
        publishingController -> articleRepository "Saves published articles"
        publishingController -> queuePublisher "Triggers message to ArticleQueue"
      }

      container profanityService "ProfanityService" {
        description "Checks for inappropriate content."
        technology "REST API"
      }

      container articleDatabase "ArticleDatabase" {
        description "Stores all published articles."
        technology "Database"
      }

      container articleQueue "ArticleQueue" {
        description "Queues published articles for newsletter delivery."
        technology "Message Queue"
      }
    }
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


