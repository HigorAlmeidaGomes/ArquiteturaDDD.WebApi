using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class NewsModel
    {
        public int IdNews { get; set; }

        public string Title { get; set; }

        public string Information {  get; set; }

        public string IdUser { get; set; }
    }
}
