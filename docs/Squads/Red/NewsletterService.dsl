Draw level 3 diagram of this service using DSL here.
WebApi = component "WebApi"
Service = component "Service"


hh.newsletterService.WebApi -> hh.newsletterService.service "Sends requests to service"
hh.newsletterService.service -> hh.articleService.webapi "Sends request to get articles from the restapi for the newsletter"
hh.newsletterService.service -> hh.subscribers.webapi "Sends request to get subscribers"
hh.newsletterService.service -> hh.mailpit "sends out latest news to subscribers"
