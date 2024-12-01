module Lungs.Middlewares

open System.Threading.Tasks
open Giraffe
open Microsoft.AspNetCore.Http

type Middleware = HttpFunc -> HttpContext -> Task<HttpContext option>
 