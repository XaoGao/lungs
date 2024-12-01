module Sessions.SessionsRoutes

open Giraffe
open SessionsHandler

let routes : HttpHandler =
    choose [
        POST >=> choose [
            route "/login" >=> login
        ]
    ]