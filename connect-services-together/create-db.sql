CREATE TABLE Courses (
    Id int NOT NULL,
    Name varchar(255) NOT NULL,
    rating int,
    PRIMARY KEY (Id)
);

insert into Courses(Id,Name,rating) values(1,'AZ-900',4);

insert into Courses(Id,Name,rating) values(2,'AZ-204',5);

insert into Courses(Id,Name,rating) values(3,'AZ-500',6);

drop table Courses;