using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

public class Application
{
    [Key]
    public int Id { get; set; }

    [Required]
    [DisplayName("DTS No.")]
    public string DTSNumber { get; set; } = string.Empty;

    [Required]
    [DisplayName("Application")]
    public string ApplicationName { get; set; } = string.Empty;

    [Required]
    [DisplayName("User Type")]
    public string UserType { get; set; } = string.Empty;

    [Required]
    [DisplayName("Applicant Name")]
    public string ApplicantName { get; set; } = string.Empty;

    [Required]
    [DisplayName("Filed Date")]
    [DataType(DataType.Date)]
    public DateTime FiledDate { get; set; }

    [Required]
    [DisplayName("Evaluators")]
    [Range(1, 10, ErrorMessage = "Evaluators must be between 1 and 10")]
    public int Evaluators { get; set; }

    [Required]
    [DisplayName("Days to Evaluate")]
    [Range(3, 10, ErrorMessage = "Days to Evaluate must be between 3 and 10")]
    public int DaysToEvaluate { get; set; }

    [Required]
    [DisplayName("Status")]
    public string Status { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var validStatuses = new List<string> { "Pending", "Approve", "Reject" };

        if (!validStatuses.Contains(Status))
        {
            yield return new ValidationResult("Invalid status value. Status must be 'Pending', 'Approve', or 'Reject'.", new[] { "Status" });
        }
    }
}
