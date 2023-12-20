using AutoMapper;
using SupriMaster.AppMvc.ViewModels;
using SupriMaster.Business.Core.Notificacoes;
using SupriMaster.Business.Models.Fornecedores;
using SupriMaster.Business.Models.Fornecedores.Services;
using SupriMaster.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SupriMaster.AppMvc.Controllers
{
	public class FornecedoresController : BaseController
	{
		private readonly IFornecedorRepository _fornecedorRepository;
		private readonly IFornecedorService _fornecedorService;
		private readonly IMapper _mapper;

		public FornecedoresController(IFornecedorRepository fornecedorRepository, IMapper mapper, IFornecedorService fornecedorService, INotificador notificador) : base(notificador)
		{
			_fornecedorRepository = fornecedorRepository;
			_mapper = mapper;
			_fornecedorService = fornecedorService;
		}

		[Route("lista-de-fornecedores")]
		public async Task<ActionResult> Index()
		{
			return View(_mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos()));
		}

		[Route("dados-do-fornecedor/{id:guid}")]
		public async Task<ActionResult> Details(Guid id)
		{
			var fornecedorViewModel = await ObterFornecedorEndereco(id);

			if (fornecedorViewModel == null)
				return HttpNotFound();

			return View(fornecedorViewModel);
		}

		[Route("novo-fornecedor")]
		public ActionResult Create()
		{
			return View();
		}

		[Route("novo-fornecedor")]
		[HttpPost]
		public async Task<ActionResult> Create(FornecedorViewModel fornecedorViewModel)
		{
			if (!ModelState.IsValid)
				return View(fornecedorViewModel);

			var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
			await _fornecedorService.Adicionar(fornecedor);

			return RedirectToAction("Index");
		}

		[Route("editar-fornecedor/{id:guid}")]
		public async Task<ActionResult> Edit(Guid id)
		{
			var fornecedorViewModel = await ObterFornecedorProdutosEndereco(id);

			if (fornecedorViewModel == null)
				return HttpNotFound();

			return View(fornecedorViewModel);
		}

		[Route("editar-fornecedor/{id:guid}")]
		[HttpPost]
		public async Task<ActionResult> Edit(Guid id, FornecedorViewModel fornecedorViewModel)
		{
			if (id != fornecedorViewModel.Id)
				return HttpNotFound();

			if (!ModelState.IsValid)
				return View(fornecedorViewModel);

			var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
			await _fornecedorService.Atualizar(fornecedor);

			// Se nao der certo...

			return RedirectToAction("Index");
		}

		[Route("excluir-fornecedor/{id:guid}")]
		public async Task<ActionResult> Delete(Guid id)
		{
			var fornecedorViewModel = await ObterFornecedorEndereco(id);

			if (fornecedorViewModel == null)
				return HttpNotFound();

			return View(fornecedorViewModel);
		}

		[Route("excluir-fornecedor/{id:guid}")]
		[HttpPost, ActionName("Delete")]
		public async Task<ActionResult> DeleteConfirmed(Guid id)
		{
			var fornecedorViewModel = await ObterFornecedorEndereco(id);

			if (fornecedorViewModel == null)
				return HttpNotFound();

			await _fornecedorService.Remover(id);

			return RedirectToAction("Index");
		}

		[Route("obter-endereco-fornecedor/{id:guid}")]
		[HttpPost]
		public async Task<ActionResult> ObterEndereco(Guid id)
		{
			var fornecedorViewModel = await ObterFornecedorEndereco(id);

			if (fornecedorViewModel == null)
				return HttpNotFound();

			return PartialView("_DetalhesEndereco", fornecedorViewModel);
		}

		[Route("atualizar-endereco-fornecedor/{id:guid}")]
		[HttpGet]
		public async Task<ActionResult> AtualizarFornecedor(Guid id)
		{
			var fornecedorViewModel = await ObterFornecedorEndereco(id);

			if (fornecedorViewModel == null)
				return HttpNotFound();

			return PartialView("_AtualizarEndereco", new FornecedorViewModel { Endereco = fornecedorViewModel.Endereco });
		}

		[Route("atualizar-endereco-fornecedor/{id:guid}")]
		[HttpPost]
		public async Task<ActionResult> AtualizarFornecedor(FornecedorViewModel fornecedorViewModel)
		{
			ModelState.Remove("Nome");
			ModelState.Remove("Documento");

			if (!ModelState.IsValid)
				return PartialView("_AtualizarEndereco", fornecedorViewModel);

			await _fornecedorService.AtualizarEndereco(_mapper.Map<Endereco>(fornecedorViewModel.Endereco));

			var url = Url.Action("ObterEndereco", "Fornecedores", new { id = fornecedorViewModel.Endereco.Fornecedor });

			return Json(new { success = true, url });
		}

		private async Task<FornecedorViewModel> ObterFornecedorEndereco(Guid id)
		{
			return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorEndereco(id));
		}

		private async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(Guid id)
		{
			return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));
		}


	}
}