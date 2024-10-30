using DeliveryApp.Models;

namespace DeliveryApp.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<List<Order>> GetAllOders();
}