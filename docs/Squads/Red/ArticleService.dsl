WebApi = component "WebApi"
Service = component "Service"
Database = component "Article Database"

hh.articleService.WebApi -> hh.articleService.service "Sends requests to service"
hh.articleService.service -> hh.articleService.database "Sends requests to database"