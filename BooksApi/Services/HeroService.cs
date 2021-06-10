using HeroesApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace HeroesApi.Services
{
    public class HeroService
    {
        private readonly IMongoCollection<Hero> _heroes;

        public HeroService(IHeroesDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _heroes = database.GetCollection<Hero>(settings.HeroesCollectionName);
        }

        public List<Hero> Get() =>
            _heroes.Find(hero => true).ToList();

        public Hero Get(string id) =>
            _heroes.Find<Hero>(hero => hero.Id == id).FirstOrDefault();

        public Hero Create(Hero hero)
        {
            _heroes.InsertOne(hero);
            return hero;
        }

        public void Update(string id, Hero heroIn) =>
            _heroes.ReplaceOne(hero => hero.Id == id, heroIn);

        public void Remove(Hero heroIn) =>
            _heroes.DeleteOne(hero => hero.Id == heroIn.Id);

        public void Remove(string id) => 
            _heroes.DeleteOne(hero => hero.Id == id);
    }
}