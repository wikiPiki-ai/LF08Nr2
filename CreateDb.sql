CREATE DATABASE CourseRegistration;



CREATE TABLE Courses(
	id int not null AUTO_INCREMENT,
	name VARCHAR(24),
	Theme VARCHAR2,
	startTime int,
	endTime int,
	PRIMARY KEY(id),
	);

CREATE TABLE Rooms(
	id int not null AUTO_INCREMENT,
	name VARCHAR2,
	PRIMARY KEY(id),
	);

CREATE TABLE Customers(
	id int not null AUTO_INCREMENT,
	name varchar2,
	secondName varchar2,
	PRIMARY KEY(id),
	);
	

Create table CustomersCourses(
	CourseID int,
	CustomerID int,
	RoomID int,
	PRIMARY KEY(id),
	FOREIGN KEY(CustomerID) REFERENCES Customers(id),
	FOREIGN KEY(RoomID) REFERENCES Rooms(id)
	FOREIGN KEY(CourseID) REFERENCES Courses(id)
	);
	
	
	
	
