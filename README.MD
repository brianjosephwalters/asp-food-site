### Ode to Food
*  Course offering by Pluralsight: [ASP .Net Core Fundamentals](https://app.pluralsight.com/library/courses/aspnet-core-fundamentals/table-of-contents)
*  npm install dependencies before running.

#### Module 2: Drilling Into Data

* Pages couple a `.cshtml` file and a `.cshtml.cs` file.
	* @page tells Razor that this is a razor page.
	* @model tells the type of the model for the page.
	* private get/set properties on the `PageModel` class are available through the @Model property.
	* Can access values in `appsettings.json` by `IConfiguration`.
* Storing models in Separate Projects
	* Create a .Net Core Class Library
* Storing Data sources in Separate Projects
	* Creating another .Net Core Class Library
* Injected data source into PageModel to populate in the razor page using foreach
#### Module 3: Working with Models and Model Binding
* See request information using HttpContext.Request
	* But, should use Model Binding
	* Move information from request to input model.
* Add parameter to controller methods
	* Searches request for variable name.
	* does some type checking
* Input Models vs. Output Models
	* method parameters are input models.
	* Pages with instance variables are your output models.
	* Can use [BindProperty] to treat and instance variable as *both* an input and output model.
		* By default, these are only bound during POST operations.
		* special tag helper that works with model binding and input fields: asp-for
		* Model is already assumed by tag helper
* Passing information between pages
	* asp-route- to add query parameters to route
	* use @page={id:int} in the template to configure route so that asp-route uses path variables instead
* Errors
	* By default, if a page method doesn't return anything (void), it will render the .cs.html file.
		* returning Page() does this too.
	* For errors, specify return type as IActionResult
	* Redirect vs RedirectPermanent = 301 vs. 302
### Module 4: Editing Data with Razor Pages
* Select tags can use asp-for and asp-items to build out select options.  But select options need to be of type SelectListItem
	* Use Html helper.GetEnumSelectList to build SelectLists from Enum.
	* Build in page model. (makes it easier to change/test)
	* Ensures selected value in dropdowns match what is already on model.
* Model Validation
	* Common validation is done for you using attributes on the model objects.
	* IValidatableObject allows custom code for validation checks.
	* Validation checks are performed during model binding, and the results are available in the method.
		* ModelState
		* asp-validation-for
	* Post modifies data.  Don't leave users on a page where data is created/edited.  Refreshing resubmits the form.
		* Post-Redirect-Get pattern (PRG)
* TempData
	* Shared with the next request only, then the data is gone.
	* still probably a bad idea.
### Module 5: Working with SQL Server and Entity Core Framework
* Installing Dependencies:
	1. `Microsoft.EntityFrameworkCore`
	2. `Microsoft.EntityFrameworkCore.SqlServer`
	3. `Microsoft.EntityFrameworkCore.Design`
* Create a class that extends DbContext from Microsoft.EntityFrameworkCore
	* Create DbSet<> for types of data stored in the database.
	* Identify the connection string for the database and store it the appsettings.json
	* Use the conncetion string in the ConfigureServices() method, passing the connection string as option.
	* Make sure the DbContext class has a contructor that can take the options parameter and pass it to the base class.
* Add Database migrations.
	* .Data project doesn't have access to all of the stuff it needs.  
	* Add dependencies to each project.
	* Attach() tells EF to track the object.  Set it as modified. Issue an update statement on SaveChanges();
	* Use Commit
	* Update ConfigureServices() and changed from AddSingleton to AddScoped
		* Typically, services should be scoped to a request.
		* Allows DbContext to collect all of the changes made in a single request.

### Module 6: Building the User Interface
* Pages vs. Partial Views
	* @page allows template to respond to a URL.
	* directives are just Razor Views.  
	* leading underscores is not required but indicates the .cshtml should not render view on its own.  It's a part of something else.

* Layouts
	* How does a Page know where to plug in its content?
		* Always in the @RenderBody() portion of the _Layout view.
		* @RenderSection("name") can allow other views to put content into these section.
			* can specify whether it is required or not.
			* In Page, use @section name {} to identify part of a page that goes in the @RenderSection
	* How can it choose another place to put its content?
		* @{Layout = } in the template to specify.
		* By default, it will search in a few different locations for that layout file.
		* If you don't specify a Layout in the template, then the _ViewStart.cshtml default is used.
* ViewImports.cshtml
	* Add using statements that will be included in all other pages in the folder.
	* Sets default namespace
	* Adds tag helpers to make them work.
* Partial Views
	* Break up large pages into smaller pieces.
	* Reuses html
	* Don't create a cs file for it.  They should receive their data from the parent view.
		* Remove @page directive
		* give it a model.
	* partial is a tag help as a full element.
		* name, model
* View Components
	* Partial Views with their own data access.
	* Layout views are partial views, so these also don't have a way to do data access.
	* Creating Pages with sections just for this seems like it would get repetitive.
		* Tend to use ViewData[] as a property for passing information to _Layout.
		* Lots of pages will set things like ViewData["Title"] in their opening code blocks of the view because the _Layout page needs it.
			* Would get difficult to manage over time.
	* More similar to MVC framework than Razor Pages.
	* Don't respond to Http requests.
	* ASP .Net Core calls `IViewComponent Invoke()`
		* return a View() and pass the model data.  Can pass the view name as first parameter too.
	* Also create the view in the Shared folder, since components can be used anywhere in application.
		* Under Components/ComponentName (without ViewComponent)
		* File called Default, which is what ASP .Net Core will search for unless specified.
	* Don't forget to import in `_ViewImports`.
	* use `<vc:component-name>`
	* Ask for parameters in the Invoke() method, then pass into element by name.
* Quick Scaffolding from Models

### Module 7 - Working with Client-side Libraries
* Serving static assets
	* environment tags help to restrict areas of code based on environments
	* launchsettings sets things like environments
* Client-side validation
	* JQuery Validation Unobtrusive that ties asp .net core validation and jquery validation.
	* not included by default, but can be added to _ValidationScriptsPartial.cshtml
		* Only really needed on our form views (Create, Edit)
	* @RenderSection("Scripts", required: false) is were asp.net can add _ValidationScriptsPartial.
		You just need to provide that section in your page.
* Building API-based sites.
	* can do this with ASP .Net Core pages by just not taking advantage of the models.
	* scaffold a basic API controller by creating a new `API Controller with actions, using EF`
	* in Core 3, you need to configure the controllers separately.
* Serving NPM modules
	* npm install modules from root of the main project (not solution)
	* Adding node_modules folder to server application requires app.UseNodeModules()


