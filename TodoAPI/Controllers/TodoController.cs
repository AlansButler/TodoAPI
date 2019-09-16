using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]  //This attribute indicates that the controller responds to web API requests. 
	public class TodoController : ControllerBase
	{
		private readonly TodoContext _context;

		//The database context is used in each of the CRUD methods in the controller.

		//Adds an item named Item1 to the database if the database is empty.This code is in the constructor, 
		//so it runs every time there's a new HTTP request. If you delete all items, 
		//the constructor creates Item1 again the next time an API method is called. 
		//So it may look like the deletion didn't work when it actually did work.
		//You would derive from Controller not ControllerBase if building a web page.  Controller descends from Controllerbase and adds support for views.
		public TodoController(TodoContext context)
		{
			_context = context;

			//TodoItems is a dbset of TodoItem
			if (_context.TodoItems.Count() == 0)
			{
				// Create a new TodoItem if collection is empty,
				// which means you can't delete all TodoItems.
				//Definition of TodoItems from in todoController.cs: public DbSet<TodoItem> TodoItems { get; set; }
				_context.TodoItems.Add(new TodoItem { Name = "Item1" });
				_context.SaveChanges();
			}
		}

		// GET: api/Todo endpoint
		//The return type of the GetTodoItems and GetTodoItem methods is ActionResult<T> type. 
		[HttpGet]
		public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
		{
			return await _context.TodoItems.ToListAsync();
		}

		// GET: api/Todo/5  endpoint
		[HttpGet("{id}")]
		public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
		{
			var todoItem = await _context.TodoItems.FindAsync(id);

			if (todoItem == null)
			{
				return NotFound();
			}

			return todoItem;
		}

		//Post: api/Todo
		[HttpPost]
		public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem item)
		{
			_context.TodoItems.Add(item);
			await _context.SaveChangesAsync();

			//CreatedAtAction returans a 201 if successful
			return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
		}



		// PUT: api/Todo/5
		//This does an update.  you must specify the whole 9 yds with a put.  A patch is to be used for partial updates.
		[HttpPut("{id}")]
		public async Task<IActionResult> PutTodoItem(long id, TodoItem item)
		{
			if (id != item.Id)
			{
				return BadRequest();
			}

			_context.Entry(item).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return NoContent();
		}


		// DELETE: api/Todo/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTodoItem(long id)
		{
			var todoItem = await _context.TodoItems.FindAsync(id);

			if (todoItem == null)
			{
				return NotFound();
			}

			_context.TodoItems.Remove(todoItem);
			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}