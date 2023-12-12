using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SupriMaster.AppMvc.Models;
using SupriMaster.AppMvc.ViewModels;
using SupriMaster.Business.Models.Produtos;
using SupriMaster.Business.Models.Produtos.Services;
using SupriMaster.Infra.Data.Repository;
using SupriMaster.Business.Core.Notificacoes;
using AutoMapper;

namespace SupriMaster.AppMvc.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public ProdutosController()
        {
            _produtoRepository = new ProdutoRepository();
            _produtoService = new ProdutoService(_produtoRepository, new Notificador());

		}

        // GET: Produtos
        public async Task<ActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterTodos()););
        }

        // GET: Produtos/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

			if (produtoViewModel == null)
            {
                return HttpNotFound();
            }
            return View(produtoViewModel);
        }

        // GET: Produtos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Produtos/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProdutoViewModel produtoViewModel)
        {
            if (ModelState.IsValid)
            {
                await _produtoService.Adicionar(_mapper.Map<Produto>(produtoViewModel));

                return RedirectToAction("Index");
            }

            return View(produtoViewModel);
        }

        // GET: Produtos/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProdutoViewModel produtoViewModel = await db.ProdutoViewModels.FindAsync(id);
            if (produtoViewModel == null)
            {
                return HttpNotFound();
            }
            return View(produtoViewModel);
        }

        // POST: Produtos/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FornecedorId,Nome,Descricao,Imagem,Valor,DataCadastro,Ativo")] ProdutoViewModel produtoViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produtoViewModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(produtoViewModel);
        }

        // GET: Produtos/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProdutoViewModel produtoViewModel = await db.ProdutoViewModels.FindAsync(id);
            if (produtoViewModel == null)
            {
                return HttpNotFound();
            }
            return View(produtoViewModel);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            ProdutoViewModel produtoViewModel = await db.ProdutoViewModels.FindAsync(id);
            db.ProdutoViewModels.Remove(produtoViewModel);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private async Task<ProdutoViewModel> ObterProduto(Guid id)
        {
            var produto = _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoFornecedor(id));
            return produto;
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
