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

    type IObservableType<'T> =        
        abstract member value : obj with get, set
        abstract member map<'T,'U> : ('T -> 'U) -> IObservableType<'U>

    type ObservableFactory =
        [<Emit("$0($1...)")>] abstract Invoke: name: string -> IObservableType<string>
        [<Emit("$0($1...)")>] abstract Invoke: ``val``: float -> IObservableType<float>
        [<Emit("$0($1...)")>] abstract Invoke: num: int -> IObservableType<int>        
        //[<Emit("$0($1...)")>] abstract Invoke: unit -> IObservableType

    //let ObservableType:IObservableType = failwith "JS only"
    
    type Globals =
        static member observable with get(): ObservableFactory = failwith "JS only" and set(v: ObservableFactory): unit = failwith "JS only"

    //[<Import("", "FuseJS/Observable")>] 
    //let create =
    //    Globals.observable.Invoke()

    [<Import("", "FuseJS/Observable")>] 
    let createInt (i:int) =
        Globals.observable.Invoke(i)

    [<Import("", "FuseJS/Observable")>] 
    let createString (s:string) = 
        Globals.observable.Invoke(s)

    [<Import("", "FuseJS/Observable")>] 
    let createFloat (n:float) =
        Globals.observable.Invoke(n)

#if false
module FuseJS =
    type IObservable =  
        abstract spock: unit -> unit
        [<Emit("$(0($1...)")>] abstract Invoke: name: string -> IObservable
        [<Emit("$(0($1...)")>] abstract Invoke: value: float -> IObservable
        
    let Observable:IObservable = failwith "JS only"



module Observable =       

    [<Import("", "FuseJS/Observable")>]
    //[<Emit("_Observable($0)")>]
    let create defaultValue : Types.Observable = failwith "JS only"

    //[<Emit("_Observable($0...)")>]    
    let createMany values : Types.Observable = failwith "JS only"

    [<Emit("$0.replaceAll($1)")>]
    let replaceAll<'T> (instance : Types.Observable) (value : 'T array) : unit = failwith "JS only"

    [<Emit("$0.replaceAll($1)")>]
    let replaceAll' (instance : Types.Observable) value : unit = failwith "JS only"

    [<Emit("$0.value")>]
    let value<'T> (instance : Types.Observable) : 'T = failwith "JS only"

    [<Emit("$1.value = $0")>]
    let setValue<'T> (value: 'T) (instance : Types.Observable) : unit = failwith "JS only"
  
    [<Emit("$1.map($0)")>]
    let map (f : Types.Observable -> Types.Observable) (instance : Types.Observable) : Types.Observable = failwith "JS only"
#endif   