namespace Utils

module Operations =
    let (=>>) opt func =
        match opt with
        | Ok x -> func x
        | Error e -> opt

    let (||>>) (opt1, opt2) func =
        let newOpt1, newOpt2 = func opt1 opt2
        newOpt1, newOpt2