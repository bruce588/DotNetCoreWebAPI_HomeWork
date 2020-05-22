IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Person] (
    [ID] int NOT NULL IDENTITY,
    [LastName] nvarchar(50) NOT NULL,
    [FirstName] nvarchar(50) NOT NULL,
    [HireDate] datetime NULL,
    [EnrollmentDate] datetime NULL,
    [Discriminator] nvarchar(128) NOT NULL DEFAULT (('Instructor')),
    CONSTRAINT [PK_Person] PRIMARY KEY ([ID])
);

GO

CREATE TABLE [Department] (
    [DepartmentID] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NULL,
    [Budget] money NOT NULL,
    [StartDate] datetime NOT NULL,
    [InstructorID] int NULL,
    [RowVersion] rowversion NOT NULL,
    CONSTRAINT [PK_Department] PRIMARY KEY ([DepartmentID]),
    CONSTRAINT [FK_dbo.Department_dbo.Instructor_InstructorID] FOREIGN KEY ([InstructorID]) REFERENCES [Person] ([ID]) ON DELETE NO ACTION
);

GO

CREATE TABLE [OfficeAssignment] (
    [InstructorID] int NOT NULL,
    [Location] nvarchar(50) NULL,
    CONSTRAINT [PK_dbo.OfficeAssignment] PRIMARY KEY ([InstructorID]),
    CONSTRAINT [FK_dbo.OfficeAssignment_dbo.Instructor_InstructorID] FOREIGN KEY ([InstructorID]) REFERENCES [Person] ([ID]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Course] (
    [CourseID] int NOT NULL IDENTITY,
    [Title] nvarchar(50) NULL,
    [Credits] int NOT NULL,
    [DepartmentID] int NOT NULL DEFAULT (((1))),
    CONSTRAINT [PK_Course] PRIMARY KEY ([CourseID]),
    CONSTRAINT [FK_dbo.Course_dbo.Department_DepartmentID] FOREIGN KEY ([DepartmentID]) REFERENCES [Department] ([DepartmentID]) ON DELETE CASCADE
);

GO

CREATE TABLE [CourseInstructor] (
    [CourseID] int NOT NULL,
    [InstructorID] int NOT NULL,
    CONSTRAINT [PK_dbo.CourseInstructor] PRIMARY KEY ([CourseID], [InstructorID]),
    CONSTRAINT [FK_dbo.CourseInstructor_dbo.Course_CourseID] FOREIGN KEY ([CourseID]) REFERENCES [Course] ([CourseID]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.CourseInstructor_dbo.Instructor_InstructorID] FOREIGN KEY ([InstructorID]) REFERENCES [Person] ([ID]) ON DELETE CASCADE
);

GO

CREATE TABLE [Enrollment] (
    [EnrollmentID] int NOT NULL IDENTITY,
    [CourseID] int NOT NULL,
    [StudentID] int NOT NULL,
    [Grade] int NULL,
    CONSTRAINT [PK_Enrollment] PRIMARY KEY ([EnrollmentID]),
    CONSTRAINT [FK_dbo.Enrollment_dbo.Course_CourseID] FOREIGN KEY ([CourseID]) REFERENCES [Course] ([CourseID]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.Enrollment_dbo.Person_StudentID] FOREIGN KEY ([StudentID]) REFERENCES [Person] ([ID]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_DepartmentID] ON [Course] ([DepartmentID]);

GO

CREATE INDEX [IX_CourseID] ON [CourseInstructor] ([CourseID]);

GO

CREATE INDEX [IX_InstructorID] ON [CourseInstructor] ([InstructorID]);

GO

CREATE INDEX [IX_InstructorID] ON [Department] ([InstructorID]);

GO

CREATE INDEX [IX_CourseID] ON [Enrollment] ([CourseID]);

GO

CREATE INDEX [IX_StudentID] ON [Enrollment] ([StudentID]);

GO

CREATE INDEX [IX_InstructorID] ON [OfficeAssignment] ([InstructorID]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200522112343_init', N'3.1.4');

GO

