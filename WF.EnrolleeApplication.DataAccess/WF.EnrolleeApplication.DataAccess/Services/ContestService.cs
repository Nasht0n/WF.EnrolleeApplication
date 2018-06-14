using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class ContestService : IContestService
    {
        private EnrolleeContext context;

        public ContestService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        public void DeleteContest(Contest contest)
        {
            Contest contestToDelete = context.Contest.FirstOrDefault(c => c.ContestId == contest.ContestId);
            context.Contest.Remove(contestToDelete);
            context.SaveChanges();
        }

        public Contest GetContest(int id)
        {
            Contest contestById = context.Contest.AsNoTracking().FirstOrDefault(c => c.ContestId == id);
            return contestById;
        }

        public Contest GetContest(string name)
        {
            Contest contestByName = context.Contest.AsNoTracking().FirstOrDefault(c => c.Name == name);
            return contestByName;
        }

        public List<Contest> GetContests()
        {
            List<Contest> contests = context.Contest.AsNoTracking().ToList();
            return contests;
        }

        public Contest InsertContest(Contest contest)
        {
            context.Contest.Add(contest);
            context.SaveChanges();
            return contest;
        }

        public Contest UpdateContest(Contest contest)
        {
            Contest contestToUpdate = context.Contest.FirstOrDefault(c => c.ContestId == contest.ContestId);
            contestToUpdate.Name = contest.Name;
            context.SaveChanges();
            return contestToUpdate;
        }
    }
}
