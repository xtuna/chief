using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
    public class Evaluator
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [DisplayName("Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DisplayName("Specialization")]
        public string Specialization { get; set; } = string.Empty;

        [Required]
        [DisplayName("Type")]
        public string Type { get; set; } = string.Empty;

        [Required]
        [DisplayName("Status")]
        public string Status { get; set; } = "Inactive";

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validTypes = new List<string> { "Internal", "External" };
            if (!validTypes.Contains(Type))
            {
                yield return new ValidationResult("Type must be either 'Internal' or 'External'.", new[] { "Type" });
            }

            var validStatuses = new List<string> { "Active", "Inactive" };
            if (!validStatuses.Contains(Status))
            {
                yield return new ValidationResult("Status must be either 'Active' or 'Inactive'.", new[] { "Status" });
            }
        }
    }
