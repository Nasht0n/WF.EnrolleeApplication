using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface IDecreeService
    {
        Decree InsertDecree(Decree decree);
        Decree UpdateDecree(Decree decree);
        void DeleteDecree(Decree decree);
        List<Decree> GetDecrees();
        Decree GetDecree(int id);
        Decree GetDecree(DateTime decreeDate);
    }
}
