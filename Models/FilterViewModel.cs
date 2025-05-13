namespace Prog7311_POE.Models
{
    public class FilterViewModel
    {
        public required string FarmerName { get; set; }
        public required string ProductName { get; set; } 
        public decimal Price { get; set; }
        public required string Category { get; set; } 
        public DateTime ProductionDate { get; set; }
        public int Stock { get; set; }
        public required string Description { get; set; }
    }
}