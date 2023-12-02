using FluentValidation;
using SupriMaster.Business.Core.Validations.Documentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupriMaster.Business.Models.Fornecedores.Validations
{
	public class FornecedorValidation : AbstractValidator<Fornecedor>
	{
		public FornecedorValidation()
		{
			RuleFor(f => f.Nome)
				.NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
				.Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

			When(f => f.TipoFornecedor == ETipoFornecedor.PessoaFisica, () =>
			{
				RuleFor(f => f.Documento.Length).Equal(CpfValidacao.TamanhoCpf)
					.WithMessage("O campo Documento precida ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}");

				RuleFor(f => CpfValidacao.Validar(f.Documento)).Equal(true)
					.WithMessage("O documento fornecido é inválido.");
			});

			When(f => f.TipoFornecedor == ETipoFornecedor.PessoaJuridica, () =>
			{
				RuleFor(f => f.Documento.Length).Equal(CpfValidacao.TamanhoCpf)
					.WithMessage("O campo Documento precida ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}");

				RuleFor(f => CpfValidacao.Validar(f.Documento)).Equal(true)
					.WithMessage("O documento fornecido é inválido.");
			});
		}
	}
}
