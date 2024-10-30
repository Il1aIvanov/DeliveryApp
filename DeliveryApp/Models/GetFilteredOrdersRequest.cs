using System.Text.Json.Serialization;
using DeliveryApp.Formatters;
using System.ComponentModel.DataAnnotations;

namespace DeliveryApp.Models;

public class GetFilteredOrdersRequest 
{
    [Required(ErrorMessage = "Area is required")]
    public string Area { get; set; }

    [Required(ErrorMessage = "FirstDateTime is required.")]
    [DataType(DataType.DateTime, ErrorMessage = "FirstDateTime must be a valid date and time.")]
    [JsonConverter(typeof(DateTimeJsonFormatter))]
    public DateTime FirstDateTime { get; set; }
        
}
