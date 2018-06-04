using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface IContestService
    {
        Contest InsertContest(Contest contest);
        Contest UpdateContest(Contest contest);
        void DeleteContest(Contest contest);
        List<Contest> GetContests();
        Contest GetContest(int id);
        Contest GetContest(string name);
    }
}
