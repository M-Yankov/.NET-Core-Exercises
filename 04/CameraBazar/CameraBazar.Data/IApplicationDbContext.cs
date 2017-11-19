namespace CameraBazar.Data
{
    using CameraBazar.Data.Models;
    using JetBrains.Annotations;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public interface IApplicationDbContext
    {
        DbSet<Camera> Cameras { get; set; }

        EntityEntry<TEntity> Entry<TEntity>( TEntity entity) where TEntity : class;
        int SaveChanges();
    }
}