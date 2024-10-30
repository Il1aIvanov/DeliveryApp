using DeliveryApp.Models;

namespace DeliveryApp.Services.Interfaces;

public interface IOrderService
{
    Task<List<Order>> GetFilteredOrdersAsync(GetFilteredOrdersRequest request);
    Task<List<Order>> GetAllFilteredOrdersFromToAsync(string area, DateTime startDateTime, DateTime endDateTime);
}
