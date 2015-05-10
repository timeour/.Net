using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Coment
    {
        public int id { get; set;}

        [DataType(DataType.MultilineText)]
        public string text { get; set;}

        public virtual ApplicationUser author { get; set; }

        public virtual Post post { get; set;}
    }
}