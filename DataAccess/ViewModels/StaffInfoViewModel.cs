using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.ViewModels
{
    public class StaffInfoViewModel
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string EPostaSirket { get; set; }
        public string YıllıkIzinHakki { get; set; }
        public List<StaffInfoViewModel> StaffList { get; set; }
        public StaffInfoViewModel()
        {
            StaffList = new List<StaffInfoViewModel>();
        }
    }
}
