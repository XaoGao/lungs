module Lungs.Routing

open Giraffe
open SystemHandlers.NotFoundHandler

open Registrations
open Sessions

let routes : HttpFunc -> HttpFunc =
    choose [
        GET >=> choose [
            route "/"   >=> text "pong"
        ]
        RegistrationRouting.routes
        SessionsRoutes.routes
        notFoundHandler
    ]
