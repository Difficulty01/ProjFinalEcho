namespace ProjFinalEcho1.Migrations
{
    using ProjFinalEcho1.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProjFinalEcho1.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ProjFinalEcho1.Models.ApplicationDbContext context)
        {
            var categorias = new List<Categorias> {
                new Categorias {ID=1, Nome="Desporto"},
                new Categorias {ID=2, Nome="Lazer"},
                new Categorias {ID=3, Nome="Cultura"}
             };

            categorias.ForEach(aa => context.Categorias.AddOrUpdate(a => a.Nome, aa));

            context.SaveChanges();

            var utilizadores = new List<Utilizadores>
            {
                new Utilizadores{ID = 1 , Email = "admin@admin.admin"},
                new Utilizadores{ID = 2 , Email = "admin1@admin.admin"},
                new Utilizadores{ID = 3 , Email = "User@User.User"}
            };
            utilizadores.ForEach(aa => context.Utilizadores.AddOrUpdate(a => a.Email, aa));

            context.SaveChanges();
            var posts = new List<Posts> {
                new Posts {ID=1, Titulo="Post 1" , Conteudo = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris ac orci purus. Suspendisse ultricies posuere massa id ullamcorper. Vivamus vel porttitor sem, vitae facilisis felis. Curabitur dignissim ante a mi eleifend, condimentum scelerisque dui bibendum. Vestibulum fringilla ex nibh, ut vehicula urna cursus et. Interdum et malesuada fames ac ante ipsum primis in faucibus. Pellentesque nec massa ac lorem lobortis bibendum ut vel ipsum. Vestibulum fringilla ut augue eget semper. Mauris ut accumsan risus, ut posuere quam. Morbi commodo velit ac ante imperdiet bibendum. Vivamus interdum blandit nisi quis dignissim. In a velit pharetra, eleifend nibh et, molestie odio. Interdum et malesuada fames ac ante ipsum primis in faucibus. Pellentesque volutpat porttitor convallis. Nam luctus libero eget tortor interdum hendrerit. Sed bibendum augue dapibus facilisis ornare. Etiam luctus, nulla sed rutrum tristique, enim risus sodales metus, sit amet suscipit ipsum metus non augue. Nulla at mollis tellus, in gravida eros. Integer ut maximus lorem. Aliquam a consequat leo. Morbi luctus justo nulla, sit amet volutpat nisl elementum in. Ut quis sem efficitur justo venenatis pretium. Etiam nec tellus pretium, gravida lorem eget, dignissim urna. Proin tristique aliquam aliquam. Praesent vehicula velit id eros viverra, a blandit velit porta. Aenean sed nulla suscipit dui interdum commodo at at turpis. Sed vulputate pharetra tempus. Fusce massa mauris, rutrum ut pretium et, interdum id enim. Aliquam quis turpis sit amet nibh scelerisque ultricies gravida sit amet metus. Mauris leo neque, sagittis vitae nisi euismod, iaculis eleifend nisl." , Imagem="flag.png" , Hidden= false , Deleted=false , CategoriasId =1 , UtilizadorId = 1},
                new Posts {ID=2, Titulo="Post 2" , Conteudo = "Proin feugiat a libero ultricies tempor. Praesent ultrices consequat velit, rutrum pulvinar tortor maximus in. Sed accumsan pulvinar nulla a commodo. Curabitur efficitur auctor egestas. Proin vestibulum massa justo, et pulvinar nisl vestibulum et. Vestibulum posuere rutrum libero ut malesuada. Integer blandit lacus sit amet efficitur viverra. In vestibulum nec enim in euismod. Phasellus turpis tortor, elementum nec felis vel, ornare congue purus. Vestibulum porttitor a nunc at pellentesque. Maecenas consequat risus sit amet lacus rutrum hendrerit. Mauris finibus, eros et placerat feugiat, enim ligula volutpat erat, volutpat bibendum urna lacus et nulla. In tincidunt velit magna, vitae feugiat elit suscipit eget. Nullam ante lorem, porttitor vel tristique in, euismod sit amet est. Sed diam mi, egestas et posuere quis, elementum id dui. Ut eu consectetur neque." , Imagem="images.jpg" , Hidden= false , Deleted=false , CategoriasId =2 , UtilizadorId = 1},
                new Posts {ID=3, Titulo="Post 3" , Conteudo = "Nam vehicula rutrum est, sit amet bibendum velit consectetur eget. Nunc vel sodales enim. Praesent consectetur metus in mauris euismod, a efficitur risus tempor. Maecenas mollis convallis viverra. Integer at lorem sapien. Curabitur pellentesque elit sit amet lectus commodo semper. Fusce finibus gravida risus nec hendrerit. Vestibulum sodales nunc ut mi iaculis, at posuere lorem accumsan. Nulla luctus mi eu mi luctus, vel consectetur arcu sollicitudin. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque laoreet odio sit amet tristique aliquet. Vestibulum eros eros, ultrices vitae feugiat ut, semper non ex. Aenean convallis finibus mollis. Nunc ornare dignissim auctor. Cras at posuere libero." , Imagem="download.jpg" , Hidden= true , Deleted=false , CategoriasId =3 , UtilizadorId = 2}
             };

            posts.ForEach(aa => context.Posts.AddOrUpdate(a => a.Titulo, aa));

            context.SaveChanges();

            var comentarios = new List<Comentarios> {
                new Comentarios {ID=1, Coment="coment 1" ,DataDoComentario = new DateTime(2019,3,15), PostId=2, UtilizadorId=1},
                new Comentarios {ID=2, Coment="coment 2" ,DataDoComentario = new DateTime(2019,3,15), PostId=2, UtilizadorId=2},
                new Comentarios {ID=3, Coment="coment 3" ,DataDoComentario = new DateTime(2019,3,15), PostId=3, UtilizadorId=3}
            };

            comentarios.ForEach(aa => context.Comentarios.AddOrUpdate(a => a.ID, aa));

            context.SaveChanges();

            var votes = new List<Votes> {
                new Votes {ID=1,PostId=2 , UtilizadorFK=1},
                new Votes {ID=2, PostId=2, UtilizadorFK=2},
                new Votes {ID=3, PostId=3 , UtilizadorFK=2}
            };

            votes.ForEach(aa => context.Votes.AddOrUpdate(a => a.ID, aa));

            context.SaveChanges();
        }
    }
}
