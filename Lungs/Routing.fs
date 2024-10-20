module Lungs.Routing

open Giraffe
open Registrations
open Utils.UtilsHandler
open RegistrationRouting

let routes : HttpFunc -> HttpFunc =
    choose [
        GET >=> choose [
            route "/"   >=> text "pong"
        ]
        RegistrationRouting.routes
        notFoundHandler
    ]
