using System.Text.Json;
using DeliveryApp.Repositories.Interfaces;
using DeliveryApp.Models;

namespace DeliveryApp.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly string _filePath = "Data/orders.json";

    public async Task<List<Order>> GetAllOders()
    {
        if (!File.Exists(_filePath))
        {
            throw new FileNotFoundException("Orders file not found.");
        }

        var jsonData = await File.ReadAllTextAsync(_filePath);
        var orders = JsonSerializer.Deserialize<List<Order>>(jsonData);
        return orders ?? new List<Order>();
    }
}