using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class AtributeService : IAtributeService
    {
        private EnrolleeContext context;

        public AtributeService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }

        public void DeleteAtribute(Atribute atribute)
        {
            Atribute atributeToDelete = context.Atribute.AsNoTracking().FirstOrDefault(a => a.AtributeId == atribute.AtributeId);
            context.Atribute.Remove(atributeToDelete);
            context.SaveChanges();
        }

        public Atribute GetAtribute(int id)
        {
            Atribute atributeById = context.Atribute.AsNoTracking().FirstOrDefault(a => a.AtributeId == id);
            return atributeById;
        }

        public Atribute GetAtribute(string fullname)
        {
            Atribute atributeByFullname = context.Atribute.AsNoTracking().FirstOrDefault(a => a.Fullname == fullname);
            return atributeByFullname;
        }

        public List<Atribute> GetAtributes()
        {
            List<Atribute> atributes = context.Atribute.AsNoTracking().ToList();
            return atributes;
        }

        public List<Atribute> GetAtributes(bool IsDiscount)
        {
            List<Atribute> atributes = context.Atribute.AsNoTracking().Where(a => a.IsDiscount == IsDiscount).ToList();
            return atributes;
        }

        public Atribute InsertAtribute(Atribute atribute)
        {
            context.Atribute.Add(atribute);
            context.SaveChanges();
            return atribute;
        }

        public Atribute UpdateAtribute(Atribute atribute)
        {
            Atribute atributeToUpdate = context.Atribute.FirstOrDefault(a => a.AtributeId == atribute.AtributeId);
            atributeToUpdate.Fullname = atribute.Fullname;
            atributeToUpdate.Shortname = atribute.Shortname;
            atributeToUpdate.IsDiscount = atribute.IsDiscount;
            context.SaveChanges();
            return atributeToUpdate;
        }
    }
}
