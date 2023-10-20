IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [User] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] nvarchar(250) NOT NULL,
    [UserName] nvarchar(250) NOT NULL,
    [Email] nvarchar(250) NULL,
    [BirthDate] datetime2 NOT NULL,
    [Gender] int NULL,
    [Phone] nvarchar(250) NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231019155429_init-db', N'7.0.12');
GO

COMMIT;
GO

