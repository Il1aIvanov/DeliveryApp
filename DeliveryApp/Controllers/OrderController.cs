using Microsoft.AspNetCore.Mvc;
using DeliveryApp.Models;
using DeliveryApp.Services.Interfaces;

namespace DeliveryApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }
    
    [HttpGet("FilteredInRange")]
    public async Task<IActionResult> GetAllFilteredOrdersFromToAsync([FromQuery] string area, DateTime startDateTimeTime, DateTime endDateTime)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogInformation("Invalid data provided for filtering orders.");
            return BadRequest(ModelState);
        }

        try
        {
            var filteredOrders = await _orderService.GetAllFilteredOrdersFromToAsync(area, startDateTimeTime, endDateTime);
            _logger.LogInformation("Order filtering completed successfully.");
            return Ok(filteredOrders);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error during order filtering: {ex.Message}");
            return StatusCode(500, "Something wrong with your request.");
        }
    }
    
    [HttpGet("Filtered")]
    public async Task <IActionResult> GetFilteredOrders([FromQuery] GetFilteredOrdersRequest request)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogInformation("Invalid data provided for filtering orders.");
            return BadRequest(ModelState);
        }

        try
        {
            var filteredOrders = await _orderService.GetFilteredOrdersAsync(request);
            _logger.LogInformation("Order filtering completed successfully.");
            return Ok(filteredOrders);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error during order filtering: {ex.Message}");
            return StatusCode(500, "Something wrong with your request.");
        }
    }
}   