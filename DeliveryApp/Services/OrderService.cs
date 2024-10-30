using DeliveryApp.Repositories.Interfaces;
using DeliveryApp.Services.Interfaces;
using DeliveryApp.Models;
using DeliveryApp.Utilities;

namespace DeliveryApp.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<OrderService> _logger;
    private readonly FileWriter _fileWriter;

    public OrderService(IOrderRepository orderRepository, ILogger<OrderService> logger, FileWriter fileWriter)
    {
        _orderRepository = orderRepository;
        _logger = logger;
        _fileWriter = fileWriter;
    }

    public async Task<List<Order>> GetAllFilteredOrdersFromToAsync(string area, DateTime startDateTime, DateTime endDateTime)
    {
        var orders = await _orderRepository.GetAllOders();
        
        var filteredOrders = orders
            .Where(o => o.Area.Equals(area, StringComparison.OrdinalIgnoreCase) 
                        && o.Date >= startDateTime && o.Date <= endDateTime)
            .ToList();
        
        _logger.LogInformation($"Filtered {orders.Count} order(s) with next parameters: " +
                               $"Area='{area}', From='{startDateTime}', To='{endDateTime}'");
        return filteredOrders;
    }

    public async Task<List<Order>> GetFilteredOrdersAsync(GetFilteredOrdersRequest request)
    {
        var orders = await _orderRepository.GetAllOders();
        DateTime endDateTime = request.FirstDateTime.AddMinutes(30);
        var filteredOrders = orders
            .Where(o => o.Area.Contains(request.Area) &&
                        o.Date >= request.FirstDateTime &&
                        o.Date <= endDateTime)
            .OrderBy(o => o.Date)
            .ToList();
        _logger.LogInformation($"Filtered {filteredOrders.Count} orders for area '{request.Area}' from '{request.FirstDateTime}' to '{endDateTime}'.");
        await _fileWriter.WriteOrdersToFileAsync(filteredOrders);
        return filteredOrders;
    }
}