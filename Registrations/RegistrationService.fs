namespace Registrations

open CommonDomain.Models
open Microsoft.Extensions.Logging
open MongoDB.Bson
open RegistrationRepository
open Registrations.RegistrationDomain
open Registrations.UserMapper

module RegistrationService =
    type IRegistrationService =
        abstract member CreateUser : CreateUserParams -> Result<User, string> 
        abstract member UpdateUser : string -> UpdateUserParams -> Result<User, string>
        abstract member DeleteUser : string -> Result<User, string> 
        
    type RegistrationService(registrationRepository : IRegistrationRepository,
                             logger : ILogger<RegistrationService>) =
        interface IRegistrationService with
            member _.CreateUser(createUserParams) : Result<User, string> =
                try
                    let user = toUser createUserParams
                    registrationRepository.CreateUser user
                    Ok user
                with ex ->
                    logger.LogError(ex, "Error creating user")
                    Error "Error creating user" 
                    
            member _.UpdateUser (userId : string) (updateUser : UpdateUserParams) : Result<User, string> =
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
                    let userOption = registrationRepository.FindById(ObjectId.Parse(userId))
                    match userOption with
                    | None -> Error "User not found" 
                    | Some user ->
                        match registrationRepository.DeleteUser user.Id with
                        | None -> Error "Error deleting user" 
                        | _ -> Ok user
                with ex ->
                    logger.LogError(ex, "Error deleting user")
                    Error "Error deleting user"
