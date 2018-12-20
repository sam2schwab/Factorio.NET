using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using Factorio.NET.Prototypes;
using NLua;

namespace Factorio.NET
{
    public class FactorioData : Dictionary<string, Dictionary<string, Prototype>>
    {
        public LuaTable RawData { get; }

        public FactorioData(string dataPath)
        {
            var lua = new Lua
            {
                ["package.path"] = string.Join(";",
                    $@"{dataPath}\core\lualib\?.lua",
                    $@"{dataPath}\base\?.lua",
                    $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\?.lua"
                )
            };
            lua.DoString(@"
                require ""defines""
                require ""dataloader""
            ");
            lua.DoFile($@"{dataPath}\base\data.lua");
            RawData = (LuaTable) lua["data.raw"];
            ParsePrototypes();
        }

        private void ParsePrototypes()
        {
            foreach (object typeKey in RawData.Keys)
            {
                string type = typeKey.ToString();
                var luaPrototypes = (LuaTable)RawData[typeKey];
                var prototypes = luaPrototypes.Keys.Cast<object>().ToDictionary(protoKey => protoKey.ToString(),
                    protoKey =>
                    {
                        Prototype prototype = Prototype.Parse((LuaTable) luaPrototypes[protoKey]);
                        prototype.Version = this;
                        return prototype;
                    });
                Add(type, prototypes);
            }
        }
    }
}