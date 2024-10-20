namespace Utils

module Operations =
    let (=>>) opt func =
        match opt with
        | Ok x -> func x
        | Error e -> opt
