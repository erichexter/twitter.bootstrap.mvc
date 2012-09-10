@using BootstrapSupport
@model $rootnamespace$.Models.HomeInputModel
@{
    ViewBag.Title = "Index";
}
<h2>Index</h2>
@using (Html.BeginForm()) {
    @Html.ValidationSummary(true)

    <fieldset class="form-horizontal">
        <legend>HomeInputModel</legend>


         @Html.BeginControlGroupFor(model=>model.Name)

            @Html.LabelFor(model => model.Name,new {@class="control-label"})

        
        <div class="controls">

            @Html.EditorFor(model => model.Name,new {@class="input-xlarge"})

            @Html.ValidationMessageFor(model => model.Name,null,new{@class="help-inline"})
		</div>
        @Html.EndControlGroup()


         @Html.BeginControlGroupFor(model=>model.Blog)

            @Html.LabelFor(model => model.Blog,new {@class="control-label"})

        
        <div class="controls">

            @Html.EditorFor(model => model.Blog,new {@class="input-xlarge"})

            @Html.ValidationMessageFor(model => model.Blog,null,new{@class="help-inline"})
		</div>
        @Html.EndControlGroup()


         @Html.BeginControlGroupFor(model=>model.StartDate)

            @Html.LabelFor(model => model.StartDate,new {@class="control-label"})

        
        <div class="controls">

            @Html.EditorFor(model => model.StartDate,new {@class="input-xlarge"})

            @Html.ValidationMessageFor(model => model.StartDate,null,new{@class="help-inline"})
		</div>
        @Html.EndControlGroup()


         @Html.BeginControlGroupFor(model=>model.Password)

            @Html.LabelFor(model => model.Password,new {@class="control-label"})

        
        <div class="controls">

            @Html.EditorFor(model => model.Password,new {@class="input-xlarge"})

            @Html.ValidationMessageFor(model => model.Password,null,new{@class="help-inline"})
		</div>
        @Html.EndControlGroup()


		<div class="form-actions">
            <button type="submit" class="btn btn-primary">Save changes</button>
            <button class="btn">Cancel</button>
          </div>
    </fieldset>
}

<div>
    @Html.ActionLink("Back to List", "Index")
</div>
