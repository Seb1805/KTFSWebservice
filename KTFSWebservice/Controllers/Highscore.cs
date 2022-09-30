using KTFSWebservice.Models;
using KTFSWebservice.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Text.Json;

namespace KTFSWebservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class highscore : ControllerBase
    {
        private readonly GameService _gameService;

        public highscore(GameService gameService) =>
            _gameService = gameService;

        [HttpGet]
        public async Task<List<PlayerScore>> Get() =>
            await _gameService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<PlayerScore>> Get(string id)
        {
            var book = await _gameService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm]String name, [FromForm]int score )
        {
            PlayerScore newScore = new PlayerScore(score, name);
            await _gameService.CreateAsync(newScore);
            //await _gameService.CreateAsync(newScore);

            return CreatedAtAction(nameof(Get), new { id = newScore.Id }, newScore);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, PlayerScore updatedScore)
        {
            var playerScore = await _gameService.GetAsync(id);

            if (playerScore is null)
            {
                return NotFound();
            }

            updatedScore.Id = playerScore.Id;

            await _gameService.UpdateAsync(id, updatedScore);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var playerScore = await _gameService.GetAsync(id);

            if (playerScore is null)
            {
                return NotFound();
            }

            await _gameService.RemoveAsync(id);

            return NoContent();
        }
        //    static MongoClient dbClient = new MongoClient("mongodb+srv://ktfsgameserver:ktfsfpsgame@ktfs.o5iwxxd.mongodb.net/test");
        //    static IMongoDatabase database = dbClient.GetDatabase("Game");

        //    [HttpGet(Name = "GetHighscore")]
        //    public IEnumerable<PlayerScore> Get()
        //    {
        //        var collection = database.GetCollection<PlayerScore>("scores");
        //        List<PlayerScore> result = collection.Find(Builders<PlayerScore>.Filter.Empty).ToList();
        //        return result;
        //    }



        //    [HttpPost(Name = "PostHighscore")]
        //    public void Post(string name, int score)
        //    {
        //        // convert JSON string to PlayerScore
        //        //var test = JsonSerializer.Deserialize<PlayerScore>(postedData);

        //        // Pull data from the sendt form
        //        System.Console.WriteLine(name);
        //        System.Console.WriteLine(score);            
        //        //System.Console.WriteLine(typeof(name));
        //        System.Console.WriteLine((score));

        //        PlayerScore ps = new PlayerScore(score, name);

        //        var collection = database.GetCollection<PlayerScore>("scores");
        //        collection.InsertOne(ps);

        //        //collection.InsertOne(/* PlayerScore */);
        //    }


        //}
    }
}


