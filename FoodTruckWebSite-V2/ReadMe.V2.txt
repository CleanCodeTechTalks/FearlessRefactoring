V2 -> V3

Use EF Core in-memory database for testing

NuGet packages
Microsoft.EntityFrameworkCore.InMemory

Context = new FoodTruckContext(
    optionsBuilder
    .UseInMemoryDatabase(databaseName: "FoodTruckDemo")
    .Options);
