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
            String[] tmp = courses[3].Split(" ");
            tmp[2] = tmp[2].Substring(1, tmp[2].Length - 2);
            DatabaseModel databaseModel = new DatabaseModel();
            databaseModel.addData("courses", tmp[1], tmp[2]);
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
