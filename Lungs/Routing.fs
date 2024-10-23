module Lungs.Routing

open Giraffe
open Registrations
open Sessions
open Utils.UtilsHandler

let routes : HttpFunc -> HttpFunc =
    choose [
        GET >=> choose [
            route "/"   >=> text "pong"
        ]
        RegistrationRouting.routes
        SessionsRoutes.routes
        notFoundHandler
    ]
