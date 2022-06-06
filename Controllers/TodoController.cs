using MeuToDo.Data;
using MeuToDo.Models;
using MeuToDo.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MeuToDo.Controllers
{
    [ApiController]
    [Route(template: "v1")]
    public class TodoController : ControllerBase
    {
        [HttpGet]
        [Route(template: "todos")]
        public async Task<IActionResult> GetAsync(
        [FromServices] AppDbContext context)
        {
            var todos = await context.Todos.AsNoTracking().ToListAsync();
            return Ok(todos);
        }

        [HttpGet]
        [Route(template: "todos/{id}")]
        public async Task<IActionResult> GetByIdAsync(
        [FromServices] AppDbContext context,
        [FromRoute] int id)
        {
            var todo = await context.
            Todos.
            AsNoTracking()
            .FirstOrDefaultAsync(ToDo => ToDo.Id == id);
            return todo == null ? NotFound() : Ok(todo);
        }

        [HttpPost(template: "todos")]
        public async Task<IActionResult> PostAsync(
        [FromServices] AppDbContext context,
        [FromBody] CreateTodoViewModel model
    )
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var todo = new ToDo
            {
                Date = DateTime.Now,
                Done = false,
                Title = model.Title
            };
            try
            {
                await context.Todos.AddAsync(todo);
                await context.SaveChangesAsync();
                return Created(uri: "v1/todos/{todo.Id}", todo);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
        }
        [HttpPut(template: "todos/{id}")]
        public async Task<IActionResult> PutAsync(
       [FromServices] AppDbContext context,
       [FromBody] CreateTodoViewModel model,
       [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var todo = await context.Todos.FirstOrDefaultAsync(ToDo => ToDo.Id == id);

            if (todo == null) return NotFound();

            try
            {
                todo.Title = model.Title;
                context.Todos.Update(todo);
                await context.SaveChangesAsync();
                return Ok(todo);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex);
                return BadRequest();
            }
        }
        [HttpDelete(template: "todos/{id}")]
        public async Task<IActionResult> DeletetAsync(
        [FromServices] AppDbContext context,
        [FromRoute] int id)
        {
            var todo = await context.Todos.FirstOrDefaultAsync(ToDo => ToDo.Id == id);

            try
            {
                context.Todos.Remove(todo);
                await context.SaveChangesAsync();
                return Ok(todo);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex);
                return BadRequest();
            }
        }
    }
}