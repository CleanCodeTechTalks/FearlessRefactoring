CREATE TABLE dbo.Locations
(
	LocationId        int IDENTITY(1,1) NOT NULL,
	Name              varchar(40)       NOT NULL,
	StreetAddress     varchar(50)       NOT NULL,
	City              varchar(40)       NOT NULL,
	State             varchar(2)        NOT NULL,
	ZipCode           varchar(5)        NOT NULL,
	FormattedAddress  varchar(512)      NOT NULL,
    CONSTRAINT PK_Locations PRIMARY KEY 
        (LocationId)
);

GO
