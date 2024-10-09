using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class EvaluatorDocument
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Display(Name = "DTS Number")]
    public string DTSNumber { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Document Title")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Application Type")]
    public string ApplicationType { get; set; } = string.Empty;

    [ForeignKey("Evaluator")]
    [Display(Name = "Evaluator ID")]
    public int EvaluatorId { get; set; }

    public Evaluator? Evaluator { get; set; }
}
