using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SupriMaster.Business.Core.Notificacoes;
using SupriMaster.Business.Models.Fornecedores;
using SupriMaster.Business.Models.Fornecedores.Services;
using SupriMaster.Business.Models.Produtos;
using SupriMaster.Business.Models.Produtos.Services;
using SupriMaster.Infra.Data.Content;
using SupriMaster.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SupriMaster.AppMvc.App_Start
{
	public class DependencyInjectionConfig
	{
		public static void RegisterDIContainer()
		{
			var container = new Container();
			container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

			InitializeContainer(container);
			
			container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
			container.Verify();

			DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
		}

		public static void InitializeContainer (Container container)
		{
			container.Register<SupriMasterDbContext>(Lifestyle.Scoped);

			container.Register<IProdutoRepository, ProdutoRepository>(Lifestyle.Scoped);
			container.Register<IProdutoService, ProdutoService>(Lifestyle.Scoped);
			container.Register<IFornecedorRepository, FornecedorRepository>(Lifestyle.Scoped);
			container.Register<IEnderecoRepository, EnderecoRepository>(Lifestyle.Scoped);
			container.Register<IFornecedorService, FornecedorService>(Lifestyle.Scoped);
			container.Register<INotificador, Notificador>(Lifestyle.Scoped);

			container.RegisterSingleton(() => AutoMapperConfig.GetMapperConfiguration().CreateMapper(container.GetInstance));

		}
	}
}