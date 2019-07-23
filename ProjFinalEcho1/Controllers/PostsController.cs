using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjFinalEcho1.Models;
using System.IO;

namespace ProjFinalEcho1.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ActionResult Index()
        {
            ViewBag.categorias = db.Categorias.ToList();
            if (User.IsInRole("administrador"))
                return View(db.Posts.ToList());
            else
                return View(db.Posts.Where(p => p.Hidden != true).ToList());
        }

        // GET: Posts/Votes/5
        [Authorize]
        public ActionResult Votes(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                string userName = User.Identity.Name;
                var dados = db.Utilizadores.Where(u => u.Email == userName);
                int idFinal = -1;
                foreach (var Item in dados)
                    idFinal = Item.ID;

                Votes votes = new Votes
                {
                    PostId = (int)id,
                    UtilizadorFK = idFinal
                };
                db.Votes.Add(votes);
                try
                {
                    // guardar os dados na BD
                    db.SaveChanges();
                    return RedirectToAction("Details", new { id = id });
                }
                catch (Exception)
                {
                    return RedirectToAction("Details", new { id  = id});
                }
                
            }
        }

        // GET: Posts/Votes/5
        [Authorize]
        public ActionResult VotesDelete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                var dados = db.Utilizadores.Where(u => u.Email == userName).Select(column => column.ID);
                var limitedProductQuery = dados.Take(1);
                String idfinal = limitedProductQuery.ToString();
                Votes votes = new Votes
                {
                    PostId = (int)id
                };
                var a = db.Votes.Where(c => c.UtilizadorFK == Int32.Parse(idfinal)).FirstOrDefault(v => v.PostId == id);
                if (a != null)
                {
                    db.Votes.Remove(a);
                    try
                    {
                        // guardar os dados na BD
                        db.SaveChanges();
                        return RedirectToAction("Details", new { id = id });
                    }
                    catch (Exception)
                    {
                        return RedirectToAction("Details", new { id = id });
                    }
                }
                else
                    return RedirectToAction("Details", new { id = id });
            }
        }

        // GET: Posts/Search/5
        public ActionResult Search(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            if (User.IsInRole("administrador"))
            { ViewBag.Posts = db.Posts.Where(c => c.CategoriasId == id).ToList(); }
            else
            ViewBag.Posts = db.Posts.Where(c => c.CategoriasId == id).Where(p => p.Hidden != true).ToList();
            ViewBag.categorias = db.Categorias.ToList();
            return View();
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Posts posts = db.Posts.Find(id);
            if (posts == null)
            {
                return HttpNotFound();
            }



            ViewBag.comments = db.Comentarios.Where(c => c.PostId == id).ToList();
            string a = "👍 "+ db.Votes.Where(c => c.PostId == id).ToList().Count;
            ViewBag.likes = a;
            return View(posts);
        }

        // GET: Posts/Create
        [Authorize(Roles = "administrador")]
        public ActionResult Create()
        {
            Session["IdPost"] = -1;
            Session["acao"] = "Posts/Create";
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrador")]
        public ActionResult Create([Bind(Include = "ID,Titulo,Conteudo,Hidden,Deleted")] Posts posts, HttpPostedFileBase Imagem)
        {
            //utilizador mal intencionado
            if((String)Session["acao"] != "Posts/Create")
            return RedirectToAction("Index");

            // vars auxiliares
            string caminho = "";
            bool imagemValida = false;
            //posts.id = 0;
            /// foi fornecido um ficheiro?
            if (Imagem == null)
            {
                // a foto não existe
                // vou atribuir uma fotografia por defeito
                posts.Imagem = "nouser.jpg";
            }
            else
            {
                // existe ficheiro
                /// é uma imagem (fotografia)?
                // aceitamos JPEG e PNG
                if (Imagem.ContentType == "image/jpeg" ||
                   Imagem.ContentType == "image/png")
                {
                    // estamos perante uma Foto válida
                    /// se é fotografia, 
                    ///     guardar a imagem e 
                    ///       - definir um nome
                    Guid g;
                    g = Guid.NewGuid();
                    string extensaoDoFicheiro = Path.
                                                GetExtension(Imagem.FileName).
                                                ToLower();
                    string nomeFicheiro = g.ToString() + extensaoDoFicheiro;

                    ///       - definir um local onde a guardar
                    caminho = Path.Combine(Server.MapPath("~/Imagens/"), nomeFicheiro);

                    ///     associar ao agente
                    posts.Imagem = nomeFicheiro;

                    // marca o ficheiro como válido
                    imagemValida = true;
                }
                else
                {
                    /// se não é um ficheiro do tipo imagem (JPEG ou PNG), 
                    ///     atribuir ao agente uma 'imagem por defeito'
                    posts.Imagem = "nouser.jpg";
                }
            }

            // avalia se os dados fornecidos estão de acordo com o modelo
            if (ModelState.IsValid)
            {
                // adicionar os dados do novo Agente ao Modelo
                db.Posts.Add(posts);
                try
                {
                    // guardar os dados na BD
                    db.SaveChanges();
                    // guardar a imagem no disco rígido do servidor
                    if (imagemValida) Imagem.SaveAs(caminho);
                    // redirecionar o utilizador para a página de INDEX
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro desconhecido. " +
                                                 "Pedimos deculpa pela ocorrência.");
                }
            }
            // se cheguei aqui é pq alguma coisa correu mal...
            return View(posts);
        }



        /* if (ModelState.IsValid)
         {
             db.Posts.Add(posts);
             db.SaveChanges();
             return RedirectToAction("Index");
         }

         return View(posts);*/
        //}

        // GET: Posts/Edit/5
        [Authorize(Roles = "administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Posts posts = db.Posts.Find(id);
            if (posts == null)
            {
                return RedirectToAction("Index");
            }
            Session["IdPost"] = id;
            Session["acao"] = "Posts/Edit";
            return View(posts);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrador")]
        public ActionResult Edit([Bind(Include = "ID,CategoriasId,Imagem,Titulo,Conteudo,Hidden,Deleted")] Posts posts)
        {
            //uttilizador mal intencionado
            if((int)Session["IdPost"] != posts.ID || (String)Session["acao"] != "Posts/Edit")
                return RedirectToAction("Index");
            if (ModelState.IsValid)
            {
                string userName = User.Identity.Name;
                var dados = db.Utilizadores.Where(u => u.Email == userName);
                int idFinal = -1;
                foreach (var Item in dados)
                    idFinal = Item.ID;
                posts.UtilizadorId = idFinal;
                db.Entry(posts).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(posts);
        }

        // GET: Posts/Delete/5
        [Authorize(Roles = "administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Posts posts = db.Posts.Find(id);
            if (posts == null)
            {
                return RedirectToAction("Index");
            }
            Session["IdPost"] = id;
            Session["acao"] = "Posts/Delete";
            return View(posts);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrador")]
        public ActionResult DeleteConfirmed(int id)
        {
            if ((int)Session["IdPost"] != id || (String)Session["acao"] != "Posts/Delete")
                return RedirectToAction("Index");
            Posts posts = db.Posts.Find(id);
            db.Posts.Remove(posts);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Categorias/Create
        public ActionResult NewComentario(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Session["IdPost"] = id;
            Session["acao"] = "Posts/Create";
            return View();
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewComentario([Bind(Include = "ID,PostId")] Comentarios comentarios)
        {
            if ((String)Session["acao"] != "Posts/Create" || (int)Session["IdPost"] == comentarios.PostId)
                return RedirectToAction("Index");
            DateTime value = new DateTime();
            comentarios.DataDoComentario = value;
            string userName = User.Identity.Name;
            var dados = db.Utilizadores.Where(u => u.Email == userName);
            int idFinal = -1;
            foreach (var Item in dados)
                idFinal = Item.ID;
            comentarios.UtilizadorId = idFinal;
            if (ModelState.IsValid)
            {
                db.Comentarios.Add(comentarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Details", new { id = id });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
