using DierenHok.Communication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DierenHok.Data
{
    public static class DierenProvider
    {
        private const string DierenFileName = "DierenHok.json";
        private static string FileStore;

        private static string _volumeName;
        public static string VolumeName
        {
            get
            {
                return _volumeName;
            }
            set
            {
                _volumeName = value;
                InitProvider();
            }
        }

        public static void InitProvider()
        {
            if (!Directory.Exists(VolumeName)) throw new Exception("volume not mounted!!");

            FileStore = Path.Combine(VolumeName, DierenFileName);

            if (!File.Exists(FileStore))
            {
                Stream fileStream = File.Create(FileStore);
                fileStream.Close(); // don't leave file handle opened (locks the file)
            }
        }

        public static IEnumerable<Dier> GetDieren()
        {
            Console.WriteLine($"************ Requesting all dieren...");

            string json = File.ReadAllText(FileStore);
            return JsonConvert.DeserializeObject<IEnumerable<Dier>>(json) ?? Enumerable.Empty<Dier>();
        }

        public static Dier GetDier(int Id)
        {
            Console.WriteLine($"************ Requesting dier {Id}...");

            return GetDieren().FirstOrDefault(dier => dier.Id == Id);
        }

        public static bool AddDier(Dier dierToAdd)
        {
            var alleDieren = GetDieren().ToList();
            dierToAdd.Id = alleDieren.Count;
            alleDieren.Add(dierToAdd);

            Console.WriteLine($"Saving dier {dierToAdd.Id}...");

            return SaveDieren(alleDieren);
        }

        public static bool SaveDieren(IEnumerable<Dier> alleDieren)
        {
            bool success = false;

            try
            {
                string json = JsonConvert.SerializeObject(alleDieren);
                File.WriteAllText(FileStore, json);
                success = true;
                Console.WriteLine("************ Successfully saved dieren...");
            }
            catch (Exception ex)
            {
                var wasistloos = ex;
                Console.WriteLine($"************ Error saving dier: {ex.GetType().FullName} occurred: {ex.Message}...");
            }

            return success;
        }
    }
}
