using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MonitoringPU.Models
{
    public class Project:BaseNotify
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public ObservableCollection<ItemPenilaian> ListPenilaian { get; set; }

        private ICommand progresssCommand;

        public ICommand ProgressCommand
        {
            get { return progresssCommand; }
            set { progresssCommand = value; }
        }


        private ICommand locationCommand;

        public ICommand LocationCommand
        {
            get { return locationCommand; }
            set { locationCommand = value; }
        }

        private ICommand penilaianCommand;

        public ICommand PenilaianCommand
        {
            get { return penilaianCommand; }
            set { penilaianCommand = value; }
        }


    }



}