using DeliveryApp.Models;

namespace DeliveryApp.Utilities;

public class FileWriter
{
    private readonly ILogger<FileWriter> _logger;
    private const string ResultFilePath = "Data/_deliveryOrder.txt";

    public FileWriter(ILogger<FileWriter> logger)
    {
        _logger = logger;
    }

    public async Task WriteOrdersToFileAsync(List<Order> orders)
    {
        var lines = orders.Select(o => $"Address: {o.Area}, Delivery Date: {o.Date:yyyy-MM-dd HH:mm:ss}").ToList();

        try
        {
            await File.WriteAllLinesAsync(ResultFilePath, lines);
            _logger.LogInformation($"Filtered orders successfully written to {ResultFilePath}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something goes wrong wile writing orders to file.");
        }
    }
}