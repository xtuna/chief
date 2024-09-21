using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

public class EndorseModel : PageModel
{
    [BindProperty]
    public IFormFile UploadedFile { get; set; }

    [BindProperty]
    public string Remarks { get; set; }

    public string Message { get; set; }
    public string ErrorMessage { get; set; }

    public void OnGet()
    {
        // This method handles the GET request to load the Endorse page
    }
    public int ApplicationId { get; set; }

    public void OnGet(int id)
    {
        ApplicationId = id;
        // Logic to load the application details using the ID if necessary
    }

    public async Task<IActionResult> OnPostAsync(IFormFile uploadedFile, string remarks)
    {
        if (uploadedFile == null)
        {
            ModelState.AddModelError("FileError", "You must upload a file.");
            return Page(); // Return the current page with the error message.
        }

        try
        {
            // Example: Saving the uploaded file
            var filePath = Path.Combine("uploads", uploadedFile.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(stream);
            }

            // Additional logic for handling the endorsement can be added here
            // For example, saving the remarks to the database, etc.

            // Redirect to a confirmation page or back to the application list after successful endorsement.
            return RedirectToPage("/ApplicationMaster/Index");
        }
        catch (Exception ex)
        {
            // Handle the exception if something goes wrong during the file upload or endorsement process
            ModelState.AddModelError("FileUploadError", $"An error occurred: {ex.Message}");
            return Page(); // Return the current page with the error message.
        }
    }
}
