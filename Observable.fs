namespace Fuse

open System
open Fable.Core
open Fable.Import.JS    

module Observable =   

    type IObservable<'T> =
        abstract member value : 'T with get, set        
        abstract member map<'T,'U> : ('T -> 'U) -> IObservable<'U>
        abstract member where: ('T -> bool) -> IObservable<'T>
        // Lambdas with more than one argument must be converted to delegates to be called
        // properly from JS: See https://github.com/fsprojects/Fable/blob/master/docs/source/docs/interacting.md#calling-f#-code-from-javascript
        [<Emit("$0.map($1)")>] abstract member mapi<'T, 'U> : Func<'T,int,'U> -> IObservable<'U>
        abstract member count: unit -> IObservable<int>
        [<Emit("$0.count($1)")>] abstract member countWhere: ('T -> bool) -> IObservable<int>     
        abstract member add: 'T -> unit
        abstract member remove: 'T -> unit
        abstract member tryRemove: 'T -> bool
        abstract member removeAt: int -> unit
        abstract member removeWhere: ('T -> bool) -> unit
        abstract member refreshAll: IObservable<'T> -> Func<'T,'T,bool> -> Func<'T,'T,unit> -> ('T -> 'T) -> unit
        abstract member forEach : ('T -> unit) -> unit        
        abstract member clear : unit -> unit
        abstract member indexOf : 'T -> int
        abstract member contains : 'T -> bool
        abstract member filter : ('T -> bool) -> IObservable<'T>
        abstract member expand : unit -> IObservable<'T>
        abstract member addSubscriber: (IObservable<'T> -> unit) -> unit
        abstract member removeSubscriber: (IObservable<'T> -> unit) -> unit
        abstract member length: int with get
        abstract member toArray: unit -> 'T array
        abstract member getAt : int -> 'T
        abstract member replaceAt : int -> 'T -> unit
        abstract member replaceAll : 'T array -> unit

    type IUnsafeObservable<'T> = 
        inherit IObservable<'T>
        [<Emit("$0.value{{=$1}}")>] abstract member valueOverride : obj with get, set

    type IObservable = 
        inherit IObservable<obj>                

    type private ObservableFactory =
        [<Emit("$0($1)")>] abstract Invoke<'T> : element: 'T -> IObservable<'T>
        [<Emit("$0()")>] abstract Invoke<'T> : unit -> IObservable<'T>
        [<Emit("$0($1)")>] abstract InvokeUnsafe<'T> : element: 'T -> IUnsafeObservable<'T>
        [<Emit("$0()")>] abstract InvokeUnsafe<'T> : unit -> IUnsafeObservable<'T>
        [<Emit("$0()")>] abstract Invoke : unit -> IObservable
        [<Emit("$0($1...)")>] abstract InvokeList<'T> : [<ParamArray>] elements : 'T array -> IObservable<'T>

    // An empty string is also equivalent to "default"
    [<Import("default", "FuseJS/Observable")>]
    let private observable: ObservableFactory = failwith "JS only"

    let createWith<'T> (elem : 'T) = observable.Invoke(elem)
    let createTyped<'T>() = observable.Invoke<'T>()
    let createUnsafeWith<'T> (elem : 'T) = observable.InvokeUnsafe(elem)
    let createUnsafeTyped<'T> = observable.InvokeUnsafe<'T>()
    let create () = observable.Invoke()
    // A problem has been found in Fable (to be fixed) if this function is not inlined
    let inline createList<'T> (elements : 'T array) = observable.InvokeList<'T>(elements)
    
