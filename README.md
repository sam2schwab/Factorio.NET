# Factorio.NET
[![Build status](https://ci.appveyor.com/api/projects/status/3ugtoek85aabhu7l/branch/dev?svg=true)](https://ci.appveyor.com/project/sam2schwab/factorio-net/branch/dev)
[![NuGet version](https://badge.fury.io/nu/Factorio.NET.svg)](https://badge.fury.io/nu/Factorio.NET)

C# wrapper for the Factorio Lua prototype definitions data

This project is a WIP, as of now only recipes are fully parsed

## Features

- Downloads, reads and parses lua data from the base mod of the game (data.raw)
- Supports old versions down to 0.5.0 (as long as they are here: https://github.com/wube/factorio-data/releases)

## Usage

This project was created to build a C# tool similar to the factoratio tool available on https://factorio.rotol.me/

## Libraries used

- [Json.NET](https://newtonsoft.com/json).
- [Nlua](http://nlua.org/).

## License

This project is under the [MIT license](LICENSE).
