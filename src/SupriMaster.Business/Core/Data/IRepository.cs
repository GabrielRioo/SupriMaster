﻿using SupriMaster.Business.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupriMaster.Business.Core.Data
{
	public interface IRepository<TEntity> : IDisposable where TEntity : Entity
	{
		Task Adicionar(TEntity entity);
		Task<TEntity> ObterPorId(Guid id);
		Task<List<TEntity>> ObterTodos();
		Task Atualizar(TEntity entity);
		Task Remover(Guid id);
	}
}
