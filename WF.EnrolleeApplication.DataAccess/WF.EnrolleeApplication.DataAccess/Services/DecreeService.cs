using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class DecreeService : IDecreeService
    {
        private EnrolleeContext context;

        public DecreeService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        public void DeleteDecree(Decree decree)
        {
            Decree decreeToDelete = context.Decree.AsNoTracking().FirstOrDefault(d => d.DecreeId == decree.DecreeId);
            context.Decree.Remove(decreeToDelete);
            context.SaveChanges();
        }

        public Decree GetDecree(int id)
        {
            Decree decreeById = context.Decree.AsNoTracking().FirstOrDefault(d => d.DecreeId == id);
            return decreeById;
        }

        public Decree GetDecree(DateTime decreeDate)
        {
            Decree decreeById = context.Decree.AsNoTracking().FirstOrDefault(d => d.DecreeDate == decreeDate.Date);
            return decreeById;
        }

        public List<Decree> GetDecrees()
        {
            List<Decree> decrees = context.Decree.AsNoTracking().ToList();
            return decrees;
        }

        public Decree InsertDecree(Decree decree)
        {
            context.Decree.Add(decree);
            context.SaveChanges();
            return decree;
        }

        public Decree UpdateDecree(Decree decree)
        {
            Decree decreeToUpdate = context.Decree.FirstOrDefault(d => d.DecreeId == decree.DecreeId);
            decreeToUpdate.DecreeNumber = decree.DecreeNumber;
            decreeToUpdate.DecreeDate = decree.DecreeDate;
            decreeToUpdate.Content = decree.Content;
            decreeToUpdate.ProtocolNumber = decree.ProtocolNumber;
            decreeToUpdate.ProtocolDate = decree.ProtocolDate;
            context.SaveChanges();
            return decreeToUpdate;
        }
    }
}
