module PropertyTestsFs.PropertyTestsFs

open FsCheck
open NUnit.Framework
open Prop
open Gen
open CaseStudy

type SS = StringSplitting

let myConfig = Config.QuickThrowOnFailure

let prop_join_split c xs =
    SS.Join(c, SS.Split(c, xs)) = xs

[<Test>]
let ``Join of Split gives original string``() =
    Check.One(myConfig, prop_join_split)

let collect c xs =
     match SS.Split(c, xs) with
         | null -> "(null)"
         | ys -> Seq.length ys |> string

let prop_join_split_collect c xs =
    prop_join_split c xs |> Prop.collect (collect c xs)

[<Test>]
let ``Join of Split gives original string - with collect``() =
    Check.One(myConfig, prop_join_split_collect)

let prop_join_split' xs =
    forAll (Arb.fromGen (elements xs)) (fun c -> prop_join_split c xs)

[<Test>]
let ``Join of Split gives original string - where sep char is taken from non empty string``() =
    let arb = Arb.Default.NonEmptyString()
    Check.One(myConfig, forAll arb (fun nes -> prop_join_split' nes.Get))

[<Test>]
let ``Join of Split gives original string - where sep char is taken from non empty string - with collect``() =
    let gen = gen {
        let! nes = Arb.Default.NonEmptyString().Generator
        let xs = nes.Get
        let! c = elements xs
        return (c, xs)
    }
    let arb = Arb.fromGen gen
    Check.One(myConfig, forAll arb (fun tuple -> tuple ||> prop_join_split_collect))
