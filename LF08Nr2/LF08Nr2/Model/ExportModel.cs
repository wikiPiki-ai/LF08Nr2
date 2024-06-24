using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Core;
using UglyToad.PdfPig.Fonts.Standard14Fonts;
using UglyToad.PdfPig.Writer;
using System.Data.Entity.Infrastructure;

namespace LF08Nr2.Model
{
    public class ExportModel
    {
        public void csvExport()
        {
            DatabaseModel dbModel = new DatabaseModel();
            csvStuff(dbModel.databaseToString());
            
        }
        public void pdfExport() 
        {
            DatabaseModel dbModel = new DatabaseModel();
            pdfStuff(dbModel.databaseToString());
        }

        private void csvStuff(List<String> db)
        {

            /*
            for (int i = 0; i < db.Count; i++)
            {
                //int strLength = db[i].Length;
                //db[i] = db[i].Replace(Environment.NewLine, ',');
                //test1[i] = test1[i].Insert(strLength, Environment.NewLine);

            }
            */

            using (System.IO.StreamWriter file = new System.IO.StreamWriter("DBAusgabe.csv"))
            {
                file.WriteLine(string.Join(",", db));
            }

            MessageBox.Show("Die .csv Datei wurde erfolgreich erstellt!", ".csv export", MessageBoxButton.OK);
        }

        private void pdfStuff(List<String> db)
        {
            //var file = new File();

            //string[] test1 = { "1 tim bauer deutsch", "2 günther braun mathe" };

            PdfDocumentBuilder builder = new PdfDocumentBuilder();
            
            PdfPageBuilder page = builder.AddPage(PageSize.A4);

            PdfDocumentBuilder.AddedFont helvetica = builder.AddStandard14Font(Standard14Font.Helvetica);
            PdfDocumentBuilder.AddedFont helveticaBold = builder.AddStandard14Font(Standard14Font.HelveticaBold);

            PdfPoint closeToTop = new PdfPoint(15, page.PageSize.Top - 25);


            foreach (String s in db)
            {
                page.AddText(s, 12, closeToTop, helvetica);
                //page.AddText(csvStuff(db),12, closeToTop, helvetica);
            }

            File.WriteAllBytes("Export.pdf", builder.Build());
            MessageBox.Show("Die .pdf Datei wurde erfolgreich erstellt!", ".pdf export", MessageBoxButton.OK);
        }
    }
}
