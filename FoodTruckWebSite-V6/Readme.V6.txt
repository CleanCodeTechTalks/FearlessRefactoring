V6 -> V7

In LocationsController Edit method replace inline geocoder call with call to Geocoder.GetGeocodeAsync.

Wrap FoodTruckMvcBaseConfiguration class in new LocationsControllerTests base class.
Create new LocationsControllerEditTests class

Drop GoogleGeocoderTests.cs.
