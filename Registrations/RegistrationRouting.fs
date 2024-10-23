namespace Registrations

open Giraffe
open Registrations.RegistrationHandler

module RegistrationRouting =
    let routes : HttpHandler =
        choose [
            POST >=> choose [
                route "/registration" >=> registration
            ]
            PUT >=> choose [
                routef "/edit_profile/%s" editProfile 
            ]
            DELETE >=> choose [
                routef "/delete_profile/%s" deleteProfile 
            ]
        ]
