using System.ComponentModel.DataAnnotations;

namespace NN_Inmuebles.Models;

public class Devolucion
{
    [Key]
    public int DevolucionID { get; set; }

    [Display(Name = "Fecha de devolucion")]
    [DataType(DataType.Date)]
    public DateTime FechaDevolucion { get; set; }

    [Display(Name = "Alquiler")]
    public int AlquilerID { get; set; }

    [Display(Name = "Cliente")]
    public int ClienteID { get; set; }

    [Display(Name = "Casa")]
    public int CasaID { get; set; }

    [Display(Name = "Nombre del cliente")]
    public string? ClienteNombre { get; set; }

    [Display(Name = "Nombre de la casa")]
    public string? CasaNombre { get; set; }
    
    public virtual Cliente? Cliente { get; set;}
    public virtual Casa? Casa { get; set; }
}