using HeroesApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HeroesApi.Services;
using Microsoft.AspNetCore.Cors;
namespace HeroesApi.Controllers
{

    [Route("api/Heroes")]
    [ApiController]
    [EnableCors("AllowOrigin")] 

    public class HeroesController : ControllerBase
    {
        private readonly HeroService _heroService;

        public HeroesController(HeroService heroService)
        {
            _heroService = heroService;
        }
        [HttpGet]
        public ActionResult<List<Hero>> Get() =>
            _heroService.Get();

        [HttpGet("{id}", Name = "GetHero")]
        public ActionResult<Hero> Get(string id)
        {
            var hero = _heroService.Get(id);

            if (hero == null)
            {
                return NotFound();
            }

            return hero;
        }
        [HttpPost]
        public ActionResult<Hero> Create(Hero hero)
        {
            _heroService.Create(hero);

            return CreatedAtRoute("GetHero", new { id = hero.Id.ToString() }, hero);
        }
 
        [HttpPut("{id}")]
        public IActionResult Update(string id, Hero heroIn)
        {
            var hero = _heroService.Get(id);

            if (hero == null)
            {
                return NotFound();
            }

            _heroService.Update(id, heroIn);

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var hero= _heroService.Get(id);

            if (hero == null)
            {
                return NotFound();
            }

            _heroService.Remove(hero.Id);

            return NoContent();
        }
    }
}