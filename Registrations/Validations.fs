module Registrations.Validations

open System
open Registrations.RegistrationDomain
open Utils.Operations

type public ValidationError =
    | Empty of string
    | InvalidFormat of string
    | InvalidPassword of string
    | Other of string
    
let private errorMsg (error : ValidationError) =
    match error with
    | Empty fieldName -> $"The {fieldName} cannot be empty"
    | InvalidFormat fieldName -> $"The {fieldName} has an Invalid format"
    | InvalidPassword errorMsg -> $"Invalid password {errorMsg}"
    | Other err -> err
   
let private validateName (user : CreateUserParams) (errors : ValidationError list) : CreateUserParams * ValidationError list =
    match String.IsNullOrWhiteSpace user.Name with
    | true -> user, Empty("Name") :: errors
    | false -> user, errors
    
let private validateEmail (user : CreateUserParams) (errors : ValidationError list) : CreateUserParams * ValidationError list =
    match String.IsNullOrWhiteSpace user.Email with
    | true -> user, Empty "Email" :: errors
    | false -> match user.Email.Contains("@") with
                | true -> user, errors
                | false -> user, InvalidFormat("Email") :: errors
                
let private validatePassword (user : CreateUserParams) (errors : ValidationError list) : CreateUserParams * ValidationError list =
    match String.IsNullOrWhiteSpace user.Password with
    | true -> user,  Empty "Password" :: errors
    | false -> match user.Password.Length >= 8 with
                    | false -> user, InvalidPassword "to short" :: errors
                    | true -> user , errors
                
let public validateCreateUser (createUser : CreateUserParams) =
     let user, errors = (createUser, [])
                                    ||>> validateName
                                    ||>> validateEmail
                                    ||>> validatePassword
     match List.Empty = errors with
     | true -> Ok user
     | false -> Error (errors |> List.map errorMsg |> String.concat "; ")
                                    