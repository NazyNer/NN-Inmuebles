using System.ComponentModel.DataAnnotations;

namespace NN_Inmuebles.Models;

public class Cliente
{
    [Key]
    public int ClienteID { get; set; }

    [Display(Name = "Nombre del cliente")]
    [MaxLength(25, ErrorMessage = "Maximo 25 caracteres")]
    [Required(ErrorMessage = "Es necesario que completes esta casilla")]
    [RegularExpression(@"[A-Z\s]*$",ErrorMessage = "Solo mayusculas y espacios")]
    public string? NombreCliente { get; set;}

    [Display(Name = "Apellido del cliente")]
    [MaxLength(15,ErrorMessage = "Maximo 15 caracteres")]
    [Required(ErrorMessage = "Es necesario que completes esta casilla")]
    [RegularExpression(@"[A-Z]*$",ErrorMessage = "Solo mayusculas")]
    public string? ApellidoCliente { get; set; }

    [Display(Name = "Documento nacional de identidad")]
    [Required(ErrorMessage = "Es necesario que completes esta casilla")]
    [RegularExpression(@"[0-9\d]*$", ErrorMessage = "Solo numeros")]
    public int DNI { get; set; }

    [Display(Name = "Fecha de nacimiento")]
    [Required(ErrorMessage = "Es necesario que completes esta casilla")]
    [DataType(DataType.Date)]
    public DateTime FechaNacimiento { get; set; }

}