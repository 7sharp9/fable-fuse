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



module Lifecycle =
    
    type Lifecycle = 
        abstract member onEnteringForeground : (unit -> unit) with get, set
        abstract member onEnteringBackground : (unit -> unit) with get, set
        abstract member onEnteringInteractive : (unit -> unit) with get, set
        abstract member onExitedInteractive : (unit -> unit) with get, set


    [<Import("", "FuseJS/Lifecycle")>]
    let lifecycle : Lifecycle = failwith "JS only"

    let onEnteringForeground (action: unit -> unit) : unit =
        lifecycle.onEnteringForeground <- action
    
    let onEnteringBackground (action: unit -> unit) : unit =
        lifecycle.onEnteringBackground <- action

    let onEnteringInteractive (action: unit -> unit) : unit =
        lifecycle.onEnteringInteractive <- action

    let onExitedInteractive (action: unit -> unit) : unit =
        lifecycle.onExitedInteractive <- action

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

