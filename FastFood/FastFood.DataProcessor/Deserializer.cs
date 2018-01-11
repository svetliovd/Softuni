using System;
using FastFood.Data;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Newtonsoft.Json;
using FastFood.DataProcessor.Dto.Import;
using FastFood.Models;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Globalization;
using FastFood.Models.Enums;

namespace FastFood.DataProcessor
{
	public static class Deserializer
	{
		private const string FailureMessage = "Invalid data format.";
		private const string SuccessMessage = "Record {0} successfully imported.";

		public static string ImportEmployees(FastFoodDbContext context, string jsonString)
		{
            var sb = new StringBuilder();

            var deserializedEmployees = JsonConvert.DeserializeObject<EmployeeDto[]>(jsonString);

            var validEmployees = new List<Employee>();

            var validPositions = new List<Position>();

            foreach (var employeeDto in deserializedEmployees)
            {
                if (!IsValid(employeeDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var positionExists = validPositions.Any(p => p.Name == employeeDto.Position);

                var employeeExists = validEmployees.Any(e => e.Name == employeeDto.Name);

                var position = new Position();

                if (!positionExists)
                {
                    position.Name = employeeDto.Position;

                    validPositions.Add(position);
                }
                else
                {
                    position = validPositions.FirstOrDefault(p => p.Name == employeeDto.Position);
                }

                if (employeeExists)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var employee = new Employee()
                {
                    Name = employeeDto.Name,
                    Age = employeeDto.Age,
                    Position = position
                };

                validEmployees.Add(employee);
                sb.AppendLine(string.Format(SuccessMessage, employeeDto.Name));
            }

            if (validPositions.Count > 0)
            {
                context.Positions.AddRange(validPositions);
            }

            context.Employees.AddRange(validEmployees);

            context.SaveChanges();

            var result = sb.ToString();
            return result;
		}

		public static string ImportItems(FastFoodDbContext context, string jsonString)
		{
            var sb = new StringBuilder();

            var deserializedItems = JsonConvert.DeserializeObject<ItemDto[]>(jsonString);

            var validCategories = new List<Category>();

            var validItems = new List<Item>();

            foreach (var itemDto in deserializedItems)
            {
                if (!IsValid(itemDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var categoryExists = validCategories.Any(c => c.Name == itemDto.Category);

                var itemExists = validItems.Any(i => i.Name == itemDto.Name);

                var category = new Category();

                if (!categoryExists)
                {
                    category.Name = itemDto.Category;
                    validCategories.Add(category);
                }
                else
                {
                    category = validCategories.FirstOrDefault(c => c.Name == itemDto.Category);
                }

                if (itemExists)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                var item = new Item()
                {
                    Name = itemDto.Name,
                    Price = itemDto.Price,
                    Category = category
                };
                validItems.Add(item);
                sb.AppendLine(string.Format(SuccessMessage, itemDto.Name));
            }
            if (validCategories.Count > 0)
            {
                context.Categories.AddRange(validCategories);
            }
            context.AddRange(validItems);

            context.SaveChanges();

            var result = sb.ToString();
            return result;
        }

		public static string ImportOrders(FastFoodDbContext context, string xmlString)
		{
            var sb = new StringBuilder();

            var validOrders = new List<Order>();

            var serializer = new XmlSerializer(typeof(OrderDto[]), new XmlRootAttribute("Orders"));

            var deserializedOrders = (OrderDto[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));

            foreach (var orderDto in deserializedOrders)
            {
                if (!IsValid(orderDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                var employeeExists = context.Employees.Any(e => e.Name == orderDto.Employee);
                var itemsExist = true;

                foreach (var item in orderDto.Items)
                {
                    var itemIsThere = (context.Items.Any(i => i.Name == item.Name));
                    if (!itemIsThere)
                    {
                        itemsExist = false;
                    }
                }

                if (!employeeExists || !itemsExist)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                var employee = context.Employees.FirstOrDefault(e => e.Name == orderDto.Employee);
                var dateTime = DateTime.ParseExact(orderDto.DateTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                var type = Enum.Parse<OrderType>(orderDto.Type);
                var orderItems = new List<OrderItem>();

                foreach (var item in orderDto.Items)
                {
                    var itemId = context.Items.FirstOrDefault(i => i.Name == item.Name).Id;
                    orderItems.Add(new OrderItem() {ItemId = itemId, Quantity = item.Quantity });
                }

                var order = new Order()
                {
                    Customer = orderDto.Customer,
                    Employee = employee,
                    Type = type,
                    OrderItems = orderItems
                };

                validOrders.Add(order);
                sb.AppendLine(string.Format($"Order for {orderDto.Customer} on {orderDto.DateTime} added"));
            }
            context.Orders.AddRange(validOrders);

            context.SaveChanges();

            var result = sb.ToString();
            return result;
        }



        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);

            var validationResult = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);

            return isValid;
        }
    }
}