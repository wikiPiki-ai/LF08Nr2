using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Xml.Linq;

namespace LF08Nr2.Model
{
    //https://stackoverflow.com/questions/31266217/how-to-create-sqlite-database-in-wpf
    public class DatabaseModel
    {
        SQLiteConnection dbConnection;
        SQLiteCommand command;
        string sqlCommand;
        string dbPath = System.Environment.CurrentDirectory + "\\DB";
        string dbFilePath;

        public void checkIfDbExist()
        {
            createDbFile();
            bool isSucessfull = false;
            try
            {
                createDbConnection();
                isSucessfull = true;
            }
            catch (Exception ex)
            {
                //add Exception message maybe?
            }
            if (isSucessfull)
            {
                createTable("Courses", "id Integer not NULL PRIMARY KEY AUTOINCREMENT,course VARCHAR(255),topic VARCHAR(255),room VARCHAR(255)");
                createTable("Students", "id Integer not NULL PRIMARY KEY AUTOINCREMENT,firstName varchar(255),lastName varchar(255),className varchar(255)");
                createTable("Times", "id Integer not NULL PRIMARY KEY AUTOINCREMENT,dayName varchar(255),startTime time,endTime time");
                createTable("StudentsCoursesTimes", "id Integer not NULL PRIMARY KEY AUTOINCREMENT,courseID int,studentID int,timeID int,FOREIGN KEY(studentID) REFERENCES Students(id),FOREIGN KEY(timeID) REFERENCES Times(id)FOREIGN KEY(courseID) REFERENCES Courses(id)");
            }
        }
        public void createDbFile()
        {
            if (!string.IsNullOrEmpty(dbPath) && !Directory.Exists(dbPath))
                Directory.CreateDirectory(dbPath);
            dbFilePath = dbPath + "\\CourseRegistration.db";
            if (!System.IO.File.Exists(dbFilePath))
            {
                SQLiteConnection.CreateFile(dbFilePath);
            }
        }

        public string createDbConnection()
        {
            string strCon = string.Format("Data Source={0};", dbFilePath);
            dbConnection = new SQLiteConnection(strCon);
            dbConnection.Open();
            command = dbConnection.CreateCommand();
            return strCon;
        }

        public void createTable(String tableName, String sqlCommandUser)
        {
            if (!checkIfExist(tableName))
            {
                sqlCommand = "CREATE TABLE " + tableName + " (" + sqlCommandUser + ");";
                executeQuery(sqlCommand);
            }

        }

        public bool checkIfExist(string tableName)
        {
            command.CommandText = "SELECT name FROM sqlite_master WHERE name='" + tableName + "'";
            var result = command.ExecuteScalar();

            return result != null && result.ToString() == tableName ? true : false;
        }

        public void executeQuery(string sqlCommand)
        {
            SQLiteCommand triggerCommand = dbConnection.CreateCommand();
            triggerCommand.CommandText = sqlCommand;
            triggerCommand.ExecuteNonQuery();
        }

        public bool checkIfTableContainsData(string tableName)
        {
            command.CommandText = "SELECT count(*) FROM " + tableName;
            var result = command.ExecuteScalar();

            return Convert.ToInt32(result) > 0 ? true : false;
        }


        public void fillTable()
        {

            //TODO
            if (!checkIfTableContainsData("MY_TABLE"))
            {
                sqlCommand = "insert into MY_TABLE (code_test_type) values (999)";
                executeQuery(sqlCommand);
            }
        }

        public void addDataCourses(String course, string topic, String room)
        {
            //TODO check if Data alreadzy in database
            if (isDataAlreadyInDbCourse(course, topic, room))
            {
                try
                {
                    dbFilePath = dbPath + "\\CourseRegistration.db";
                    createDbConnection();
                    sqlCommand = "insert into Courses" + " (course,topic,room) " + "values('" + course + "'" + ",'" + topic + "','" + room + "');";
                    executeQuery(sqlCommand);
                }
                catch (Exception ex)
                {

                }
            }
        }

        public void addDataTimes(String dayName, string startTime, String endTime)
        {
            //TODO check if Data alreadzy in database
            if (isDataAlreadyInDbTimes(dayName, startTime, endTime))
            {
                try
                {
                    dbFilePath = dbPath + "\\CourseRegistration.db";
                    createDbConnection();
                    sqlCommand = "insert into Times" + " (dayName,startTime,endTime) " + "values('" + dayName + "','" + startTime + "'" + ",'" + endTime + "');";
                    executeQuery(sqlCommand);
                }
                catch (Exception ex)
                {

                }
            }
        }

        private bool isDataAlreadyInDbCourse(String course, String topic, String room) 
        {
            try
            {
                dbFilePath = dbPath + "\\CourseRegistration.db";
                createDbConnection();
                command.CommandText =
                @"
                    SELECT course, topic, room
                    FROM courses
                    WHERE course = $course
                    AND topic = $topic
                    AND room = $room
                ";

                command.Parameters.AddWithValue("$course", course);
                command.Parameters.AddWithValue("$topic", topic);
                command.Parameters.AddWithValue("$room", room);

                var result = command.ExecuteScalar();

                return result == null ? true : false;
               
                /*using (var reader = command.ExecuteReader()) 
                {
                    while (reader.Read())
                    {
                        var name = reader.GetString(0);

                        Trace.WriteLine($"Hello, {name}!");
                    }
                }
                */

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool isDataAlreadyInDbTimes(String dayName, String startTime, String endTime)
        {
            try
            {
                dbFilePath = dbPath + "\\CourseRegistration.db";
                createDbConnection();
                command.CommandText =
                @"
                    SELECT dayName, startTime, endTime
                    FROM times
                    WHERE dayName = $dayName
                    AND startTime = $startTime
                    AND endTime = $endTime
                ";

                command.Parameters.AddWithValue("$dayName", dayName);
                command.Parameters.AddWithValue("$startTime", startTime);
                command.Parameters.AddWithValue("$endTime", endTime);

                var result = command.ExecuteScalar();

                return result == null ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
