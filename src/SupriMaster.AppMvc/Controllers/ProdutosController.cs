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
    public class ProdutosController : BaseController
	{
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public ProdutosController(IProdutoRepository produtoRepository, IProdutoService produtoService, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
            _mapper = mapper;
        }

        [Route("lista-de-produtos")]
		[HttpGet]
		public async Task<ActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterTodos()));
        }

		[Route("dados-do-produto/{id:guid}")]
		[HttpGet]
		public async Task<ActionResult> Details(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

			if (produtoViewModel == null)
            {
                return HttpNotFound();
            }
            return View(produtoViewModel);
        }

		[Route("novo-produto")]
		[HttpGet]
		public ActionResult Create()
        {
            return View();
        }

		[Route("novo-produto")]
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

		[Route("editar-produto/{id:guid}")]
		[HttpGet]
		public async Task<ActionResult> Edit(Guid id)
        {
			var produtoViewModel = await ObterProduto(id);

			if (produtoViewModel == null)
            {
                return HttpNotFound();
            }
            return View(produtoViewModel);
        }

		[Route("editar-produto/{id:guid}")]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProdutoViewModel produtoViewModel)
        {
            if (ModelState.IsValid)
            {
                await _produtoService.Atualizar(_mapper.Map<Produto>(produtoViewModel));

                return RedirectToAction("Index");
            }
            return View(produtoViewModel);
        }

		[Route("excluir-produto/{id:guid}")]
		[HttpGet]
		public async Task<ActionResult> Delete(Guid id)
        {
			var produtoViewModel = await ObterProduto(id);

			if (produtoViewModel == null)
            {
                return HttpNotFound();
            }
            return View(produtoViewModel);
        }

		[Route("editar-produto/{id:guid}")]
		[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
			var produtoViewModel = await ObterProduto(id);

			if (produtoViewModel == null)
			{
				return HttpNotFound();
			}

            _produtoService.Remover(id);

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
                _produtoRepository.Dispose();
                _produtoService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
