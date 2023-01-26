using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
       /* private static List<SuperHero> heroes = new List<SuperHero>
        {
            new SuperHero {Id = 1, name = "Batman", FirstName = "Bruce", LastName = "Wayne", Place = "Gotham"},
            new SuperHero {Id = 2, name = "HULK", FirstName = "Bruce", LastName = "Banner", Place = "Ohio"}
        };*/
        private DataContext _context;

        public SuperHeroController(DataContext context) {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            //var hero = heroes.Find(h => h.Id == id);
            var hero = await _context.SuperHeroes.FindAsync(id);
            return hero != null ? Ok(hero) : BadRequest("Hero not found.");
        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            //heroes.Add(hero);
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            //var hero = heroes.Find(h => h.Id == request.Id);
            var dbHero = await _context.SuperHeroes.FindAsync(request.Id);
            if (dbHero == null) return BadRequest("Hero does not exist.");
            dbHero.name = request.name; dbHero.FirstName = request.FirstName;
            dbHero.LastName = request.LastName; dbHero.Place = request.Place;
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            //var hero = heroes.Find(h => h.Id == request.Id);
            var dbHero = await _context.SuperHeroes.FindAsync(id);
            if (dbHero == null) return BadRequest("Hero not found.");
            //heroes.Remove(hero);
            _context.SuperHeroes.Remove(dbHero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}