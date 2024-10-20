namespace Registrations


open Database.Users
open Microsoft.Extensions.Logging
open MongoDB.Bson
open RegistrationRepository

open Utils.Authorization

module RegistrationService =
    type IRegistrationService =
        abstract member CreateUser : CreateUser -> Result<User, string> 
        abstract member UpdateUser : string -> UpdateUser -> Result<User, string>
        abstract member DeleteUser : string -> Result<User, string> 
        
    type RegistrationService(registrationRepository : IRegistrationRepository, logger : ILogger<RegistrationService>) =
        interface IRegistrationService with
            member _.CreateUser(createUser) : Result<User, string> =
                try
                    let user = {
                        Id = ObjectId.GenerateNewId()
                        Name = createUser.Name
                        Email = createUser.Email
                        PasswordDigest = generatePasswordDigest createUser.Password
                    }
                    registrationRepository.CreateUser user
                    Ok user
                with ex ->
                    logger.LogError(ex, "Error creating user")
                    Error "Error creating user" 
                    
            member _.UpdateUser (userId : string) (updateUser : UpdateUser) : Result<User, string> =
                try
                    let user = registrationRepository.FindById(ObjectId.Parse(userId))
                    match user with
                    | None -> Error "User not found" 
                    | Some u ->
                        let updatedUser = { u with Name = updateUser.Name; Email = updateUser.Email }
                        let result = registrationRepository.UpdateUser updatedUser 
                        match result with
                        | None -> Error "Error updating user" 
                        | _ -> Ok updatedUser
                with ex ->
                    logger.LogError(ex, "Error updating user")
                    Error "Error updating user"
        
            member _.DeleteUser(userId) : Result<User, string> =
                try
                    let user = registrationRepository.FindById(ObjectId.Parse(userId))
                    match user with
                    | None -> Error "User not found" 
                    | Some u ->
                        match registrationRepository.DeleteUser u.Id with
                        | None -> Error "Error deleting user" 
                        | _ -> Ok u
                with ex ->
                    logger.LogError(ex, "Error deleting user")
                    Error "Error deleting user"
