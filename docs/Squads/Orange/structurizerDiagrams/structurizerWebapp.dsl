# Draw level 3 diagram of this service using DSL here.

workspace {

  model {
    # Define the person interacting with the system
    publisher = person "Publisher" {
      description "A user who writes and publishes articles."
    }

    # Define the software system
    webappSystem = softwareSystem "Webapp" {
      description "The frontend interface for publishers to manage drafts and submit articles."

      # Define the frontend container
      webappFrontend = container "Webapp Frontend" {
        description "A frontend interface used by publishers to manage drafts and publish articles."
        technology "React / Vue"

        # Define components inside the frontend
        draftList = component "DraftListComponent" {
          description "Displays the overview of all current drafts and their statuses."
          technology "React Component"
        }

        editor = component "EditorComponent" {
          description "Rich text editor used to write or update article content."
          technology "React Component"
        }

        publishButton = component "PublishButtonComponent" {
          description "Submits the article to PublisherService when ready to publish."
          technology "React Component"
        }
      }

      # Define the backend container
      publisherService = container "PublisherService" {
        description "Backend service that handles draft publishing logic."
        technology "REST API"
      }
    }

    # ✅ Fixed Relationships
    publisher -> webappFrontend "Interacts with the frontend"
    publisher -> draftList "Views list of drafts"
    publisher -> editor "Writes/edit drafts"
    publisher -> publishButton "Publishes articles"

    draftList -> publisherService "Fetches drafts"
    publishButton -> publisherService "Sends publish request"
  }

  views {
    systemContext webappSystem {
      include *
      autolayout lr
      title "Webapp - System Context View"
    }

    container webappSystem {
      include *
      autolayout lr
      title "Webapp - Container View"
    }

    component webappFrontend {
      include *
      autolayout lr
      title "Webapp Frontend - Component View (Level 3)"
    }

    theme default
  }
}
