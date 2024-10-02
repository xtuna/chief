using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ApplicationFile
{
    [Key]
    [Column(Order = 0)]  // Set the primary key order
    public int ApplicationId { get; set; }  // This will be the primary key

    [Required]
    [DisplayName("Uploaded File Name")]
    public string UploadedFileName { get; set; } = string.Empty;

    // Navigation property
    [ForeignKey("ApplicationId")]
    public Application Application { get; set; }  // This represents the relationship to Application
}
