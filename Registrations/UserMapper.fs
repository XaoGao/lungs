namespace Registrations

open CommonDomain.Models
open MongoDB.Bson
open Registrations.RegistrationDomain
open Utils.PasswordHelper

module UserMapper =
    let toUser (createUserParams : CreateUserParams) : User =
        {
            Id = ObjectId.GenerateNewId()
            Name = createUserParams.Name
            Email = createUserParams.Email
            PasswordDigest = generatePasswordDigest createUserParams.Password
        }
