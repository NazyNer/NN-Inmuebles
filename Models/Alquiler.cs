using System.ComponentModel.DataAnnotations;

namespace NN_Inmuebles.Models;

public class Alquiler
{
    [Key]
    public int AlquilerID { get; set; }

    [Display(Name ="fecha de alquiler")]
    [DataType(DataType.Date)]
    public DateTime FechaAlquiler { get; set; }

    [Display(Name = "Cliente")]
    public int ClienteID { get; set; }

    [Display(Name = "Casa")]
    public int CasaID { get; set; }

    [Display(Name = "Nombre del cliente")]
    public string? ClienteNombre { get; set; }

    [Display(Name = "Nombre de la casa")]
    public string? CasaNombre { get; set; }
    
    public virtual Casa? Casa { get; set; }
    public virtual Cliente? Cliente { get; set; }
}