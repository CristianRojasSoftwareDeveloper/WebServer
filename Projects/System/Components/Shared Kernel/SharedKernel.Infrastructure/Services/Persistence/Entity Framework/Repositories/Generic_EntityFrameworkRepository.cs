using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using SharedKernel.Domain.Models.Abstractions;
using SharedKernel.Domain.Models.Abstractions.Interfaces;
using SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts;
using System.Linq.Expressions;

namespace SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Repositories {

    /// <summary>
    /// Implementación abstracta de un repositorio genérico utilizando Entity Framework Core.
    /// Proporciona operaciones CRUD básicas y manejo optimizado del Change Tracking.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad que maneja el repositorio. Debe ser una clase que implemente IGenericEntity.</typeparam>
    /// <remarks>
    /// Esta implementación está optimizada para:
    /// - Usar eficientemente el Change Tracking de EF Core
    /// - Separar consultas de lectura y escritura para mejor rendimiento
    /// - Manejar actualizaciones parciales y completas de entidades
    /// - Reducir consultas innecesarias a la base de datos
    /// </remarks>
    public abstract class Generic_EntityFrameworkRepository<EntityType> : IGenericRepository<EntityType> where EntityType : class, IGenericEntity {

        /// <summary>
        /// Instancia del contexto de base de datos de Entity Framework.
        /// Proporciona acceso a la base de datos y gestiona el Change Tracking.
        /// </summary>
        protected ApplicationDbContext DbContext { get; }

        /// <summary>
        /// DbSet que representa la colección de entidades en la base de datos.
        /// Proporciona operaciones específicas de Entity Framework para el tipo de entidad.
        /// </summary>
        protected DbSet<EntityType> EntityRepository { get; }

        /// <summary>
        /// Obtiene un IQueryable optimizado para consultas de solo lectura.
        /// </summary>
        /// <remarks>
        /// Utiliza AsNoTracking() para mejorar el rendimiento cuando no se necesita Change Tracking.
        /// Use esta propiedad para consultas que no necesitan modificar los datos, como obtener listados o buscar entidades solo para mostrar información.
        /// </remarks>
        protected IQueryable<EntityType> ReadOnlyQueryable => EntityRepository.AsNoTracking();

        /// <summary>
        /// Obtiene un IQueryable que mantiene el Change Tracking activo.
        /// </summary>
        /// <remarks>
        /// Útil para operaciones que necesitan modificar las entidades.
        /// Use esta propiedad para consultas que preceden a operaciones de modificación, como actualizaciones o eliminaciones de entidades.
        /// </remarks>
        protected IQueryable<EntityType> TrackingQueryable => EntityRepository;

        /// <summary>
        /// Obtiene un IQueryable según la configuración de tracking especificada.
        /// </summary>
        /// <param name="enableTracking">
        ///     Si es true, habilita el tracking de Entity Framework.
        ///     Si es false, lo deshabilita para mejor rendimiento en consultas de solo lectura.
        /// </param>
        /// <returns>IQueryable con o sin tracking según el parámetro especificado.</returns>
        protected IQueryable<EntityType> GetQueryable (bool enableTracking = true) => enableTracking ? TrackingQueryable : ReadOnlyQueryable;

        /// <summary>
        /// Inicializa una nueva instancia del repositorio genérico.
        /// </summary>
        /// <param name="dbContext">El contexto de base de datos de Entity Framework.</param>
        /// <exception cref="ArgumentNullException">Se lanza si dbContext es null.</exception>
        protected Generic_EntityFrameworkRepository (ApplicationDbContext dbContext) {
            // Validación del contexto de base de datos
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            // Inicialización del DbSet para el tipo de entidad
            EntityRepository = DbContext.Set<EntityType>();
        }

        #region Operaciones asíncronas

