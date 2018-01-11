using System.ComponentModel.DataAnnotations;

namespace FastFood.DataProcessor.Dto.Export
{
    public class OrderEmployeeDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public EmployeesOrdersDto[] Orders { get; set; }

        [Required]
        public decimal TotalMade { get; set; }

    }

    public class EmployeesOrdersDto
    {
        [Required]
        public string Customer { get; set; }

        [Required]
        public OrderItemsDto[] Items { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }
    }

    public class OrderItemsDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
