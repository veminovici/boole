# Simplee |> Boole

<br />

## Builds
[![Github CI](https://github.com/veminovici/boole/workflows/CINet/badge.svg)](https://github.com/veminovici/boole/actions)
[![Languages](https://img.shields.io/github/languages/top/veminovici/boole?color=%23b845fc)](https://github.com/veminovici/boole)
[![Release](https://img.shields.io/github/v/release/veminovici/boole?include_prereleases)](https://github.com/veminovici/boole/releases)
[![Repo size](https://img.shields.io/github/repo-size/veminovici/boole)](https://github.com/veminovici/boole)
[![Nuget](https://buildstats.info/nuget/Simplee.Boole?includePreReleases=true)](https://www.nuget.org/packages/Simplee.Boole/)
[![License](https://img.shields.io/github/license/veminovici/boole)](https://opensource.org/licenses/Apache-2.0)
[![Coverage Status](https://coveralls.io/repos/github/veminovici/boole/badge.svg?branch=main&bust=1)](https://coveralls.io/github/veminovici/boole)

[![Github Actions](https://buildstats.info/github/chart/veminovici/boole)](https://github.com/veminovici/boole)

<br />

## Description
A F# library for common automata algorithms such **Conway** game of Life.
For more details please see the full [documentation](https://veminovici.github.io/boole/).

## 1. Conway's Game of Life
An implementation of the zero-player game of life. 

- Namespaces: *Simplee.Intelligence.Conway*
- Source: [Conway.fs](https://github.com/veminovici/boole/blob/main/src/Boole/Conway.fs)
- Test: [TConway.fs](https://github.com/veminovici/boole/blob/main/tests/XUno/TConway.fs)

```fsharp
open Simplee.Intelligence
open Simplee.Intelligence.Conway

[| 
Dead; Live; Dead; 
Dead; Live; Dead; 
Dead; Live; Dead
|]
|> Board.ofCells 3 3
|> run
|> Seq.skip 1
|> Seq.head
|> Board.pretty
|> printfn "%s"
```

<br />

### Thank you!

> You can contact me at veminovici@hotmail.com. Code designed and written in Päädu, on the beautiful island of [**Saaremaa**](https://goo.gl/maps/DmB9ewY2R3sPGFnTA), Estonia.