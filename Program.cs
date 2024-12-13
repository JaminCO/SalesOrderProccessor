using System.Text.Json;

namespace SalesOrderProcessor
{
    // Order Class
    public class Order
    {
        public string OrderId { get; set; } = string.Empty; // Default to non-null value
        public string CustomerName { get; set; } = string.Empty; // Default to non-null value
        public List<Item> Items { get; set; } = new List<Item>(); // Default to an empty list
    }

    // Item Class
    public class Item
    {
        public string ItemId { get; set; } = string.Empty; // Default to non-null value
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
    }

    // Processed Order Class
    public class ProcessedOrder
    {
        public string OrderId { get; set; } = string.Empty; // Default to non-null value
        public string CustomerName { get; set; } = string.Empty; // Default to non-null value
        public double Subtotal { get; set; }
        public bool DiscountApplied { get; set; }
        public double FinalTotal { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Step 1: Read Input JSON
                string inputFilePath = "input.json";
                if (!File.Exists(inputFilePath))
                {
                    Console.WriteLine($"Input file '{inputFilePath}' not found.");
                    return;
                }

                string inputData = File.ReadAllText(inputFilePath);
                List<Order>? orders = JsonSerializer.Deserialize<List<Order>>(inputData);

                if (orders == null || orders.Count == 0)
                {
                    Console.WriteLine("No valid orders found in the input file.");
                    return;
                }

                // Step 2: Process Orders
                var processedOrders = new List<ProcessedOrder>();

                foreach (var order in orders)
                {
                    // Check for null or empty items
                    if (order.Items == null || order.Items.Count == 0)
                    {
                        Console.WriteLine($"Order {order.OrderId} has no items and will be skipped.");
                        continue;
                    }

                    double subtotal = order.Items.Sum(item => item.Quantity * item.UnitPrice);
                    bool discountApplied = subtotal > 500;
                    double discount = discountApplied ? subtotal * 0.10 : 0;
                    double vat = (subtotal - discount) * 0.075;
                    double finalTotal = subtotal - discount + vat;

                    processedOrders.Add(new ProcessedOrder
                    {
                        OrderId = order.OrderId,
                        CustomerName = order.CustomerName,
                        Subtotal = subtotal,
                        DiscountApplied = discountApplied,
                        FinalTotal = finalTotal
                    });
                }

                // Step 3: Save Output JSON
                string outputFilePath = "output.json";
                string outputData = JsonSerializer.Serialize(processedOrders, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(outputFilePath, outputData);
                Console.WriteLine($"Processed orders saved to '{outputFilePath}'.");

                // Step 4: Print Summary
                Console.WriteLine("Order Summary:");
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine("Order ID | Customer Name | Subtotal  | Discount | Final Total");
                foreach (var processedOrder in processedOrders)
                {
                    Console.WriteLine($"{processedOrder.OrderId,-9} | {processedOrder.CustomerName,-14} | {processedOrder.Subtotal,10:C} | {processedOrder.DiscountApplied,-8} | {processedOrder.FinalTotal,11:C}");
                }
                Console.WriteLine("-------------------------------------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
