using Domain.Interfaces.Generics;
using Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Generics
{
    public class GenericRepository<T> : IGenerics<T>, IDisposable where T : class
    {
        private readonly DbContextOptions<Context> _dbContextBuilder;

        public GenericRepository()
        {
            _dbContextBuilder = new DbContextOptions<Context>();
        }

        public async Task Add(T Objeto)
        {

            try
            {
                using var data = new Context(_dbContextBuilder);
                await data.Set<T>().AddAsync(Objeto);
                await data.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(paramName: nameof(ex), message: "Erro ao tentar Adicionar");
            }

        }

        public async Task Delete(T Objeto)
        {
            try
            {
                using var data = new Context(_dbContextBuilder);
                data.Set<T>().Remove(Objeto);
                await data.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(paramName: nameof(ex), message: "Erro ao tentar excluir");
            }
        }

        public async Task<List<T>> GetAll()
        {
            using (var data = new Context(_dbContextBuilder))
            {
                try
                {

                    return await data.Set<T>().AsNoTracking().ToListAsync();
                }
                catch (Exception ex)
                {
                    throw new ArgumentNullException(paramName: nameof(ex), message: "Erro ao realizar a buscar");
                }
            }

        }

        public async Task<T> GetById(int id)
        {
            using (var data = new Context(_dbContextBuilder))
            {
                try
                {
                    return await data.Set<T>().FindAsync(id);
                }
                catch (Exception ex)
                {
                    throw new ArgumentNullException(paramName: nameof(ex), message: "Erro ao realizar a buscar pelo o Id");
                }
            }
        }

        public async Task Update(T Objeto)
        {
            using (var data = new Context(_dbContextBuilder))
            {
                try
                {
                    data.Set<T>().Update(Objeto);
                    await data.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new ArgumentNullException(paramName: nameof(ex), message: "Erro ao realizar o update");
                }
            }
        }

        #region Disposed https://docs.microsoft.com/pt-br/dotnet/standard/garbage-collection/implementing-dispose
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);



        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }

        #endregion
    }
}
