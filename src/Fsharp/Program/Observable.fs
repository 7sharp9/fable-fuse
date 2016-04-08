namespace Fuse

open Fable.Core
open Fable.Import.JS    
    
module Console =
    [<Emit("console.log($0)")>]
    let log line = failwith "JS only"
    
module Json = 
    [<Emit("JSON.stringify($0)")>]
    let stringify obj = failwith "JS only"


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

    type ObservableFactory =
        [<Emit("$0($1...)")>] abstract Invoke: name: string -> IObservable<string>
        [<Emit("$0($1...)")>] abstract Invoke: ``val``: float -> IObservable<float>
        [<Emit("$0($1...)")>] abstract Invoke: num: int -> IObservable<int>        
        [<Emit("$0($1...)")>] abstract Invoke: b: bool -> IObservable<bool>       
        [<Emit("$0($1...)")>] abstract Invoke<'T> : elements: 'T list -> IObservable<'T list>
        abstract Invoke : unit -> IObservable

    type private Globals =
        static member observable with get(): ObservableFactory = failwith "JS only" and set(v: ObservableFactory): unit = failwith "JS only"

    
    [<Import("", "FuseJS/Observable")>] 
    let createList (elements : obj array) =
        let emptyList = Globals.observable.Invoke()
        emptyList.replaceAll(elements)
        emptyList

    module private implementation =     
        let createInt (i:int) =
            Globals.observable.Invoke(i)

        let createString (s:string) = 
            Globals.observable.Invoke(s)

        let createFloat (n:float) =
            Globals.observable.Invoke(n)

        let createTypedList<'T> (elements : 'T list) =
            Globals.observable.Invoke(elements)

        let createObservable () =
            Globals.observable.Invoke()



    [<Import("", "FuseJS/Observable")>] 
    let createString string = 
        implementation.createString string

    [<Import("", "FuseJS/Observable")>] 
    let createnInt int = 
        implementation.createInt int

    [<Import("", "FuseJS/Observable")>] 
    let createFloat float = 
        implementation.createFloat float

    [<Import("", "FuseJS/Observable")>] 
    let createObservable () = 
        implementation.createObservable ()