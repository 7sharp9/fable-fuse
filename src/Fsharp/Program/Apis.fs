namespace Fuse

open Fable.Core
open Fable.Import.JS        

module Console =
    [<Emit("console.log($0)")>]
    let log line = failwith "JS only"
    
module Json = 
    [<Emit("JSON.stringify($0)")>]
    let stringify obj = failwith "JS only"


#if false
module Lifecycle =
    type [<Import("", "FuseJS/Lifecycle")>] ILifecycle =
        abstract member onEnteringForeground : unit -> unit with get, set 
        abstract member onEnteringBackground : unit -> unit with get, set

    let Lifecycle : ILifecycle = failwith "JS only"

    let onEnteringForeground (handler : unit -> unit) = 
        Lifecycle.onEnteringBackground = handler
#endif