        /// <summary>
        /// Guarda de manera asíncrona todos los cambios pendientes en el ChangeTracker del contexto.
        /// </summary>
        /// <remarks>
        /// Este método es responsable de persistir en la base de datos todos los cambios trackeados por Entity Framework, incluyendo:
        /// - Entidades nuevas marcadas para inserción (Added)
        /// - Entidades existentes marcadas para actualización (Modified)
        /// - Entidades marcadas para eliminación (Deleted)
        /// 
        /// El método:
        /// 1. Ejecuta SaveChangesAsync() del DbContext
        /// 2. Aplica todos los cambios en una única transacción
        /// 3. Mantiene la consistencia de la base de datos
        /// 4. Retorna el número de entidades afectadas
        /// 
        /// Es importante notar que:
        /// - Solo se guardan los cambios de entidades que están siendo trackeadas
        /// - Si ocurre un error durante el guardado, ningún cambio se aplica (comportamiento transaccional)
        /// - El número retornado incluye la suma de todas las operaciones (inserciones + actualizaciones + eliminaciones)
        /// </remarks>
        /// <returns>
        /// El número total de entidades afectadas en la base de datos. 
        /// Este número representa la suma de todas las inserciones, actualizaciones y eliminaciones realizadas.
        /// </returns>
        /// <exception cref="DbUpdateException">Se lanza cuando ocurre un error al guardar los cambios en la base de datos.</exception>
        /// <exception cref="DbUpdateConcurrencyException">Se lanza cuando ocurre un error de concurrencia al guardar los cambios.</exception>
        protected Task<int> SaveChanges () => DbContext.SaveChangesAsync();

        /// <summary>
        /// Agrega de manera asíncrona una nueva entidad al repositorio.
        /// </summary>
        /// <remarks>
        /// Este método realiza las siguientes validaciones y operaciones:
        /// 
        /// 1. Verifica que la entidad proporcionada no sea null.
        /// 2. Valida que el ID de la entidad no esté asignado, permitiendo la generación automática por la base de datos.
        /// 3. Establece las fechas de creación y/o actualización si se solicita y las propiedades existen en la entidad.
        /// 4. Agrega la entidad al contexto de Entity Framework, marcandola para su inserción en la base de datos.
        /// 5. Persiste los cambios en la base de datos de forma asíncrona.
        /// 
        /// </remarks>
        /// <param name="newEntity">La entidad a agregar. No puede ser null.</param>
        /// <returns>La entidad agregada, incluyendo el ID generado por la base de datos, y trackeada por Entity Framework.</returns>
        /// <exception cref="ArgumentNullException">Se lanza si <paramref name="newEntity"/> es null.</exception>
        /// <exception cref="InvalidOperationException">Se lanza si el ID de la entidad ya está asignado, indicando que debe usarse UpdateEntity en su lugar.</exception>
        public async Task<EntityType> AddEntity (EntityType newEntity) {
            // Validación de la entidad: se asegura que la entidad no sea nula.
            ArgumentNullException.ThrowIfNull(newEntity);

            // Valida que el ID de la entidad no esté asignado.
            if (newEntity.ID is not null)
                throw new InvalidOperationException(
                    $"Ha ocurrido un error al intentar agregar la entidad de tipo «{typeof(EntityType).Name}»: La propiedad «ID» ya tiene un valor asignado ({newEntity.ID}). " +
                    "Para modificar una entidad existente, utilice el método «UpdateEntity». " +
                    "Si desea crear una nueva entidad, asegúrese de que la propiedad «ID» de la entidad tenga el valor por defecto para permitir que la base de datos lo genere y asigne automáticamente."
                );

            // Agrega la entidad al contexto de Entity Framework.
            var entityEntry = await EntityRepository.AddAsync(newEntity);

            // Guarda los cambios en la base de datos de forma asíncrona.
            await SaveChanges();

            // Retorna la entidad agregada, con el ID generado y trackeada por EntityFramework.
            return entityEntry.Entity;
        }

        /// <summary>
        /// Obtiene de manera asíncrona todas las entidades del repositorio.
        /// </summary>
        /// <param name="enableTracking">Si es true, habilita el tracking de Entity Framework para las entidades retornadas.
        /// Si es false (por defecto), deshabilita el tracking para mejor rendimiento en consultas de solo lectura.</param>
        /// <returns>Lista de todas las entidades en el repositorio.</returns>
        public Task<List<EntityType>> GetEntities (bool enableTracking = false) => GetQueryable(enableTracking).ToListAsync();

