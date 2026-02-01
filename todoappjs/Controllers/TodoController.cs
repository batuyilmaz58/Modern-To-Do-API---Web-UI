using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todoappjs.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace todoappjs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        public readonly AppDbContext _context;
        public TodoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/<TodoController>
        [HttpGet("getalltodo")]
        public async Task<IActionResult> GetTodos()
        {
            var todos = await _context.Todos.ToListAsync();

            var response = new ServiceResponse<Todo>
            {
                Entities = todos,
                IsSuccess = true,
                Message = "Tüm Tablolar Başarıyla Getirildi."

            };
            return Ok(response);
        }

        // GET api/<TodoController>/5
        [HttpGet("gettodo/{id}")]
        public async Task<IActionResult> GetTodo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo == null)
            {
                return NotFound(new ServiceResponse<Todo> { HasError = true, Message="Todo Bulunamadı"});
            }
            return Ok(new ServiceResponse<Todo> { Entity = todo, IsSuccess = true });
        }

        // POST api/<TodoController>
        [HttpPost("addtodo")]
        public async Task<IActionResult> PostTodo([FromBody] TodoModel model)
        {
            var newTodo = new Todo
            {
                Title = model.Title,
                CreatedTime = DateTime.UtcNow
            };

            _context.Todos.Add(newTodo);
            await _context.SaveChangesAsync();

            return Ok(new ServiceResponse<Todo> { Entity = newTodo, IsSuccess = true, Message="Todo Başarıyla Eklendi." });
        }

        [HttpPut("updatetodo/{id}")]
        public async Task<IActionResult> PutTodo(int id, [FromBody] TodoModel model) // [FromBody] EKLENDİ - 400 hatasını çözer
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo == null)
            {
                return NotFound(new ServiceResponse<Todo> { HasError = true, Message = "Todo bulunamadı" });
            }

            todo.Title = model.Title;

            _context.Todos.Update(todo);
            await _context.SaveChangesAsync(); // await eklendi

            return Ok(new ServiceResponse<Todo> { IsSuccess = true, Message = "Başarıyla güncellendi" });
        }

        // DELETE api/<TodoController>/5
        [HttpDelete("deletetodo/{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo == null)
            {
                return NotFound(new ServiceResponse<Todo> { HasError = true, Message = "Todo Bulunamadı" });
            }   

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return Ok(new ServiceResponse<Todo> { IsSuccess = true, Message = "Todo başarıyla silindi" });
        }
    }
}
