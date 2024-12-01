module Sessions.SessionsRoutes

open Authorization.Authorize
open Giraffe
open SessionsHandler

let routes : HttpHandler =
    choose [
        POST >=> choose [
            route "/login" >=> login
        ]
        DELETE >=> authorize >=> choose [
            route "/logout" >=> logout
        ]
    ]