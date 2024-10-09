using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

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

    public List<ChecklistItem> ChecklistItems { get; set; } = new List<ChecklistItem>();
}
