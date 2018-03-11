If you're following along, you will need
* The code from this repository
* Access to a SQL Server instance - I'm running the developer edition locally.
* You will need to request a free API key for the Google geocode API
  - https://maps.googleapis.com/maps/api/geocode


V1 -> V2

Adding a Test Project

File / New / Project
	.Net Core / XUnit Test	FoodTruckMvcTests
Add NuGet packages:
    Microsoft.Extensions.Configuration
    Microsoft.Extensions.Configuration.FileExtensions
    Microsoft.EntityFrameworkCore.SqlServer

Class LocationsControllerCreateTests

Constructor
	Configuration
	Context

Test business logic in the Create method of the LocationsController with
	LocationsControllerDoesNotReturnAddressIfAddressNotFound
	LocationsControllerShouldNotPersistTheSameLocationTwice
