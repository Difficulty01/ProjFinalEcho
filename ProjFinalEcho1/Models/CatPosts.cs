using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjFinal_alpha.Models
{
    public class CatPosts
    {
        public int ID { get; set; }
        
        [ForeignKey("Post")]
        public int PostId { get; set; }
        public Posts Post { get; set; }

        [ForeignKey("Cat")]
        public int CatId { get; set; }
        public Categorias Cat { get; set; }
    }
}