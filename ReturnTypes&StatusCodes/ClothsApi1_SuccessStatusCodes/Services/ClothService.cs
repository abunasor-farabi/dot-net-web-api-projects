using ClothsApi1_SuccessStatusCodes.Models;

namespace ClothsApi1_SuccessStatusCodes.Services
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
                InStock = true 
            },
            new Cloth { 
                Id = 2, 
                Name = "Denim Jacket", 
                Category = "Women", 
                Size = "L", 
                Color = "Black", 
                Price = 2499, 
                InStock = true 
            },
            new Cloth { 
                Id = 3, 
                Name = "Formal Shirt", 
                Category = "Men", 
                Size = "XL", 
                Color = "White", 
                Price = 1299, 
                InStock = false 
            },
            new Cloth { 
                Id = 4, 
                Name = "Sports Shoes", 
                Category = "Men", 
                Size = "9", 
                Color = "Grey", 
                Price = 3499, 
                InStock = true 
            },
            new Cloth { 
                Id = 5, 
                Name = "Silk Saree", 
                Category = "Women", 
                Size = "Free", 
                Color = "Red", 
                Price = 4999, 
                InStock = true 
            },
            new Cloth { 
                Id = 6, 
                Name = "Kids Hoodie", 
                Category = "Kids", 
                Size = "S", 
                Color = "Yellow", 
                Price = 899, 
                InStock = true 
            },
            new Cloth { 
                Id = 7, 
                Name = "Leather Belt", 
                Category = "Accessories", 
                Size = "32", 
                Color = "Brown", 
                Price = 399, 
                InStock = false 
            }
        };

        // 200 OK: Returns all cloths
        public static async Task<List<Cloth>> GetAllClothsAsync()
        {
            await Task.Delay(100);
            return _cloths;
        }

        // 200 OK: Returns single cloth by ID
        public static async Task<Cloth?> GetClothByIdAsync(int id)
        {
            await Task.Delay(50);
            return _cloths.FirstOrDefault(c => c.Id == id);
        }

        // 201 Created: Adds new cloth
        public static async Task<Cloth> AddClothAsync(Cloth newCloth)
        {
            await Task.Delay(100);
            newCloth.Id = _cloths.Max(c => c.Id) + 1;
            _cloths.Add(newCloth);
            return newCloth;
        }

        // 204 No Content: Deletes cloth
        public static async Task<bool> DeleteClothAsync(int id)
        {
            await Task.Delay(80);
            var cloth = _cloths.FirstOrDefault(c => c.Id == id);
            if (cloth == null) return false;
            return _cloths.Remove(cloth);
        }

        // 202 Accepted: Simulates async background processing
        public static async Task<bool> ProcessBulkOrderAsync()
        {
            await Task.Delay(500);
            return true;
        }
    }
}

