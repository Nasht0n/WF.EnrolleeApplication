using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class TypeOfFinanceService : ITypeOfFinanceService
    {
        private EnrolleeContext context;
        public TypeOfFinanceService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }

        public void DeleteTypeOfFinance(TypeOfFinance typeOfFinance)
        {
            TypeOfFinance typeOfFinanceToDelete = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == typeOfFinance.FinanceTypeId);
            context.TypeOfFinance.Remove(typeOfFinanceToDelete);
            context.SaveChanges();
        }

        public TypeOfFinance GetTypeOfFinance(int id)
        {
            TypeOfFinance typeOfFinance = context.TypeOfFinance.AsNoTracking().FirstOrDefault(tf => tf.FinanceTypeId == id);
            return typeOfFinance;
        }

        public TypeOfFinance GetTypeOfFinance(string fullname)
        {
            TypeOfFinance typeOfFinance = context.TypeOfFinance.AsNoTracking().FirstOrDefault(tf => tf.Fullname == fullname);
            return typeOfFinance;
        }

        public List<TypeOfFinance> GetTypeOfFinances()
        {
            List<TypeOfFinance> typeOfFinances = context.TypeOfFinance.AsNoTracking().ToList();
            return typeOfFinances;
        }

        public TypeOfFinance InsertTypeOfFinance(TypeOfFinance typeOfFinance)
        {
            context.TypeOfFinance.Add(typeOfFinance);
            context.SaveChanges();
            return typeOfFinance;
        }

        public TypeOfFinance UpdateTypeOfFinance(TypeOfFinance typeOfFinance)
        {
            TypeOfFinance typeOfFinanceToUpdate = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == typeOfFinance.FinanceTypeId);
            typeOfFinanceToUpdate.Fullname = typeOfFinance.Fullname;
            context.SaveChanges();
            return typeOfFinanceToUpdate;
        }
    }
}
