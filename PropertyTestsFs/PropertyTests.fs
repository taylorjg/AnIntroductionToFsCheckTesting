module PropertyTests

open FsCheck
open FsCheck.NUnit
open Prop
open Gen
open CaseStudy

type SS = StringSplitting

let myConfig = Config.QuickThrowOnFailure

let prop_join_split c xs =
    SS.Join(c, SS.Split(c, xs)) = xs

[<Property>]
let ``Join of Split gives original string``() =
    Check.One(myConfig, prop_join_split)

let collect c xs =
     match SS.Split(c, xs) with
         | null -> "(null)"
         | ys -> Seq.length ys |> string

let prop_join_split_collect c xs =
    prop_join_split c xs |> Prop.collect (collect c xs)

[<Property>]
let ``Join of Split gives original string - with collect``() =
    Check.One(myConfig, prop_join_split_collect)

let prop_join_split' xs =
    forAll (Arb.fromGen (elements xs)) prop_join_split

[<Property>]
let ``Join of Split gives original string - where sep char is taken from non empty string``() =
    let arb = Arb.Default.NonEmptyString()
    Check.One(myConfig, Prop.forAll arb (fun nes -> prop_join_split' nes.Get))
