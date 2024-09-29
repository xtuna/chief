using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

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
    [DisplayName("Status")]
    public string Status { get; set; } = "Unread";

    [Required]
    [DisplayName("Date Created")]
    [DataType(DataType.DateTime)]
    public DateTime DateCreated { get; set; } = DateTime.Now;

    // Optional method to validate the status
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var validStatuses = new List<string> { "Unread", "Read" };

        if (!validStatuses.Contains(Status))
        {
            yield return new ValidationResult("Invalid status value. Status must be 'Unread' or 'Read'.", new[] { "Status" });
        }
    }
}
