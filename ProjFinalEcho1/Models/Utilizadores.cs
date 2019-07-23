using ProjFinalEcho1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjFinalEcho1.Models
{
    public class Utilizadores
    {
        
            public Utilizadores()
            {
                //Posts = new HashSet<Posts>();
                //Votes = new HashSet<Votes>();
                Comentarios = new HashSet<Comentarios>();
            }

            public int ID { get; set; }

            public string Email { get; set; }

            //lista de CatPost
            //public virtual ICollection<Posts> Posts { get; set; }
            //lista de votos
            //public virtual ICollection<Votes> Votes { get; set; }
            //lista de comentários
            public virtual ICollection<Comentarios> Comentarios { get; set; }
        }
    }