V4 -> V5

Add IGeocoder interface to GoogleGeocoder
Add IGeocoder to LocationsController constructor

* Change code in Create to use GoogleGeocoder.

Add Singleton Geocoder in ConfigureServices - in Startup.cs
    services.AddSingleton<IGeocoder, GoogleGeocoder>();
LocationsControllerTests - Need to pass in Geocoder