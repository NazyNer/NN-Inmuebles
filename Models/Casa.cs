using System.ComponentModel.DataAnnotations;

namespace NN_Inmuebles.Models;

public class Casa
{
    [Key]
    public int CasaID { get; set; }

    [Display(Name = "Nombre de la casa")]
    [Required(ErrorMessage = "Es necesario que completes esta casilla")]
    [MaxLength(30, ErrorMessage = "Maximo 30 Caracteres")]
    [RegularExpression(@"[A-Z\s]*$", ErrorMessage = "Solo mayusculas y espacios")]
    public string? NombreCasa { get; set; }

    [Display(Name = "Domicilio")]
    [Required(ErrorMessage = "Es necesario que completes esta casilla")]
    [MaxLength(40, ErrorMessage = "Maximo 30 Caracteres")]
    [RegularExpression(@"[A-Z\s\d]*$", ErrorMessage = "Solo mayuculas, espacios y numeros")]
    public string? Domicilio { get; set; }

    [Display(Name = "Nombre del dueño")]
    [Required(ErrorMessage = "Es necesario que completes esta casilla")]
    [MaxLength(30, ErrorMessage = "Maximo 30 Caracteres")]
    [RegularExpression(@"[A-Z\s]*$", ErrorMessage = "Solo mayuculas y espacios")]
    public string? NombreDueño { get; set; }

    [Display(Name = "Imagen de la casa")]
    public byte[]? ImagenCasa { get; set; }

    public bool Alquilada { get; set; }
    
    public bool Eliminada { get; set; }

}