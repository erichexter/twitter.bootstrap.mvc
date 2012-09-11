@using BootstrapSupport
@using $rootnamespace$.Models
@model IEnumerable<$rootnamespace$.Models.HomeInputModel>
@{
    ViewBag.Title = "Index";
}

    <h2>Scafolding Example</h2>
<p>This is a basic example of some CRUD controllers and views which utilize the templates. 
    This manages an in memory model list. This example models a Post Redirect Get PRG pattern, it uses temp data and bootstrap to 
    add addtional context across the PRG.</p>
    <p>
        @Html.ActionLink("Create New", "Create", null, new {@class = "btn"})
    </p>
    
    <table class="table table-striped">
        <caption>New widget</caption>
        <thead>
            <tr>
                <th>
                    @Html.DisplayNameFor(model => model.Name)
                </th>
                <th>
                    @Html.DisplayNameFor(model => model.Blog)
                </th>
                <th>
                    @Html.DisplayNameFor(model => model.StartDate)
                </th>
                <th>
                    @Html.DisplayNameFor(model => model.Password)
                </th>
                <th></th>
            </tr>
        </thead>
        @foreach (HomeInputModel item in Model)
        {
            <tr>
                <td>
                    @Html.DisplayFor(modelItem => item.Name)
                </td>
                <td>
                    @Html.DisplayFor(modelItem => item.Blog)
                </td>
                <td>
                    @Html.DisplayFor(modelItem => item.StartDate)
                </td>
                <td>
                    @Html.DisplayFor(modelItem => item.Password)
                </td>
                <td>
                    <div class="btn-group">
                        <a class="btn dropdown-toggle" data-toggle="dropdown" href="#">
                            Action
                            <span class="caret"></span>
                        </a>
                        <ul class="dropdown-menu">
                            <li>@Html.ActionLink("Edit", "Edit", new {id=item.Id})</li>
                            <li>@Html.ActionLink("Details", "Details", new {id=item.Id})</li>
                            <li class="divider"></li>
                            <li>@Html.ActionLink("Delete", "Delete", new { id=item.Id})</li>
                        </ul>
                    </div>

                </td>
            </tr>
        }

    </table>
