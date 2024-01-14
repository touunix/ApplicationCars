using System.ComponentModel.DataAnnotations;

namespace ApplicationCars.Models;

public class Car
{
    public int Id { get; set; }

    [StringLength(60, MinimumLength = 3)]
    [Required]
    public string? Brand { get; set; }

    public string? Model { get; set; }
    public decimal Price { get; set; }

    [Display(Name = "Production Year")]
    [DataType(DataType.Date)]
    public DateTime ProductionYear { get; set; }
}