        /// <summary>
        /// Obtiene de manera asíncrona una entidad por su ID utilizando FindAsync de Entity Framework.
        /// </summary>
        /// <remarks>
        /// Este método tiene un comportamiento especial respecto al tracking de Entity Framework:
        /// 
        /// 1. Primero busca la entidad en el ChangeTracker del contexto actual. Si la encuentra,
        ///    retorna esa instancia ya trackeada sin realizar una consulta a la base de datos.
        /// 
        /// 2. Si la entidad no está en el ChangeTracker, realiza una consulta optimizada a la base de datos
        ///    usando el índice de la clave primaria.
        /// 
        /// 3. Si encuentra la entidad en la base de datos, la retorna y SIEMPRE será trackeada por Entity Framework,
        ///    a diferencia de FirstOrDefault() y GetEntities() que pueden configurar el tracking mediante un parámetro.
        /// 
        /// Este comportamiento hace que GetEntityByID sea ideal para:
        /// - Operaciones que necesitarán modificar la entidad posteriormente
        /// - Escenarios donde es probable que la entidad ya esté siendo trackeada
        /// - Consultas por ID que aprovechan el índice de la clave primaria
        /// </remarks>
        /// <param name="entityID">El ID de la entidad a buscar.</param>
        /// <returns>La entidad encontrada y trackeada, o null si no existe.</returns>
        public Task<EntityType?> GetEntityByID (int entityID, bool enableTracking = false) =>
            // Si no se requiere tracking, usar SingleOrDefaultAsync con AsNoTracking
            enableTracking ? EntityRepository.FindAsync(entityID).AsTask() : ReadOnlyQueryable.SingleOrDefaultAsync(e => e.ID == entityID);

        /// <summary>
        /// Obtiene la primera entidad que cumple con el predicado especificado.
        /// </summary>
        /// <param name="predicate">Expresión que define las condiciones que debe cumplir la entidad.</param>
        /// <param name="enableTracking">Si es true, habilita el tracking de Entity Framework para la entidad retornada.
        /// Si es false (por defecto), deshabilita el tracking para mejor rendimiento en consultas de solo lectura.</param>
        /// <returns>La primera entidad que cumple con el predicado, o null si no existe.</returns>
        public Task<EntityType?> FirstOrDefault (Expression<Func<EntityType, bool>> predicate, bool enableTracking = false) =>
            GetQueryable(enableTracking).FirstOrDefaultAsync(predicate);

