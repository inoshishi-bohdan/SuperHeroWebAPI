using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContex dataContex;
        public SuperHeroController(DataContex dataContex)
        {
            this.dataContex = dataContex;
        }
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {

            return Ok(await dataContex.SuperHeroes.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await dataContex.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found");
            return Ok(hero);
        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        { 
            dataContex.SuperHeroes.Add(hero);
            await dataContex.SaveChangesAsync();
            return Ok(await dataContex.SuperHeroes.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var hero = await dataContex.SuperHeroes.FindAsync(request.Id); 
            if (hero == null)
                return BadRequest("Hero not found");
            hero.Name = request.Name;   
            hero.FirstName = request.FirstName;
            hero.LastName = request.LastName;
            hero.Place = request.Place;
            await dataContex.SaveChangesAsync();
            return Ok(await dataContex.SuperHeroes.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<SuperHero>> Delete(int id)
        {
            var hero = await dataContex.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found");
            dataContex.SuperHeroes.Remove(hero);
            await dataContex.SaveChangesAsync();
            return Ok(await dataContex.SuperHeroes.ToListAsync());
        }
    }
}
