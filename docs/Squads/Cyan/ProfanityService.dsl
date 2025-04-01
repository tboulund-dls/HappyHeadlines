WebApi = component "WebApi(rest)"
sfrs = component "Service"
sfrr = component "Infrastructer(dall)"

// This is not a call into, but more a zoom into the service and how it's build.
// hh.profanityPortal  -> hh.profanityService.WebApi "frontend calls webapi"

hh.profanityService.WebApi -> hh.profanityService.sfrs "WebApit sends request to Service Layer"
hh.profanityService.sfrs -> hh.profanityService.sfrr "Sends request to InfraStucture"
// hh.profanityService.sfrr -> hh.profanityDatabase  "queries Database"
