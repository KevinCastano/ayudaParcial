using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace WebApplication2.Nucleo
{
    public class Conexion : DbContext
    {
        private int tamaño = 20; //Crea solo 20 datos en la base datos
        public string? StringConnection { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(StringConnection!, p => { });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        //Se crea un DbSet por cada entidad que se tenga
        protected DbSet<Pagos>? Pagos { get; set; }
        protected DbSet<Personas>? Personas { get; set; }

        //En conexion estan todos los metodos que permiten modificar la base de datos
        public virtual DbSet<T> ObtenerSet<T>() where T : class, new()
        {
            return Set<T>();
        }

        public virtual List<T> Listar<T>() where T : class, new()
        {
            return Set<T>().ToList();
        }

        public virtual List<T> Buscar<T>(Expression<Func<T, bool>> condiciones) where T : class, new()
        {
            return Set<T>().Where(condiciones).ToList();
        }

        public virtual bool Existe<T>(Expression<Func<T, bool>> condiciones) where T : class, new()
        {
            return Set<T>().Any(condiciones);
        }

        public virtual void Guardar<T>(T entidad) where T : class, new()
        {
            Set<T>().Add(entidad);
        }

        public virtual void Modificar<T>(T entidad) where T : class
        {
            var entry = Entry(entidad);
            entry.State = EntityState.Modified;
        }

        public virtual void Borrar<T>(T entidad) where T : class, new()
        {
            Set<T>().Remove(entidad);
        }

        public virtual void Separar<T>(T entidad) where T : class, new()
        {
            Entry(entidad).State = EntityState.Detached;
        }

        public virtual void GuardarCambios()
        {
            SaveChanges();
        }
    }
}

