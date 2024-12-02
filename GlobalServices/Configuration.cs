using Microsoft.Extensions.Configuration;

namespace GlobalServices
{
    public static class Configuration
    {
        private static readonly IConfigurationBuilder _configurationBuilder;
        private static readonly IConfigurationRoot _configurationRoot;
        private readonly static string fileName = "GlobalSettigns.json";
        readonly static string jsonFilePath;
        
        static Configuration()
        {
            jsonFilePath = GetCurrentDirectory();
            _configurationBuilder = new ConfigurationBuilder();
            _configurationRoot = _configurationBuilder.AddJsonFile(jsonFilePath).Build();
            
        }

        public static string GetDatabaseString(string databaseName)
        {
            string connectionString = _configurationRoot.GetSection($"ConnectionStrings:{databaseName}").Value!;
            return connectionString!;
        }

        public static string GetDefaultAssigmentGroup()
        {
            string defaultAssigmentGroup = _configurationRoot.GetSection($"GeneralProjectSettings:DefaultAssigmentGroup").Value!;
            return defaultAssigmentGroup;
        }

        private static string GetCurrentDirectory()
        {
            return Path.Combine(Directory.GetParent(Environment.CurrentDirectory).FullName, "GlobalServices", fileName);
            
        }
    }
}
