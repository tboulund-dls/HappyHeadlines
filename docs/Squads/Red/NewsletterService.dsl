
WebApi = component "WebApi"
Service = component "Service"


hh.newsletterService.WebApi -> hh.newsletterService.service "Sends requests to service"
hh.newsletterService.service -> hh.articleService.webapi "Sends request to get articles from the restapi for the newsletter"
hh.newsletterService.service -> hh.subscriberService "sends request for subscribers"
hh.newsletterService.service -> mailpit "sends newsletter to mail out"