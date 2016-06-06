# fable |> fuse

Fable bindings for Fuse
See: https://www.fusetools.com",

## Installation

```sh
$ npm install --save fable-core
$ npm install --save-dev fable-fuse
```

## Usage

### In a F# project (.fsproj)

```xml
  <ItemGroup>
    <Reference Include="node_modules/fable-core/Fable.Core.dll" />
    <Compile Include="node_modules/fable-fuse/Fable.Observable.fs" />
    <Compile Include="node_modules/fable-fuse/Fable.Apis.fs" />
    <Compile Include="node_modules/fable-fuse/Fable.PromiseExt.fs" />
  </ItemGroup>
```

### In a F# script (.fsx)

```fsharp
#r "node_modules/fable-core/Fable.Core.dll"
#load "node_modules/fable-fuse/Fable.Observable.fs"
#load "node_modules/fable-fuse/Fable.Apis.fs"
#load "node_modules/fable-fuse/Fable.PromiseExt.fs"

open Fable.Core
open Fuse
```