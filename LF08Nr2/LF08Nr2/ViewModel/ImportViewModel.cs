using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using LF08Nr2.ViewModel.Base;

namespace LF08Nr2.ViewModel
{
    public class ImportViewModel : ViewModelBase
    {
        private string files ="test";

        // CollectionView für das DataGrid
        public string Files
        {
            get => files;
            set
            {
                files= value;
                OnPropertyChanged();
            }
        }

        public int counter = 0;

        public void BindingExampel()
        {
            counter++;
            Files = $"newString {counter}";
            
        }

        public void AddDataFromExplorer()
        {
        }

    }
}
