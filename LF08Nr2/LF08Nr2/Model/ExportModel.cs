using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF08Nr2.Model
{
    public class ExportModel
    {
        public void csvExport()
        {
            DatabaseModel dbModel = new DatabaseModel();
            csvStuff(dbModel.databaseToString());
        }

        private void csvStuff(List<String> db) 
        {
            

            for (int i = 0; i < db.Count; i++)
            {
                //int strLength = db[i].Length;
                //db[i] = db[i].Replace(Environment.NewLine, ',');
                //test1[i] = test1[i].Insert(strLength, Environment.NewLine);

            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter("DBAusgabe.csv"))
            {
                file.WriteLine(string.Join(",", db));
            }
        }
    }
}
