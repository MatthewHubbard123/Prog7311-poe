using Prog7311.Poe.Models;

public class ProductModel
{
    public int Id { get; set; }

    public required string ProductName { get; set; }

    public required decimal Price { get; set; }

    public required string Category { get; set; }

    public required DateOnly ProductionDate { get; set; }
    
    public required int Stock { get; set; }

    public required string Description { get; set; }

    public UserModel? FarmerName { get; set; }
    public string? UserId { get; internal set; }
    public int FarmerId { get; internal set; }
}

