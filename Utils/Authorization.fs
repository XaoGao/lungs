namespace Utils

open System
open System.Security.Cryptography
open System.Text

module Authorization =
    let generatePasswordDigest (password : string) =
        let sha256 = SHA256.Create()
        let passwordBytes = Encoding.UTF8.GetBytes(password)
        let saltBytes = Encoding.UTF8.GetBytes("123")
        let combinedBytes = Array.append passwordBytes saltBytes
        let hashBytes = sha256.ComputeHash(combinedBytes)
        Convert.ToBase64String(hashBytes)