        /// <summary>
        /// Actualiza una entidad en la base de datos, soportando tanto actualizaciones completas (PUT)
        /// como parciales (PATCH). Este método optimiza el rendimiento al utilizar el ChangeTracker de 
        /// Entity Framework para minimizar las consultas y asegurar que solo las propiedades modificadas
        /// se reflejen en la base de datos.
        /// </summary>
        /// <typeparam name="EntityType">
        /// El tipo de la entidad que implementa <see cref="IGenericEntity"/>.
        /// </typeparam>
        /// <param name="entityUpdate">
        /// Una instancia de <see cref="Partial{EntityType}"/> que contiene el ID de la entidad a actualizar y 
        /// las propiedades con sus nuevos valores.
        /// </param>
        /// <returns>
        /// La entidad actualizada, incluida en el contexto del ChangeTracker de Entity Framework.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se lanza si <paramref name="entityUpdate"/> es null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Se lanza si el ID de la entidad no está definido o si no se encuentra la entidad correspondiente
        /// en la base de datos.
        /// </exception>
        public async Task<EntityType> UpdateEntity (Partial<EntityType> entityUpdate) {

            // Valida que la instancia proporcionada no sea nula.
            ArgumentNullException.ThrowIfNull(entityUpdate);
            // Verifica que el ID de la entidad a actualizar sea válido.
            // Si es null o el valor por defecto (por ejemplo, 0 en caso de enteros), lanza una excepción.
            if (!entityUpdate.ID.HasValue || entityUpdate.ID.Value == 0)
                throw new InvalidOperationException($"El valor de la propiedad {nameof(entityUpdate.ID)} no puede ser null ni el valor por defecto.");

            // PASO 1: Intentar obtener la entidad existente.
            // Busca primero en el ChangeTracker para evitar consultas innecesarias a la base de datos.
            // Si la entidad no está siendo trackeada, consulta directamente la base de datos.
            var existingEntity = await EntityRepository.FindAsync(entityUpdate.ID) ??
                throw new InvalidOperationException($"No se encontró la entidad {typeof(EntityType).Name} con ID {entityUpdate.ID}");

            // PASO 2: Obtiene la entrada (EntityEntry) del ChangeTracker para interactuar con los estados de las propiedades.
            var entry = DbContext.Entry(existingEntity);

            // PASO 3: Iterar sobre las propiedades proporcionadas en el objeto parcial.
            // Este bucle recorre las propiedades especificadas en «entityUpdate.Properties».
            foreach (var (propertyName, updatedValue) in entityUpdate.Properties) {
                // Obtiene la propiedad de la entidad existente por su nombre.
                var propertyInfo = entityUpdate.GetPropertyInfoByName(propertyName);
                // Obtiene el valor actual de la propiedad en la entidad existente.
                var currentValue = propertyInfo.GetValue(existingEntity);
                // Compara el valor actual con el valor actualizado.
                // Usa «Equals» para manejar comparaciones seguras, incluidas comparaciones con valores nulos.
                if (!Equals(currentValue, updatedValue)) {
                    // Si los valores son diferentes, actualiza la propiedad en la entidad existente.
                    propertyInfo.SetValue(existingEntity, updatedValue);
                    // Informa explícitamente al ChangeTracker que esta propiedad fue modificada.
                    // Esto asegura que Entity Framework incluya esta propiedad en el comando SQL generado.
                    entry.Property(propertyName).IsModified = true;
                }
            }

            // PASO 4: Guardar los cambios en la base de datos.
            // Solo las propiedades marcadas como modificadas se incluirán en la instrucción SQL generada.
            await SaveChanges();

            // Devuelve la entidad actualizada, que ahora está siendo trackeada por el contexto de EF.
            return existingEntity;

        }

        /// <summary>
        /// Elimina de manera asíncrona una entidad del repositorio por su ID.
        /// </summary>
        /// <remarks>
        /// Este método sigue un proceso optimizado para la eliminación de entidades:
        /// 
        /// 1. Utiliza GetEntityByID() para buscar la entidad, el cual:
        ///    - Primero busca en el ChangeTracker del contexto
        ///    - Si no está en memoria, realiza una consulta optimizada por ID a la base de datos
        ///    - La entidad retornada siempre estará trackeada por Entity Framework
        /// 
        /// 2. Si la entidad no existe, lanza NotFoundError
        /// 
        /// 3. Marca la entidad para eliminación en el ChangeTracker
        /// 
        /// 4. Guarda los cambios en la base de datos
        /// 
        /// Este enfoque asegura que:
        /// - Se mantiene la consistencia al verificar la existencia de la entidad
        /// - Se optimiza el rendimiento al usar el ChangeTracker cuando es posible
        /// - Se garantiza que la entidad está trackeada antes de la eliminación
        /// </remarks>
        /// <param name="entityID">El ID de la entidad a eliminar.</param>
        /// <returns>True si la entidad fue eliminada exitosamente, False en caso contrario.</returns>
        /// <exception cref="NotFoundError">Se lanza cuando la entidad con el ID especificado no existe.</exception>
        public async Task<bool> DeleteEntityByID (int entityID) {
            // Buscamos la entidad usando GetEntityByID que optimiza el uso del ChangeTracker
            // y siempre retorna una entidad trackeada si existe
            var entityToDelete = await GetEntityByID(entityID, true) ?? throw NotFoundError.Create(typeof(EntityType).Name, entityID);

            // Marcamos la entidad para ser eliminada en el ChangeTracker
            EntityRepository.Remove(entityToDelete);

            // Guardamos los cambios y retornamos true si se afectó al menos una fila
            return await SaveChanges() > 0;
        }

        #endregion

    }

}