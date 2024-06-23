using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using LF08Nr2.Model;
using LF08Nr2.ViewModel.Base;
using Microsoft.Win32;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;
using UglyToad.PdfPig;

namespace LF08Nr2.ViewModel
{
    public class ImportViewModel : ViewModelBase
    {
        private List<FileModel> privateFiles;

        // CollectionView für das DataGrid
        public List<FileModel> Files
        {
            get => privateFiles;
            set
            {
                privateFiles = value;
                OnPropertyChanged();
                
            }
        }

        public int counter = 0;

        public void BindingExampel(List<FileModel> coolFiles)
        {
            //counter++;
            Files = coolFiles;
            
        }

        public void AddDataFromExplorer(List<FileModel> Files)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".pdf";
            dlg.Filter = "PDF Files (*.pdf)|*.pdf";
            dlg.Multiselect = true;

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
           if (result == true)
           {
                foreach (string filename in dlg.FileNames) {
                    Files.Add(new FileModel() { FileName = filename});
                }
                // Open document 
                //string filename = dlg.FileName;
                //textBox1.Text = filename;
            }
           
            //if (dlg.ShowDialog() == DialogResult.OK)
            try
            {
                //TODO wenn nichts ausgewaehlt wird gibt es eine nicht abgefangene Exception
                var sr = new StreamReader(dlg.FileName);
                //SetText(sr.ReadToEnd());
            }
            catch (SecurityException ex)
            {
                MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                $"Details:\n\n{ex.StackTrace}");
            }
        }
        public void getPdfInfo(List<FileModel> Files) {
            foreach (FileModel file in Files) {
                using (var pdf = PdfDocument.Open("@"+file.FileName))
                {
                    foreach (var page in pdf.GetPages())
                    {
                        // Either extract based on order in the underlying document with newlines and spaces.
                        var text = ContentOrderTextExtractor.GetText(page);
    
                        // Or based on grouping letters into words.
                        var otherText = string.Join(" ", page.GetWords());

                        // Or the raw text of the page's content stream.
                        var rawText = page.Text;

                        Console.WriteLine(text);
                }
            }

    }

}
