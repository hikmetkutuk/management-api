using System.ComponentModel.DataAnnotations;

namespace ManagementApi.DTOs;

public class UserCreateDto
{
    [Required(ErrorMessage = "Ad boş olamaz.")]
    [StringLength(100, ErrorMessage = "Ad en fazla 100 karakter olabilir.")]
    public string FirstName { get; set; } = null!;
    
    [Required(ErrorMessage = "Soyad boş olamaz.")]
    [StringLength(100, ErrorMessage = "Soyad en fazla 100 karakter olabilir.")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "E-posta adresi boş olamaz.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
    public string Email { get; set; } = null!;
    
    [Required(ErrorMessage = "Telefon numarası boş olamaz.")] 
    [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")] 
    public string PhoneNumber { get; set; } = null!;
}