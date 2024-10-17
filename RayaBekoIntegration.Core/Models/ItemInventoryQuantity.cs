using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RayaBekoIntegration.Core.Models
{
    public class ItemInventoryQuantity
    {
        public int quantity { get; set; }
        public string inventory { get; set; }
        // Method to convert the object to JSON
        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
