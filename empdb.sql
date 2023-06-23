select * from Department;
select * from Position;
select * from Employee;
insert into Position (positionName,DepartmentId)values('Junior Software Developer',1);
insert into Position (positionName,DepartmentId)values('Senior Software Developer',1);
insert into Position (positionName,DepartmentId)values('Junior Software Engineer',1);
insert into Position (positionName,DepartmentId)values('Senior Software Engineer',1);

update Position set positionName='FrontEnd Developer' where posId=1;
delete from Position where posId=3;

insert into Position (positionName,DepartmentId)values('Manager',2);
insert into Position (positionName,DepartmentId)values('Assistant Manager',2);


insert into Department(depname)values('IT');
insert into Department(depname)values('HR');


drop table Employee;
drop table Position;
drop table Department;


CREATE TABLE [Department] (
          [depId] int NOT NULL IDENTITY,
          [depname] nvarchar(max) NOT NULL,
          CONSTRAINT [PK_Department] PRIMARY KEY ([depId])
      );

CREATE TABLE [Position] (
          [posId] int NOT NULL IDENTITY,
          [positionName] nvarchar(max) NOT NULL,
          [DepartmentId] int NOT NULL,
          CONSTRAINT [PK_Position] PRIMARY KEY ([posId]),
          CONSTRAINT [FK_Position_Department_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Department] ([depId]) ON DELETE CASCADE
      );

CREATE TABLE [Employee] (
    [empNo] int NOT NULL IDENTITY,
    [empName] nvarchar(max) NOT NULL,
    [DOB] datetime2 NOT NULL,
    [gender] nvarchar(max) NOT NULL,
    [DepartmentId] int NOT NULL,
    [PositionId] int NOT NULL,
    CONSTRAINT [PK_Employee] PRIMARY KEY ([empNo]),
    CONSTRAINT [FK_Employee_Department_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Department] ([depId]) ON DELETE CASCADE
    
);

 

	 