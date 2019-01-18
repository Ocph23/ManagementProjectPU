using System;
using System.Collections.Generic;
using System.Text;

namespace MonitoringPU.Models
{
   public class ItemPenilaian  :BaseNotify
    {
        private int id;

        public int Id
        {
            get { return id; }
            set {SetProperty(ref  id ,value); }
        }

        private string nama;

        public string Nama
        {
            get { return nama; }
            set { SetProperty(ref nama, value); }
        }

        private string deskripsi;

        public string Description
        {
            get { return deskripsi; }
            set { SetProperty(ref deskripsi, value); }
        }


        private double target;
        private double capaian;
        private double _value;


        public double Target
        {
            get { return target; }
            set { SetProperty(ref target, value); }
        }

        private double minimum;

        public double Minimum
        {
            get { return minimum; }
            set { SetProperty(ref minimum, value); }
        }

        public double Capaian
        {
            get { return capaian; }
            set { SetProperty(ref capaian, value); }
        }


        public double ValueView
        {
            get
            {
                return (capaian / target);
            }
            set
            {
                SetProperty(ref _value, value);
            }
        }


    }
}
