
CREATE DATABASE CourseRegistration;



CREATE TABLE Courses(
	id int not null AUTO_INCREMENT,
	course VARCHAR(255),
	topic VARCHAR(255),
	PRIMARY KEY(id),
	);



CREATE TABLE Students(
	id int not null AUTO_INCREMENT,
	firstName varchar(255),
	lastName varchar(255),
	className varchar(255),
	PRIMARY KEY(id),
	);
	
	
CREATE TABLE Times(
	id int not null AUTO_INCREMENT,
	dayName varchar(255),
	startTime time,
	endTime time,
	PRIMARY KEY(id),
	);


Create table StudentsCoursesTimes(
	courseID int,
	studentID int,
	timeID int,
	PRIMARY KEY(id),
	FOREIGN KEY(studentID) REFERENCES Students(id),
	FOREIGN KEY(timeID) REFERENCES Times(id)
	FOREIGN KEY(courseID) REFERENCES Courses(id)
	);



-- INSERT statements mit beispielvalues

INSERT INTO Times
VALUES ('Monday',00:00:00,00:00:00);

INSERT INTO Courses
VALUES ('D-1','Deutsch');

INSERT INTO Students
VALUES ('Remmud','Remmacs','ITA22');

INSERT INTO StudentsCoursesTimes
VALUES (0,0,0);


-- DELETE statements mit Beispielvalues

DELETE FROM Courses WHERE id=1;

DELETE FROM Times WHERE id=1;

DELETE FROM Students WHERE id=1;

DELETE FROM StudentsCoursesTimes WHERE studentID=1;


