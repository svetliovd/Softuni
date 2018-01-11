using System;
using System.Collections.Generic;
using System.Text;

namespace FastFood.DataProcessor.Dto.Export
{
    public class EmployeeToExportDto
    {
        public ICollection<object> Info { get; set; }

        public decimal TotalMade { get; set; }
    }
}
