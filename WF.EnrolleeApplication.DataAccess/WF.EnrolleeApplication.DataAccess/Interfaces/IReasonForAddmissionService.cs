using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface IReasonForAddmissionService
    {
        ReasonForAddmission InsertReasonForAddmission(ReasonForAddmission reasonForAddmission);
        ReasonForAddmission UpdateReasonForAddmission(ReasonForAddmission reasonForAddmission);
        void DeleteReasonForAddmission(ReasonForAddmission reasonForAddmission);
        List<ReasonForAddmission> GetReasonForAddmissions();
        List<ReasonForAddmission> GetReasonForAddmissions(Contest contest);
        ReasonForAddmission GetReasonForAddmission(int id);
        ReasonForAddmission GetReasonForAddmission(string fullname);
    }
}
