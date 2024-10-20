namespace Utils

open System

open Giraffe
open Microsoft.Extensions.Logging

module UtilsHandler =
    let errorHandler (ex : Exception) (logger : ILogger) =
        logger.LogError(EventId(), ex, "An unhandled exception has occurred while executing the request.")
        clearResponse >=> ServerErrors.INTERNAL_ERROR ex.Message
        
    let notFoundHandler : HttpHandler =
        RequestErrors.NOT_FOUND "Not Found"
