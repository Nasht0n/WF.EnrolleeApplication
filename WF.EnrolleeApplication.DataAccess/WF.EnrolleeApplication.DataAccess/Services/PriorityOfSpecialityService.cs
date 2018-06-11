using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class PriorityOfSpecialityService : IPriorityOfSpecialityService
    {
        private EnrolleeContext context;
        public PriorityOfSpecialityService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }

        public void DeletePriorityOfSpeciality(PriorityOfSpeciality priorityOfSpeciality)
        {
            PriorityOfSpeciality priorityOfSpecialityToDelete = context.PriorityOfSpeciality.FirstOrDefault(ps => ps.PriorityId == priorityOfSpeciality.PriorityId);
            context.PriorityOfSpeciality.Remove(priorityOfSpecialityToDelete);
            context.SaveChanges();
        }

        public List<PriorityOfSpeciality> GetPriorityOfSpecialities()
        {
            List<PriorityOfSpeciality> priorityOfSpecialities = context.PriorityOfSpeciality.ToList();
            if(priorityOfSpecialities.Count!= 0)
            {
                foreach(var priorityOfSpeciality in priorityOfSpecialities)
                {
                    priorityOfSpeciality.Enrollee = context.Enrollee.FirstOrDefault(e => e.EnrolleeId == priorityOfSpeciality.EnrolleeId);
                    priorityOfSpeciality.Enrollee.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == priorityOfSpeciality.Enrollee.SpecialityId);
                    priorityOfSpeciality.Enrollee.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == priorityOfSpeciality.Enrollee.Speciality.FacultyId);
                    priorityOfSpeciality.Enrollee.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == priorityOfSpeciality.Enrollee.Speciality.FormOfStudyId);
                    priorityOfSpeciality.Enrollee.Citizenship = context.Citizenship.FirstOrDefault(e => e.CitizenshipId == priorityOfSpeciality.Enrollee.CitizenshipId);
                    priorityOfSpeciality.Enrollee.Country = context.Country.FirstOrDefault(c => c.CountryId == priorityOfSpeciality.Enrollee.CountryId);
                    priorityOfSpeciality.Enrollee.Area = context.Area.FirstOrDefault(a => a.AreaId == priorityOfSpeciality.Enrollee.AreaId);
                    priorityOfSpeciality.Enrollee.District = context.District.FirstOrDefault(d => d.DistrictId == priorityOfSpeciality.Enrollee.DistrictId);
                    priorityOfSpeciality.Enrollee.TypeOfSettlement = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == priorityOfSpeciality.Enrollee.SettlementTypeId);
                    priorityOfSpeciality.Enrollee.TypeOfStreet = context.TypeOfStreet.FirstOrDefault(ts => ts.StreetTypeId == priorityOfSpeciality.Enrollee.StreetTypeId);
                    priorityOfSpeciality.Enrollee.Document = context.Document.FirstOrDefault(d => d.DocumentId == priorityOfSpeciality.Enrollee.DocumentId);
                    priorityOfSpeciality.Enrollee.TypeOfSchool = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == priorityOfSpeciality.Enrollee.SchoolTypeId);
                    priorityOfSpeciality.Enrollee.ForeignLanguage = context.ForeignLanguage.FirstOrDefault(fl => fl.LanguageId == priorityOfSpeciality.Enrollee.ForeignLanguageId);
                    priorityOfSpeciality.Enrollee.ReasonForAddmission = context.ReasonForAddmission.FirstOrDefault(rfa => rfa.ReasonForAddmissionId == priorityOfSpeciality.Enrollee.ReasonForAddmissionId);
                    priorityOfSpeciality.Enrollee.ReasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == priorityOfSpeciality.Enrollee.ReasonForAddmission.ContestId);
                    priorityOfSpeciality.Enrollee.TypeOfState = context.TypeOfState.FirstOrDefault(ts => ts.StateId == priorityOfSpeciality.Enrollee.StateTypeId);
                    priorityOfSpeciality.Enrollee.Employee = context.Employee.FirstOrDefault(e => e.EmployeeId == priorityOfSpeciality.Enrollee.EmployeeId);
                    priorityOfSpeciality.Enrollee.Employee.EmployeePost = context.EmployeePost.FirstOrDefault(e => e.PostId == priorityOfSpeciality.Enrollee.Employee.PostId);
                    priorityOfSpeciality.Enrollee.TypeOfFinance = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == priorityOfSpeciality.Enrollee.FinanceTypeId);
                    if (priorityOfSpeciality.Enrollee.DecreeId.HasValue) priorityOfSpeciality.Enrollee.Decree = context.Decree.FirstOrDefault(d => d.DecreeId == priorityOfSpeciality.Enrollee.DecreeId);
                    if (priorityOfSpeciality.Enrollee.SecondarySpecialityId.HasValue) priorityOfSpeciality.Enrollee.SecondarySpeciality = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == priorityOfSpeciality.Enrollee.SecondarySpecialityId);
                    if (priorityOfSpeciality.Enrollee.TargetWorkPlaceId.HasValue) priorityOfSpeciality.Enrollee.TargetWorkPlace = context.TargetWorkPlace.FirstOrDefault(tw => tw.TargetId == priorityOfSpeciality.Enrollee.TargetWorkPlaceId);
                    priorityOfSpeciality.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == priorityOfSpeciality.SpecialityId);
                    priorityOfSpeciality.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == priorityOfSpeciality.Speciality.FacultyId);
                    priorityOfSpeciality.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == priorityOfSpeciality.Speciality.FormOfStudyId);
                }
            }
            return priorityOfSpecialities;
        }

        public List<PriorityOfSpeciality> GetPriorityOfSpecialities(Enrollee enrollee)
        {
            List<PriorityOfSpeciality> priorityOfSpecialities = context.PriorityOfSpeciality.Where(ps => ps.EnrolleeId == enrollee.EnrolleeId).ToList();
            if (priorityOfSpecialities.Count != 0)
            {
                foreach (var priorityOfSpeciality in priorityOfSpecialities)
                {
                    priorityOfSpeciality.Enrollee = context.Enrollee.FirstOrDefault(e => e.EnrolleeId == priorityOfSpeciality.EnrolleeId);
                    priorityOfSpeciality.Enrollee.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == priorityOfSpeciality.Enrollee.SpecialityId);
                    priorityOfSpeciality.Enrollee.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == priorityOfSpeciality.Enrollee.Speciality.FacultyId);
                    priorityOfSpeciality.Enrollee.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == priorityOfSpeciality.Enrollee.Speciality.FormOfStudyId);
                    priorityOfSpeciality.Enrollee.Citizenship = context.Citizenship.FirstOrDefault(e => e.CitizenshipId == priorityOfSpeciality.Enrollee.CitizenshipId);
                    priorityOfSpeciality.Enrollee.Country = context.Country.FirstOrDefault(c => c.CountryId == priorityOfSpeciality.Enrollee.CountryId);
                    priorityOfSpeciality.Enrollee.Area = context.Area.FirstOrDefault(a => a.AreaId == priorityOfSpeciality.Enrollee.AreaId);
                    priorityOfSpeciality.Enrollee.District = context.District.FirstOrDefault(d => d.DistrictId == priorityOfSpeciality.Enrollee.DistrictId);
                    priorityOfSpeciality.Enrollee.TypeOfSettlement = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == priorityOfSpeciality.Enrollee.SettlementTypeId);
                    priorityOfSpeciality.Enrollee.TypeOfStreet = context.TypeOfStreet.FirstOrDefault(ts => ts.StreetTypeId == priorityOfSpeciality.Enrollee.StreetTypeId);
                    priorityOfSpeciality.Enrollee.Document = context.Document.FirstOrDefault(d => d.DocumentId == priorityOfSpeciality.Enrollee.DocumentId);
                    priorityOfSpeciality.Enrollee.TypeOfSchool = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == priorityOfSpeciality.Enrollee.SchoolTypeId);
                    priorityOfSpeciality.Enrollee.ForeignLanguage = context.ForeignLanguage.FirstOrDefault(fl => fl.LanguageId == priorityOfSpeciality.Enrollee.ForeignLanguageId);
                    priorityOfSpeciality.Enrollee.ReasonForAddmission = context.ReasonForAddmission.FirstOrDefault(rfa => rfa.ReasonForAddmissionId == priorityOfSpeciality.Enrollee.ReasonForAddmissionId);
                    priorityOfSpeciality.Enrollee.ReasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == priorityOfSpeciality.Enrollee.ReasonForAddmission.ContestId);
                    priorityOfSpeciality.Enrollee.TypeOfState = context.TypeOfState.FirstOrDefault(ts => ts.StateId == priorityOfSpeciality.Enrollee.StateTypeId);
                    priorityOfSpeciality.Enrollee.Employee = context.Employee.FirstOrDefault(e => e.EmployeeId == priorityOfSpeciality.Enrollee.EmployeeId);
                    priorityOfSpeciality.Enrollee.Employee.EmployeePost = context.EmployeePost.FirstOrDefault(e => e.PostId == priorityOfSpeciality.Enrollee.Employee.PostId);
                    priorityOfSpeciality.Enrollee.TypeOfFinance = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == priorityOfSpeciality.Enrollee.FinanceTypeId);
                    if (priorityOfSpeciality.Enrollee.DecreeId.HasValue) priorityOfSpeciality.Enrollee.Decree = context.Decree.FirstOrDefault(d => d.DecreeId == priorityOfSpeciality.Enrollee.DecreeId);
                    if (priorityOfSpeciality.Enrollee.SecondarySpecialityId.HasValue) priorityOfSpeciality.Enrollee.SecondarySpeciality = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == priorityOfSpeciality.Enrollee.SecondarySpecialityId);
                    if (priorityOfSpeciality.Enrollee.TargetWorkPlaceId.HasValue) priorityOfSpeciality.Enrollee.TargetWorkPlace = context.TargetWorkPlace.FirstOrDefault(tw => tw.TargetId == priorityOfSpeciality.Enrollee.TargetWorkPlaceId);
                    priorityOfSpeciality.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == priorityOfSpeciality.SpecialityId);
                    priorityOfSpeciality.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == priorityOfSpeciality.Speciality.FacultyId);
                    priorityOfSpeciality.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == priorityOfSpeciality.Speciality.FormOfStudyId);
                }
            }
            return priorityOfSpecialities;
        }

        public PriorityOfSpeciality GetPriorityOfSpeciality(int id)
        {
            PriorityOfSpeciality priorityOfSpeciality = context.PriorityOfSpeciality.FirstOrDefault(ps => ps.EnrolleeId == id);
            priorityOfSpeciality.Enrollee = context.Enrollee.FirstOrDefault(e => e.EnrolleeId == priorityOfSpeciality.EnrolleeId);
            priorityOfSpeciality.Enrollee.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == priorityOfSpeciality.Enrollee.SpecialityId);
            priorityOfSpeciality.Enrollee.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == priorityOfSpeciality.Enrollee.Speciality.FacultyId);
            priorityOfSpeciality.Enrollee.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == priorityOfSpeciality.Enrollee.Speciality.FormOfStudyId);
            priorityOfSpeciality.Enrollee.Citizenship = context.Citizenship.FirstOrDefault(e => e.CitizenshipId == priorityOfSpeciality.Enrollee.CitizenshipId);
            priorityOfSpeciality.Enrollee.Country = context.Country.FirstOrDefault(c => c.CountryId == priorityOfSpeciality.Enrollee.CountryId);
            priorityOfSpeciality.Enrollee.Area = context.Area.FirstOrDefault(a => a.AreaId == priorityOfSpeciality.Enrollee.AreaId);
            priorityOfSpeciality.Enrollee.District = context.District.FirstOrDefault(d => d.DistrictId == priorityOfSpeciality.Enrollee.DistrictId);
            priorityOfSpeciality.Enrollee.TypeOfSettlement = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == priorityOfSpeciality.Enrollee.SettlementTypeId);
            priorityOfSpeciality.Enrollee.TypeOfStreet = context.TypeOfStreet.FirstOrDefault(ts => ts.StreetTypeId == priorityOfSpeciality.Enrollee.StreetTypeId);
            priorityOfSpeciality.Enrollee.Document = context.Document.FirstOrDefault(d => d.DocumentId == priorityOfSpeciality.Enrollee.DocumentId);
            priorityOfSpeciality.Enrollee.TypeOfSchool = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == priorityOfSpeciality.Enrollee.SchoolTypeId);
            priorityOfSpeciality.Enrollee.ForeignLanguage = context.ForeignLanguage.FirstOrDefault(fl => fl.LanguageId == priorityOfSpeciality.Enrollee.ForeignLanguageId);
            priorityOfSpeciality.Enrollee.ReasonForAddmission = context.ReasonForAddmission.FirstOrDefault(rfa => rfa.ReasonForAddmissionId == priorityOfSpeciality.Enrollee.ReasonForAddmissionId);
            priorityOfSpeciality.Enrollee.ReasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == priorityOfSpeciality.Enrollee.ReasonForAddmission.ContestId);
            priorityOfSpeciality.Enrollee.TypeOfState = context.TypeOfState.FirstOrDefault(ts => ts.StateId == priorityOfSpeciality.Enrollee.StateTypeId);
            priorityOfSpeciality.Enrollee.Employee = context.Employee.FirstOrDefault(e => e.EmployeeId == priorityOfSpeciality.Enrollee.EmployeeId);
            priorityOfSpeciality.Enrollee.Employee.EmployeePost = context.EmployeePost.FirstOrDefault(e => e.PostId == priorityOfSpeciality.Enrollee.Employee.PostId);
            priorityOfSpeciality.Enrollee.TypeOfFinance = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == priorityOfSpeciality.Enrollee.FinanceTypeId);
            if (priorityOfSpeciality.Enrollee.DecreeId.HasValue) priorityOfSpeciality.Enrollee.Decree = context.Decree.FirstOrDefault(d => d.DecreeId == priorityOfSpeciality.Enrollee.DecreeId);
            if (priorityOfSpeciality.Enrollee.SecondarySpecialityId.HasValue) priorityOfSpeciality.Enrollee.SecondarySpeciality = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == priorityOfSpeciality.Enrollee.SecondarySpecialityId);
            if (priorityOfSpeciality.Enrollee.TargetWorkPlaceId.HasValue) priorityOfSpeciality.Enrollee.TargetWorkPlace = context.TargetWorkPlace.FirstOrDefault(tw => tw.TargetId == priorityOfSpeciality.Enrollee.TargetWorkPlaceId);
            priorityOfSpeciality.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == priorityOfSpeciality.SpecialityId);
            priorityOfSpeciality.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == priorityOfSpeciality.Speciality.FacultyId);
            priorityOfSpeciality.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == priorityOfSpeciality.Speciality.FormOfStudyId);
            return priorityOfSpeciality;
        }

        public PriorityOfSpeciality GetPriorityOfSpeciality(Enrollee enrollee, Speciality speciality)
        {
            PriorityOfSpeciality priorityOfSpeciality = context.PriorityOfSpeciality.FirstOrDefault(ps => ps.EnrolleeId == enrollee.EnrolleeId && ps.SpecialityId == speciality.SpecialityId);
            priorityOfSpeciality.Enrollee = context.Enrollee.FirstOrDefault(e => e.EnrolleeId == priorityOfSpeciality.EnrolleeId);
            priorityOfSpeciality.Enrollee.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == priorityOfSpeciality.Enrollee.SpecialityId);
            priorityOfSpeciality.Enrollee.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == priorityOfSpeciality.Enrollee.Speciality.FacultyId);
            priorityOfSpeciality.Enrollee.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == priorityOfSpeciality.Enrollee.Speciality.FormOfStudyId);
            priorityOfSpeciality.Enrollee.Citizenship = context.Citizenship.FirstOrDefault(e => e.CitizenshipId == priorityOfSpeciality.Enrollee.CitizenshipId);
            priorityOfSpeciality.Enrollee.Country = context.Country.FirstOrDefault(c => c.CountryId == priorityOfSpeciality.Enrollee.CountryId);
            priorityOfSpeciality.Enrollee.Area = context.Area.FirstOrDefault(a => a.AreaId == priorityOfSpeciality.Enrollee.AreaId);
            priorityOfSpeciality.Enrollee.District = context.District.FirstOrDefault(d => d.DistrictId == priorityOfSpeciality.Enrollee.DistrictId);
            priorityOfSpeciality.Enrollee.TypeOfSettlement = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == priorityOfSpeciality.Enrollee.SettlementTypeId);
            priorityOfSpeciality.Enrollee.TypeOfStreet = context.TypeOfStreet.FirstOrDefault(ts => ts.StreetTypeId == priorityOfSpeciality.Enrollee.StreetTypeId);
            priorityOfSpeciality.Enrollee.Document = context.Document.FirstOrDefault(d => d.DocumentId == priorityOfSpeciality.Enrollee.DocumentId);
            priorityOfSpeciality.Enrollee.TypeOfSchool = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == priorityOfSpeciality.Enrollee.SchoolTypeId);
            priorityOfSpeciality.Enrollee.ForeignLanguage = context.ForeignLanguage.FirstOrDefault(fl => fl.LanguageId == priorityOfSpeciality.Enrollee.ForeignLanguageId);
            priorityOfSpeciality.Enrollee.ReasonForAddmission = context.ReasonForAddmission.FirstOrDefault(rfa => rfa.ReasonForAddmissionId == priorityOfSpeciality.Enrollee.ReasonForAddmissionId);
            priorityOfSpeciality.Enrollee.ReasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == priorityOfSpeciality.Enrollee.ReasonForAddmission.ContestId);
            priorityOfSpeciality.Enrollee.TypeOfState = context.TypeOfState.FirstOrDefault(ts => ts.StateId == priorityOfSpeciality.Enrollee.StateTypeId);
            priorityOfSpeciality.Enrollee.Employee = context.Employee.FirstOrDefault(e => e.EmployeeId == priorityOfSpeciality.Enrollee.EmployeeId);
            priorityOfSpeciality.Enrollee.Employee.EmployeePost = context.EmployeePost.FirstOrDefault(e => e.PostId == priorityOfSpeciality.Enrollee.Employee.PostId);
            priorityOfSpeciality.Enrollee.TypeOfFinance = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == priorityOfSpeciality.Enrollee.FinanceTypeId);
            if (priorityOfSpeciality.Enrollee.DecreeId.HasValue) priorityOfSpeciality.Enrollee.Decree = context.Decree.FirstOrDefault(d => d.DecreeId == priorityOfSpeciality.Enrollee.DecreeId);
            if (priorityOfSpeciality.Enrollee.SecondarySpecialityId.HasValue) priorityOfSpeciality.Enrollee.SecondarySpeciality = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == priorityOfSpeciality.Enrollee.SecondarySpecialityId);
            if (priorityOfSpeciality.Enrollee.TargetWorkPlaceId.HasValue) priorityOfSpeciality.Enrollee.TargetWorkPlace = context.TargetWorkPlace.FirstOrDefault(tw => tw.TargetId == priorityOfSpeciality.Enrollee.TargetWorkPlaceId);
            priorityOfSpeciality.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == priorityOfSpeciality.SpecialityId);
            priorityOfSpeciality.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == priorityOfSpeciality.Speciality.FacultyId);
            priorityOfSpeciality.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == priorityOfSpeciality.Speciality.FormOfStudyId);
            return priorityOfSpeciality;
        }

        public PriorityOfSpeciality InsertPriorityOfSpeciality(PriorityOfSpeciality priorityOfSpeciality)
        {
            context.PriorityOfSpeciality.Add(priorityOfSpeciality);
            context.SaveChanges();
            return priorityOfSpeciality;
        }

        public PriorityOfSpeciality UpdatePriorityOfSpeciality(PriorityOfSpeciality priorityOfSpeciality)
        {
            PriorityOfSpeciality priorityOfSpecialityToUpdate = context.PriorityOfSpeciality.FirstOrDefault(ps => ps.PriorityId == priorityOfSpeciality.PriorityId);
            priorityOfSpecialityToUpdate.EnrolleeId = priorityOfSpeciality.EnrolleeId;
            priorityOfSpecialityToUpdate.SpecialityId = priorityOfSpeciality.SpecialityId;
            priorityOfSpecialityToUpdate.PriorityLevel = priorityOfSpeciality.PriorityLevel;
            context.SaveChanges();
            return priorityOfSpeciality;
        }
    }
}
