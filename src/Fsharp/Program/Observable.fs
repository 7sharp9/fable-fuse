namespace Fuse

open Fable.Core
open Fable.Import

module Types = 
    type Observable = 
        class end        
    
module Console =
    [<Emit("console.log($0)")>]
    let log line = failwith "JS only"
    
module Json = 
    [<Emit("JSON.stringify($0)")>]
    let stringify obj = failwith "JS only"

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
  
                   