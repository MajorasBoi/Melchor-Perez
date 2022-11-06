using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Repository
{
    public sealed partial class DBRepository : BaseRepository
    {
        /// <summary>
        /// Contexto de la base de datos.
        /// </summary>
        private RepositoryContext _context;

        /// <summary>
        /// Inicializa un objeto <see cref="DBRepository"/>.
        /// </summary>
        /// <param name="connectionString">
        /// Un objeto <see cref="ConfigurationDBConnectionString"/> conteniendo la
        /// cadena de conexión a la base de datos.
        /// </param>
        public DBRepository(string connectionString) : base(connectionString)
        {
            _context = new RepositoryContext(connectionString);

            // Verifying database created
            if (!_context.Database.GetService<IRelationalDatabaseCreator>().Exists())
            {
                // Initializing database
                _context.Database.Migrate();

                _context.Database.BeginTransaction();
                // Add data
                _context.SaveChanges();


                _context.Database.CommitTransaction();
            }
            else
            {
                // Migrating database
                _context.Database.Migrate();
            }

            _context.Dispose();
        }

        #region Transaction Management

        /// <summary>
        /// Inicia una transacción.
        /// </summary>
        public override void BeginTransaction()
        {
            if (!IsInTransaction)
            {
                _context = new RepositoryContext(ConnectionString);
                IsInTransaction = true;
            }
            DisposedValue = false;
        }

        /// <summary>
        /// Realiza una salva manteniendo la transacción abierta.
        /// </summary>
        public override void PartialCommit()
        {
            if (!IsInTransaction)
                throw new Exception("The Repository is not in a transaction");
            try
            {
                _context.SaveChanges();
            }
            catch (Exception exception)
            {
                throw new Exception(
                    "An error occurred during the Commit of the transaction.",
                    exception);
            }
        }

        /// <summary>
        /// Finaliza y da persistenca a una transacción.
        /// </summary>
        public override void CommitTransaction()
        {
            if (!IsInTransaction)
                throw new Exception("The Repository is not in a transaction");
            try
            {
                _context.SaveChanges();
                _context.Dispose();
                IsInTransaction = false;
            }
            catch (Exception exception)
            {
                _context.Dispose();
                throw new Exception(
                    "An error occurred during the Commit of the transaction.",
                    exception);
            }
        }

        /// <summary>
        /// Deshace una transacción, si esta no se ha finalizado.
        /// </summary>
        public override void RollbackTransaction()
        {
            if (!IsInTransaction)
                throw new Exception("The Repository is not in a transaction");
            IsInTransaction = false;
            _context.Dispose();
        }

        #endregion

        #region IDisposable Support

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (!DisposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                DisposedValue = true;
            }
        }

        /// <summary>
        /// This code added to correctly implement the disposable pattern.
        /// </summary>
        public override void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        public override List<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "") where TEntity : class
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
                query = query.Where(filter);

            foreach (string includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            return orderBy != null
                ? orderBy(query).ToList()
                : query.ToList();
        }

        public override async Task<List<TEntity>> GetAsync<TEntity>(CancellationTokenSource tokenSource = null, Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "") where TEntity : class
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
                query = query.Where(filter);

            foreach (string includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);
            if (tokenSource == null)
                return orderBy != null
                    ? await orderBy(query).ToListAsync()
                    : await query.ToListAsync();
            return orderBy != null
                ? await orderBy(query).ToListAsync(tokenSource.Token)
                : await query.ToListAsync(tokenSource.Token);
        }

        public override List<TResult> Get<TEntity, TResult>(Func<TEntity, TResult> selector,
            Expression<Func<TEntity, bool>> filter = null, bool ordered = false) where TEntity : class
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
                query = query.Where(filter);

            IEnumerable<TResult> result = query.Select(selector);

            return ordered
                ? result.ToList()
                : result.OrderBy(x => x).ToList();
        }

        public override async Task<List<TResult>> GetAsync<TEntity, TResult>(Func<TEntity, TResult> selector,
            Expression<Func<TEntity, bool>> filter = null, bool ordered = false, CancellationTokenSource tokenSource = null)
            where TEntity : class
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
                query = query.Where(filter);

            IQueryable<TResult> result = query.Select(selector).AsQueryable();

            return ordered
                ? await result.ToListAsync(tokenSource.Token)
                : await result.OrderBy(x => x).ToListAsync(tokenSource.Token);
        }

        

        public override void Add<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Add(entity);
        }

        public override void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public override void Delete<TEntity>(TEntity entityToDelete) where TEntity : class
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
                _context.Set<TEntity>().Attach(entityToDelete);
            _context.Set<TEntity>().Remove(entityToDelete);
        }

        public override void DeleteRange<TEntity>(IEnumerable<TEntity> entitiesToDelete)
        {
            foreach (var entityToAttach in entitiesToDelete.Where(e => _context.Entry(e).State == EntityState.Detached))
                _context.Set<TEntity>().Attach(entityToAttach);
            _context.Set<TEntity>().RemoveRange(entitiesToDelete);
        }

        public override void Update<TEntity>(TEntity entityToUpdate) where TEntity : class
        {
            _context.Set<TEntity>().Update(entityToUpdate);
        }

        public override void AttachEntity<T>(T entity) where T : class
        {
            _context.Set<T>().Attach(entity);
        }
    }
}
