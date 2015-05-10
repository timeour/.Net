using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    
    public class Post
    {
        public int id { get; set; }

        [DataType(DataType.MultilineText)]
        public string text { get; set; }

        [DataType(DataType.MultilineText)]
        public string description { get; set; }
        public string name { get; set; }

        public virtual ICollection<Coment> comments { get; set; }

        public virtual ApplicationUser author { get; set; }

    }
}