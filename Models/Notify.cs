using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using chief.DAL;

public class Notify
{
    [Key]
    public int Id { get; set; }

    [Required]
    [DisplayName("User ID")]
    public string UserId { get; set; } = string.Empty;

    [Required]
    [DisplayName("Title")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [DisplayName("Message")]
    public string Message { get; set; } = string.Empty;

    [Required]
    public string Status { get; set; } = "Unread";

    [Required]
    [DisplayName("Date Created")]
    [DataType(DataType.DateTime)]
    public DateTime DateCreated { get; set; } = DateTime.Now;

}