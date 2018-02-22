CREATE TABLE dbo.Appointments
(
	AppointmentId  int IDENTITY(1,1) NOT NULL,
	LocationId     int               NOT NULL,
	FoodTruckId    int               NOT NULL,
	StartTime      datetime          NOT NULL,
	EndTime        datetime          NOT NULL,
    CONSTRAINT PK_Appointments PRIMARY KEY 
        (AppointmentId),
	CONSTRAINT FK_Appointments_FoodTruckId FOREIGN KEY
	    (FoodTruckId) REFERENCES FoodTrucks (FoodTruckId),
	CONSTRAINT FK_Appointments_LocationId FOREIGN KEY
	    (LocationId) REFERENCES Locations (LocationId)
);
GO
