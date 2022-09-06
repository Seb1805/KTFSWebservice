using KTFSWebservice.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace KTFSWebservice.Services
{
    public class GameService
    {
        private readonly IMongoCollection<PlayerScore> _scoreCollection;

        public GameService(
            IOptions<GameDatabaseSettings> gameDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                gameDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                gameDatabaseSettings.Value.DatabaseName);

            _scoreCollection = mongoDatabase.GetCollection<PlayerScore>(
                gameDatabaseSettings.Value.GameCollectionName);
        }

        public async Task<List<PlayerScore>> GetAsync() =>
            await _scoreCollection.Find(_ => true).ToListAsync();

        public async Task<PlayerScore?> GetAsync(string id) =>
            await _scoreCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(PlayerScore newBook) =>
            await _scoreCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, PlayerScore updatedBook) =>
            await _scoreCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveAsync(string id) =>
            await _scoreCollection.DeleteOneAsync(x => x.Id == id);
    }
}
