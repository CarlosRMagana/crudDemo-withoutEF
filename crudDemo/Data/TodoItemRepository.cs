using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.Common;
using crudDemo.Models;

namespace crudDemo.Data
{
    public class TodoItemRepository
    {
        private readonly string _connectionString;

        public TodoItemRepository()
        {
            _connectionString = "Server=localHost;Database=TodoList;Trusted_Connection=True;";
        }

        public IEnumerable<TodoItem> GetAllTodoItems()
        {
            List<TodoItem> todoItems = new List<TodoItem>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM TodoItems", connection);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TodoItem todoItem = new TodoItem()
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        IsComplete = (bool)reader["IsComplete"]
                    };

                    todoItems.Add(todoItem);
                }
            }

            return todoItems;
        }

        public TodoItem GetTodoItemById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM TodoItems WHERE Id = @Id", connection);

                command.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    TodoItem todoItem = new TodoItem()
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        IsComplete = (bool)reader["IsComplete"]
                    };

                    return todoItem;
                }
                else
                {
                    return null;
                }
            }
        }

        public void AddTodoItem(TodoItem todoItem)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("INSERT INTO TodoItems (Name, IsComplete) VALUES (@Name, @IsComplete)", connection);

                command.Parameters.AddWithValue("@Name", todoItem.Name);
                command.Parameters.AddWithValue("@IsComplete", todoItem.IsComplete);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateTodoItem(TodoItem todoItem)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("UPDATE TodoItems SET Name = @Name, IsComplete = @IsComplete WHERE Id = @Id", connection);

                command.Parameters.AddWithValue("@Name", todoItem.Name);
                command.Parameters.AddWithValue("@IsComplete", todoItem.IsComplete);
                command.Parameters.AddWithValue("@Id", todoItem.Id);

                command.ExecuteNonQuery();
            }

        }

        public void DeleteTodoItem(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("DELETE FROM TodoItems WHERE Id = @Id", connection);

                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }
        }

    }

}