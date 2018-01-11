using System;
using System.Collections.Generic;
using System.Text;

namespace FastFood.DataProcessor.Dto.Export
{
    public class ExportCategories
    {
        public string Name { get; set; }

        public PopularItemDto MostPopularItem { get; set; }
    }

    public class PopularItemDto
    {
        public string Name { get; set; }

        public decimal TotalMade { get; set; }

        public int TimesSold { get; set; }
    }
}
