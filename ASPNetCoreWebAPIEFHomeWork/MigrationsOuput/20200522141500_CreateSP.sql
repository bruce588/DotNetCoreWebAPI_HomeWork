

CREATE PROCEDURE [dbo].[sp_Department_insert](
	@Name nvarchar,
	@Budget money,
	@StartDate datetime,
	@InstructorID int,
	@DateModified datetime,
	@IsDeleted bit)
AS
BEGIN
	INSERT INTO [dbo].[Department] ([Name], [Budget], [StartDate], [InstructorID], [DateModified], [IsDeleted]) VALUES (@Name, @Budget, @StartDate, @InstructorID, @DateModified, @IsDeleted);
END
GO

CREATE PROCEDURE [dbo].[sp_Department_update](
	@Name nvarchar,
	@Budget money,
	@StartDate datetime,
	@InstructorID int,
	@DateModified datetime,
	@IsDeleted bit,
	@Original_DepartmentID int,
	@IsNull_Name int,
	@Original_Name nvarchar,
	@Original_Budget money,
	@Original_StartDate datetime,
	@IsNull_InstructorID int,
	@Original_InstructorID int,
	@IsNull_DateModified int,
	@Original_DateModified datetime,
	@Original_IsDeleted bit)
AS
BEGIN
	UPDATE [dbo].[Department] SET [Name] = @Name, [Budget] = @Budget, [StartDate] = @StartDate, [InstructorID] = @InstructorID, [DateModified] = @DateModified, [IsDeleted] = @IsDeleted WHERE (([DepartmentID] = @Original_DepartmentID) AND ((@IsNull_Name = 1 AND [Name] IS NULL) OR ([Name] = @Original_Name)) AND ([Budget] = @Original_Budget) AND ([StartDate] = @Original_StartDate) AND ((@IsNull_InstructorID = 1 AND [InstructorID] IS NULL) OR ([InstructorID] = @Original_InstructorID)) AND ((@IsNull_DateModified = 1 AND [DateModified] IS NULL) OR ([DateModified] = @Original_DateModified)) AND ([IsDeleted] = @Original_IsDeleted));
END
GO

CREATE PROCEDURE [dbo].[sp_Department_delete](
	@Original_DepartmentID int,
	@IsNull_Name int,
	@Original_Name nvarchar,
	@Original_Budget money,
	@Original_StartDate datetime,
	@IsNull_InstructorID int,
	@Original_InstructorID int,
	@IsNull_DateModified int,
	@Original_DateModified datetime,
	@Original_IsDeleted bit)
AS
BEGIN
	DELETE FROM [dbo].[Department] WHERE (([DepartmentID] = @Original_DepartmentID) AND ((@IsNull_Name = 1 AND [Name] IS NULL) OR ([Name] = @Original_Name)) AND ([Budget] = @Original_Budget) AND ([StartDate] = @Original_StartDate) AND ((@IsNull_InstructorID = 1 AND [InstructorID] IS NULL) OR ([InstructorID] = @Original_InstructorID)) AND ((@IsNull_DateModified = 1 AND [DateModified] IS NULL) OR ([DateModified] = @Original_DateModified)) AND ([IsDeleted] = @Original_IsDeleted));
END
