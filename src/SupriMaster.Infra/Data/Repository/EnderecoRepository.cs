using SupriMaster.Business.Models.Fornecedores;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SupriMaster.Infra.Data.Repository
{
	public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
	{
		public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId)
		{
			return await ObterPorId(fornecedorId);
		}
	}
}
