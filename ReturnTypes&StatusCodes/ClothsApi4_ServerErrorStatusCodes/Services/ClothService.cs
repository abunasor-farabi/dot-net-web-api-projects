using ClothsApi4_ServerErrorStatusCodes.Models;

namespace ClothsApi4_ServerErrorStatusCodes.Services
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
            },
            new Cloth { 
                Id = 2, 
                Name = "Denim Jacket", 
                Category = "Women", 
                Size = "L", 
                Color = "Black", 
                Price = 2499, 
                InStock = true ,
            },
            new Cloth { 
                Id = 3, 
                Name = "Formal Shirt", 
                Category = "Men", 
                Size = "XL", 
                Color = "White", 
                Price = 1299, 
                InStock = false,
            },
            new Cloth { 
                Id = 4, 
                Name = "Sports Shoes", 
                Category = "Men", 
                Size = "9", 
                Color = "Grey", 
                Price = 3499, 
                InStock = true, 
            },
            new Cloth { 
                Id = 5, 
                Name = "Silk Saree", 
                Category = "Women", 
                Size = "Free", 
                Color = "Red", 
                Price = 4999, 
                InStock = true,
            },
            new Cloth { 
                Id = 6, 
                Name = "Kids Hoodie", 
                Category = "Kids", 
                Size = "S", 
                Color = "Yellow", 
                Price = 899, 
                InStock = true,
            },
            new Cloth { 
                Id = 7, 
                Name = "Leather Belt", 
                Category = "Accessories", 
                Size = "32", 
                Color = "Brown", 
                Price = 399, 
                InStock = false,
            }
        };

        // flag to simulate database connection failure (500)
        public static bool SimulateDatabaseFailure { get; set; } = false;

        // flag to simulate upstream service failure (502)
        public static bool SimulateUpstreamFailure { get; set; } = false;

        // flag to simulate maintenance mode (503)
        public static bool IsUnderMaintenance { get; set; } = false;

        // flag to simulate slow response (504)
        public static bool SimulateSlowResponse { get; set; } = false;
        
        // Get all cloths (can fail for 500 demo)
        public static async Task<List<Cloth>> GetAllClothsAsync()
        {
            await Task.Delay(50);

            // Simulate database connection failure --> 500 Internal Server Error
            if(SimulateDatabaseFailure)
                throw new Exception("Database connection failed. Unable to retrieve data.");
            
            return _cloths;
        }

        // Get Cloth by ID (can fail for 500 demo)
        public static async Task<Cloth?> GetClothByIdAsync(int id)
        {
            await Task.Delay(30);

            // Simulate database connection failure --> 500 Internal Server Error
            if(SimulateDatabaseFailure)
                throw new Exception("Database connection failed. Unable to retrieve data.");
            
            return _cloths.FirstOrDefault(c => c.Id == id);
        }

        // Simulate upstream service call (for 502)
        public static async Task<string> CallUpstreamServiceAsync()
        {
            await Task.Delay(100);
            
            if (SimulateUpstreamFailure)
                return "invalid_response";  // Simulates invalid response from upstream
            
            return "valid_response";
        }

        // Simulate slow upstream service (for 504)
        public static async Task<string> CallSlowUpstreamServiceAsync()
        {
            if (SimulateSlowResponse)
                await Task.Delay(60000);  // Simulate timeout (60 seconds)
            else
                await Task.Delay(100);
            
            return "valid_response";
        }

        // Add new cloth
        public static async Task<Cloth> AddClothAsync(Cloth newCloth)
        {
            await Task.Delay(100);
            newCloth.Id = _cloths.Max(c => c.Id) + 1;
            _cloths.Add(newCloth);
            return newCloth;
        }
    }
}

