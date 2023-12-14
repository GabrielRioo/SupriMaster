using SupriMaster.Business.Models.Produtos;
using SupriMaster.Infra.Data.Content;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SupriMaster.Infra.Data.Repository
{
	public class ProdutoRepository : Repository<Produto>, IProdutoRepository
	{
		public ProdutoRepository(SupriMasterDbContext context) : base(context) { }

		public async Task<Produto> ObterProdutoFornecedor(Guid id)
		{
			return await Db.Produtos.AsNoTracking()
				.Include(f => f.Fornecedor)
				.FirstOrDefaultAsync(p => p.Id == id);
		}

		public async Task<IEnumerable<Produto>> ObterProdutosFornecedor()
		{
			return await Db.Produtos.AsNoTracking()
				.Include(f => f.Fornecedor)
				.OrderBy(p => p.Nome)
				.ToListAsync();
		}

		public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId)
		{
			return await Buscar(p => p.FornecedorId == fornecedorId);
		}
	}
}
