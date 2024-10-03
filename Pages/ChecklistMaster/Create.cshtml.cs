@page
@model chief.Pages.ChecklistMaster.CreateModel

@{
    ViewData["Title"] = "Create";
}

<h1>Create</h1>

<h4>Checklist</h4>
<hr />
<div class="row">
    <div class="col-md-4">
        <form method="post">
            <div asp-validation-summary="ModelOnly" class="text-danger"></div>
            <div class="form-group">
                <label asp-for="Checklist.DocumentName" class="control-label"></label>
                <input asp-for="Checklist.DocumentName" class="form-control" />
                <span asp-validation-for="Checklist.DocumentName" class="text-danger"></span>
            </div>
            <div class="form-group">
                <label asp-for="Checklist.ApplicationType" class="control-label"></label>
                <input asp-for="Checklist.ApplicationType" class="form-control" />
                <span asp-validation-for="Checklist.ApplicationType" class="text-danger"></span>
            </div>

            <div id="checklist-items">
                @for (int i = 0; i < Model.Checklist.ChecklistItems.Count; i++)
                {
                    <div class="form-group checklist-item">
                        <label asp-for="Checklist.ChecklistItems[@i].ItemName" class="control-label"></label>
                        <input asp-for="Checklist.ChecklistItems[@i].ItemName" class="form-control" />
                        <span asp-validation-for="Checklist.ChecklistItems[@i].ItemName" class="text-danger"></span>
                        <button type="button" class="btn btn-danger remove-item" onclick="removeItem(this)">Remove</button>
                    </div>
                }
            </div>

            <button type="button" class="btn btn-secondary" onclick="addItem()">Add Checklist Item</button>

            <div class="form-group">
                <input type="submit" value="Create" class="btn btn-primary" />
            </div>
        </form>
    </div>
</div>

<div>
    <a asp-page="Index">Back to List</a>
</div>

@section Scripts {
    <script>
        function addItem() {
            var index = document.querySelectorAll('#checklist-items .checklist-item').length;
            var newItemHtml = `
                        <div class="form-group checklist-item">
                            <label for="Checklist_ChecklistItems_${index}__ItemName" class="control-label"></label>
                            <input name="Checklist.ChecklistItems[${index}].ItemName" class="form-control" />
                            <span class="text-danger"></span>
                            <button type="button" class="btn btn-danger remove-item" onclick="removeItem(this)">Remove</button>
                        </div>
                    `;
            document.getElementById('checklist-items').insertAdjacentHTML('beforeend', newItemHtml);
        }

        function removeItem(button) {
            button.parentElement.remove();
        }
    </script>
}
