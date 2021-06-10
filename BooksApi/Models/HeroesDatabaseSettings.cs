namespace HeroesApi.Models
{
    public class HeroesDatabaseSettings : IHeroesDatabaseSettings
    {
        public string HeroesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IHeroesDatabaseSettings
    {
        string HeroesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}