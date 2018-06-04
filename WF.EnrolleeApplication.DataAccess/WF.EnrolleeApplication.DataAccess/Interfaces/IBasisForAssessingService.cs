using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface IBasisForAssessingService
    {
        BasisForAssessing InsertBasisForAssessing(BasisForAssessing basisForAssessing);
        BasisForAssessing UpdateBasisForAssessing(BasisForAssessing basisForAssessing);
        void DeleteBasisForAssessing(BasisForAssessing basisForAssessing);
        List<BasisForAssessing> GetBasisForAssessings();
        BasisForAssessing GetBasisForAssessing(int id);
        BasisForAssessing GetBasisForAssessing(string name);
    }
}
