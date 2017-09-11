using System;
using Xunit;
using ToDoApp.Controllers;
using ToDoApp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Helpers;
using System.Collections.Generic;

namespace ToDoApp.Tests
{
    

    public class ToDoAppUnitTest
    {       

        public void AddToDoToDatabase()
        {
            var builder = new DbContextOptionsBuilder<ToDoTempContext>()
                .UseInMemoryDatabase(databaseName: "Write_to_db")
                .Options;


            // Run the test against instance of the context
            using (var context = new ToDoTempContext(builder))
            {
                var controller = new ToDoController(context);
                context.ToDoTempItems.Add(new ToDoItem { Id = 1, Name = $"To do something1", Note = "Sample To Do1", DeadLine = DateTime.Now.AddDays(2) });
                context.SaveChanges();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new ToDoTempContext(builder))
            {
                Assert.Equal(1, context.ToDoTempItems.Count());
                Assert.Equal("To do something1", context.ToDoTempItems.Single().Name);
            }
        }        

        [Fact]
        public void GetToDo()
        {
            var builder = new DbContextOptionsBuilder<ToDoTempContext>()
                .UseInMemoryDatabase(databaseName: "Search_db")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new ToDoTempContext(builder))
            {
                var controller = new ToDoController(context);
                context.ToDoTempItems.Add(new ToDoItem { Id = 2, Name = $"To do something2", Note = "Sample To Do1", DeadLine = DateTime.Now.AddDays(2) });
                context.ToDoTempItems.Add(new ToDoItem { Id = 3, Name = $"To do something3", Note = "Sample To Do2", DeadLine = DateTime.Now.AddDays(1) });
                context.ToDoTempItems.Add(new ToDoItem { Id = 4, Name = $"To do something4", Note = "Sample To Do3", DeadLine = DateTime.Now.AddDays(4) });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new ToDoTempContext(builder))
            {
                var service = new ToDoController(context);
                dynamic resultById = new DynamicObjectResultValue(service.GetById(2));
                Assert.Equal("To do something2", resultById.Value.Name);

                var resultAll = service.GetAll().ToList();
                Assert.Equal(4, resultAll.Count);
                
            }
        }

        

    }
}
