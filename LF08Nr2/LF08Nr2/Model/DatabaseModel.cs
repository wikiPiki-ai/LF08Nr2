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

        public void checkIfDbExist() {
            createDbFile();
            Boolean isSucessfull = false;
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
                createTable("Courses", "id Integer not NULL PRIMARY KEY AUTOINCREMENT,course VARCHAR(255),topic VARCHAR(255)");
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
            //TODO
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
    }
}
