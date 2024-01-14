using System.ComponentModel.DataAnnotations;

namespace ApplicationCars.Models;

public class Car
{
    public int Id { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public decimal Price { get; set; }

    [Display(Name = "Production Year")]
    [DataType(DataType.Date)]
    public DateTime ProductionYear { get; set; }
}