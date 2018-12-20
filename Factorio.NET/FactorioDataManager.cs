using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Factorio.NET
{
    public class FactorioDataManager
    {
        private const string DATA_URL_BASE = "https://github.com/wube/factorio-data/archive/";
        private const string VERSION_URL = "https://api.github.com/repos/wube/factorio-data/git/refs/tags";
        
        private string SaveDir { get; }
        
        public FactorioDataManager(string saveDir)
        {
            if (!saveDir.EndsWith(@"\")) saveDir = $@"{saveDir}\";
            SaveDir = saveDir;
        }

        public FactorioData GetData(string version = "latest")
        {
            if (version == "latest")
                version = GetLatestVersion();
            FetchData(version);
            return new FactorioData(GetDataPath(version));
        }

        private static string GetLatestVersion()
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("user-agent", "Factorio.NET");
                string json = client.DownloadString(VERSION_URL);
                JArray array = JArray.Parse(json);
                string fullRef = ((JObject) array.Last)["ref"].ToString();
                return fullRef.Split('/').Last();
            }
        }

        private void FetchData(string version)
        {
            if (Directory.Exists(GetDataPath(version))) return;
            string zippedDataPath = GetZippedDataPath(version);
            if (!File.Exists(zippedDataPath))
            {
                DownloadData(version);
            }
            ZipFile.ExtractToDirectory(zippedDataPath, SaveDir);
        }

        private void DownloadData(string version)
        {
            using (var client = new WebClient())
            {
                string fullUrl = $"{DATA_URL_BASE}{version}.zip";
                client.DownloadFile(fullUrl, GetZippedDataPath(version));
            }
        }

        private string GetZippedDataPath(string version)
        {
            return $"{GetDataPath(version)}.zip";
        }

        private string GetDataPath(string version)
        {
            return $"{SaveDir}factorio-data-{version}";
        }
    }
}