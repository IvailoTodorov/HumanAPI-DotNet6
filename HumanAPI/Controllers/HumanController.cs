namespace HumanAPI.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class HumanController : ControllerBase
    {
        private readonly DataContext data;

        public HumanController(DataContext data)
        {
            this.data = data;
        }

        [HttpGet]
        public async Task<ActionResult<List<Human>>> Get()
        {
            return Ok(await data.Humans.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Human>> Get(int id)
        {
            var human = await data.Humans.FindAsync(id);

            if (human == null)
            {
                return BadRequest("Human not found.");
            }

            return Ok(human);
        }

        [HttpPost]
        public async Task<ActionResult<List<Human>>> AddHuman(Human human)
        {
            data.Humans.Add(human);
            await data.SaveChangesAsync();
            return Ok(await data.Humans.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Human>>> UpdateHuman(Human request)
        {
            var human = await data.Humans.FindAsync(request.Id);

            if (human == null)
            {
                return BadRequest("Human not found.");
            }

            human.Nickname = request.Nickname;
            human.FirstName = request.FirstName;
            human.LastName = request.LastName;
            human.Age = request.Age;

            await data.SaveChangesAsync();

            return Ok(await data.Humans.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Human>> Delete(int id)
        {
            var human = await data.Humans.FindAsync(id);

            if (human == null)
            {
                return BadRequest("Human not found.");
            }

            data.Humans.Remove(human);

            await data.SaveChangesAsync();

            return Ok(await data.Humans.ToListAsync());
        }
    }
}
