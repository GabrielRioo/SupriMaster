using SupriMaster.Business.Models.Fornecedores;
using System;
using System.Threading.Tasks;
using System.Data.Entity;
using SupriMaster.Infra.Data.Content;

namespace SupriMaster.Infra.Data.Repository
{
	public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
	{
		public FornecedorRepository(SupriMasterDbContext context) : base(context) { }
		public async Task<Fornecedor> ObterFornecedorEndereco(Guid id)
		{
			return await Db.Fornecedores
				.AsNoTracking()
				.Include(f => f.Endereco)
				.FirstOrDefaultAsync(f => f.Id == id);
		}

		public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id)
		{
			return await Db.Fornecedores
				.AsNoTracking()
				.Include(f => f.Endereco)
				.Include(f => f.Produtos)
				.FirstOrDefaultAsync(f => f.Id == id);
		}

	}
}
