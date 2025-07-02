using System.ComponentModel.DataAnnotations;

public class RoleCreateDto
{
    [Required]
    public string Admin { get; set; } = string.Empty;

    [Required]
    public string User { get; set; } = string.Empty;
}
