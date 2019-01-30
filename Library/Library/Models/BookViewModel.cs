using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class BookViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string AuthorName { get; set; }
        public bool IsActive { get; set; }
        public string RenterName { get; set; }

        public string Time { get; set; }
        public string Location { get; set; }
    }
}