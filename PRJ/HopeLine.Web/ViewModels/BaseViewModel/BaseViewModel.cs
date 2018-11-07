

using System.ComponentModel.DataAnnotations;

public class BaseViewModel : IBaseViewModel
{
    [Required]
    public int Id { get; set; }
}

