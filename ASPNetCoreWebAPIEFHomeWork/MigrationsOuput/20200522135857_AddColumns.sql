ALTER TABLE [Person] ADD [DateModified] datetime NULL;

GO

ALTER TABLE [Person] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);

GO

ALTER TABLE [Department] ADD [DateModified] datetime NULL;

GO

ALTER TABLE [Department] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);

GO

ALTER TABLE [Course] ADD [DateModified] datetime NULL;

GO

ALTER TABLE [Course] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200522135857_AddColumns', N'3.1.4');

GO

