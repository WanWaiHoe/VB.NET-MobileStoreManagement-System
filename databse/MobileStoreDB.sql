CREATE DATABASE MobileStoreDB;
GO
USE MobileStoreDB;
GO

CREATE TABLE [dbo].[Attendances] (
    [AttendanceId] INT           IDENTITY (1, 1) NOT NULL,
    [Time]         VARCHAR (50)  NOT NULL,
    [Status]       VARCHAR (MAX) NOT NULL,
    [Date]         DATE          NOT NULL,
    [StaffId]      NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([AttendanceId] ASC),
    CONSTRAINT [FK_Attendances_Staffs] FOREIGN KEY ([StaffId]) REFERENCES [dbo].[Staffs] ([StaffId])
);

CREATE TABLE [dbo].[Sales] (
    [SalesId]         INT             IDENTITY (1, 1) NOT NULL,
    [SalesDate]       DATE            NOT NULL,
    [StaffId]         NVARCHAR (50)   NOT NULL,
    [Quantity]        INT             NOT NULL,
    [CustomerName]    NVARCHAR (MAX)  NOT NULL,
    [CustomerContact] NVARCHAR (MAX)  NOT NULL,
    [Price]           DECIMAL (18, 2) NOT NULL,
    [ProductName]     NVARCHAR (50)   NOT NULL,
    PRIMARY KEY CLUSTERED ([SalesId] ASC),
    CONSTRAINT [FK_Sales_Staffs] FOREIGN KEY ([StaffId]) REFERENCES [dbo].[Staffs] ([StaffId]),
    CONSTRAINT [FK_Sales_Stores] FOREIGN KEY ([ProductName]) REFERENCES [dbo].[Stores] ([ProductName])
);

CREATE TABLE [dbo].[Stores] (
    [ProductName]   NVARCHAR (50)  NOT NULL,
    [Quantity]      INT            NOT NULL,
    [ProductStatus] NVARCHAR (MAX) NOT NULL,
    [StockIn]       DATETIME       NOT NULL,
    [StockOut]      DATETIME       NULL,
    [Remark]        NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([ProductName] ASC)
);

CREATE TABLE [dbo].[Staffs] (
    [StaffId]   NVARCHAR (50)  NOT NULL,
    [Password]  NVARCHAR (MAX) NOT NULL,
    [StaffType] NVARCHAR (MAX) NOT NULL,
    [Contact]   NVARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([StaffId] ASC)
);