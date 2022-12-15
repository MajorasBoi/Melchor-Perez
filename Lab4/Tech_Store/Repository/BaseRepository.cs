using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class BaseRepository : IRepository
    {
        /// <summary>
        /// Cadena de conexión a la base de datos.
        /// </summary>
        protected readonly string ConnectionString;

        /// <summary>
        /// To detect redundant calls.
        /// </summary>
        protected bool DisposedValue = false;

        /// <summary>
        /// Inicializa un objeto <see cref="BaseRepository"/>;
        /// </summary>
        /// <param name="connectionString">
        /// Cadena de conexión a la base de datos.
        /// </param>
        protected BaseRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        /// <summary>
        /// Indicador de transacción en curso.
        /// <see langword="true"/>, si existe una transacción en curso;
        /// de lo contrario, <see langword="false"/>.
        /// </summary>
        public bool IsInTransaction { get; protected set; }

        /// <summary>
        /// Inicia una transacción.
        /// </summary>        
        public abstract void BeginTransaction();

        /// <summary>
        /// Realiza una salva manteniendo la transacción abierta.
        /// </summary>
        public abstract void PartialCommit();

        /// <summary>
        /// Finaliza y da persistencia a una transacción.
        /// </summary>
        public abstract void CommitTransaction();

        /// <summary>
        /// Deshace una transacción, si esta no se ha finalizado.
        /// </summary>
        public abstract void RollbackTransaction();

        /// <summary>
        /// Obtiene un conjunto de entidades de la base de datos
        /// teniendo en cuenta criterios para su filtrado, ordenamiento
        /// e inclusión de propiedades.
        /// </summary>
        /// <typeparam name="TEntity">
        /// Tipo de la entidad.
        /// </typeparam>
        /// <param name="filter">
        /// Criterio para el filtrado.
        /// </param>
        /// <param name="orderBy">
        /// Criterio para el ordenamiento.
        /// </param>
        /// <param name="includeProperties">
        /// Propiedades a incluir.
        /// </param>
        /// <returns>
        /// Colección de <see typeref="TEntity"/>.
        /// </returns>
        public abstract List<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "") where TEntity : class;

        /// <summary>
        /// Obtiene un conjunto de entidades de la base de datos
        /// teniendo en cuenta criterios para su filtrado, ordenamiento
        /// y selección de propiedades.
        /// </summary>
        /// <typeparam name="TEntity">
        /// Tipo de la entidad.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// Tipo de la entidad resultante.
        /// </typeparam>
        /// <param name="selector">
        /// Selector de propiedades.
        /// </param>
        /// <param name="filter">
        /// Criterio para el filtrado.
        /// </param>
        /// <param name="ordered">
        /// Indicador de ordenamiento. 
        /// <see langword="true"/> para obtener la colección ordenada;
        /// de lo contrario <see langword="false"/>.
        /// </param>
        /// <returns>
        /// Colección de <see typeref="TResult"/>.
        /// </returns>
        public abstract List<TResult> Get<TEntity, TResult>(Func<TEntity, TResult> selector,
           Expression<Func<TEntity, bool>> filter = null, bool ordered = false) where TEntity : class;

        /// <summary>
        /// Obtiene un conjunto de entidades de la base de datos
        /// teniendo en cuenta criterios para su filtrado, ordenamiento
        /// e inclusión de propiedades.
        /// </summary>
        /// <typeparam name="TEntity">
        /// Tipo de la entidad.
        /// </typeparam>
        /// <param name="tokenSource">
        /// Un objeto <see cref="CancellationTokenSource"/> para la 
        /// cancelación de la operación.
        /// </param>
        /// <param name="filter">
        /// Criterio para el filtrado.
        /// </param>
        /// <param name="orderBy">
        /// Criterio para el ordenamiento.
        /// </param>
        /// <param name="includeProperties">
        /// Propiedades a incluir.
        /// </param>
        /// <returns>
        /// Un objeto <see cref="Task{List{TEntity}}"/> conteniendo la operación asincrónica
        /// que contiene la colección de entidades.
        /// </returns>
        public abstract Task<List<TEntity>> GetAsync<TEntity>(CancellationTokenSource tokenSource = null, Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "") where TEntity : class;

        /// <summary>
        /// Obtiene un conjunto de entidades de la base de datos
        /// teniendo en cuenta criterios para su filtrado, ordenamiento
        /// y selección de propiedades.
        /// </summary>
        /// <typeparam name="TEntity">
        /// Tipo de la entidad.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// Tipo de la entidad resultante.
        /// </typeparam>
        /// <param name="selector">
        /// Selector de propiedades.
        /// </param>
        /// <param name="filter">
        /// Criterio para el filtrado.
        /// </param>
        /// <param name="ordered">
        /// Indicador de ordenamiento. 
        /// <see langword="true"/> para obtener la colección ordenada;
        /// de lo contrario <see langword="false"/>.
        /// </param>
        /// <param name="tokenSource">
        /// Un objeto <see cref="CancellationTokenSource"/> para la 
        /// cancelación de la operación.
        /// </param>
        /// <returns>
        /// Un objeto <see cref="Task{List{TResult}}"/> conteniendo la operación asincrónica
        /// que contiene la colección de entidades resultantes.
        /// </returns>
        public abstract Task<List<TResult>> GetAsync<TEntity, TResult>(Func<TEntity, TResult> selector,
           Expression<Func<TEntity, bool>> filter = null, bool ordered = false, CancellationTokenSource tokenSource = null) where TEntity : class;

        /// <summary>
        /// Adiciona una entidad a la base de datos.
        /// </summary>
        /// <typeparam name="TEntity">
        /// Tipo de la entidad.
        /// </typeparam>
        /// <param name="entity">
        /// Un objeto <see typeref="TEntity"/> con la información
        /// de la entidad.
        /// </param>
        public abstract void Add<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Adiciona un conjunto de entidades a la base de datos.
        /// </summary>
        /// <typeparam name="TEntity">
        /// Tipo de la entidad.
        /// </typeparam>
        /// <param name="entities">
        /// Colección de <see typeref="TEntity"/>.
        /// </param>
        public abstract void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        /// <summary>
        /// Elimina una entidad de la base de datos.
        /// </summary>
        /// <typeparam name="TEntity">
        /// Tipo de la entidad.
        /// </typeparam>
        /// <param name="entityToDelete">
        /// Un objeto <see typeref="TEntity"/> con la información
        /// de la entidad.
        /// </param>
        public abstract void Delete<TEntity>(TEntity entityToDelete) where TEntity : class;

        /// <summary>
        /// Elimina un conjunto de entidades de la base de datos.
        /// </summary>
        /// <typeparam name="TEntity">
        /// Tipo de la entidad.
        /// </typeparam>
        /// <param name="entitiesToDelete">
        /// Colección de <see typeref="TEntity"/>.
        /// </param>
        public abstract void DeleteRange<TEntity>(IEnumerable<TEntity> entitiesToDelete) where TEntity : class;

        /// <summary>
        /// Actualiza una entidad en la base de datos.
        /// </summary>
        /// <typeparam name="TEntity">
        /// Tipo de la entidad.
        /// </typeparam>
        /// <param name="entityToUpdate">
        /// Un objeto <see typeref="TEntity"/> con la información
        /// de la entidad.
        /// </param>
        public abstract void Update<TEntity>(TEntity entityToUpdate) where TEntity : class;

        /// <summary>
        /// Vincula una entidad a la base de datos.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de la entidad.
        /// </typeparam>
        /// <param name="entity">
        /// Un objeto <see typeref="T"/> con la información
        /// de la entidad.
        /// </param>
        public abstract void AttachEntity<T>(T entity) where T : class;

        /// <summary>
        /// This code added to correctly implement the disposable pattern.
        /// </summary>
        public abstract void Dispose();
    }
}