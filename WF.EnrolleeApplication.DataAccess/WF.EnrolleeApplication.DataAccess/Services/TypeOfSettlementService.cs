using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class TypeOfSettlementService : ITypeOfSettlementService
    {
        private EnrolleeContext context;
        public TypeOfSettlementService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }

        public void DeleteTypeOfSettlement(TypeOfSettlement typeOfSettlement)
        {
            TypeOfSettlement typeOfSettlementToDelete = context.TypeOfSettlement.AsNoTracking().FirstOrDefault(ts => ts.SettlementTypeId == typeOfSettlement.SettlementTypeId);
            context.TypeOfSettlement.Remove(typeOfSettlementToDelete);
            context.SaveChanges();
        }

        public TypeOfSettlement GetTypeOfSettlement(int id)
        {
            TypeOfSettlement typeOfSettlement = context.TypeOfSettlement.AsNoTracking().FirstOrDefault(ts => ts.SettlementTypeId == id);
            return typeOfSettlement;
        }

        public TypeOfSettlement GetTypeOfSettlement(string fullname)
        {
            TypeOfSettlement typeOfSettlement = context.TypeOfSettlement.AsNoTracking().FirstOrDefault(ts => ts.Fullname == fullname);
            return typeOfSettlement;
        }

        public List<TypeOfSettlement> GetTypeOfSettlements()
        {
            List<TypeOfSettlement> typeOfSettlements = context.TypeOfSettlement.AsNoTracking().ToList();
            return typeOfSettlements;
        }

        public TypeOfSettlement InsertTypeOfSettlement(TypeOfSettlement typeOfSettlement)
        {
            context.TypeOfSettlement.Add(typeOfSettlement);
            context.SaveChanges();
            return typeOfSettlement;
        }

        public TypeOfSettlement UpdateTypeOfSettlement(TypeOfSettlement typeOfSettlement)
        {
            TypeOfSettlement typeOfSettlementToUpdate = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == typeOfSettlement.SettlementTypeId);
            typeOfSettlementToUpdate.Fullname = typeOfSettlement.Fullname;
            typeOfSettlementToUpdate.Shortname = typeOfSettlement.Shortname;
            typeOfSettlementToUpdate.IsTown = typeOfSettlement.IsTown;
            context.SaveChanges();
            return typeOfSettlementToUpdate;
        }
    }
}
