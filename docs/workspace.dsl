workspace {

    !identifiers hierarchical

    model {


        // Actors
        reader = person "Reader" "Drops by the website to get positive news from around the world."
        basicSubscriber = person "BasicSubscriber" "Gets his positive news from mails or reader applications."
        proSubscriber = person "ProSubscriber" "Receives a mail with the latest news immediately after they are published"
        publisher = person "Publisher" "Writes articles for the HappyHeadlines news."
        moderator = person "Moderator" "Moderate the comments people post to the articles."

        mailpit = softwareSystem "Mailpit" "Service that sends out newsletters to subscribers." "ThirdParty" {
            !docs "Mailpit"
        }

        hh = softwareSystem "Happy Headlines" "Your go-to source for uplifting, inspiring, and feel-good news from around the world." {
            website = container "Website" "Shows the ten most recent articles with one focus article in the very top." "" "WebBrowser" 
            articleService = container "ArticleService" "Responsible for collecting articles for (sub)systems that request them." "API" "Service"
            articleDatabase = container "ArticleDatabase" "Stores all articles that are published on the website." "" "Database"

            website -> articleService "Requesting articles"
            articleService -> articleDatabase "Fetching articles"
            reader -> website "Consumes news from the website"
            reader -> website "Subscribes to the daily newsletter"
            reader -> website "Subscribes to the immediate newsletter"

            webapp = container "Webapp" "The webapp where publishers can write and publish articles." "" "WebBrowser"
            publisherService = container "PublisherService" "Responsible for handling the publishing of articles." "API" "Service"
            draftService = container "DraftService" "Responsible for saving drafts of articles." "" "Service"
            draftDatabase = container "DraftDatabase" "Stores all drafts of articles." "" "Database"
            articleQueue = container "ArticleQueue" "Queue where articles are put in when they are published." "" "Queue"

            publisher -> webapp "Writes articles for the system"
            webapp -> draftService "Saving a draft"
            webapp -> draftService "Fetching drafts"
            draftService -> draftDatabase "Storing a draft"
            
            webapp -> publisherService "Publishing an article"
            publisherService -> articleDatabase "Publishing an article"
            publisherService -> articleQueue "When an article is being published the article will be put into this queue."

            newsletterService = container "NewsletterService" "Responsible for sending out newsletters to subscribers." "" "Service"
            
            newsletterService -> articleService "Request article for daily newsletter"
            newsletterService -> articleQueue "Subscribes to receive the latest news first for immediate newsletter"
            newsletterService -> mailpit "Sends out the latest news immediately after they are published."
            mailpit -> proSubscriber "Sends out the latest news immediately after they are published."
            mailpit -> basicSubscriber "Sends out the latest news daily."

            commentService = container "CommentService" "Responsible for handling comments on articles." "" "Service"
            commentDatabase = container "CommentDatabase" "Stores all comments that are posted on articles." "" "Database"
            website -> commentService "Requesting comments"
            commentService -> commentDatabase "Fetching comments"
            website -> commentService "Posting a comment"
            commentService -> commentDatabase "Storing a comment"

            subscriberService = container "SubscriberService" "Responsible for handling subscribers." "" "Service"
            subscriberDatabase = container "SubscriberDatabase" "Stores all subscribers." "" "Database"
            website -> subscriberService "Subscribe to newsletters"
            website -> subscriberService "Unsubscribe from newsletters"
            subscriberService -> subscriberDatabase "Storing a subscriber"
            subscriberService -> subscriberDatabase "Removing a subscriber"
        }
        hh.newsletterService -> mailpit "Sends out newsletters to subscribers."
    }

    views {
        systemContext hh "SystemContext" {
            include *
            include basicSubscriber
            include proSubscriber
            autolayout lr
        }

        container hh "Comments" {
            include hh.commentService
            include hh.commentDatabase
            autolayout lr
        }

        container hh "HappyHeadlines" {
            include *
            include basicSubscriber
            include proSubscriber
        }
        
        styles {
            element "ThirdParty" {
                background lightgray
            }
            element "Element" {
                color #ffffff
            }
            element "Person" {
                background #9b191f
                shape person
            }
            element "Software System" {
                background #ba1e25
            }
            element "App" {
                shape "MobileDeviceLandscape"
            }
            element "Database" {
                shape cylinder
            }
            element "Container" {
                background #d9232b
            }
            element "Component" {
                background #E66C5A
            }
            element "WebBrowser" {
                shape WebBrowser
            }
            element "Queue" {
                shape Pipe
                stroke #d9232b
                background white
                color black
            }
            element "Service" {
                shape Hexagon
            }
        }
    }
}