namespace Registrations

open Giraffe
open Authorization.Authorize
open Registrations.RegistrationHandler

module RegistrationRouting =
    let routes : HttpHandler =
        choose [
            POST >=> choose [
                route "/registration" >=> registration
            ]
            PUT >=> authorize >=> choose [
                route "/edit_profile" >=> editProfile 
            ]
            // DELETE >=> choose [
            //     routef "/delete_profile/%s" deleteProfile 
            // ]
        ]
