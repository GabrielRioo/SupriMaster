using SupriMaster.Business.Models.Produtos;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupriMaster.Infra.Data.Mappings
{
	public class ProdutoConfig : EntityTypeConfiguration<Produto>
	{
		public ProdutoConfig()
		{
			HasKey(p => p.Id);

			Property(f => f.Nome)
				.IsRequired()
				.HasMaxLength(200);

			Property(f => f.Descricao)
				.IsRequired()
				.HasMaxLength(1000);

			Property(f => f.Imagem)
				.IsRequired()
				.HasMaxLength(100);

			HasRequired(p => p.Fornecedor)
				.WithMany(f => f.Produtos)
				.HasForeignKey(p => p.FornecedorId);

			ToTable("Produtos");
		}

	}
}
