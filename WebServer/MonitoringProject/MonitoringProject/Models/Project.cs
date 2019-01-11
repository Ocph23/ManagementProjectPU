using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MonitoringProject.Models
{
   public class Project:PropertyChange
    {
        private string projectName;

        public string ProjectName
        {
            get { return projectName; }
            set { SetProperty(ref projectName ,value); }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }
        private string bidang;

        public string Bidang
        {
            get { return bidang; }
            set { SetProperty(ref bidang, value); }
        }


        private ICommand progressCommand;

        public ICommand ProgressCommand
        {
            get { return progressCommand; }
            set { SetProperty(ref progressCommand, value); }
        }

        private ICommand galeryCommand;

        public ICommand GalleryCommand
        {
            get { return galeryCommand; }
            set { SetProperty(ref galeryCommand, value); }
        }

        private ICommand locationCommand;

        public ICommand LocationCommand
        {
            get { return locationCommand; }
            set { SetProperty(ref locationCommand, value); }
        }



    }
}
