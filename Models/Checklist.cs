using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

public class Checklist
{
    [Key]
    public int Id { get; set; }

    [Required]
    [DisplayName("ID No.")]
    public string ChecklistId { get; set; } = string.Empty;

    [Required]
    [DisplayName("Document Name")]
    public string DocumentName { get; set; } = string.Empty;

    [Required]
    [DisplayName("Application Type")]
    public string ApplicationType { get; set; } = string.Empty;

    [DisplayName("Actions")]
    public string Actions { get; set; }

    // Assuming that actions will be predefined as either 'Edit' or 'Delete'
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var validActions = new List<string> { "Edit", "Delete" };

        if (!string.IsNullOrEmpty(Actions) && !validActions.Contains(Actions))
        {
            yield return new ValidationResult("Invalid action. Actions must be 'Edit' or 'Delete'.", new[] { "Actions" });
        }
    }
}
