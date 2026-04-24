namespace ClothsApi.Models
{
    // Represents a Cloth/Cloathing item in our online shop
    public class Cloth
    {
        // Unique identifier
        public int Id { get; set; }
        
        // Name, e.g., 'Cotton T-Shirt'
        public string Name { get; set; } = string.Empty;
        
        // Category, e.g., 'Men', 'Women', 'Kids'
        public string Category { get; set; } = string.Empty;
        
        // Type, e.g., 'Shirt', 'Pant', 'Jacket'
        public string Type { get; set; } = string.Empty;

        // Size, e.g., 'S', 'M','L', 'XL'
        public string Size { get; set; } = string.Empty;

        // Color, e.g., 'Red', 'Blue', 'Black'
        public string Color { get; set; } = string.Empty;

        // Price in decimal
        public decimal Price { get; set; }
        // Avaibablity flag
        public bool InStock { get; set; }
    }
}

