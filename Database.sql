CREATE LOGIN ArmadilloWrangler WITH PASSWORD='abc123', CHECK_POLICY=OFF;
GO
USE TheFarm;
GO
CREATE USER ArmadilloWrangler;
GO
EXEC sp_addrolemember 'db_owner', 'ArmadilloWrangler';
GO
CREATE TABLE Armadillo
(
	ID int primary key identity(1, 1),
	Name varchar(50),
	Age int,
	ShellHardness int,
	IsPainted bit,
	Homeland varchar(50),
	Timestamp datetime default(getdate())
)
GO
CREATE PROCEDURE Armadillo_Get
(
	@ID int = NULL
	
)
AS
BEGIN
	
	SELECT * 
	FROM Armadillo 
	WHERE ID = @ID;
END
GO
CREATE PROCEDURE Armadillo_GetList
AS
BEGIN
	SELECT * 
	FROM Armadillo
	ORDER BY Name;
END
GO
CREATE PROCEDURE Armadillo_InsertUpdate
(
	@ID int = NULL,
	@Name varchar(50),
	@Age int,
	@ShellHardness int,
	@IsPainted bit,
	@Homeland varchar(50)
)
AS
BEGIN
IF @ID IS NULL
	BEGIN
	INSERT INTO Armadillo 
	(
		Name,
		Age,
		ShellHardness,
		IsPainted,
		Homeland
	)
	VALUES
	(
		@Name,
		@Age,
		@ShellHardness,
		@IsPainted,
		@Homeland
	)
	END
	ELSE
	BEGIN
	 UPDATE Armadillo SET Name=@Name, Age=@Age, ShellHardness=@ShellHardness, IsPainted=@IsPainted, Homeland=@Homeland
	 WHERE ID=@ID;
	END
END
