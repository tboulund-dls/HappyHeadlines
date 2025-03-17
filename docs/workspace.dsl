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
            !docs System

            website = container "Website" "Shows the ten most recent articles with one focus article in the very top." "" "WebBrowser,BlueSquad" {
                !docs Squads/Blue/Website.md
                !include Squads/Blue/Website.dsl
            }
            articleService = container "ArticleService" "Responsible for collecting articles for (sub)systems that request them." "REST API"  "Service,RedSquad" {
                !docs Squads/Red/ArticleService.md
                !include Squads/Red/ArticleService.dsl
            }
            articleDatabase = container "ArticleDatabase" "Stores all articles that are published on the website." "" "Database,RedSquad" {
                !docs Squads/Red/ArticleDatabase.md
            }

            website -> articleService "Requesting articles"
            articleService -> articleDatabase "Fetching articles"
            reader -> website "Consumes news from the website"
            reader -> website "Subscribes to the daily newsletter"
            reader -> website "Subscribes to the immediate newsletter"

            webapp = container "Webapp" "The webapp where publishers can write and publish articles." "" "WebBrowser,OrangeSquad" {
                !docs Squads/Orange/Webapp.md
                !include Squads/Orange/Webapp.dsl
            }
            publisherService = container "PublisherService" "Responsible for handling the publishing of articles." "REST API" "Service,OrangeSquad" {
                !docs Squads/Orange/PublisherService.md
                !include Squads/Orange/PublisherService.dsl
            }
            draftService = container "DraftService" "Responsible for saving drafts of articles." "REST API" "Service,BlackSquad" {
                !docs Squads/Black/DraftService.md
                !include Squads/Black/DraftService.dsl
            }
            draftDatabase = container "DraftDatabase" "Stores all drafts of articles." "" "Database,BlackSquad" {
                !docs Squads/Black/DraftDatabase.md
            }
            articleQueue = container "ArticleQueue" "Queue where articles are put in when they are published." "" "Queue,OrangeSquad" {
                !docs Squads/Orange/ArticleQueue.md
            }

            publisher -> webapp "Writes articles for the system"
            webapp -> draftService "Saving a draft"
            webapp -> draftService "Fetching drafts"
            draftService -> draftDatabase "Storing a draft"
            
            webapp -> publisherService "Publishing an article"
            articleService -> articleQueue "Subscribes to the latest articles in order to persist them in a database."
            publisherService -> articleQueue "When an article is being published the article will be put into this queue."


            commentService = container "CommentService" "Responsible for handling comments on articles." "REST API" "Service,GreenSquad" {
                !docs Squads/Green/CommentService.md
                !include Squads/Green/CommentService.dsl
            }
            commentDatabase = container "CommentDatabase" "Stores all comments that are posted on articles." "" "Database,GreenSquad" {
                !docs Squads/Green/CommentDatabase.md
            }
            website -> commentService "Requesting comments"
            commentService -> commentDatabase "Fetching comments"
            website -> commentService "Posting a comment"
            commentService -> commentDatabase "Storing a comment"

            subscriberService = container "SubscriberService" "Responsible for handling subscribers." "REST API" "Service,PurpleSquad" {
                !docs Squads/Purple/SubscriberService.md
                !include Squads/Purple/SubscriberService.dsl
            }
            subscriberDatabase = container "SubscriberDatabase" "Stores all subscribers." "" "Database,PurpleSquad" {
                !docs Squads/Purple/SubscriberDatabase.md
            }
            subscriberQueue = container "SubscriberQueue" "Queue where subscribers are put in when they subscribe." "" "Queue,PurpleSquad" {
                !docs Squads/Purple/SubscriberQueue.md
            }
            website -> subscriberService "Subscribe to newsletters"
            website -> subscriberService "Unsubscribe from newsletters"
            subscriberService -> subscriberDatabase "Storing a subscriber"
            subscriberService -> subscriberDatabase "Removing a subscriber"
            subscriberService -> subscriberQueue "When a subscriber subscribes, the subscriber will be put into this queue."

            newsletterService = container "NewsletterService" "Responsible for sending out newsletters to subscribers." "REST API" "Service,RedSquad"{
                !docs Squads/Red/NewsletterService.md
                !include Squads/Red/NewsletterService.dsl
            }
            newsletterService -> articleService "Request article for daily newsletter"
            newsletterService -> articleQueue "Subscribes to receive the latest news first for immediate newsletter"
            newsletterService -> mailpit "Sends out the latest news immediately after they are published."
            newsletterService -> subscriberQueue "Send a welcome mail to new subscribers"
            newsletterService -> subscriberService "Gets all subscribers to send the newsletters to."
            mailpit -> proSubscriber "Sends out the latest news immediately after they are published."
            mailpit -> basicSubscriber "Sends out the latest news daily."

            profanityService = container "ProfanityService" "Responsible for filtering out profanity in comments." "REST API" "Service,CyanSquad" {
                !docs Squads/Cyan/ProfanityService.md
                !include Squads/Cyan/ProfanityService.dsl
            }
            profanityDatabase = container "ProfanityDatabase" "Stores all profanity words." "" "Database,CyanSquad" {
                !docs Squads/Cyan/ProfanityDatabase.md
            }
            commentService -> profanityService "Filtering out profanity"
            profanityService -> profanityDatabase "Fetching profanity words"
            webapp -> profanityService "Adding a new profanity word"
            webapp -> profanityService "Removing a profanity word"
            webapp -> profanityService "Fetching all profanity words"

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

        container hh "HappyHeadlines" {
            include *
            include basicSubscriber
            include proSubscriber
        }

        component hh.website "Website" {
            include *
            autoLayout lr
        }

        component hh.draftService "DraftService" {
            include *
            autoLayout lr
        }

        component hh.commentService "CommentService" {
            include *
            autoLayout lr
        }

        component hh.publisherService "PublisherService" {
            include *
            autoLayout lr
        }

        component hh.webapp "Webapp" {
            include *
            autoLayout lr
        }
        
        component hh.subscriberService "SubscriberService" {
            include *
            autoLayout lr
        }

        component hh.articleService "ArticleService" {
            include *
            autoLayout lr
        }

        component hh.newsletterService "NewsletterService" {
            include *
            autoLayout lr
        }

        component hh.profanityService "ProfanityService" {
            include *
            autoLayout lr
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
            element "BlueSquad" {
                background blue
            }
            element "GreenSquad" {
                background green
            }
            element "OrangeSquad" {
                background orange
            }
            element "BlackSquad" {
                background black
            }
            element "PurpleSquad" {
                background purple
            }
            element "RedSquad" {
                background red
            }
            element "CyanSquad" {
                background cyan
                color black
            }
            element "Queue" {
                shape Pipe
            }
            element "Service" {
                shape Hexagon
            }
        }
    }
}