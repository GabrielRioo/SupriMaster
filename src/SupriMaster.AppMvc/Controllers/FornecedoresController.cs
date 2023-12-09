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
    public class FornecedoresController : Controller
    {
        private readonly IFornecedorService _fornecedorService;

        public FornecedoresController()
        {
            _fornecedorService = new FornecedorService(new FornecedorRepository(), new EnderecoRepository());
        }

        
        public async Task<ActionResult> Index()
        {
            var fornecedor = new Fornecedor()
            {
                Nome = "",
                Documento = "11111",
                Endereco = new Endereco(),
                TipoFornecedor = ETipoFornecedor.PessoaFisica,
                Ativo = true
            };

            await _fornecedorService.Adicionar(fornecedor);

            return new EmptyResult() ;
        }
    }
}