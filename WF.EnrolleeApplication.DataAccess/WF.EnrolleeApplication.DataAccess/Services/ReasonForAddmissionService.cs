using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class ReasonForAddmissionService : IReasonForAddmissionService
    {
        private EnrolleeContext context;
        public ReasonForAddmissionService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }

        public void DeleteReasonForAddmission(ReasonForAddmission reasonForAddmission)
        {
            ReasonForAddmission reasonForAddmissionToDelete = context.ReasonForAddmission.FirstOrDefault(r => r.ReasonForAddmissionId == reasonForAddmission.ReasonForAddmissionId);
            context.ReasonForAddmission.Remove(reasonForAddmissionToDelete);
            context.SaveChanges();
        }

        public ReasonForAddmission GetReasonForAddmission(int id)
        {
            ReasonForAddmission reasonForAddmission = context.ReasonForAddmission.FirstOrDefault(r => r.ReasonForAddmissionId == id);
            if (reasonForAddmission != null)
            {
                reasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == reasonForAddmission.ContestId);
            }
            return reasonForAddmission;
        }

        public ReasonForAddmission GetReasonForAddmission(string fullname)
        {
            ReasonForAddmission reasonForAddmission = context.ReasonForAddmission.FirstOrDefault(r => r.Fullname == fullname);
            if (reasonForAddmission != null)
            {
                reasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == reasonForAddmission.ContestId);
            }
            return reasonForAddmission;
        }

        public List<ReasonForAddmission> GetReasonForAddmissions()
        {
            List<ReasonForAddmission> reasonForAddmissions = context.ReasonForAddmission.ToList();
            if (reasonForAddmissions.Count != 0)
            {
                foreach (ReasonForAddmission reasonForAddmission in reasonForAddmissions)
                {
                    reasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == reasonForAddmission.ContestId);
                }
            }
            return reasonForAddmissions;
        }

        public List<ReasonForAddmission> GetReasonForAddmissions(Contest contest)
        {
            List<ReasonForAddmission> reasonForAddmissions = context.ReasonForAddmission.Where(r => r.ContestId == contest.ContestId).ToList();
            if (reasonForAddmissions.Count != 0)
            {
                foreach (ReasonForAddmission reasonForAddmission in reasonForAddmissions)
                {
                    reasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == reasonForAddmission.ContestId);
                }
            }
            return reasonForAddmissions;
        }

        public ReasonForAddmission InsertReasonForAddmission(ReasonForAddmission reasonForAddmission)
        {
            context.ReasonForAddmission.Add(reasonForAddmission);
            context.SaveChanges();
            return reasonForAddmission;
        }

        public ReasonForAddmission UpdateReasonForAddmission(ReasonForAddmission reasonForAddmission)
        {
            ReasonForAddmission reasonForAddmissionToUpdate = context.ReasonForAddmission.FirstOrDefault(r => r.ReasonForAddmissionId == reasonForAddmission.ReasonForAddmissionId);
            reasonForAddmissionToUpdate.ContestId = reasonForAddmission.ContestId;
            reasonForAddmissionToUpdate.Fullname = reasonForAddmission.Fullname;
            reasonForAddmissionToUpdate.Shortname = reasonForAddmission.Shortname;
            context.SaveChanges();
            return reasonForAddmission;
        }
    }
}
