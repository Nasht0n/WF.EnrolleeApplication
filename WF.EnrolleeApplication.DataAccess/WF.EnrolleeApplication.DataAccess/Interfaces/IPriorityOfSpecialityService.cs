using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface IPriorityOfSpecialityService
    {
        PriorityOfSpeciality InsertPriorityOfSpeciality(PriorityOfSpeciality priorityOfSpeciality);
        PriorityOfSpeciality UpdatePriorityOfSpeciality(PriorityOfSpeciality priorityOfSpeciality);
        void DeletePriorityOfSpeciality(PriorityOfSpeciality priorityOfSpeciality);
        List<PriorityOfSpeciality> GetPriorityOfSpecialities();
        List<PriorityOfSpeciality> GetPriorityOfSpecialities(Enrollee enrollee);
        PriorityOfSpeciality GetPriorityOfSpeciality(int id);
        PriorityOfSpeciality GetPriorityOfSpeciality(Enrollee enrollee, Speciality speciality);
    }
}
