
{
  "profiles": {
    "http": {
      "commandName": "Project",
      "launchBrowser": true,
      "launchUrl": "swagger",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      },
      "dotnetRunMessages": true,
      "applicationUrl": "http://localhost:5170"
    },
    "https": {
      "commandName": "Project",
      "launchBrowser": true,
      "launchUrl": "swagger",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      },
      "dotnetRunMessages": true,
      "applicationUrl": "https://localhost:7027;http://localhost:5170"
    },
    "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "launchUrl": "swagger",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "WSL": {
      "commandName": "WSL2",
      "launchBrowser": true,
      "launchUrl": "https://localhost:7027/swagger",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development",
        "ASPNETCORE_URLS": "https://localhost:7027;http://localhost:5170"
      },
      "distributionName": ""
    }
  },
  "$schema": "http://json.schemastore.org/launchsettings.json",
  "iisSettings": {
    "windowsAuthentication": false,
    "anonymousAuthentication": true,
    "iisExpress": {
      "applicationUrl": "http://localhost:18642",
      "sslPort": 44356
    }
  }
}

USE [workforcedb-dev]
GO
/****** Object:  Schema [emp]    Script Date: 11/9/2024 7:27:03 AM ******/
CREATE SCHEMA [emp]
GO
/****** Object:  Table [emp].[CommonMenu]    Script Date: 11/9/2024 7:27:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [emp].[CommonMenu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MenuName] [nvarchar](100) NOT NULL,
	[MenuURL] [nvarchar](255) NULL,
	[ParentMenuId] [int] NULL,
	[Remark] [nvarchar](200) NOT NULL,
	[ImageIcon] [varbinary](max) NULL,
	[ForPlatform] [int] NULL,
	[IsMenuDisplayInUI] [bit] NOT NULL,
	[IsSubMenu] [bit] NOT NULL,
	[IsDisplayable] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[HasAccess] [bit] NOT NULL,
	[AddedById] [bigint] NULL,
	[AddedDateTime] [datetime] NOT NULL,
	[UpdatedById] [bigint] NULL,
	[UpdatedDateTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [emp].[Employee]    Script Date: 11/9/2024 7:27:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [emp].[Employee](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeDocumentId] [int] NOT NULL,
	[EmployementCode] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[MiddleName] [nvarchar](100) NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[DateOfBirth] [date] NULL,
	[DateOfOnBoarding] [date] NULL,
	[DateOfExit] [date] NULL,
	[SpecializationId] [int] NULL,
	[DesignationId] [int] NULL,
	[EmployeeTypeId] [int] NULL,
	[DepartmentId] [int] NULL,
	[OfficialEmail] [nvarchar](255) NULL,
	[HasPermanent] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[FunctionalId] [int] NULL,
	[ReferalId] [int] NULL,
	[Remark] [nvarchar](200) NULL,
	[AddedById] [bigint] NOT NULL,
	[AddedDateTime] [datetime] NOT NULL,
	[UpdatedById] [bigint] NULL,
	[UpdatedDateTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [emp].[LoginCredential]    Script Date: 11/9/2024 7:27:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [emp].[LoginCredential](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[LoginId] [nvarchar](255) NULL,
	[Password] [nvarchar](255) NULL,
	[HasFirstLogin] [bit] NULL,
	[MacAddress] [nvarchar](255) NULL,
	[IpAddress] [nvarchar](255) NULL,
	[IsActive] [bit] NULL,
	[Remark] [nvarchar](255) NULL,
	[Latitude] [float] NOT NULL,
	[Longitude] [float] NOT NULL,
	[LoginDevice] [int] NOT NULL,
	[AddedById] [bigint] NULL,
	[AddedDateTime] [datetime] NULL,
	[UpdatedById] [bigint] NULL,
	[UpdatedDateTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [emp].[ModuleDetail]    Script Date: 11/9/2024 7:27:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [emp].[ModuleDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ModuleName] [nvarchar](100) NOT NULL,
	[ModuleURL] [nvarchar](255) NULL,
	[ParentModuleId] [int] NULL,
	[IsModuleDisplayInUI] [bit] NOT NULL,
	[IsSubModule] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Remark] [nvarchar](200) NULL,
	[AddedById] [bigint] NOT NULL,
	[AddedDateTime] [datetime] NOT NULL,
	[UpdatedById] [bigint] NULL,
	[UpdatedDateTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [emp].[Operation]    Script Date: 11/9/2024 7:27:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [emp].[Operation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OperationName] [nvarchar](200) NOT NULL,
	[Remark] [nvarchar](200) NULL,
	[IsActive] [bit] NOT NULL,
	[AddedById] [bigint] NULL,
	[AddedDateTime] [datetime] NOT NULL,
	[UpdatedById] [bigint] NULL,
	[UpdateDateTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [emp].[Role]    Script Date: 11/9/2024 7:27:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [emp].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](100) NOT NULL,
	[Remark] [nvarchar](200) NULL,
	[IsActive] [bit] NOT NULL,
	[AddedById] [bigint] NULL,
	[AddedDateTime] [datetime] NOT NULL,
	[UpdatedById] [bigint] NULL,
	[UpdatedDateTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [emp].[RolesPermission]    Script Date: 11/9/2024 7:27:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [emp].[RolesPermission](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[ModuleId] [int] NOT NULL,
	[OperationId] [int] NOT NULL,
	[HasAccess] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Remark] [nvarchar](200) NULL,
	[ImageIcon] [varbinary](max) NULL,
	[AddedById] [bigint] NULL,
	[AddedDateTime] [datetime] NOT NULL,
	[UpdatedById] [bigint] NULL,
	[UpdateDateTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [emp].[UserRole]    Script Date: 11/9/2024 7:27:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [emp].[UserRole](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[RoleId] [int] NOT NULL,
	[IsActive] [bit] NULL,
	[Remark] [nvarchar](500) NULL,
	[AssignedDateTime] [datetime] NULL,
	[RemovedDateTime] [datetime] NULL,
	[AssignedById] [bigint] NOT NULL,
	[RoleStartDate] [datetime] NULL,
	[AddedById] [bigint] NOT NULL,
	[AddedDateTime] [datetime] NULL,
	[UpdatedById] [bigint] NULL,
	[UpdatedDateTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [emp].[CommonMenu] ADD  DEFAULT ((1)) FOR [ForPlatform]
GO
ALTER TABLE [emp].[CommonMenu] ADD  DEFAULT ((0)) FOR [IsSubMenu]
GO
ALTER TABLE [emp].[CommonMenu] ADD  DEFAULT ((1)) FOR [IsDisplayable]
GO
ALTER TABLE [emp].[CommonMenu] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [emp].[CommonMenu] ADD  DEFAULT ((0)) FOR [HasAccess]
GO
ALTER TABLE [emp].[CommonMenu] ADD  DEFAULT (getdate()) FOR [AddedDateTime]
GO
ALTER TABLE [emp].[LoginCredential] ADD  DEFAULT ((0)) FOR [Latitude]
GO
ALTER TABLE [emp].[LoginCredential] ADD  DEFAULT ((0)) FOR [Longitude]
GO
ALTER TABLE [emp].[LoginCredential] ADD  DEFAULT ((0)) FOR [LoginDevice]
GO
ALTER TABLE [emp].[ModuleDetail] ADD  DEFAULT ((0)) FOR [IsSubModule]
GO
ALTER TABLE [emp].[ModuleDetail] ADD  DEFAULT (getdate()) FOR [AddedDateTime]
GO
ALTER TABLE [emp].[Operation] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [emp].[Operation] ADD  DEFAULT (getdate()) FOR [AddedDateTime]
GO
ALTER TABLE [emp].[RolesPermission] ADD  DEFAULT ((0)) FOR [HasAccess]
GO
ALTER TABLE [emp].[RolesPermission] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [emp].[RolesPermission] ADD  DEFAULT (getdate()) FOR [AddedDateTime]
GO
ALTER TABLE [emp].[UserRole] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [emp].[UserRole] ADD  DEFAULT (getdate()) FOR [AssignedDateTime]
GO
ALTER TABLE [emp].[UserRole] ADD  DEFAULT (getdate()) FOR [AddedDateTime]
GO
ALTER TABLE [emp].[CommonMenu]  WITH CHECK ADD  CONSTRAINT [FK_ParentMenuId] FOREIGN KEY([ParentMenuId])
REFERENCES [emp].[CommonMenu] ([Id])
GO
ALTER TABLE [emp].[CommonMenu] CHECK CONSTRAINT [FK_ParentMenuId]
GO
ALTER TABLE [emp].[LoginCredential]  WITH CHECK ADD  CONSTRAINT [FK_LoginCredential_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [emp].[Employee] ([Id])
GO
ALTER TABLE [emp].[LoginCredential] CHECK CONSTRAINT [FK_LoginCredential_Employee]
GO
ALTER TABLE [emp].[ModuleDetail]  WITH CHECK ADD  CONSTRAINT [FK_ParentModule] FOREIGN KEY([ParentModuleId])
REFERENCES [emp].[ModuleDetail] ([Id])
GO
ALTER TABLE [emp].[ModuleDetail] CHECK CONSTRAINT [FK_ParentModule]
GO
ALTER TABLE [emp].[RolesPermission]  WITH CHECK ADD FOREIGN KEY([ModuleId])
REFERENCES [emp].[ModuleDetail] ([Id])
GO
ALTER TABLE [emp].[RolesPermission]  WITH CHECK ADD FOREIGN KEY([OperationId])
REFERENCES [emp].[Operation] ([Id])
GO
ALTER TABLE [emp].[RolesPermission]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [emp].[Role] ([Id])
GO
ALTER TABLE [emp].[UserRole]  WITH CHECK ADD FOREIGN KEY([EmployeeId])
REFERENCES [emp].[Employee] ([Id])
GO
ALTER TABLE [emp].[UserRole]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [emp].[Role] ([Id])
GO








 INSERT INTO [workforcedb-dev].[emp].[Employee]
    ([EmployeeDocumentId], [EmployementCode], [LastName], [MiddleName], [FirstName], [DateOfBirth], 
     [DateOfOnBoarding], [DateOfExit], [SpecializationId], [DesignationId], [EmployeeTypeId], [DepartmentId], 
     [OfficialEmail], [HasPermanent], [IsActive], [FunctionalId], [ReferalId], [Remark], 
     [AddedById], [AddedDateTime], [UpdatedById], [UpdatedDateTime])
VALUES
    (1001, 'EMP001', 'Gupta', NULL, 'Deepesh', '1990-01-15', '2022-06-01', NULL, 1, 3, 1, 2, 
     'deepesh.gupta@company.com', 1, 1, 101, NULL, 'Admin role assigned', 1, GETDATE(), NULL, NULL),
    (1002, 'EMP002', 'Yadav', NULL, 'Ranjeet', '1991-02-10', '2021-04-15', NULL, 2, 4, 2, 1, 
     'ranjeet.yadav@company.com', 1, 1, 102, NULL, 'Manager role assigned', 1, GETDATE(), NULL, NULL),
    (1003, 'EMP003', 'Santosi', NULL, 'Dilip', '1988-05-20', '2020-11-20', NULL, 3, 2, 1, 3, 
     'dilip.santosi@company.com', 1, 1, 103, NULL, 'HR role assigned', 1, GETDATE(), NULL, NULL),
    (1004, 'EMP004', 'Verma', 'Kumar', 'Amit', '1992-03-25', '2019-09-05', NULL, 1, 1, 2, 2, 
     'amit.verma@company.com', 1, 1, 101, NULL, 'Employee in sales', 1, GETDATE(), NULL, NULL),
    (1005, 'EMP005', 'Sharma', 'Prakash', 'Anjali', '1987-07-15', '2018-03-10', NULL, 2, 3, 1, 4, 
     'anjali.sharma@company.com', 1, 1, 102, NULL, 'Employee in marketing', 1, GETDATE(), NULL, NULL),
    (1006, 'EMP006', 'Singh', 'Raj', 'Neha', '1993-04-05', '2017-01-15', NULL, 3, 5, 2, 5, 
     'neha.singh@company.com', 1, 1, 103, NULL, 'IT Support role assigned', 1, GETDATE(), NULL, NULL),
    (1007, 'EMP007', 'Patel', NULL, 'Ravi', '1994-09-10', '2023-02-05', NULL, 1, 6, 1, 1, 
     'ravi.patel@company.com', 1, 1, 101, NULL, 'HR role assigned', 1, GETDATE(), NULL, NULL),
    (1008, 'EMP008', 'Chauhan', NULL, 'Priya', '1995-11-20', '2019-12-12', NULL, 2, 4, 2, 3, 
     'priya.chauhan@company.com', 1, 1, 102, NULL, 'Finance department role', 1, GETDATE(), NULL, NULL),
    (1009, 'EMP009', 'Tiwari', NULL, 'Rakesh', '1989-08-17', '2020-07-15', NULL, 3, 3, 1, 6, 
     'rakesh.tiwari@company.com', 1, 1, 103, NULL, 'Technical lead', 1, GETDATE(), NULL, NULL),
    (1010, 'EMP010', 'Shukla', NULL, 'Sonu', '1990-12-25', '2021-05-20', NULL, 1, 2, 1, 2, 
     'sonu.shukla@company.com', 1, 1, 101, NULL, 'Supervisor in operations', 1, GETDATE(), NULL, NULL);


INSERT INTO [workforcedb-dev].[emp].[LoginCredential]
    ([EmployeeId], [LoginId], [Password], [HasFirstLogin], [MacAddress], 
     [IpAddress], [IsActive], [Remark], [Latitude], [Longitude], [LoginDevice], 
     [AddedById], [AddedDateTime], [UpdatedById], [UpdatedDateTime])
VALUES
    (1, 'deepesh', '123', 1, '00-14-22-01-23-45', '192.168.1.1', 1, 'First login for Deepesh', 28.6139, 77.2090, 1, 1, GETDATE(), NULL, NULL),
    (2, 'ranjeet', '123', 0, '00-14-22-01-23-46', '192.168.1.2', 1, 'First login for Ranjeet', 28.7041, 77.1025, 2, 1, GETDATE(), NULL, NULL),
    (3, 'dilip', '123', 1, '00-14-22-01-23-47', '192.168.1.3', 1, 'First login for Dilip', 19.0760, 72.8777, 1, 1, GETDATE(), NULL, NULL),
    (4, 'amit', '123', 1, '00-14-22-01-23-48', '192.168.1.4', 1, 'First login for Amit', 22.5726, 88.3639, 2, 1, GETDATE(), NULL, NULL),
    (5, 'anjali', '123', 0, '00-14-22-01-23-49', '192.168.1.5', 1, 'First login for Anjali', 13.0827, 80.2707, 1, 1, GETDATE(), NULL, NULL),
    (6, 'neha', '123', 1, '00-14-22-01-23-50', '192.168.1.6', 1, 'First login for Neha', 23.0225, 72.5714, 2, 1, GETDATE(), NULL, NULL),
    (7, 'ravi', '123', 0, '00-14-22-01-23-51', '192.168.1.7', 1, 'First login for Ravi', 26.9124, 75.7873, 1, 1, GETDATE(), NULL, NULL),
    (8, 'priya', '123', 1, '00-14-22-01-23-52', '192.168.1.8', 1, 'First login for Priya', 28.4595, 77.0266, 2, 1, GETDATE(), NULL, NULL),
    (9, 'vikas', '123', 1, '00-14-22-01-23-53', '192.168.1.9', 1, 'First login for Vikas', 18.5204, 73.8567, 1, 1, GETDATE(), NULL, NULL),
    (10, 'rahul', '123', 0, '00-14-22-01-23-54', '192.168.1.10', 1, 'First login for Rahul', 25.3176, 82.9739, 2, 1, GETDATE(), NULL, NULL);





INSERT INTO [workforcedb-dev].[emp].[CommonMenu]
    ([MenuName], [MenuURL], [ParentMenuId], [Remark], [ImageIcon], 
     [ForPlatform], [IsMenuDisplayInUI], [IsSubMenu], [IsDisplayable], 
     [IsActive], [HasAccess], [AddedById], [AddedDateTime], 
     [UpdatedById], [UpdatedDateTime])
VALUES
    ('Dashboard', '/dashboard', NULL, 'Main dashboard view', null, 1, 1, 0, 1, 1, 1, 1, GETDATE(), NULL, NULL),
    ('Mark Attendance', '/attendance/mark', NULL, 'Mark daily attendance', null, 1, 1, 0, 1, 1, 1, 1, GETDATE(), NULL, NULL),
    ('Apply Leave', '/leave/apply', NULL, 'Apply for leave', null, 1, 1, 0, 1, 1, 1, 1, GETDATE(), NULL, NULL),
    ('View Profile', '/profile/view', NULL, 'View personal profile', null, 1, 1, 0, 1, 1, 1, 1, GETDATE(), NULL, NULL),
    ('Settings', '/settings', NULL, 'User and system settings', null, 1, 1, 0, 1, 1, 1, 1, GETDATE(), NULL, NULL),
    ('Leave History', '/leave/history', 3, 'View past leave applications', null, 1, 1, 1, 1, 1, 1, 1, GETDATE(), NULL, NULL),
    ('Attendance History', '/attendance/history', 2, 'View attendance records', null, 1, 1, 1, 1, 1, 1, 1, GETDATE(), NULL, NULL),
    ('Notifications', '/notifications', NULL, 'User notifications and alerts', null, 1, 1, 0, 1, 1, 1, 1, GETDATE(), NULL, NULL),
    ('Help & Support', '/help', NULL, 'Help and support section', null, 1, 1, 0, 1, 1, 1, 1, GETDATE(), NULL, NULL),
    ('Logout', '/logout', NULL, 'Logout from system', null, 1, 1, 0, 1, 1, 1, 1, GETDATE(), NULL, NULL);




INSERT INTO [workforcedb-dev].[emp].[ModuleDetail]
    ([ModuleName], [ModuleURL], [ParentModuleId], [IsModuleDisplayInUI], 
     [IsSubModule], [IsActive], [Remark], [AddedById], [AddedDateTime], 
     [UpdatedById], [UpdatedDateTime])
VALUES
    ('Employee Management', '/employee', NULL, 1, 0, 1, 'Main module for employee operations', 1, GETDATE(), NULL, NULL),
    ('Leave Management', '/leave', NULL, 1, 0, 1, 'Main module for managing leaves', 1, GETDATE(), NULL, NULL),
    ('Attendance Management', '/attendance', NULL, 1, 0, 1, 'Main module for attendance tracking', 1, GETDATE(), NULL, NULL),
    ('Profile Management', '/profile', NULL, 1, 0, 1, 'Module for managing employee profiles', 1, GETDATE(), NULL, NULL),
    ('Settings', '/settings', NULL, 1, 0, 1, 'Module for application settings', 1, GETDATE(), NULL, NULL),
    ('Employee List', '/employee/list', 1, 1, 1, 1, 'Sub-module to list all employees', 1, GETDATE(), NULL, NULL),
    ('Add Employee', '/employee/add', 1, 1, 1, 1, 'Sub-module to add a new employee', 1, GETDATE(), NULL, NULL),
    ('Leave Requests', '/leave/requests', 2, 1, 1, 1, 'Sub-module for viewing leave requests', 1, GETDATE(), NULL, NULL),
    ('Mark Attendance', '/attendance/mark', 3, 1, 1, 1, 'Sub-module to mark attendance', 1, GETDATE(), NULL, NULL),
    ('View Profile', '/profile/view', 4, 1, 1, 1, 'Sub-module to view profile details', 1, GETDATE(), NULL, NULL);



INSERT INTO [workforcedb-dev].[emp].[Operation]
    ([OperationName], [Remark], [IsActive], [AddedById], [AddedDateTime], [UpdatedById], [UpdateDateTime])
VALUES
    ('View', 'Operation to view records', 1, 1, GETDATE(), NULL, NULL),
    ('Add', 'Operation to add new records', 1, 1, GETDATE(), NULL, NULL),
    ('Edit', 'Operation to edit existing records', 1, 1, GETDATE(), NULL, NULL),
    ('Delete', 'Operation to delete records', 1, 1, GETDATE(), NULL, NULL),
    ('Approve', 'Operation to approve requests', 1, 1, GETDATE(), NULL, NULL),
    ('Reject', 'Operation to reject requests', 1, 1, GETDATE(), NULL, NULL),
    ('Export', 'Operation to export data', 1, 1, GETDATE(), NULL, NULL),
    ('Import', 'Operation to import data', 1, 1, GETDATE(), NULL, NULL),
    ('Download', 'Operation to download files', 1, 1, GETDATE(), NULL, NULL),
    ('Upload', 'Operation to upload files', 1, 1, GETDATE(), NULL, NULL);


INSERT INTO [workforcedb-dev].[emp].[Role]
    ([RoleName], [Remark], [IsActive], [AddedById], [AddedDateTime], [UpdatedById], [UpdatedDateTime])
VALUES
    ('Admin', 'Full access to all modules and settings', 1, 1, GETDATE(), NULL, NULL),
    ('Manager', 'Access to manage team and approve requests', 1, 1, GETDATE(), NULL, NULL),
    ('HR', 'Access to HR-specific modules like Employee and Leave Management', 1, 1, GETDATE(), NULL, NULL),
    ('Employee', 'Access to personal modules like Attendance and Profile', 1, 1, GETDATE(), NULL, NULL),
    ('Supervisor', 'Access to oversee and manage operational tasks', 1, 1, GETDATE(), NULL, NULL),
    ('IT Support', 'Access to manage technical issues and support tickets', 1, 1, GETDATE(), NULL, NULL),
    ('Finance', 'Access to financial modules like Payroll and Expense Management', 1, 1, GETDATE(), NULL, NULL),
    ('Guest', 'Limited access for viewing public information', 1, 1, GETDATE(), NULL, NULL),
    ('Intern', 'Limited access for training purposes', 1, 1, GETDATE(), NULL, NULL),
    ('Consultant', 'Access for external consultants with limited functionality', 1, 1, GETDATE(), NULL, NULL);


INSERT INTO [workforcedb-dev].[emp].[RolesPermission]
    ([RoleId], [ModuleId], [OperationId], [HasAccess], [IsActive], [Remark], [ImageIcon], [AddedById], [AddedDateTime], [UpdatedById], [UpdateDateTime])
VALUES
    (1, 1, 1, 1, 1, 'Admin access to Dashboard', null, 1, GETDATE(), NULL, NULL),
    (1, 2, 2, 1, 1, 'Admin can mark attendance', null, 1, GETDATE(), NULL, NULL),
    (2, 3, 3, 1, 1, 'Manager access to Employee List', null, 1, GETDATE(), NULL, NULL),
    (3, 4, 4, 1, 1, 'HR access to Leave Management', null, 1, GETDATE(), NULL, NULL),
    (4, 5, 5, 1, 1, 'Employee access to View Profile', null, 1, GETDATE(), NULL, NULL),
    (4, 1, 1, 1, 1, 'Employee access to Dashboard', null, 1, GETDATE(), NULL, NULL),
    (5, 6, 6, 1, 1, 'Supervisor access to Reports', null, 1, GETDATE(), NULL, NULL),
    (6, 7, 7, 1, 1, 'IT Support access to Settings', null, 1, GETDATE(), NULL, NULL),
    (7, 8, 8, 1, 1, 'Finance access to Payroll Module', null, 1, GETDATE(), NULL, NULL),
    (8, 9, 9, 0, 1, 'Guest restricted from confidential modules', null, 1, GETDATE(), NULL, NULL);


	INSERT INTO [workforcedb-dev].[emp].[UserRole]
    ([EmployeeId], [RoleId], [IsActive], [Remark], [AssignedDateTime], [RemovedDateTime], 
     [AssignedById], [RoleStartDate], [AddedById], [AddedDateTime], [UpdatedById], [UpdatedDateTime])
VALUES
    (1, 1, 1, 'Assigned Admin Role', '2024-11-07 18:57:15', NULL, 1, '2024-01-01', 1, GETDATE(), NULL, NULL),
    (2, 2, 1, 'Assigned Manager Role', '2024-11-07 18:57:15', NULL, 1, '2024-01-05', 1, GETDATE(), NULL, NULL),
    (3, 3, 1, 'Assigned HR Role', '2024-11-07 18:57:15', NULL, 1, '2024-01-10', 1, GETDATE(), NULL, NULL),
    (4, 4, 1, 'Assigned Employee Role', '2024-11-07 18:57:15', NULL, 1, '2024-01-15', 1, GETDATE(), NULL, NULL),
    (5, 5, 1, 'Assigned Supervisor Role', '2024-11-07 18:57:15', NULL, 1, '2024-02-01', 1, GETDATE(), NULL, NULL),
    (6, 6, 1, 'Assigned IT Support Role', '2024-11-07 18:57:15', NULL, 1, '2024-02-05', 1, GETDATE(), NULL, NULL),
    (7, 2, 1, 'Assigned Manager Role', '2024-11-07 18:57:15', NULL, 1, '2024-02-10', 1, GETDATE(), NULL, NULL),
    (8, 1, 1, 'Assigned Admin Role', '2024-11-07 18:57:15', NULL, 1, '2024-02-15', 1, GETDATE(), NULL, NULL),
    (9, 4, 1, 'Assigned Employee Role', '2024-11-07 18:57:15', NULL, 1, '2024-02-20', 1, GETDATE(), NULL, NULL),
    (10, 5, 1, 'Assigned Supervisor Role', '2024-11-07 18:57:15', NULL, 1, '2024-02-25', 1, GETDATE(), NULL, NULL);

