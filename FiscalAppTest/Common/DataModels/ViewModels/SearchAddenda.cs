using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.ViewModels
{
    public class SearchAddenda
    {
        public string Search { get; set; } = string.Empty;
        public int PageSize { get; set; } = 20;
    }
}
