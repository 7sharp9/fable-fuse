namespace Fuse

open System
open Fable.Core
open Fable.Import.JS        

module Console =
    [<Emit("console.log($0)")>]
    let log line = failwith "JS only"
    
module Json = 
    [<Emit("JSON.stringify($0)")>]
    let stringify obj = failwith "JS only"

module Args =
    let toString arg =
        sprintf "%O" arg



[<Import("", "FuseJS/Lifecycle")>]
module Lifecycle =
    
    let mutable (onEnteringForeground : unit -> unit) = failwith "JS only"
    let mutable (onEnteringBackground : unit -> unit) = failwith "JS only"
    let mutable (onEnteringInteractive : unit -> unit) = failwith "JS only"
    let mutable (onExitingInteractive : unit -> unit) = failwith "JS only"


[<Import("", "FuseJS/Phone")>]
module Phone = 
    let call number = 
        failwith "JS only"

[<Import("", "FuseJS/Timer")>]
module Timer = 
    let create (action: unit -> unit) (interval:int) (repeat:bool) : unit =
        failwith "JS only"


[<Import("", "FuseJS/Vibration")>]
module Vibration =
    let vibrate (duration:float) =
        failwith "JS only"

[<Import("", "FuseJS/InterApp")>]
module InterApp =
    let launchUri (uri:string) =
        failwith "JS only"

    let mutable (onReceivedUri : string -> unit) = failwith "JS only"


[<Import("", "FuseJS/Storage")>]
module Storage =
    let write (filename:string) (data:string) =
        failwith "JS only"

    let read (filename:string) : string =
        failwith "JS only"

