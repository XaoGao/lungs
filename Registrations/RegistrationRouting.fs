namespace Registrations

open Giraffe

module RegistrationRouting =
    let routes : HttpHandler =
        choose [
            POST >=> choose [
                route "/registration" >=> RegistrationHandler.create
            ]
            PUT >=> choose [
                routef "/edit_profile/%s" RegistrationHandler.update
            ]
            DELETE >=> choose [
                routef "/delete_profile/%s" RegistrationHandler.delete
            ]
        ]
