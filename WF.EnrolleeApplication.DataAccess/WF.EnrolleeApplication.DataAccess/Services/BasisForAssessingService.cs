using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class BasisForAssessingService : IBasisForAssessingService
    {
        private EnrolleeContext context;
        public BasisForAssessingService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        public void DeleteBasisForAssessing(BasisForAssessing basisForAssessing)
        {
            BasisForAssessing basisForAssessingToDelete = context.BasisForAssessing.FirstOrDefault(b => b.BasisForAssessingId == basisForAssessing.BasisForAssessingId);
            context.BasisForAssessing.Remove(basisForAssessingToDelete);
            context.SaveChanges();
        }

        public BasisForAssessing GetBasisForAssessing(int id)
        {
            BasisForAssessing basisForAssessingById = context.BasisForAssessing.AsNoTracking().FirstOrDefault(b => b.BasisForAssessingId == id);
            return basisForAssessingById;
        }

        public BasisForAssessing GetBasisForAssessing(string name)
        {
            BasisForAssessing basisForAssessingByName = context.BasisForAssessing.AsNoTracking().FirstOrDefault(b => b.Name == name);
            return basisForAssessingByName;
        }

        public List<BasisForAssessing> GetBasisForAssessings()
        {
            List<BasisForAssessing> basisForAssessings = context.BasisForAssessing.AsNoTracking().ToList();
            return basisForAssessings;
        }

        public BasisForAssessing InsertBasisForAssessing(BasisForAssessing basisForAssessing)
        {
            context.BasisForAssessing.Add(basisForAssessing);
            context.SaveChanges();
            return basisForAssessing;
        }

        public BasisForAssessing UpdateBasisForAssessing(BasisForAssessing basisForAssessing)
        {
            BasisForAssessing basisForAssessingToUpdate = context.BasisForAssessing.FirstOrDefault(b => b.BasisForAssessingId == basisForAssessing.BasisForAssessingId);
            basisForAssessingToUpdate.Name = basisForAssessing.Name;
            context.SaveChanges();
            return basisForAssessingToUpdate;
        }
    }
}
