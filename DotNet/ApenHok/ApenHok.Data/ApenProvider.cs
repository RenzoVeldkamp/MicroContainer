using ApenHok.Communication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ApenHok.Data
{
    public static class ApenProvider
    {
        //private const string VolumeName = "/apenvolume"; // @"C:\DATA\TEMP\APENVOLUME"; //  
        //private const string VolumeName =  @"C:\DATA\TEMP\APENVOLUME";  
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

        static ApenProvider()
        { }

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

        public static IEnumerable<Aap> GetApen()
        {
            Console.WriteLine($"************ Requesting all apen...");

            string json = File.ReadAllText(FileStore);
            return JsonConvert.DeserializeObject<IEnumerable<Aap>>(json) ?? Enumerable.Empty<Aap>();
        }

        public static Aap GetAap(int Id)
        {
            Console.WriteLine($"************ Requesting aap {Id}...");

            return GetApen().FirstOrDefault(aap => aap.Id == Id);
        }

        public static bool AddAap(Aap aapToAdd)
        {
            var alleApen = GetApen().ToList();
            aapToAdd.Id = alleApen.Count;
            alleApen.Add(aapToAdd);

            Console.WriteLine($"Saving aap {aapToAdd.Id}...");

            return SaveApen(alleApen);
        }

        public static bool SaveApen(IEnumerable<Aap> alleApen)
        {
            bool success = false;

            try
            {
                string json = JsonConvert.SerializeObject(alleApen);
                File.WriteAllText(FileStore, json);
                success = true;
                Console.WriteLine("************ Successfully saved apen...");
            }
            catch (Exception ex)
            {
                var wasistloos = ex;
                Console.WriteLine($"************ Error saving aap: {ex.GetType().FullName} occurred: {ex.Message}...");
            }

            return success;
        }
    }
}
