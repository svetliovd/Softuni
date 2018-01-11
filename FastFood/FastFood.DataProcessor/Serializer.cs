using System;
using System.IO;
using FastFood.Data;
using System.Linq;
using FastFood.Models.Enums;
using FastFood.DataProcessor.Dto.Export;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using FastFood.Models;
using System.Text;

namespace FastFood.DataProcessor
{
	public class Serializer
	{
		public static string ExportOrdersByEmployee(FastFoodDbContext context, string employeeName, string orderType)
		{


            var ordersByEmployee = context.Orders
                    .Where(o => o.Employee.Name == employeeName && o.Type == Enum.Parse<OrderType>(orderType))
                    .Select(o => new OrderEmployeeDto
                    {
                        Name = o.Employee.Name,
                        Orders = o.Employee.Orders.Select(or => new EmployeesOrdersDto()
                        {
                            Customer = o.Customer,
                            Items = o.OrderItems.Select(oi => new OrderItemsDto
                            {
                                Name = oi.Item.Name,
                                Price = oi.Item.Price,
                                Quantity = oi.Quantity
                            }).ToArray(),
                            TotalPrice = o.OrderItems.Sum(oi => oi.Item.Price * oi.Quantity)
                        })
                        .OrderByDescending(or => or.TotalPrice)
                        .ThenByDescending(or => or.Items.Count())
                        .ToArray(),
                    }).ToArray();

            string jsonString = JsonConvert.SerializeObject(ordersByEmployee, Formatting.Indented);

            return jsonString;
		}

		public static string ExportCategoryStatistics(FastFoodDbContext context, string categoriesString)
		{
            var categoriesNames = categoriesString.Split(',');

            var sb = new StringBuilder();

            var mostPopularItemChicken = context.Items
                .Where(c => c.Name == categoriesNames[0])
                .OrderByDescending(i => i.OrderItems.Sum(oi => oi.Item.Price))
                .ToArray();

            var mostPopularCategory = context.Categories
                .Where(c => c.Name == categoriesNames[0])
                .Select(c => new ExportCategories
                {
                    Name = c.Name,
                    MostPopularItem = new PopularItemDto
                    {
                        Name = mostPopularItemChicken[0].Name,
                        TotalMade = mostPopularItemChicken[0].OrderItems.Sum(oi => oi.Item.Price),
                        TimesSold = mostPopularItemChicken[0].OrderItems.Count()
                    }
                }  ).ToString();
            sb.AppendLine(mostPopularCategory);

            return sb.ToString();
        }
	}
}