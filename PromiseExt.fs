namespace Fuse
module Promise =
    open System
    open Fable.Core
    open Fable.Import
    open Fable.Import.JS
    
    let success (a : 'T -> 'R) (pr : Promise<'T>) : Promise<'R> =
        pr?``then`` $ a |> unbox 

    let bind (a : 'T -> Promise<'R>) (pr : Promise<'T>) : Promise<'R> =
        pr?bind $ a |> unbox

    //catch Func<obj, U2<'T, JS.PromiseLike<'T>>>> -> JS.Promise<'T>
    //catch Func<obj, Unit>> -> JS.Promise<'T> 
    let fail (a : obj -> 'T)  (pr : Promise<'T>) : Promise<'T> =
        pr.catch (unbox<Func<obj, U2<'T, PromiseLike<'T>>>> a)
        
    //then Func<'T, U2<'R, JS.PromiseLike<'R>>>> * Func<obj, U2<'R, JS.PromiseLike<'R>>>> -> 'R
    //then Func<'T, U2<'R, JS.PromiseLike<'R>>>> * <Func<obj, Unit>> onrejected) -> 'R
    let either a  (b: Func<obj, U2<'R, JS.PromiseLike<'R>>>) (pr : Promise<'T>) : Promise<'R> =
        pr.``then``(a, b)
        //pr?``then`` $ (a, b) |> unbox

    let lift<'T> (a : 'T) : Promise<'T> =
        Promise.resolve(U2.Case1 a)
    
type PromiseBuilder() =
        member inline x.Bind(m,f) = Promise.success f m
        member inline x.Return(a) = Promise.lift a
        member inline x.ReturnFrom(a) = a
        member inline x.Zero() = Fable.Import.JS.Promise.resolve()

[<AutoOpen>]
module PromiseBuilderImp =
    let promise = PromiseBuilder()