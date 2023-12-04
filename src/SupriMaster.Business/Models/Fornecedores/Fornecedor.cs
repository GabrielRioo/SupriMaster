using SupriMaster.Business.Core.Models;
using SupriMaster.Business.Models.Fornecedores.Validations;
using SupriMaster.Business.Models.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupriMaster.Business.Models.Fornecedores
{
	public class Fornecedor : Entity
	{
		public string Nome { get; set; }
		public string Documento { get; set; }
		public ETipoFornecedor TipoFornecedor { get; set; }
		public Endereco Endereco { get; set; }
		public bool Ativo { get; set; }

		//Entity Framework Relactions
		public ICollection<Produto> Produtos { get; set; }

		
	}
}
