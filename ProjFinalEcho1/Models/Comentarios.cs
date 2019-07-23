using ProjFinalEcho1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjFinalEcho1.Models
{
    public class Comentarios
    {
        public int ID { get; set; }
        
        public string Coment { get; set; }

        public DateTime DataDoComentario{ get; set; }

        /*[ForeignKey("Utilizadores")]
        public int UtilizadoresId { get; set; }
        public Utilizadores utilizadores { get; set; }*/

        [ForeignKey("Post")]
        public int PostId { get; set; }
        public Posts Post { get; set; }
    }
}