using ProjFinalEcho1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjFinalEcho1.Models
{
    public class Posts
    {

        public Posts()
        {
           
            Votes = new HashSet<Votes>();
            Comentarios = new HashSet<Comentarios>();
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "o Titulo é de preenchimento obrigatório.")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "o Conteudo é de preenchimento obrigatório.")]
        public string Conteudo { get; set; }
        [Required(ErrorMessage = "A imagem é de preenchimento obrigatório.")]
        public string Imagem { get; set; }
        public Boolean Hidden { get; set; }

        public Boolean Deleted { get; set; }

        [ForeignKey("Utilizador")]
        public int UtilizadorId { get; set; }
        public Utilizadores Utilizador { get; set; }

        [ForeignKey("Categorias")]
        public int CategoriasId { get; set; }
        public Categorias Categorias { get; set; }
        
        //lista de votos
        public virtual ICollection<Votes> Votes { get; set; }
        //lista de comentários
        public virtual ICollection<Comentarios> Comentarios { get; set; }

    }
}