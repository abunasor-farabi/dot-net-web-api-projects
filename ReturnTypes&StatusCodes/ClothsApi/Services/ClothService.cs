using ClothsApi.Models;

namespace ClothsApi.Services
{
    public static class ClothService
    {
        // In-memory product list simulating a database table
        private static readonly List<Cloth> _cloths = new()
        {
            new Cloth { 
                Id = 1, 
                Name = "Cotton T-Shirt", 
                Category = "Men", 
                Type = "Shirt", 
                Size = "M", 
                Color = "Blue", 
                Price = 599, 
                InStock = true 
            },
            new Cloth { 
                Id = 2, 
                Name = "Denim Jacket", 
                Category = "Women", 
                Type = "Jacket", 
                Size = "L", 
                Color = "Black", 
                Price = 2499, 
                InStock = true 
            },
            new Cloth { 
                Id = 3, 
                Name = "Formal Shirt", 
                Category = "Men", 
                Type = "Shirt", 
                Size = "XL", 
                Color = "White", 
                Price = 1299, 
                InStock = false 
            },
            new Cloth { 
                Id = 4, 
                Name = "Sports Shoes", 
                Category = "Men", 
                Type = "Shoes", 
                Size = "9", 
                Color = "Grey", 
                Price = 3499, 
                InStock = true 
            },
            new Cloth { 
                Id = 5, 
                Name = "Silk Saree", 
                Category = "Women", 
                Type = "Saree", 
                Size = "Free", 
                Color = "Red", 
                Price = 4999, 
                InStock = true 
            },
            new Cloth { 
                Id = 6, 
                Name = "Kids Hoodie", 
                Category = "Kids", 
                Type = "Hoodie", 
                Size = "S", 
                Color = "Yellow", 
                Price = 899, 
                InStock = true 
            },
            new Cloth { 
                Id = 7, 
                Name = "Leather Belt", 
                Category = "Accessories", 
                Type = "Belt", 
                Size = "32", 
                Color = "Brown", 
                Price = 399, 
                InStock = false 
            }
        };

        // ===============================================================
        // ALL SERVICE METHODS ARE ASYNC (Simulate database I/O)
        // Returns Task<T> wrapper for async operations
        // ===============================================================

        // Async: Returns total number of cloth items (primitive type)
        public static async Task<int> GetClothCountAsync()
        {
            await Task.Delay(100); // Simulate async DB call
            return _cloths.Count;
        }

        // Async: Returns all cloth item (collection of complex type)
        public static async Task<List<Cloth>> GetAllClothsAsync()
        {
            await Task.Delay(200);
            return _cloths;
        }

        // Async: Returns a single cloth by ID (complex type or null)
        public static async Task<Cloth?> GetClothByIdAsync(int id)
        {
            await Task.Delay(100);
            return _cloths.FirstOrDefault(c => c.Id == id);
        }

        // Async: Returns only cloth names (collection of primitive types)
        public static async Task<List<string>> GetAllClothNamesAsync()
        {
            await Task.Delay(150);
            return _cloths.Select(c => c.Name).ToList();
        }

        // Async: Returns cloths by catrgory (filtered collection)
        public static async Task<List<Cloth>> GetClothsByCategoryAsync(string category)
        {
            await Task.Delay(100);
            return _cloths.Where(
                c => c.Category.Equals(
                    category, StringComparison.OrdinalIgnoreCase
                )
            ).ToList();
        }
    }
}

