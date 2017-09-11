using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
        /// <summary>
        /// The To Do Controller for the in memory Database
        /// </summary>
        [Route("api/[controller]")]
        public class ToDoController : Controller
        {
            private readonly ToDoTempContext _context;

            /// <summary>
            /// Initialiazes the database if there's no record
            /// </summary>    
            public ToDoController(ToDoTempContext context)
            {
                _context = context;

                if (_context.ToDoTempItems.Count() == 0)
                {
                    _context.ToDoTempItems.Add(new ToDoItem { Name = "To do something", DeadLine = DateTime.Now, IsComplete = false, Note = "Some Note."});
                    _context.SaveChanges();
                }
            }
            /// <summary>
            /// Retrieves all the Todo Items.
            /// </summary>
            [HttpGet]
            public IEnumerable<ToDoItem> GetAll()
            {
                var items = _context.ToDoTempItems;
                
                foreach(ToDoItem a in items)
                {
                    if(a.DeadLine <= DateTime.Now && a.IsComplete == false)
                    {
                        PastDue(a);
                    }   
                }
                
                return _context.ToDoTempItems.ToList();
            }

            /// <summary>
            /// Gets a specific Todo Item by id.
            /// </summary>
            /// <param name="id"></param>        
            [HttpGet("{id}", Name = "GetToDo")]
            public IActionResult GetById(long id)
            {
                var item = _context.ToDoTempItems.FirstOrDefault(t => t.Id == id);
                if (item == null)
                {
                    return NotFound();
                }

                if (item.DeadLine <= DateTime.Now && item.IsComplete == false)
                {
                    PastDue(item);
                }                             

                return new ObjectResult(item);
            }

            /// <summary>
            /// This method flags the to do item as past due.
            /// </summary>
            /// <param name="item"></param>   
            [HttpOptions]
            public void PastDue(ToDoItem item)
            {
                item.Priority = "Past Due";
                _context.ToDoTempItems.Update(item);
                _context.SaveChanges();
            }

            /// <summary>
            /// Creates a Todo Item.
            /// </summary>
            /// <remarks>
            /// Sample request:
            ///
            ///     POST /Todo
            ///     {
            ///        "name": "To Do Name",
            ///        "note": "Some notes",
            ///        "deadLine": "Some Date",
            ///     }
            ///
            /// </remarks>
            /// <param name="item"></param>
            /// <returns>A newly-created Todo Item</returns>
            /// <response code="201">Returns True for the newly-created item</response>
            /// <response code="400">If the item is null</response> 
            [HttpPost]
            [ProducesResponseType(typeof(ToDoItem), 201)]
            [ProducesResponseType(typeof(ToDoItem), 400)]
            public IActionResult Create([FromBody] ToDoItem item)
            {
                if (item == null)
                {
                    return BadRequest();
                }

                if (item.DeadLine <= DateTime.Now)
                {
                    throw new Exception("The Deadline Date has already passed. Please choose a future date.");
                }


                item.IsComplete = false;

                _context.ToDoTempItems.Add(item);
                _context.SaveChanges();

                return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
            }

            /// <summary>
            /// Updates a TodoItem.
            /// </summary>
            /// <remarks>
            /// Sample request:
            ///
            ///     Put /Todo
            ///     {
            ///        "id": 1,
            ///        "name": "To Do Name",
            ///        "note": "Some notes",
            ///        "deadLine": "SomeDate",
            ///        "isComplete": 0
            ///     }
            ///
            /// </remarks>
            /// <param name="id"></param>
            /// <param name="item"></param>
            /// <returns>Updates a TodoItem</returns>
            /// <response code="201">Returns True for the updated item</response>
            /// <response code="400">If the item is not found</response> 
            [HttpPut("{id}")]
            [ProducesResponseType(typeof(ToDoItem), 201)]
            [ProducesResponseType(typeof(ToDoItem), 400)]
            public IActionResult Update(long id, [FromBody] ToDoItem item)
            {
                if (item == null || item.Id != id)
                {
                    return BadRequest();
                }                

                var todo = _context.ToDoTempItems.FirstOrDefault(t => t.Id == id);
                if (todo == null)
                {
                    return NotFound();
                }

                todo.IsComplete = item.IsComplete;
                todo.Name = item.Name;                
                todo.Note = item.Note;   
                todo.DeadLine = item.DeadLine;

                _context.ToDoTempItems.Update(todo);
                _context.SaveChanges();
                return new NoContentResult();
            }

            /// <summary>
            /// Deletes a specific Todo Item.
            /// </summary>
            /// <param name="id"></param>        
            [HttpDelete("{id}")]
            public IActionResult Delete(long id)
            {
                var todo = _context.ToDoTempItems.FirstOrDefault(t => t.Id == id);
                if (todo == null)
                {
                    return NotFound();
                }

                _context.ToDoTempItems.Remove(todo);
                _context.SaveChanges();
                return new NoContentResult();
            }

    }
}
