using System.ComponentModel.DataAnnotations;

namespace BoatRazorLibrary.Models;

public class MapPlacesModel
{
    [Required]
    [StringLength(10, ErrorMessage = "Name is too long.")]
    public string? Name { get; set; }
}
