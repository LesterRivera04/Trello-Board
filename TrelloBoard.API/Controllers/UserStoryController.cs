using TrelloBoard.API.DTO;
using TrelloBoard.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace TrelloBoard.API.Controllers
{
    [ApiController]
    [Route("api/historiasdeusuario")]
    public class UserStoryController : Controller
    {
        private readonly IUserStoryServices _services;
        public UserStoryController(IUserStoryServices services)
        {
            _services = services;
        }

        //GET: api/UserStory
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userStories = await _services.GetAllAsync();
            return Ok(userStories);
        }

        //GET: api/UserStory/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userStory = await _services.GetByIdAsync(id);
            if (userStory == null)
                return NotFound();
            return Ok(userStory);
        }

        //POST: api/UserStory
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserStoryDto userStoryCreateDto)
        {
            await _services.AddAsync(userStoryCreateDto);
            return Ok();
        }

        //PUT: api/UserStory/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateUserStoryDto userStoryUpdateDto)
        {
            var existingUserStory = await _services.GetByIdAsync(id);
            if (existingUserStory == null)
            {
                Console.WriteLine("NO SE ENCONTRÓ LA HISTORIA EN DB"); //para probar error en MVC
                return NotFound();
            }

            await _services.UpdateAsync(id, userStoryUpdateDto);

            Console.WriteLine("UPDATE EJECUTADO CORRECTAMENTE");       //para probar error en MVC

            return NoContent();
        }

        //PUT: api/UserStory/Edit/{id}
        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, EditUserStoryDto userStoryEditDto)
        {
            var existingUserStory = await _services.GetByIdAsync(id);
            if (existingUserStory == null)
            {
                Console.WriteLine("NO SE ENCONTRÓ LA HISTORIA EN DB"); //para probar error en MVC
                return NotFound();
            }

            await _services.EditAsync(id, userStoryEditDto);

            Console.WriteLine("EDIT EJECUTADO CORRECTAMENTE");       //para probar error en MVC

            return NoContent();
        }

        //DELETE: api/UserStory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingUserStory = await _services.GetByIdAsync(id);
            if (existingUserStory == null)
                return NotFound();
            await _services.DeleteAsync(id);
            return NoContent();
        }
    }
}
