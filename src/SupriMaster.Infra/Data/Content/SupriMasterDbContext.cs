using SupriMaster.Business.Models.Fornecedores;
using SupriMaster.Business.Models.Produtos;
using SupriMaster.Infra.Data.Mappings;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupriMaster.Infra.Data.Content
{
	public class SupriMasterDbContext : DbContext
	{
		public SupriMasterDbContext() : base("DefaultConnection")
		{

		}

		public DbSet<Produto> Produtos { get; set; }
		public DbSet<Endereco> Enderecos { get; set; }
		public DbSet<Fornecedor> Fornecedores { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new FornecedorConfig());
			modelBuilder.Configurations.Add(new ProdutoConfig());
			modelBuilder.Configurations.Add(new EnderecoConfig());
		}
	}
}
