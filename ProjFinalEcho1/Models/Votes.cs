using ProjFinalEcho1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjFinalEcho1.Models
{
    public class Votes
    {
        public int ID { get; set; }

        /*[ForeignKey("Utilizadores")]
        public int utilizadoresFK { get; set; }
        public Utilizadores id { get; set; }*/

        [ForeignKey("Post")]
        public int PostId { get; set; }
        public Posts Post { get; set; }
    }
}