using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace chief.Pages.EvaluatorMaster
{
    public class ProfileModel : PageModel
    {
        public Evaluator Evaluator { get; set; }

        public void OnGet(int? id)
        {
            // Replace with actual data fetching logic based on id
            Evaluator = new Evaluator
            {
                Id = id ?? 0,
                Name = "Evaluator's Name",
                Specialization = "Specialization"
            };
        }
    }

    public class Evaluator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
    }
}

