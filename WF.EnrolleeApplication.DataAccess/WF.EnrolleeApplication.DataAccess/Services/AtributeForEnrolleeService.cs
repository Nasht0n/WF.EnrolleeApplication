using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class AtributeForEnrolleeService : IAtributeForEnrolleeService
    {
        private EnrolleeContext context;

        public AtributeForEnrolleeService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        public void DeleteAtributeForEnrollee(AtributeForEnrollee atributeForEnrollee)
        {
            AtributeForEnrollee atributeForEnrolleeToDelete = context.AtributeForEnrollee.FirstOrDefault(a => a.Id == atributeForEnrollee.Id);
            context.AtributeForEnrollee.Remove(atributeForEnrolleeToDelete);
            context.SaveChanges();
        }

        public AtributeForEnrollee GetAtributeForEnrollee(int id)
        {
            AtributeForEnrollee atributeForEnrollee = context.AtributeForEnrollee.AsNoTracking().FirstOrDefault(a => a.Id == id);
            return atributeForEnrollee;
        }

        public AtributeForEnrollee GetAtributeForEnrollee(Atribute atribute, Enrollee enrollee)
        {
            AtributeForEnrollee atributeForEnrollee = context.AtributeForEnrollee.AsNoTracking().FirstOrDefault(a => a.AtributeId == atribute.AtributeId && a.EnrolleeId == enrollee.EnrolleeId);
            return atributeForEnrollee;
        }

        public List<AtributeForEnrollee> GetAtributeForEnrollees()
        {
            List<AtributeForEnrollee> atributesForEnrollee = context.AtributeForEnrollee.AsNoTracking().ToList();
            return atributesForEnrollee;
        }

        public List<AtributeForEnrollee> GetAtributeForEnrollees(Enrollee enrollee)
        {
            List<AtributeForEnrollee> atributesForEnrollee = null;
            atributesForEnrollee = context.AtributeForEnrollee.AsNoTracking().Where(a => a.EnrolleeId == enrollee.EnrolleeId).ToList();
            return atributesForEnrollee;
        }

        public List<AtributeForEnrollee> GetAtributeForEnrollees(Atribute atribute)
        {
            List<AtributeForEnrollee> atributesForEnrollee = null;
            atributesForEnrollee = context.AtributeForEnrollee.AsNoTracking().Where(a => a.AtributeId == atribute.AtributeId).ToList();
            return atributesForEnrollee;
        }

        public AtributeForEnrollee InsertAtributeForEnrollee(AtributeForEnrollee atributeForEnrollee)
        {
            context.AtributeForEnrollee.Add(atributeForEnrollee);
            context.SaveChanges();
            return atributeForEnrollee;
        }

        public AtributeForEnrollee UpdateAtributeForEnrollee(AtributeForEnrollee atributeForEnrollee)
        {
            AtributeForEnrollee atributeForEnrolleeToUpdate = context.AtributeForEnrollee.FirstOrDefault(a => a.Id == atributeForEnrollee.Id);
            atributeForEnrolleeToUpdate.AtributeId = atributeForEnrollee.AtributeId;
            atributeForEnrolleeToUpdate.EnrolleeId = atributeForEnrollee.EnrolleeId;
            return atributeForEnrolleeToUpdate;
        }
    }
}
