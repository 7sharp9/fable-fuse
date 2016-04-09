namespace Fuse

open Fable.Core
open Fable.Import.JS    

module Observable =   

    type IObservable<'T> =        
        abstract member value : 'T with get, set
        abstract member map<'T,'U> : ('T -> 'U) -> IObservable<'U>
        abstract member where: ('T -> bool) -> IObservable<'T>
        [<Emit("$0.map($1...)")>] abstract member mapi<'T, 'U> : ('T * int -> 'U) -> IObservable<'U> // This is NOT implemented correctly
        abstract member count: unit -> IObservable<int>
        [<Emit("$0.count($1...)")>] abstract member countWhere: ('T -> bool) -> IObservable<int>        
        abstract member forEach : ('T -> unit) -> unit        
        abstract member clear : unit -> unit
        abstract member indexOf : 'T -> int
        abstract member contains : 'T -> bool
        abstract member filter : ('T -> bool) -> IObservable<'T>
        abstract member expand : unit -> IObservable<'T>
        abstract member addSubscriber: ('T -> unit) -> unit
        abstract member removeSubscriber: ('T -> unit) -> unit
        abstract member length: unit -> int
        abstract member toArray: unit -> 'T array
        abstract member getAt : int -> 'T
        abstract member replaceAt : int -> 'T -> unit
        abstract member replaceAll : 'T array -> unit

    type IObservable =
        abstract member value : obj with get, set
        abstract member replaceAll : obj array -> unit
        abstract member count: unit -> int
        [<Emit("$0.count($1...)")>] abstract member countWhere: (obj -> bool) -> IObservable

    type private ObservableFactory =
        abstract Invoke<'T> : element: 'T -> IObservable<'T>
        abstract Invoke<'T> : unit -> IObservable<'T>
        abstract Invoke : unit -> IObservable
        [<Emit("(0, _Observable2.default)(...$1)")>] // This doesn't work
        abstract InvokeList<'T> : elements : 'T array -> IObservable<'T>

    type private Globals =
        static member observable with get(): ObservableFactory = failwith "JS only" and set(v: ObservableFactory): unit = failwith "JS only"

    [<Import("", "FuseJS/Observable")>] 
    let createWith<'T> (elem : 'T) = 
        Globals.observable.Invoke(elem)
 
    [<Import("", "FuseJS/Observable")>]
    let createTyped<'T> =
        Globals.observable.Invoke<'T>()

    [<Import("", "FuseJS/Observable")>] 
    let create () = 
        Globals.observable.Invoke()

    // This doesn't work
    [<Import("", "FuseJS/Observable")>] 
    let createList<'T> (elements : 'T array) =
        Globals.observable.InvokeList<'T>(elements)
    
