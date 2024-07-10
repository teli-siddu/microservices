using BuildingBlocks.Messaging.Events;
using EmployeesSearch.API.Models;
using MassTransit;
using MongoDB.Entities;

namespace EmployeesSearch.API.Consumers
{
    public class EmployeeCreatedConsumer : IConsumer<EmployeeCreatedEvent>
    {
        public async Task Consume(ConsumeContext<EmployeeCreatedEvent> context)
        {
            Console.WriteLine("--> COnsuming Employee Creatyed:"+context.Message.Id);

            Item item = new Item
            {
                ID = context.Message.Id.ToString(),
                Department = context.Message.Department,
                FirstName = context.Message.FirstName,
                LastName = context.Message.LastName,
                Position = context.Message.Position,
                Salary = context.Message.Salary

            };
          await  item.SaveAsync();
        }
    }
}
