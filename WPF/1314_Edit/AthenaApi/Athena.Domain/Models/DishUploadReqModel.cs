using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class DishUploadReqModel
    {
        public string ImageName { get; set; }

        public int DishSK { get; set; }
        public IFormFile Image { get; set; }
    }
}
