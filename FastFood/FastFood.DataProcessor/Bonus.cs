using System;
using FastFood.Data;
using System.Linq;
using System.Text;

namespace FastFood.DataProcessor
{
    public static class Bonus
    {
	    public static string UpdatePrice(FastFoodDbContext context, string itemName, decimal newPrice)
	    {
            var sb = new StringBuilder();

            var foodToUpdate = context.Items
                .Where(i => i.Name == itemName);

            if (foodToUpdate.Count() > 0)
            {
                sb.AppendLine($"{itemName} Price updated from {foodToUpdate.SingleOrDefault().Price} to {newPrice}");

                foodToUpdate.SingleOrDefault().Price = newPrice;
 
            }

            else
            {
                sb.AppendLine($"Item {itemName} not found!");
            }

            context.SaveChanges();

            return sb.ToString();
	    }
    }
}
