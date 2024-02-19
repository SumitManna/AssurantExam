namespace AssurantTest.Infrastructure.Common.Utility
{
    public static class SeedData
    {
        public static string ReadSeedData(string fileName)
        {
            var exeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string currentDirectory = Path.GetDirectoryName(exeFilePath)!;
            string path = "Data";
            string fullPath = Path.Combine(currentDirectory, path, fileName);
            if (!File.Exists(fullPath))
            {
                return string.Empty;
            }
            var streamReader = new StreamReader(fullPath);
            string json = streamReader.ReadToEnd();
            streamReader.Close();
            return json;
        }

        public static bool WriteJsonData(string jsonString, string fileName)
        {
            var exeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string currentDirectory = Path.GetDirectoryName(exeFilePath)!;
            string path = "StoreData";
            string fullPath = Path.Combine(currentDirectory, path);
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
            fullPath = Path.Combine(fullPath, fileName);
            File.WriteAllText(fullPath, jsonString);
            return true;
        }
    }
}
