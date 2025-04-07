WebApi = component "WebApi"
Service = component "Service"


hh.newsletterService.WebApi -> hh.newsletterService.service "Sends requests to service"
hh.newsletterService.service -> hh.articleService.webapi "Sends request to get articles from the restapi for the newsletter"
hh.newsletterService.service -> hh.subscriberService.subscriberController "Sends request to get subscribers"
hh.newsletterService.service -> mailpit "sends out latest news to subscribers"

