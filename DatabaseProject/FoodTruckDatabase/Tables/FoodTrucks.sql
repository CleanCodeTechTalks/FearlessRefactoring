CREATE TABLE dbo.FoodTrucks
(
	FoodTruckId  int IDENTITY(1,1)  NOT NULL,
	Name         varchar(40)        NOT NULL,
	Description  varchar(150)       NOT NULL,
	Website      varchar(60)        NOT NULL,
    CONSTRAINT PK_FoodTrucks PRIMARY KEY 
        (FoodTruckId)
);
GO
