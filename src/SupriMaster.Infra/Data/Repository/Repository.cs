﻿using SupriMaster.Business.Core.Data;
using SupriMaster.Business.Core.Models;
using SupriMaster.Infra.Data.Content;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SupriMaster.Infra.Data.Repository
{
	public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
	{
		protected readonly SupriMasterDbContext Db;
		protected readonly DbSet<TEntity> DbSet;

		protected Repository(SupriMasterDbContext db)
		{
			Db = db;
			DbSet = Db.Set<TEntity>();
		}
		public virtual async Task<TEntity> ObterPorId(Guid id)
		{
			return await DbSet.FindAsync(id);
		}

		public virtual async Task<List<TEntity>> ObterTodos()
		{
			return await DbSet.ToListAsync();
		}

		public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
		{
			return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
		}

		public virtual async Task Adicionar(TEntity entity)
		{
			DbSet.Add(entity);
			await SaveChanges();
		}

		public virtual async Task Atualizar(TEntity entity)
		{
			Db.Entry(entity).State = EntityState.Modified;
			await SaveChanges();
		}

		public virtual async Task Remover(Guid id)
		{
			Db.Entry(new TEntity { Id = id}).State = EntityState.Deleted;
			await SaveChanges();
		}

		public async Task<int> SaveChanges()
		{
			return await Db.SaveChangesAsync();
		}

		public void Dispose()
		{
			Db?.Dispose();
		}
	}
}
