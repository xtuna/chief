using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ChecklistItem
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string ItemName { get; set; } = string.Empty;

    [ForeignKey("Checklist")]
    public int ChecklistId { get; set; }

    public Checklist Checklist { get; set; }
}
