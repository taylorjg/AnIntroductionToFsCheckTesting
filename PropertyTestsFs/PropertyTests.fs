module PropertyTests

open FsCheck
open FsCheck.NUnit
open CaseStudy

type SS = StringSplitting

let myConfig = Config.VerboseThrowOnFailure

let prop_join_split c xs =
    SS.Join(c, SS.Split(c, xs)) = xs

[<Property>]
let ``Join of Split gives original string``() =
    Check.One(myConfig, prop_join_split)
