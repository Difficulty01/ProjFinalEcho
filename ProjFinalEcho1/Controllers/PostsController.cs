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
            return View(db.Posts.Where(p => p.Hidden != true).ToList());
        }

        // GET: Posts/Search/5
        public ActionResult Search(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Posts = db.Posts.Where(c => c.CategoriasId == id).Where(p => p.Hidden != true).ToList();
            ViewBag.categorias = db.Categorias.ToList();
            return View();
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posts posts = db.Posts.Find(id);
            if (posts == null)
            {
                return HttpNotFound();
            }
            ViewBag.comments = db.Comentarios.Where(c => c.PostId == id).ToList();
            return View(posts);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Titulo,Conteudo,Hidden,Deleted")] Posts posts, HttpPostedFileBase Imagem)
        {

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
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posts posts = db.Posts.Find(id);
            if (posts == null)
            {
                return HttpNotFound();
            }
            return View(posts);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Titulo,Conteudo,Hidden,Deleted")] Posts posts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(posts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(posts);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posts posts = db.Posts.Find(id);
            if (posts == null)
            {
                return HttpNotFound();
            }
            return View(posts);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Posts posts = db.Posts.Find(id);
            db.Posts.Remove(posts);
            db.SaveChanges();
            return RedirectToAction("Index");
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
