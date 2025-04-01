WebApi = component "WebApi"
Service = component "Service"
Database = component "Draft Database"

hh.draftService.WebApi -> hh.draftService.Service "Sends requests to service"
hh.draftService.Service -> hh.draftService.Database "Sends requests to database"