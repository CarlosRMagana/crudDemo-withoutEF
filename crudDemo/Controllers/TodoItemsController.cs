using crudDemo.Data;
using crudDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace crudDemo.Controllers
{
    public class TodoItemsController : ApiController
    {

        TodoItemRepository context = new TodoItemRepository();

        // GET api/TodoItems
        public IEnumerable<TodoItem> Get()
        {
            return context.GetAllTodoItems();
        }

        // GET api/TodoItems/5
        public TodoItem Get(int id)
        {
            TodoItem todoItem = context.GetTodoItemById(id);

            return todoItem != null ? todoItem : null;
        }

        // POST api/TodoItems
        public void Post([FromBody] TodoItem todoItem)
        {
            context.AddTodoItem(todoItem);
        }

        // PUT api/TodoItems/5
        public void Put(int id, [FromBody] TodoItem todoItem)
        {
            context.UpdateTodoItem(todoItem);
        }

        // DELETE api/TodoItems/5
        public void Delete(int id)
        {
           context.DeleteTodoItem(id);
        }
    }
}