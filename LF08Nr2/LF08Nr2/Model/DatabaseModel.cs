using System.Data.SQLite;
using System.IO;
using System.Windows;

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

        public void addDataPerson(ImportModel model)
        {
            if (isDataAlreadyInDbPerson(model.Name, model.Lastname, model.schoolClass))
            {
                {
                    addDataPersonSQL(model);
                    addDataStudentsCoursesTimes(model);
                    MessageBox.Show(model.Name + " " + model.Lastname + " wurde Erfolgreich importiert!", "Import");
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Diese Person ist bereits in der Datenbank vorhanden...", "Import", MessageBoxButton.OK);
                /*
                switch (result)
                {
                    //TODO User an besten keine Wahl geben 
                    case MessageBoxResult.Yes:
                        addDataPersonSQL(model);
                        MessageBox.Show(model.Name + " " + model.Lastname + " wurde Erfolgreich importiert!", "Import");
                        break;
                    case MessageBoxResult.No:
                        break;
                }
                */
            }
        }

        private void addDataStudentsCoursesTimes(ImportModel model)
        {
            addDataStudentsCoursesTimesSQL(addDataPersonCourses(model));
        }

        private void addDataStudentsCoursesTimesSQL(List<int> ids)
        {
            for (int i = 0; i < ids.Count / 3; i++)
            {
                try
                {
                    dbFilePath = dbPath + "\\CourseRegistration.db";
                    createDbConnection();
                    sqlCommand = "insert into StudentsCoursesTimes" + " (studentID,timeID,courseID) " + "values('" + ids[0 + (i * 3)] + "','" + ids[1 + (i * 3)] + "'" + ",'" + ids[2 + (i * 3)] + "');";
                    executeQuery(sqlCommand);
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void addDataPersonSQL(ImportModel model)
        {
            try
            {
                dbFilePath = dbPath + "\\CourseRegistration.db";
                createDbConnection();
                sqlCommand = "insert into Students" + " (firstName,lastName,className) " + "values('" + model.Name + "','" + model.Lastname + "'" + ",'" + model.schoolClass + "');";
                executeQuery(sqlCommand);
            }
            catch (Exception ex)
            {

            }
        }

        private List<int> addDataPersonCourses(ImportModel model)
        {
            List<int> ids = new List<int>();

            if (model.isInM1Monday)
            {
                ids.Add(addDataStudentCoursesSQL(model));
                ids.Add(addDataTimesCoursesSQL("Mo.", "08:00", "11:00"));
                ids.Add(addDataCoursesCoursesSQL("M-1", "Mathe", "A.1.15"));
            }
            if (model.isInM1Tuesday)
            {
                ids.Add(addDataStudentCoursesSQL(model));
                ids.Add(addDataTimesCoursesSQL("Di.", "12:00", "15:00"));
                ids.Add(addDataCoursesCoursesSQL("M-1", "Mathe", "A.1.15"));
            }
            if (model.isInD1Monday)
            {
                ids.Add(addDataStudentCoursesSQL(model));
                ids.Add(addDataTimesCoursesSQL("Mo.", "12:00", "15:00"));
                ids.Add(addDataCoursesCoursesSQL("D-1", "Deutsch", "A1.15"));
            }
            if (model.isInD1Thursday)
            {
                ids.Add(addDataStudentCoursesSQL(model));
                ids.Add(addDataTimesCoursesSQL("Do.", "08:00", "11:00"));
                ids.Add(addDataCoursesCoursesSQL("D-1", "Deutsch", "A1.15"));
            }

            return ids;
        }

        private int addDataStudentCoursesSQL(ImportModel model)
        {
            try
            {
                dbFilePath = dbPath + "\\CourseRegistration.db";
                createDbConnection();
                command.CommandText =
                @"
                    SELECT id
                    FROM Students
                    WHERE firstName = $firstName
                    AND lastName = $lastName
                    AND className = $className
                ";

                command.Parameters.AddWithValue("$firstName", model.Name);
                command.Parameters.AddWithValue("$lastName", model.Lastname);
                command.Parameters.AddWithValue("$className", model.schoolClass);

                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                    var id = reader.GetInt32(0);
                    return id;
                }
            }
            catch (Exception ex)
            {
                return -1; //TODO Ordentlich machen
            }
        }

        private int addDataTimesCoursesSQL(String dayName, String startTime, String endTime)
        {
            try
            {
                dbFilePath = dbPath + "\\CourseRegistration.db";
                createDbConnection();
                command.CommandText =
                 @"
                    SELECT id
                    FROM times
                    WHERE dayName = $dayName
                    AND startTime = $startTime
                    AND endTime = $endTime
                ";

                command.Parameters.AddWithValue("$dayName", dayName);
                command.Parameters.AddWithValue("$startTime", startTime);
                command.Parameters.AddWithValue("$endTime", endTime);

                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                    var id = reader.GetInt32(0);
                    return id;
                    //Trace.WriteLine($"Hello, {name}!");
                }
            }
            catch (Exception ex)
            {
                return -1; //TODO Ordentlich machen
            }
        }

        private int addDataCoursesCoursesSQL(String course, String topic, String room)
        {
            try
            {
                command.CommandText =
                @"
                    SELECT id
                    FROM courses
                    WHERE course = $course
                    AND topic = $topic
                    AND room = $room
                ";

                command.Parameters.AddWithValue("$course", course);
                command.Parameters.AddWithValue("$topic", topic);
                command.Parameters.AddWithValue("$room", room);

                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                    var id = reader.GetInt32(0);
                    return id;
                }
            }
            catch (Exception ex)
            {
                return -1; //TODO Ordentlich machen
            }
        }

        private bool isDataAlreadyInDbCourse(String course, String topic, String room)
        {
            try
            {
                isDataAlreadyInDbCourseSQL(course, topic, room);

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
                isDataAlreadyInDbTimesSQL(dayName, startTime, endTime);

                var result = command.ExecuteScalar();

                return result == null ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool isDataAlreadyInDbPerson(String firstName, String lastName, String className)
        {
            try
            {
                isDataAlreadyInDbPersonSQL(firstName, lastName, className);

                var result = command.ExecuteScalar();

                return result == null ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private void isDataAlreadyInDbPersonSQL(String firstName, String lastName, String className)
        {
            dbFilePath = dbPath + "\\CourseRegistration.db";
            createDbConnection();
            command.CommandText =
            @"
                    SELECT firstName, lastName, className
                    FROM Students
                    WHERE firstName = $firstName
                    AND lastName = $lastName
                    AND className = $className
            ";

            command.Parameters.AddWithValue("$firstName", firstName);
            command.Parameters.AddWithValue("$lastName", lastName);
            command.Parameters.AddWithValue("$className", className);
        }
        private void isDataAlreadyInDbTimesSQL(String dayName, String startTime, String endTime)
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
        }
        private void isDataAlreadyInDbCourseSQL(String course, String topic, String room)
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
        }
        public List<String> databaseToString()
        {
            List<String> result = new List<String>();
            
            result.AddRange(databaseToStringCourses());
            
            result.AddRange(databaseToStringStudent());
            result.AddRange(databaseToStringStudentsCoursesTimes());
            result.AddRange(databaseToStringTimes());
            //TODO make better
            return result;
        }

        private List<String> databaseToStringCourses()
        {
            dbFilePath = dbPath + "\\CourseRegistration.db";
            createDbConnection();
            command.CommandText = """
                                  SELECT *
                                  FROM Courses
                                  """;
            List<int> dataCoursesIds = new List<int>();
            List<String> dataCourses = new List<String>();
            List<String> dataCourseAll = new List<String>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    dataCoursesIds.Add(reader.GetInt32(0));
                    dataCourses.Add(reader.GetString(1));
                    dataCourses.Add(reader.GetString(2));
                    dataCourses.Add(reader.GetString(3));
                }
            }

            foreach (var id in dataCoursesIds) 
            {
                dataCourseAll.Add(Convert.ToString(id));
            }

            foreach (var text in dataCourses)
            {
                dataCourseAll.Add(Convert.ToString(text));
            }

            return dataCourseAll;
        }

        private List<String> databaseToStringStudent()
        {
            dbFilePath = dbPath + "\\CourseRegistration.db";
            createDbConnection();
            command.CommandText = """
                                  SELECT *
                                  FROM Students
                                  """;
            List<int> dataStudentsIds = new List<int>();
            List<String> dataStudents = new List<String>();
            List<String> dataStudentsAll = new List<String>();


            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    dataStudentsIds.Add(reader.GetInt32(0));
                    dataStudents.Add(reader.GetString(1));
                    dataStudents.Add(reader.GetString(2));
                    dataStudents.Add(reader.GetString(3));
                }
            }
            
            foreach (var id in dataStudentsIds)
            {
                dataStudentsAll.Add(Convert.ToString(id));
            }

            foreach (var text in dataStudents)
            {
                dataStudentsAll.Add(Convert.ToString(text));
            }

            return dataStudentsAll;
        }

        private List<String> databaseToStringTimes()
        {
            dbFilePath = dbPath + "\\CourseRegistration.db";
            createDbConnection();
            command.CommandText = """
                                  SELECT *
                                  FROM Times
                                  """;
            List<int> dataTimesIds = new List<int>();
            List<DateTime> dataTimes = new List<DateTime>();
            List<string> dataTimeString = new List<string>();
            List<string> dataTimeStringAll = new List<string>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    dataTimesIds.Add(reader.GetInt32(0));
                    dataTimeString.Add(reader.GetString(1));
                    dataTimes.Add(reader.GetDateTime(2));
                    dataTimes.Add(reader.GetDateTime(3));
                }
            }

            foreach (var id in dataTimesIds)
            {
                dataTimeStringAll.Add(Convert.ToString(id));
            }

            foreach (var text in dataTimeString)
            {
                dataTimeStringAll.Add(Convert.ToString(text));
            }

            foreach (var time in dataTimes)
            {
                dataTimeStringAll.Add(Convert.ToString(time));
            }

            return dataTimeStringAll;
        }
        private List<String> databaseToStringStudentsCoursesTimes() {
        dbFilePath = dbPath + "\\CourseRegistration.db";
            createDbConnection();
        command.CommandText =
            @"
            SELECT *
            FROM StudentsCoursesTimes
            ";
            List<int> dataStudentsCoursesTimes = new List<int>();
            List<String> dataStudentsCoursesTimesString = new List<String>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    dataStudentsCoursesTimes.Add(reader.GetInt32(0));
                    dataStudentsCoursesTimes.Add(reader.GetInt32(1));
                    dataStudentsCoursesTimes.Add(reader.GetInt32(2));
                    dataStudentsCoursesTimes.Add(reader.GetInt32(3));
                }
            }

            foreach (var data in dataStudentsCoursesTimes) 
            {
                dataStudentsCoursesTimesString.Add(Convert.ToString(data));
            }

            return dataStudentsCoursesTimesString;
        }
    }
    
}
