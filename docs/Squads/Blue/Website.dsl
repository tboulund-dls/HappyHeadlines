workspace {
    model {
        user = person "User" "Consumes news and subscribes to newsletters"

        website = softwareSystem "Website" {
            container "Web Application" {
                component "News Reader" "Allows users to browse and read articles"
                component "Comment Manager" "Handles commenting functionality"
                component "Newsletter Manager" "Manages newsletter subscriptions"
            }
        }

        commentService = softwareSystem "Comment Service" {
            container "API" "Allows requests to post or retrieve comments"
        }

        articleService = softwareSystem "Article Service" {
            container "API" "Provides requested articles"
        }

        subscriberService = softwareSystem "Subscriber Service" {
            container "API" "Handles newsletter subscriptions"
        }

        // Relationships
        user -> website.container "Web Application" "Browses and interacts with"
        website.container "Web Application" -> commentService.container "API" "Requests and posts comments"
        website.container "Web Application" -> articleService.container "API" "Requests articles"
        website.container "Web Application" -> subscriberService.container "API" "Subscribes and unsubscribes to newsletters"
    }

    views {
        systemContext Website {
            include *
            autolayout lr
        }

        container Website {
            include *
            autolayout lr
        }

        component Website.Container "Web Application" {
            include *
            autolayout lr
        }
    }
}
