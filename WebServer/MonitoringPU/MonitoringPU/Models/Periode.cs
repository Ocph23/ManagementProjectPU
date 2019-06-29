using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonitoringPU.Models
{
    public class Periode:BaseNotify
    {
        public int PeriodeId { get; set; }
        public List<itempenilaian> Data { get; set; }

        private double _progress;
        private double _progressView;

        public double Progress
        {
            get { return Data!=null && Data.Count>0?Data.Sum(O=>O.Progress)/100:0; }
            set {SetProperty(ref _progress , value); }
        }

        public double ProgressView
        {
            get { return Data != null && Data.Count > 0 ? Data.Sum(O => O.Progress): 0; }
            set { SetProperty(ref _progressView, value); }
        }

        internal void InstanceEventChangeValue()
        {
            foreach(var item in Data)
            {
                item.OnChangeNilai += Item_OnChangeNilai;
            }
        }

        private void Item_OnChangeNilai(double val)
        {
            Progress = val;
            ProgressView = val;
        }

       
    }
}