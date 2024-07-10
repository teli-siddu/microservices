using EmployeesSearch.API.Models;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using MongoDB.Entities;
using System.Text.Json;

namespace EmployeesSearch.API.Data
{
    public class DbInitialiazer
    {
        public static async Task InitDb(WebApplication app)
        {
            Console.WriteLine("DbInitialiazer.InitDb");
            await DB.InitAsync("EmployeeSearchDb", MongoClientSettings
                .FromConnectionString(app.Configuration.GetConnectionString("mongoDbConnection")));
            await DB.Index<Item>()
                .Key(x => x.FirstName, KeyType.Text)
                .Key(x => x.LastName, KeyType.Text)
                .Key(x => x.Department, KeyType.Text)
                .Key(x=>x.Salary,KeyType.Text)
                .CreateAsync();

            var count = await DB.CountAsync<Item>();

            if (count == 0)
            {
                Console.WriteLine("No data - will atte,pt to seed");
                var itemData = await File.ReadAllTextAsync("Data/employees.json");
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var items= JsonSerializer.Deserialize<List<Item>>(itemData, options);
                await DB.SaveAsync(items);
            }
        }
    }
}
