using SupriMaster.Business.Models.Fornecedores;
using SupriMaster.Business.Models.Produtos;
using SupriMaster.Infra.Data.Mappings;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupriMaster.Infra.Data.Content
{
	public class SupriMasterDbContext : DbContext
	{
		public SupriMasterDbContext() : base("DefaultConnection")
		{
			Configuration.ProxyCreationEnabled = false;
			Configuration.LazyLoadingEnabled = false;
		}

		public DbSet<Produto> Produtos { get; set; }
		public DbSet<Endereco> Enderecos { get; set; }
		public DbSet<Fornecedor> Fornecedores { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			//Retirar a colocação de classes no plural automaticas do Entity
			modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();

			// não deletar em castava para:
			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
			modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

			// sempre que a propriedade que estiver configurando(DbSet) for do tipo string...
			modelBuilder.Properties<string>()
				.Configure(prop => prop.HasColumnType("varchar").HasMaxLength(100));

			modelBuilder.Configurations.Add(new FornecedorConfig());
			modelBuilder.Configurations.Add(new ProdutoConfig());
			modelBuilder.Configurations.Add(new EnderecoConfig());

			base.OnModelCreating(modelBuilder);
		}
	}
}
