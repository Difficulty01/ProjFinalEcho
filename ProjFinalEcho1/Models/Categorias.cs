using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjFinalEcho1.Models
{
    public class Categorias
    {
        public Categorias()
        {
            Post = new HashSet<Posts>();
        }
        public int ID { get; set; }

        public string Nome { get; set; }

        //public string Descricao { get; set; }

        //lista de CatPost
        public virtual ICollection<Posts> Post { get; set; }

    }
}