using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using chief.DAL;

public class Checklist
{
    [Key]
    public int Id { get; set; }

    [Required]
    [DisplayName("Document Name")]
    public string DocumentName { get; set; } = string.Empty;

    [Required]
    [DisplayName("Application Type")]
    public string ApplicationType { get; set; } = string.Empty;

    [DisplayName("Checklist Items")]
    public List<string> ChecklistItems { get; set; } = new List<string>();

    public Notify GenerateNotification(string action)
    {
        var title = $"{DocumentName} Update";
        var message = $"The checklist for {ApplicationType} was {action.ToLower()}.";

        return new Notify
        {
            Title = title,
            Message = message,
            DateCreated = DateTime.Now
        };
    }
}
