using ProjFinalEcho1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjFinal_alpha.Models
{
    public class Comentarios
    {
        public int ID { get; set; }
        
        public string Coment { get; set; }

        //data

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }
        public Posts Post { get; set; }
    }
}