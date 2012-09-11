@model $rootnamespace$.Models.HomeInputModel

@{
    ViewBag.Title = "Details";
}

<h2>Details</h2>

<fieldset>
    <legend>HomeInputModel</legend>

    <div class="display-label">
         @Html.DisplayNameFor(model => model.Name)
    </div>
    <div class="display-field">
        @Html.DisplayFor(model => model.Name)
    </div>

    <div class="display-label">
         @Html.DisplayNameFor(model => model.Blog)
    </div>
    <div class="display-field">
        @Html.DisplayFor(model => model.Blog)
    </div>

    <div class="display-label">
         @Html.DisplayNameFor(model => model.StartDate)
    </div>
    <div class="display-field">
        @Html.DisplayFor(model => model.StartDate)
    </div>

    <div class="display-label">
         @Html.DisplayNameFor(model => model.Password)
    </div>
    <div class="display-field">
        @Html.DisplayFor(model => model.Password)
    </div>
</fieldset>
<p>
    @Html.ActionLink("Edit", "Edit", new { id=Model.Id }) |
    @Html.ActionLink("Back to List", "Index")
</p>
