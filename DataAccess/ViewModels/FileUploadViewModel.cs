using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.ViewModels
{
    public class FileUploadViewModel
    {
        public IFormFile XlsFile { get; set; }
        public StaffInfoViewModel StaffInfoViewModel { get; set; }
        public FileUploadViewModel()
        {
            StaffInfoViewModel = new StaffInfoViewModel();
        }
    }
}
