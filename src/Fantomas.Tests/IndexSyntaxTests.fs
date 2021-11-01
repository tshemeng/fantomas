﻿module Fantomas.Tests.IndexSyntaxTests

open NUnit.Framework
open FsUnit
open Fantomas.Tests.TestHelper

[<Test>]
let ``don't convert index syntax without dot to application`` () =
    formatSourceString
        false
        """
expr1[expr2]
"""
        config
    |> prepend newline
    |> should
        equal
        """
expr1[expr2]
"""

[<Test>]
let ``slicing examples`` () =
    formatSourceString
        false
        """
let arr = [| 1;2;3 |]
arr[0] <- 2
arr[0]
arr[0..1]
arr[..1]
arr[0..]
"""
        config
    |> prepend newline
    |> should
        equal
        """
let arr = [| 1; 2; 3 |]
arr[0] <- 2
arr[0]
arr[0..1]
arr[..1]
arr[0..]
"""

[<Test>]
let ``higher-dimensional arrays`` () =
    formatSourceString
        false
        """
let arr = Array4D.create 3 4 5 6 0
arr[0,2,3,4] <- 2
arr[0,2,3,4]
"""
        config
    |> prepend newline
    |> should
        equal
        """
let arr = Array4D.create 3 4 5 6 0
arr[0, 2, 3, 4] <- 2
arr[0, 2, 3, 4]
"""

[<Test>]
let ``only add spaces when expressions are atomic`` () =
    formatSourceString
        false
        """
let a = [ 2 .. 7 ] // integers
let b = [ one .. two ] // identifiers
let c = [ .. 9 ] // also when there is only one expression
let d = [ 0.7 .. 9.2 ] // doubles
let e = [ 2L .. number / 2L ] // complex expression
let f = [| A.B .. C.D |] // identifiers with dots
let g = [ .. (39 - 3) ] // complex expression
let h = [| 1 .. MyModule.SomeConst |] // not all expressions are atomic
for x in 1 .. 2 do
    printfn " x = %d" x
let s = seq { 0..10..100 }
"""
        config
    |> prepend newline
    |> should
        equal
        """
let a = [ 2..7 ] // integers
let b = [ one..two ] // identifiers
let c = [ ..9 ] // also when there is only one expression
let d = [ 0.7 .. 9.2 ] // doubles
let e = [ 2L .. number / 2L ] // complex expression
let f = [| A.B .. C.D |] // identifiers with dots
let g = [ .. (39 - 3) ] // complex expression
let h = [| 1 .. MyModule.SomeConst |] // not all expressions are atomic

for x in 1..2 do
    printfn " x = %d" x

let s = seq { 0..10..100 }
"""