using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UglyToad.PdfPig;
using UglyToad.PdfPig.AcroForms;
using UglyToad.PdfPig.AcroForms.Fields;

namespace LF08Nr2.Model
{
    internal class pdfModel
    {
        public bool isCorrectPdf()
        {
            return false;
        }

        public void readInPdf(PdfDocument pdf)
        {
            //readInCourses(pdf);
            readInPerson(pdf);
        }
        public void readInCourses(String[] courses)
        {
            for (int i = 0; i < 2; i++)
            {
                String[] tmp = courses[3 + (i*4)].Split(" ");
                String[] tmpRoom = courses[6 + (i * 4)].Split(" ");
                try
                {
                    tmp[2] = tmp[2].Substring(1, tmp[2].Length - 2);
                    DatabaseModel databaseModel = new DatabaseModel();
                    databaseModel.addDataCourses(tmp[1], tmp[2], tmpRoom[1]);
                }
                catch (Exception ex)
                {
                    try 
                    {
                        //TODO check if specifc situation is true and then do this
                        tmp[1] = tmp[1].Replace("(", " ");
                        String[] tmp2 = tmp[1].Split(" ");
                        tmp2[1] = tmp2[1].Substring(0, tmp2[1].Length - 1);
                        DatabaseModel databaseModel = new DatabaseModel();
                        databaseModel.addDataCourses(tmp2[0], tmp2[1], tmpRoom[1]);
                    }
                    catch (Exception e)
                    { 
                    }   
                }   
            }
        }
        public void readInTimes(String[] times) {
            for (int i = 0; i < 2; i++)
            {
                String[] tmp = times[4 + (i * 4)].Split(" ");
                String[] tmp2 = times[5 + (i * 4)].Split(" ");

                tmp[0] = tmp[0].Substring(0, tmp[0].Length - 1);
                String[] tmpTimes = tmp[1].Split("-");
                tmpTimes[1].Remove(startIndex: 0, count: 1);

                tmp2[0] = tmp2[0].Substring(0, tmp2[0].Length - 1);
                tmp2[1] = tmp2[1].Substring(0, tmp2[1].Length - 1);

                DatabaseModel databaseModel = new DatabaseModel();
                databaseModel.addDataTimes(tmp[0], tmpTimes[0], tmpTimes[1]);
                databaseModel.addDataTimes(tmp2[0], tmp2[1], tmp2[2]);
            }
        }

        private void readInPerson(PdfDocument pdf)
        {
            if (!pdf.TryGetForm(out var form))
            {
                Trace.WriteLine($"No form found in file.");
                return;
            }

            //var page1Fields = form.GetFieldsForPage(1);

            foreach (var field in form.Fields)
            {
                switch (field)
                {
                    case AcroTextField text:
                        Trace.WriteLine($"Found text field on page 1 with text: {text.Value}.");
                        break;
                    case AcroCheckboxField cboxes:
                        Trace.WriteLine($"Found checkboxes field on page 1 with {cboxes.IsChecked} checkboxes.");
                        break;
                }

                //bool hasForm = pdf.TryGetForm(out AcroForm form);
                //Trace.WriteLine(hasForm);
            }
        }
    }
}
