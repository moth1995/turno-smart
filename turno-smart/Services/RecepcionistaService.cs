using Microsoft.EntityFrameworkCore;
using turno_smart.Data;
using turno_smart.Interfaces;
using turno_smart.Models;

namespace turno_smart.Services
{
    class RecepcionistaService(ApplicationDbContext DBContext) : IRecepcionistaService
    {
        private readonly ApplicationDbContext _DBContext = DBContext;

        void IRecepcionistaService.Create(Recepcionista obj)
        {
            _DBContext.Recepcionistas.Add(obj);
            _DBContext.SaveChanges();
        }

        void IRecepcionistaService.Delete(int id)
        {
            var recepcionista = _DBContext.Recepcionistas.Find(id);
            if (recepcionista != null)
            {
                _DBContext.Remove(recepcionista);
                _DBContext.SaveChanges();
            }
        }

        List<Recepcionista> IRecepcionistaService.GetAll(string filter)
        {
            List<Recepcionista> recepcionistas = [.. (from recepcionista in _DBContext.Recepcionistas where recepcionista.Nombre.Contains(filter) select recepcionista)];
            return recepcionistas;
        }

        List<Recepcionista> IRecepcionistaService.GetAll()
        {
            List<Recepcionista> recepcionistas = [.. (from recepcionista in _DBContext.Recepcionistas select recepcionista)];
            return recepcionistas;
        }

        Recepcionista? IRecepcionistaService.GetById(int id)
        {
            var query = from recepcionista in _DBContext.Recepcionistas select recepcionista;
            return query.FirstOrDefault(m => m.Id == id);
        }

        void IRecepcionistaService.Update(Recepcionista obj)
        {
            _DBContext.Update(obj);
            _DBContext.SaveChanges();
        }
    }
}