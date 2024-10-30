using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using DeliveryApp.Formatters;
namespace DeliveryApp.Models;

public class Order
{
    [Required]
    public int Id { get; set; }
        
    [Range(0.1, 100.0, ErrorMessage = "Weight must be between 0.1 and 100.0 kg.")]
    public double Weight { get; set; }
        
    [Required]
    public string Area { get; set; } = string.Empty;
        
    [Required]
    [JsonConverter(typeof(DateTimeJsonFormatter))]
    public DateTime Date { get; set; }
}