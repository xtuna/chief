using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using chief.DAL;

public class ApplicationFile
{
    [Key]
    [Column(Order = 0)]
    public int ApplicationId { get; set; }

    [Required]
    [DisplayName("Uploaded File Name")]
    public string UploadedFileName { get; set; } = string.Empty;

    [ForeignKey("ApplicationId")]
    public Application Application { get; set; } 
}
