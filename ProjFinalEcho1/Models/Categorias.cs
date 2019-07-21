using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjFinal_alpha.Models
{
    public class Categorias
    {
        public Categorias()
        {
            CatPost = new HashSet<CatPosts>();
        }
        public int ID { get; set; }

        public string Nome { get; set; }

        //lista de CatPost
        public virtual ICollection<CatPosts> CatPost { get; set; }

    }
}