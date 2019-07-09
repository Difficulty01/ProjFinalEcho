using ProjFinalEcho1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjFinal_alpha.Models
{
    public class Posts
    {

        public Posts()
        {
            CatPost = new HashSet<CatPosts>();
            Votes = new HashSet<Votes>();
            Comentarios = new HashSet<Comentarios>();
        }

        public int ID { get; set; }

        public string Titulo { get; set; }

        public string Conteudo { get; set; }

        public Boolean Hidden { get; set; }

        public Boolean Deleted { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        //lista de CatPost
        public virtual ICollection<CatPosts> CatPost { get; set; }
        //lista de votos
        public virtual ICollection<Votes> Votes { get; set; }
        //lista de comentários
        public virtual ICollection<Comentarios> Comentarios { get; set; }

    }
}