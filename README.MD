### Ode to Food
*  Course offering by Pluralsight: [ASP .Net Core Fundamentals](https://app.pluralsight.com/library/courses/aspnet-core-fundamentals/table-of-contents)

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




