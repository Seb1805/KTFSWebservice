namespace KTFSWebservice.Models
{
    public class GameDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string GameCollectionName { get; set; } = null!;
    }
}
