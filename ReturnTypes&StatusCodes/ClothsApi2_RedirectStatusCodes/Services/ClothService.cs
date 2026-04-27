using ClothsApi2_RedirectStatusCodes.Models;

namespace ClothsApi2_RedirectStatusCodes.Services
{
    public static class ClothService
    {
        // In-memory data (7 cloth items)
        private static readonly List<Cloth> _cloths = new()
        {
            new Cloth { 
                Id = 1, 
                Name = "Cotton T-Shirt", 
                Category = "Men", 
                Size = "M", 
                Color = "Blue", 
                Price = 599, 
                InStock = true ,
                ETag = "abc123"
            },
            new Cloth { 
                Id = 2, 
                Name = "Denim Jacket", 
                Category = "Women", 
                Size = "L", 
                Color = "Black", 
                Price = 2499, 
                InStock = true ,
                ETag = "def456"
            },
            new Cloth { 
                Id = 3, 
                Name = "Formal Shirt", 
                Category = "Men", 
                Size = "XL", 
                Color = "White", 
                Price = 1299, 
                InStock = false,
                ETag = "ghi789" 
            },
            new Cloth { 
                Id = 4, 
                Name = "Sports Shoes", 
                Category = "Men", 
                Size = "9", 
                Color = "Grey", 
                Price = 3499, 
                InStock = true,
                ETag = "jkl012" 
            },
            new Cloth { 
                Id = 5, 
                Name = "Silk Saree", 
                Category = "Women", 
                Size = "Free", 
                Color = "Red", 
                Price = 4999, 
                InStock = true,
                ETag = "mno345" 
            },
            new Cloth { 
                Id = 6, 
                Name = "Kids Hoodie", 
                Category = "Kids", 
                Size = "S", 
                Color = "Yellow", 
                Price = 899, 
                InStock = true,
                ETag = "pqr678" 
            },
            new Cloth { 
                Id = 7, 
                Name = "Leather Belt", 
                Category = "Accessories", 
                Size = "32", 
                Color = "Brown", 
                Price = 399, 
                InStock = false,
                ETag = "stu901" 
            }
        };

        // Get all cloths
        public static async Task<List<Cloth>> GetAllClothsAsync()
        {
            await Task.Delay(100);
            return _cloths;
        }

        // Get cloth by ID
        public static async Task<Cloth?> GetClothByIdAsync(int id)
        {
            await Task.Delay(50);
            return _cloths.FirstOrDefault(c => c.Id == id);
        }

        // Get cloth by old ID (for 301 demonstration)
        public static async Task<Cloth?> GetClothByOldIdAsync(int oldId)
        {
            await Task.Delay(50);
            // If old ID is 100, redirect to ID 1
            if (oldId == 100)
                return _cloths.FirstOrDefault(c => c.Id == 1);
            return null;
        }

        // Get ETag for cloth (for 304 demonstration)
        public static async Task<string?> GetClothETagAsync(int id)
        {
            await Task.Delay(30);
            var cloth = _cloths.FirstOrDefault(c => c.Id == id);
            return cloth?.ETag;
        }
    }
}

