using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class EnrolleeService : IEnrolleeService
    {
        private EnrolleeContext context;
        public EnrolleeService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }

        public void DeleteEnrollee(Enrollee enrollee)
        {
            Enrollee enrolleeToDelete = context.Enrollee.FirstOrDefault(e => e.EnrolleeId == enrollee.EnrolleeId);
            context.Enrollee.Remove(enrolleeToDelete);
            context.SaveChanges();
        }

        public Enrollee GetEnrollee(int id)
        {
            Enrollee enrollee = context.Enrollee.FirstOrDefault(e => e.EnrolleeId == id);
            if (enrollee != null)
            {
                enrollee.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == enrollee.SpecialityId);
                enrollee.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == enrollee.Speciality.FacultyId);
                enrollee.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == enrollee.Speciality.FormOfStudyId);
                enrollee.Citizenship = context.Citizenship.FirstOrDefault(e => e.CitizenshipId == enrollee.CitizenshipId);
                enrollee.Country = context.Country.FirstOrDefault(c => c.CountryId == enrollee.CountryId);
                enrollee.Area = context.Area.FirstOrDefault(a => a.AreaId == enrollee.AreaId);
                enrollee.District = context.District.FirstOrDefault(d => d.DistrictId == enrollee.DistrictId);
                enrollee.TypeOfSettlement = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == enrollee.SettlementTypeId);
                enrollee.TypeOfStreet = context.TypeOfStreet.FirstOrDefault(ts => ts.StreetTypeId == enrollee.StreetTypeId);
                enrollee.Document = context.Document.FirstOrDefault(d => d.DocumentId == enrollee.DocumentId);
                enrollee.TypeOfSchool = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == enrollee.SchoolTypeId);
                enrollee.ForeignLanguage = context.ForeignLanguage.FirstOrDefault(fl => fl.LanguageId == enrollee.ForeignLanguageId);
                enrollee.ReasonForAddmission = context.ReasonForAddmission.FirstOrDefault(rfa => rfa.ReasonForAddmissionId == enrollee.ReasonForAddmissionId);
                enrollee.ReasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == enrollee.ReasonForAddmission.ContestId);
                enrollee.TypeOfState = context.TypeOfState.FirstOrDefault(ts => ts.StateId == enrollee.StateTypeId);
                enrollee.Employee = context.Employee.FirstOrDefault(e => e.EmployeeId == enrollee.EmployeeId);
                enrollee.Employee.EmployeePost = context.EmployeePost.FirstOrDefault(e => e.PostId == enrollee.Employee.PostId);
                enrollee.TypeOfFinance = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == enrollee.FinanceTypeId);
                if (enrollee.DecreeId.HasValue) enrollee.Decree = context.Decree.FirstOrDefault(d => d.DecreeId == enrollee.DecreeId);
                if (enrollee.SecondarySpecialityId.HasValue) enrollee.SecondarySpeciality = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == enrollee.SecondarySpecialityId);
                if (enrollee.TargetWorkPlaceId.HasValue) enrollee.TargetWorkPlace = context.TargetWorkPlace.FirstOrDefault(tw => tw.TargetId == enrollee.TargetWorkPlaceId);
            }
            return enrollee;
        }

        public Enrollee GetEnrollee(string documentPersonalNumber)
        {
            Enrollee enrollee = context.Enrollee.FirstOrDefault(e => e.DocumentPersonalNumber == documentPersonalNumber);
            if (enrollee != null)
            {
                enrollee.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == enrollee.SpecialityId);
                enrollee.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == enrollee.Speciality.FacultyId);
                enrollee.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == enrollee.Speciality.FormOfStudyId);
                enrollee.Citizenship = context.Citizenship.FirstOrDefault(c => c.CitizenshipId == enrollee.CitizenshipId);
                enrollee.Country = context.Country.FirstOrDefault(c => c.CountryId == enrollee.CountryId);
                enrollee.Area = context.Area.FirstOrDefault(a => a.AreaId == enrollee.AreaId);
                enrollee.District = context.District.FirstOrDefault(d => d.DistrictId == enrollee.DistrictId);
                enrollee.TypeOfSettlement = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == enrollee.SettlementTypeId);
                enrollee.TypeOfStreet = context.TypeOfStreet.FirstOrDefault(ts => ts.StreetTypeId == enrollee.StreetTypeId);
                enrollee.Document = context.Document.FirstOrDefault(d => d.DocumentId == enrollee.DocumentId);
                enrollee.TypeOfSchool = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == enrollee.SchoolTypeId);
                enrollee.ForeignLanguage = context.ForeignLanguage.FirstOrDefault(f => f.LanguageId == enrollee.ForeignLanguageId);
                enrollee.ReasonForAddmission = context.ReasonForAddmission.FirstOrDefault(r => r.ReasonForAddmissionId == enrollee.ReasonForAddmissionId);
                enrollee.ReasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == enrollee.ReasonForAddmission.ContestId);
                enrollee.TypeOfState = context.TypeOfState.FirstOrDefault(ts => ts.StateId == enrollee.StateTypeId);
                enrollee.Employee = context.Employee.FirstOrDefault(e => e.EmployeeId == enrollee.EmployeeId);
                enrollee.Employee.EmployeePost = context.EmployeePost.FirstOrDefault(ep => ep.PostId == enrollee.Employee.PostId);
                enrollee.TypeOfFinance = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == enrollee.FinanceTypeId);
                if (enrollee.DecreeId != null) enrollee.Decree = context.Decree.FirstOrDefault(d => d.DecreeId == enrollee.DecreeId);
                if (enrollee.SecondarySpecialityId != null) enrollee.SecondarySpeciality = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == enrollee.SecondarySpecialityId);
                if (enrollee.TargetWorkPlaceId != null) enrollee.TargetWorkPlace = context.TargetWorkPlace.FirstOrDefault(twp => twp.TargetId == enrollee.TargetWorkPlaceId);
            }
            return enrollee;
        }

        public List<Enrollee> GetEnrollees()
        {
            List<Enrollee> enrollees = context.Enrollee.ToList();
            if (enrollees.Count != 0)
            {
                foreach (Enrollee enrollee in enrollees)
                {
                    enrollee.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == enrollee.SpecialityId);
                    enrollee.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == enrollee.Speciality.FacultyId);
                    enrollee.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == enrollee.Speciality.FormOfStudyId);
                    enrollee.Citizenship = context.Citizenship.FirstOrDefault(c => c.CitizenshipId == enrollee.CitizenshipId);
                    enrollee.Country = context.Country.FirstOrDefault(c => c.CountryId == enrollee.CountryId);
                    enrollee.Area = context.Area.FirstOrDefault(a => a.AreaId == enrollee.AreaId);
                    enrollee.District = context.District.FirstOrDefault(d => d.DistrictId == enrollee.DistrictId);
                    enrollee.TypeOfSettlement = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == enrollee.SettlementTypeId);
                    enrollee.TypeOfStreet = context.TypeOfStreet.FirstOrDefault(ts => ts.StreetTypeId == enrollee.StreetTypeId);
                    enrollee.Document = context.Document.FirstOrDefault(d => d.DocumentId == enrollee.DocumentId);
                    enrollee.TypeOfSchool = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == enrollee.SchoolTypeId);
                    enrollee.ForeignLanguage = context.ForeignLanguage.FirstOrDefault(f => f.LanguageId == enrollee.ForeignLanguageId);
                    enrollee.ReasonForAddmission = context.ReasonForAddmission.FirstOrDefault(r => r.ReasonForAddmissionId == enrollee.ReasonForAddmissionId);
                    enrollee.ReasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == enrollee.ReasonForAddmission.ContestId);
                    enrollee.TypeOfState = context.TypeOfState.FirstOrDefault(ts => ts.StateId == enrollee.StateTypeId);
                    enrollee.Employee = context.Employee.FirstOrDefault(e => e.EmployeeId == enrollee.EmployeeId);
                    enrollee.Employee.EmployeePost = context.EmployeePost.FirstOrDefault(ep => ep.PostId == enrollee.Employee.PostId);
                    enrollee.TypeOfFinance = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == enrollee.FinanceTypeId);
                    if (enrollee.DecreeId != null) enrollee.Decree = context.Decree.FirstOrDefault(d => d.DecreeId == enrollee.DecreeId);
                    if (enrollee.SecondarySpecialityId != null) enrollee.SecondarySpeciality = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == enrollee.SecondarySpecialityId);
                    if (enrollee.TargetWorkPlaceId != null) enrollee.TargetWorkPlace = context.TargetWorkPlace.FirstOrDefault(twp => twp.TargetId == enrollee.TargetWorkPlaceId);
                }
            }
            return enrollees;
        }

        public List<Enrollee> GetEnrollees(Speciality speciality)
        {
            List<Enrollee> enrollees = context.Enrollee.Where(e => e.SpecialityId == speciality.SpecialityId).ToList();
            if (enrollees.Count != 0)
            {
                foreach (Enrollee enrollee in enrollees)
                {
                    enrollee.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == enrollee.SpecialityId);
                    enrollee.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == enrollee.Speciality.FacultyId);
                    enrollee.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == enrollee.Speciality.FormOfStudyId);
                    enrollee.Citizenship = context.Citizenship.FirstOrDefault(c => c.CitizenshipId == enrollee.CitizenshipId);
                    enrollee.Country = context.Country.FirstOrDefault(c => c.CountryId == enrollee.CountryId);
                    enrollee.Area = context.Area.FirstOrDefault(a => a.AreaId == enrollee.AreaId);
                    enrollee.District = context.District.FirstOrDefault(d => d.DistrictId == enrollee.DistrictId);
                    enrollee.TypeOfSettlement = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == enrollee.SettlementTypeId);
                    enrollee.TypeOfStreet = context.TypeOfStreet.FirstOrDefault(ts => ts.StreetTypeId == enrollee.StreetTypeId);
                    enrollee.Document = context.Document.FirstOrDefault(d => d.DocumentId == enrollee.DocumentId);
                    enrollee.TypeOfSchool = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == enrollee.SchoolTypeId);
                    enrollee.ForeignLanguage = context.ForeignLanguage.FirstOrDefault(f => f.LanguageId == enrollee.ForeignLanguageId);
                    enrollee.ReasonForAddmission = context.ReasonForAddmission.FirstOrDefault(r => r.ReasonForAddmissionId == enrollee.ReasonForAddmissionId);
                    enrollee.ReasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == enrollee.ReasonForAddmission.ContestId);
                    enrollee.TypeOfState = context.TypeOfState.FirstOrDefault(ts => ts.StateId == enrollee.StateTypeId);
                    enrollee.Employee = context.Employee.FirstOrDefault(e => e.EmployeeId == enrollee.EmployeeId);
                    enrollee.Employee.EmployeePost = context.EmployeePost.FirstOrDefault(ep => ep.PostId == enrollee.Employee.PostId);
                    enrollee.TypeOfFinance = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == enrollee.FinanceTypeId);
                    if (enrollee.DecreeId != null) enrollee.Decree = context.Decree.FirstOrDefault(d => d.DecreeId == enrollee.DecreeId);
                    if (enrollee.SecondarySpecialityId != null) enrollee.SecondarySpeciality = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == enrollee.SecondarySpecialityId);
                    if (enrollee.TargetWorkPlaceId != null) enrollee.TargetWorkPlace = context.TargetWorkPlace.FirstOrDefault(twp => twp.TargetId == enrollee.TargetWorkPlaceId);
                }
            }
            return enrollees;
        }

        public List<Enrollee> GetEnrollees(Citizenship citizenship)
        {
            List<Enrollee> enrollees = context.Enrollee.Where(e => e.CitizenshipId == citizenship.CitizenshipId).ToList();
            if (enrollees.Count != 0)
            {
                foreach (Enrollee enrollee in enrollees)
                {
                    enrollee.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == enrollee.SpecialityId);
                    enrollee.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == enrollee.Speciality.FacultyId);
                    enrollee.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == enrollee.Speciality.FormOfStudyId);
                    enrollee.Citizenship = context.Citizenship.FirstOrDefault(c => c.CitizenshipId == enrollee.CitizenshipId);
                    enrollee.Country = context.Country.FirstOrDefault(c => c.CountryId == enrollee.CountryId);
                    enrollee.Area = context.Area.FirstOrDefault(a => a.AreaId == enrollee.AreaId);
                    enrollee.District = context.District.FirstOrDefault(d => d.DistrictId == enrollee.DistrictId);
                    enrollee.TypeOfSettlement = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == enrollee.SettlementTypeId);
                    enrollee.TypeOfStreet = context.TypeOfStreet.FirstOrDefault(ts => ts.StreetTypeId == enrollee.StreetTypeId);
                    enrollee.Document = context.Document.FirstOrDefault(d => d.DocumentId == enrollee.DocumentId);
                    enrollee.TypeOfSchool = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == enrollee.SchoolTypeId);
                    enrollee.ForeignLanguage = context.ForeignLanguage.FirstOrDefault(f => f.LanguageId == enrollee.ForeignLanguageId);
                    enrollee.ReasonForAddmission = context.ReasonForAddmission.FirstOrDefault(r => r.ReasonForAddmissionId == enrollee.ReasonForAddmissionId);
                    enrollee.ReasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == enrollee.ReasonForAddmission.ContestId);
                    enrollee.TypeOfState = context.TypeOfState.FirstOrDefault(ts => ts.StateId == enrollee.StateTypeId);
                    enrollee.Employee = context.Employee.FirstOrDefault(e => e.EmployeeId == enrollee.EmployeeId);
                    enrollee.Employee.EmployeePost = context.EmployeePost.FirstOrDefault(ep => ep.PostId == enrollee.Employee.PostId);
                    enrollee.TypeOfFinance = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == enrollee.FinanceTypeId);
                    if (enrollee.DecreeId != null) enrollee.Decree = context.Decree.FirstOrDefault(d => d.DecreeId == enrollee.DecreeId);
                    if (enrollee.SecondarySpecialityId != null) enrollee.SecondarySpeciality = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == enrollee.SecondarySpecialityId);
                    if (enrollee.TargetWorkPlaceId != null) enrollee.TargetWorkPlace = context.TargetWorkPlace.FirstOrDefault(twp => twp.TargetId == enrollee.TargetWorkPlaceId);
                }
            }
            return enrollees;
        }

        public List<Enrollee> GetEnrollees(Country country)
        {
            List<Enrollee> enrollees = context.Enrollee.Where(e => e.CountryId == country.CountryId).ToList();
            if (enrollees.Count != 0)
            {
                foreach (Enrollee enrollee in enrollees)
                {
                    enrollee.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == enrollee.SpecialityId);
                    enrollee.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == enrollee.Speciality.FacultyId);
                    enrollee.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == enrollee.Speciality.FormOfStudyId);
                    enrollee.Citizenship = context.Citizenship.FirstOrDefault(c => c.CitizenshipId == enrollee.CitizenshipId);
                    enrollee.Country = context.Country.FirstOrDefault(c => c.CountryId == enrollee.CountryId);
                    enrollee.Area = context.Area.FirstOrDefault(a => a.AreaId == enrollee.AreaId);
                    enrollee.District = context.District.FirstOrDefault(d => d.DistrictId == enrollee.DistrictId);
                    enrollee.TypeOfSettlement = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == enrollee.SettlementTypeId);
                    enrollee.TypeOfStreet = context.TypeOfStreet.FirstOrDefault(ts => ts.StreetTypeId == enrollee.StreetTypeId);
                    enrollee.Document = context.Document.FirstOrDefault(d => d.DocumentId == enrollee.DocumentId);
                    enrollee.TypeOfSchool = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == enrollee.SchoolTypeId);
                    enrollee.ForeignLanguage = context.ForeignLanguage.FirstOrDefault(f => f.LanguageId == enrollee.ForeignLanguageId);
                    enrollee.ReasonForAddmission = context.ReasonForAddmission.FirstOrDefault(r => r.ReasonForAddmissionId == enrollee.ReasonForAddmissionId);
                    enrollee.ReasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == enrollee.ReasonForAddmission.ContestId);
                    enrollee.TypeOfState = context.TypeOfState.FirstOrDefault(ts => ts.StateId == enrollee.StateTypeId);
                    enrollee.Employee = context.Employee.FirstOrDefault(e => e.EmployeeId == enrollee.EmployeeId);
                    enrollee.Employee.EmployeePost = context.EmployeePost.FirstOrDefault(ep => ep.PostId == enrollee.Employee.PostId);
                    enrollee.TypeOfFinance = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == enrollee.FinanceTypeId);
                    if (enrollee.DecreeId != null) enrollee.Decree = context.Decree.FirstOrDefault(d => d.DecreeId == enrollee.DecreeId);
                    if (enrollee.SecondarySpecialityId != null) enrollee.SecondarySpeciality = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == enrollee.SecondarySpecialityId);
                    if (enrollee.TargetWorkPlaceId != null) enrollee.TargetWorkPlace = context.TargetWorkPlace.FirstOrDefault(twp => twp.TargetId == enrollee.TargetWorkPlaceId);
                }
            }
            return enrollees;
        }

        public List<Enrollee> GetEnrollees(Area area)
        {
            List<Enrollee> enrollees = context.Enrollee.Where(e => e.AreaId == area.AreaId).ToList();
            if (enrollees.Count != 0)
            {
                foreach (Enrollee enrollee in enrollees)
                {
                    enrollee.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == enrollee.SpecialityId);
                    enrollee.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == enrollee.Speciality.FacultyId);
                    enrollee.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == enrollee.Speciality.FormOfStudyId);
                    enrollee.Citizenship = context.Citizenship.FirstOrDefault(c => c.CitizenshipId == enrollee.CitizenshipId);
                    enrollee.Country = context.Country.FirstOrDefault(c => c.CountryId == enrollee.CountryId);
                    enrollee.Area = context.Area.FirstOrDefault(a => a.AreaId == enrollee.AreaId);
                    enrollee.District = context.District.FirstOrDefault(d => d.DistrictId == enrollee.DistrictId);
                    enrollee.TypeOfSettlement = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == enrollee.SettlementTypeId);
                    enrollee.TypeOfStreet = context.TypeOfStreet.FirstOrDefault(ts => ts.StreetTypeId == enrollee.StreetTypeId);
                    enrollee.Document = context.Document.FirstOrDefault(d => d.DocumentId == enrollee.DocumentId);
                    enrollee.TypeOfSchool = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == enrollee.SchoolTypeId);
                    enrollee.ForeignLanguage = context.ForeignLanguage.FirstOrDefault(f => f.LanguageId == enrollee.ForeignLanguageId);
                    enrollee.ReasonForAddmission = context.ReasonForAddmission.FirstOrDefault(r => r.ReasonForAddmissionId == enrollee.ReasonForAddmissionId);
                    enrollee.ReasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == enrollee.ReasonForAddmission.ContestId);
                    enrollee.TypeOfState = context.TypeOfState.FirstOrDefault(ts => ts.StateId == enrollee.StateTypeId);
                    enrollee.Employee = context.Employee.FirstOrDefault(e => e.EmployeeId == enrollee.EmployeeId);
                    enrollee.Employee.EmployeePost = context.EmployeePost.FirstOrDefault(ep => ep.PostId == enrollee.Employee.PostId);
                    enrollee.TypeOfFinance = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == enrollee.FinanceTypeId);
                    if (enrollee.DecreeId != null) enrollee.Decree = context.Decree.FirstOrDefault(d => d.DecreeId == enrollee.DecreeId);
                    if (enrollee.SecondarySpecialityId != null) enrollee.SecondarySpeciality = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == enrollee.SecondarySpecialityId);
                    if (enrollee.TargetWorkPlaceId != null) enrollee.TargetWorkPlace = context.TargetWorkPlace.FirstOrDefault(twp => twp.TargetId == enrollee.TargetWorkPlaceId);
                }
            }
            return enrollees;
        }

        public List<Enrollee> GetEnrollees(District district)
        {
            List<Enrollee> enrollees = context.Enrollee.Where(e => e.DistrictId == district.DistrictId).ToList();
            if (enrollees.Count != 0)
            {
                foreach (Enrollee enrollee in enrollees)
                {
                    enrollee.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == enrollee.SpecialityId);
                    enrollee.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == enrollee.Speciality.FacultyId);
                    enrollee.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == enrollee.Speciality.FormOfStudyId);
                    enrollee.Citizenship = context.Citizenship.FirstOrDefault(c => c.CitizenshipId == enrollee.CitizenshipId);
                    enrollee.Country = context.Country.FirstOrDefault(c => c.CountryId == enrollee.CountryId);
                    enrollee.Area = context.Area.FirstOrDefault(a => a.AreaId == enrollee.AreaId);
                    enrollee.District = context.District.FirstOrDefault(d => d.DistrictId == enrollee.DistrictId);
                    enrollee.TypeOfSettlement = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == enrollee.SettlementTypeId);
                    enrollee.TypeOfStreet = context.TypeOfStreet.FirstOrDefault(ts => ts.StreetTypeId == enrollee.StreetTypeId);
                    enrollee.Document = context.Document.FirstOrDefault(d => d.DocumentId == enrollee.DocumentId);
                    enrollee.TypeOfSchool = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == enrollee.SchoolTypeId);
                    enrollee.ForeignLanguage = context.ForeignLanguage.FirstOrDefault(f => f.LanguageId == enrollee.ForeignLanguageId);
                    enrollee.ReasonForAddmission = context.ReasonForAddmission.FirstOrDefault(r => r.ReasonForAddmissionId == enrollee.ReasonForAddmissionId);
                    enrollee.ReasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == enrollee.ReasonForAddmission.ContestId);
                    enrollee.TypeOfState = context.TypeOfState.FirstOrDefault(ts => ts.StateId == enrollee.StateTypeId);
                    enrollee.Employee = context.Employee.FirstOrDefault(e => e.EmployeeId == enrollee.EmployeeId);
                    enrollee.Employee.EmployeePost = context.EmployeePost.FirstOrDefault(ep => ep.PostId == enrollee.Employee.PostId);
                    enrollee.TypeOfFinance = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == enrollee.FinanceTypeId);
                    if (enrollee.DecreeId != null) enrollee.Decree = context.Decree.FirstOrDefault(d => d.DecreeId == enrollee.DecreeId);
                    if (enrollee.SecondarySpecialityId != null) enrollee.SecondarySpeciality = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == enrollee.SecondarySpecialityId);
                    if (enrollee.TargetWorkPlaceId != null) enrollee.TargetWorkPlace = context.TargetWorkPlace.FirstOrDefault(twp => twp.TargetId == enrollee.TargetWorkPlaceId);
                }
            }
            return enrollees;
        }

        public List<Enrollee> GetEnrollees(TypeOfSchool typeOfSchool)
        {
            List<Enrollee> enrollees = context.Enrollee.Where(e => e.SchoolTypeId == typeOfSchool.SchoolTypeId).ToList();
            if (enrollees.Count != 0)
            {
                foreach (Enrollee enrollee in enrollees)
                {
                    enrollee.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == enrollee.SpecialityId);
                    enrollee.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == enrollee.Speciality.FacultyId);
                    enrollee.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == enrollee.Speciality.FormOfStudyId);
                    enrollee.Citizenship = context.Citizenship.FirstOrDefault(c => c.CitizenshipId == enrollee.CitizenshipId);
                    enrollee.Country = context.Country.FirstOrDefault(c => c.CountryId == enrollee.CountryId);
                    enrollee.Area = context.Area.FirstOrDefault(a => a.AreaId == enrollee.AreaId);
                    enrollee.District = context.District.FirstOrDefault(d => d.DistrictId == enrollee.DistrictId);
                    enrollee.TypeOfSettlement = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == enrollee.SettlementTypeId);
                    enrollee.TypeOfStreet = context.TypeOfStreet.FirstOrDefault(ts => ts.StreetTypeId == enrollee.StreetTypeId);
                    enrollee.Document = context.Document.FirstOrDefault(d => d.DocumentId == enrollee.DocumentId);
                    enrollee.TypeOfSchool = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == enrollee.SchoolTypeId);
                    enrollee.ForeignLanguage = context.ForeignLanguage.FirstOrDefault(f => f.LanguageId == enrollee.ForeignLanguageId);
                    enrollee.ReasonForAddmission = context.ReasonForAddmission.FirstOrDefault(r => r.ReasonForAddmissionId == enrollee.ReasonForAddmissionId);
                    enrollee.ReasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == enrollee.ReasonForAddmission.ContestId);
                    enrollee.TypeOfState = context.TypeOfState.FirstOrDefault(ts => ts.StateId == enrollee.StateTypeId);
                    enrollee.Employee = context.Employee.FirstOrDefault(e => e.EmployeeId == enrollee.EmployeeId);
                    enrollee.Employee.EmployeePost = context.EmployeePost.FirstOrDefault(ep => ep.PostId == enrollee.Employee.PostId);
                    enrollee.TypeOfFinance = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == enrollee.FinanceTypeId);
                    if (enrollee.DecreeId != null) enrollee.Decree = context.Decree.FirstOrDefault(d => d.DecreeId == enrollee.DecreeId);
                    if (enrollee.SecondarySpecialityId != null) enrollee.SecondarySpeciality = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == enrollee.SecondarySpecialityId);
                    if (enrollee.TargetWorkPlaceId != null) enrollee.TargetWorkPlace = context.TargetWorkPlace.FirstOrDefault(twp => twp.TargetId == enrollee.TargetWorkPlaceId);
                }
            }
            return enrollees;
        }

        public List<Enrollee> GetEnrollees(ReasonForAddmission reasonForAddmission)
        {
            List<Enrollee> enrollees = context.Enrollee.Where(e => e.ReasonForAddmissionId == reasonForAddmission.ReasonForAddmissionId).ToList();
            if (enrollees.Count != 0)
            {
                foreach (Enrollee enrollee in enrollees)
                {
                    enrollee.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == enrollee.SpecialityId);
                    enrollee.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == enrollee.Speciality.FacultyId);
                    enrollee.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == enrollee.Speciality.FormOfStudyId);
                    enrollee.Citizenship = context.Citizenship.FirstOrDefault(c => c.CitizenshipId == enrollee.CitizenshipId);
                    enrollee.Country = context.Country.FirstOrDefault(c => c.CountryId == enrollee.CountryId);
                    enrollee.Area = context.Area.FirstOrDefault(a => a.AreaId == enrollee.AreaId);
                    enrollee.District = context.District.FirstOrDefault(d => d.DistrictId == enrollee.DistrictId);
                    enrollee.TypeOfSettlement = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == enrollee.SettlementTypeId);
                    enrollee.TypeOfStreet = context.TypeOfStreet.FirstOrDefault(ts => ts.StreetTypeId == enrollee.StreetTypeId);
                    enrollee.Document = context.Document.FirstOrDefault(d => d.DocumentId == enrollee.DocumentId);
                    enrollee.TypeOfSchool = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == enrollee.SchoolTypeId);
                    enrollee.ForeignLanguage = context.ForeignLanguage.FirstOrDefault(f => f.LanguageId == enrollee.ForeignLanguageId);
                    enrollee.ReasonForAddmission = context.ReasonForAddmission.FirstOrDefault(r => r.ReasonForAddmissionId == enrollee.ReasonForAddmissionId);
                    enrollee.ReasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == enrollee.ReasonForAddmission.ContestId);
                    enrollee.TypeOfState = context.TypeOfState.FirstOrDefault(ts => ts.StateId == enrollee.StateTypeId);
                    enrollee.Employee = context.Employee.FirstOrDefault(e => e.EmployeeId == enrollee.EmployeeId);
                    enrollee.Employee.EmployeePost = context.EmployeePost.FirstOrDefault(ep => ep.PostId == enrollee.Employee.PostId);
                    enrollee.TypeOfFinance = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == enrollee.FinanceTypeId);
                    if (enrollee.DecreeId != null) enrollee.Decree = context.Decree.FirstOrDefault(d => d.DecreeId == enrollee.DecreeId);
                    if (enrollee.SecondarySpecialityId != null) enrollee.SecondarySpeciality = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == enrollee.SecondarySpecialityId);
                    if (enrollee.TargetWorkPlaceId != null) enrollee.TargetWorkPlace = context.TargetWorkPlace.FirstOrDefault(twp => twp.TargetId == enrollee.TargetWorkPlaceId);
                }
            }
            return enrollees;
        }

        public List<Enrollee> GetEnrollees(TypeOfState typeOfState)
        {
            List<Enrollee> enrollees = context.Enrollee.Where(e => e.StateTypeId == typeOfState.StateId).ToList();
            if (enrollees.Count != 0)
            {
                foreach (Enrollee enrollee in enrollees)
                {
                    enrollee.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == enrollee.SpecialityId);
                    enrollee.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == enrollee.Speciality.FacultyId);
                    enrollee.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == enrollee.Speciality.FormOfStudyId);
                    enrollee.Citizenship = context.Citizenship.FirstOrDefault(c => c.CitizenshipId == enrollee.CitizenshipId);
                    enrollee.Country = context.Country.FirstOrDefault(c => c.CountryId == enrollee.CountryId);
                    enrollee.Area = context.Area.FirstOrDefault(a => a.AreaId == enrollee.AreaId);
                    enrollee.District = context.District.FirstOrDefault(d => d.DistrictId == enrollee.DistrictId);
                    enrollee.TypeOfSettlement = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == enrollee.SettlementTypeId);
                    enrollee.TypeOfStreet = context.TypeOfStreet.FirstOrDefault(ts => ts.StreetTypeId == enrollee.StreetTypeId);
                    enrollee.Document = context.Document.FirstOrDefault(d => d.DocumentId == enrollee.DocumentId);
                    enrollee.TypeOfSchool = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == enrollee.SchoolTypeId);
                    enrollee.ForeignLanguage = context.ForeignLanguage.FirstOrDefault(f => f.LanguageId == enrollee.ForeignLanguageId);
                    enrollee.ReasonForAddmission = context.ReasonForAddmission.FirstOrDefault(r => r.ReasonForAddmissionId == enrollee.ReasonForAddmissionId);
                    enrollee.ReasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == enrollee.ReasonForAddmission.ContestId);
                    enrollee.TypeOfState = context.TypeOfState.FirstOrDefault(ts => ts.StateId == enrollee.StateTypeId);
                    enrollee.Employee = context.Employee.FirstOrDefault(e => e.EmployeeId == enrollee.EmployeeId);
                    enrollee.Employee.EmployeePost = context.EmployeePost.FirstOrDefault(ep => ep.PostId == enrollee.Employee.PostId);
                    enrollee.TypeOfFinance = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == enrollee.FinanceTypeId);
                    if (enrollee.DecreeId != null) enrollee.Decree = context.Decree.FirstOrDefault(d => d.DecreeId == enrollee.DecreeId);
                    if (enrollee.SecondarySpecialityId != null) enrollee.SecondarySpeciality = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == enrollee.SecondarySpecialityId);
                    if (enrollee.TargetWorkPlaceId != null) enrollee.TargetWorkPlace = context.TargetWorkPlace.FirstOrDefault(twp => twp.TargetId == enrollee.TargetWorkPlaceId);
                }
            }
            return enrollees;
        }

        public List<Enrollee> GetEnrollees(Employee employee)
        {
            List<Enrollee> enrollees = context.Enrollee.Where(e => e.EmployeeId == employee.EmployeeId).ToList();
            if (enrollees.Count != 0)
            {
                foreach (Enrollee enrollee in enrollees)
                {
                    enrollee.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == enrollee.SpecialityId);
                    enrollee.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == enrollee.Speciality.FacultyId);
                    enrollee.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == enrollee.Speciality.FormOfStudyId);
                    enrollee.Citizenship = context.Citizenship.FirstOrDefault(c => c.CitizenshipId == enrollee.CitizenshipId);
                    enrollee.Country = context.Country.FirstOrDefault(c => c.CountryId == enrollee.CountryId);
                    enrollee.Area = context.Area.FirstOrDefault(a => a.AreaId == enrollee.AreaId);
                    enrollee.District = context.District.FirstOrDefault(d => d.DistrictId == enrollee.DistrictId);
                    enrollee.TypeOfSettlement = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == enrollee.SettlementTypeId);
                    enrollee.TypeOfStreet = context.TypeOfStreet.FirstOrDefault(ts => ts.StreetTypeId == enrollee.StreetTypeId);
                    enrollee.Document = context.Document.FirstOrDefault(d => d.DocumentId == enrollee.DocumentId);
                    enrollee.TypeOfSchool = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == enrollee.SchoolTypeId);
                    enrollee.ForeignLanguage = context.ForeignLanguage.FirstOrDefault(f => f.LanguageId == enrollee.ForeignLanguageId);
                    enrollee.ReasonForAddmission = context.ReasonForAddmission.FirstOrDefault(r => r.ReasonForAddmissionId == enrollee.ReasonForAddmissionId);
                    enrollee.ReasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == enrollee.ReasonForAddmission.ContestId);
                    enrollee.TypeOfState = context.TypeOfState.FirstOrDefault(ts => ts.StateId == enrollee.StateTypeId);
                    enrollee.Employee = context.Employee.FirstOrDefault(e => e.EmployeeId == enrollee.EmployeeId);
                    enrollee.Employee.EmployeePost = context.EmployeePost.FirstOrDefault(ep => ep.PostId == enrollee.Employee.PostId);
                    enrollee.TypeOfFinance = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == enrollee.FinanceTypeId);
                    if (enrollee.DecreeId != null) enrollee.Decree = context.Decree.FirstOrDefault(d => d.DecreeId == enrollee.DecreeId);
                    if (enrollee.SecondarySpecialityId != null) enrollee.SecondarySpeciality = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == enrollee.SecondarySpecialityId);
                    if (enrollee.TargetWorkPlaceId != null) enrollee.TargetWorkPlace = context.TargetWorkPlace.FirstOrDefault(twp => twp.TargetId == enrollee.TargetWorkPlaceId);
                }
            }
            return enrollees;
        }

        public List<Enrollee> GetEnrollees(Decree decree)
        {
            List<Enrollee> enrollees = context.Enrollee.Where(e => e.DecreeId == decree.DecreeId).ToList();
            if (enrollees.Count != 0)
            {
                foreach (Enrollee enrollee in enrollees)
                {
                    enrollee.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == enrollee.SpecialityId);
                    enrollee.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == enrollee.Speciality.FacultyId);
                    enrollee.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == enrollee.Speciality.FormOfStudyId);
                    enrollee.Citizenship = context.Citizenship.FirstOrDefault(c => c.CitizenshipId == enrollee.CitizenshipId);
                    enrollee.Country = context.Country.FirstOrDefault(c => c.CountryId == enrollee.CountryId);
                    enrollee.Area = context.Area.FirstOrDefault(a => a.AreaId == enrollee.AreaId);
                    enrollee.District = context.District.FirstOrDefault(d => d.DistrictId == enrollee.DistrictId);
                    enrollee.TypeOfSettlement = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == enrollee.SettlementTypeId);
                    enrollee.TypeOfStreet = context.TypeOfStreet.FirstOrDefault(ts => ts.StreetTypeId == enrollee.StreetTypeId);
                    enrollee.Document = context.Document.FirstOrDefault(d => d.DocumentId == enrollee.DocumentId);
                    enrollee.TypeOfSchool = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == enrollee.SchoolTypeId);
                    enrollee.ForeignLanguage = context.ForeignLanguage.FirstOrDefault(f => f.LanguageId == enrollee.ForeignLanguageId);
                    enrollee.ReasonForAddmission = context.ReasonForAddmission.FirstOrDefault(r => r.ReasonForAddmissionId == enrollee.ReasonForAddmissionId);
                    enrollee.ReasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == enrollee.ReasonForAddmission.ContestId);
                    enrollee.TypeOfState = context.TypeOfState.FirstOrDefault(ts => ts.StateId == enrollee.StateTypeId);
                    enrollee.Employee = context.Employee.FirstOrDefault(e => e.EmployeeId == enrollee.EmployeeId);
                    enrollee.Employee.EmployeePost = context.EmployeePost.FirstOrDefault(ep => ep.PostId == enrollee.Employee.PostId);
                    enrollee.TypeOfFinance = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == enrollee.FinanceTypeId);
                    if (enrollee.DecreeId != null) enrollee.Decree = context.Decree.FirstOrDefault(d => d.DecreeId == enrollee.DecreeId);
                    if (enrollee.SecondarySpecialityId != null) enrollee.SecondarySpeciality = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == enrollee.SecondarySpecialityId);
                    if (enrollee.TargetWorkPlaceId != null) enrollee.TargetWorkPlace = context.TargetWorkPlace.FirstOrDefault(twp => twp.TargetId == enrollee.TargetWorkPlaceId);
                }
            }
            return enrollees;
        }

        public List<Enrollee> GetEnrollees(TypeOfFinance typeOfFinance)
        {
            List<Enrollee> enrollees = context.Enrollee.Where(e => e.FinanceTypeId == typeOfFinance.FinanceTypeId).ToList();
            if (enrollees.Count != 0)
            {
                foreach (Enrollee enrollee in enrollees)
                {
                    enrollee.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == enrollee.SpecialityId);
                    enrollee.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == enrollee.Speciality.FacultyId);
                    enrollee.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == enrollee.Speciality.FormOfStudyId);
                    enrollee.Citizenship = context.Citizenship.FirstOrDefault(c => c.CitizenshipId == enrollee.CitizenshipId);
                    enrollee.Country = context.Country.FirstOrDefault(c => c.CountryId == enrollee.CountryId);
                    enrollee.Area = context.Area.FirstOrDefault(a => a.AreaId == enrollee.AreaId);
                    enrollee.District = context.District.FirstOrDefault(d => d.DistrictId == enrollee.DistrictId);
                    enrollee.TypeOfSettlement = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == enrollee.SettlementTypeId);
                    enrollee.TypeOfStreet = context.TypeOfStreet.FirstOrDefault(ts => ts.StreetTypeId == enrollee.StreetTypeId);
                    enrollee.Document = context.Document.FirstOrDefault(d => d.DocumentId == enrollee.DocumentId);
                    enrollee.TypeOfSchool = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == enrollee.SchoolTypeId);
                    enrollee.ForeignLanguage = context.ForeignLanguage.FirstOrDefault(f => f.LanguageId == enrollee.ForeignLanguageId);
                    enrollee.ReasonForAddmission = context.ReasonForAddmission.FirstOrDefault(r => r.ReasonForAddmissionId == enrollee.ReasonForAddmissionId);
                    enrollee.ReasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == enrollee.ReasonForAddmission.ContestId);
                    enrollee.TypeOfState = context.TypeOfState.FirstOrDefault(ts => ts.StateId == enrollee.StateTypeId);
                    enrollee.Employee = context.Employee.FirstOrDefault(e => e.EmployeeId == enrollee.EmployeeId);
                    enrollee.Employee.EmployeePost = context.EmployeePost.FirstOrDefault(ep => ep.PostId == enrollee.Employee.PostId);
                    enrollee.TypeOfFinance = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == enrollee.FinanceTypeId);
                    if (enrollee.DecreeId != null) enrollee.Decree = context.Decree.FirstOrDefault(d => d.DecreeId == enrollee.DecreeId);
                    if (enrollee.SecondarySpecialityId != null) enrollee.SecondarySpeciality = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == enrollee.SecondarySpecialityId);
                    if (enrollee.TargetWorkPlaceId != null) enrollee.TargetWorkPlace = context.TargetWorkPlace.FirstOrDefault(twp => twp.TargetId == enrollee.TargetWorkPlaceId);
                }
            }
            return enrollees;
        }

        public Enrollee InsertEnrollee(Enrollee enrollee)
        {
            try
            {
                context.Enrollee.Add(enrollee);
                context.SaveChanges();
                return enrollee;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public Enrollee UpdateEnrollee(Enrollee enrollee)
        {
            Enrollee enrolleeToUpdate = context.Enrollee.FirstOrDefault(e => e.EnrolleeId == enrollee.EnrolleeId);
            enrolleeToUpdate.SpecialityId = enrollee.SpecialityId;
            enrolleeToUpdate.CitizenshipId = enrollee.CitizenshipId;
            enrolleeToUpdate.CountryId = enrollee.CountryId;
            enrolleeToUpdate.AreaId = enrollee.AreaId;
            enrolleeToUpdate.DistrictId = enrollee.DistrictId;
            enrolleeToUpdate.SettlementTypeId = enrollee.SettlementTypeId;
            enrolleeToUpdate.StreetTypeId = enrollee.StreetTypeId;
            enrolleeToUpdate.DocumentId = enrollee.DocumentId;
            enrolleeToUpdate.SchoolTypeId = enrollee.SchoolTypeId;
            enrolleeToUpdate.ForeignLanguageId = enrollee.ForeignLanguageId;
            enrolleeToUpdate.ReasonForAddmissionId = enrollee.ReasonForAddmissionId;
            enrolleeToUpdate.StateTypeId = enrollee.StateTypeId;
            enrolleeToUpdate.EmployeeId = enrollee.EmployeeId;
            enrolleeToUpdate.FinanceTypeId = enrollee.FinanceTypeId;
            enrolleeToUpdate.DecreeId = enrollee.DecreeId;
            enrolleeToUpdate.SecondarySpecialityId = enrollee.SecondarySpecialityId;
            enrolleeToUpdate.TargetWorkPlaceId = enrollee.TargetWorkPlaceId;
            enrolleeToUpdate.RuSurname = enrollee.RuSurname;
            enrolleeToUpdate.RuName = enrollee.RuName;
            enrolleeToUpdate.RuPatronymic = enrollee.RuPatronymic;
            enrolleeToUpdate.BlrSurname = enrollee.BlrSurname;
            enrolleeToUpdate.BlrName = enrollee.BlrName;
            enrolleeToUpdate.BlrPatronymic = enrollee.BlrPatronymic;
            enrolleeToUpdate.Gender = enrollee.Gender;
            enrolleeToUpdate.DateOfBirthday = enrollee.DateOfBirthday;
            enrolleeToUpdate.FatherFullname = enrollee.FatherFullname;
            enrolleeToUpdate.FatherAddress = enrollee.FatherAddress;
            enrolleeToUpdate.MotherFullname = enrollee.MotherFullname;
            enrolleeToUpdate.MotherAddress = enrollee.MotherAddress;
            enrolleeToUpdate.SettlementName = enrollee.SettlementName;
            enrolleeToUpdate.SettlementIndex = enrollee.SettlementIndex;
            enrolleeToUpdate.StreetName = enrollee.StreetName;
            enrolleeToUpdate.NumberHouse = enrollee.NumberHouse;
            enrolleeToUpdate.NumberFlat = enrollee.NumberFlat;
            enrolleeToUpdate.HomePhone = enrollee.HomePhone;
            enrolleeToUpdate.MobilePhone = enrollee.MobilePhone;
            enrolleeToUpdate.DocumentSeria = enrollee.DocumentSeria;
            enrolleeToUpdate.DocumentNumber = enrollee.DocumentNumber;
            enrolleeToUpdate.DocumentDate = enrollee.DocumentDate;
            enrolleeToUpdate.DocumentWhoGave = enrollee.DocumentWhoGave;
            enrolleeToUpdate.DocumentPersonalNumber = enrollee.DocumentPersonalNumber;
            enrolleeToUpdate.SchoolName = enrollee.SchoolName;
            enrolleeToUpdate.SchoolYear = enrollee.SchoolYear;
            enrolleeToUpdate.SchoolAddress = enrollee.SchoolAddress;
            enrolleeToUpdate.IsBRSM = enrollee.IsBRSM;
            enrolleeToUpdate.NumberOfDeal = enrollee.NumberOfDeal;
            enrolleeToUpdate.DateDeal = enrollee.DateDeal;
            enrolleeToUpdate.StateDateChange = enrollee.StateDateChange;
            enrolleeToUpdate.PersonInCharge = enrollee.PersonInCharge;
            enrolleeToUpdate.WorkPlace = enrollee.WorkPlace;
            enrolleeToUpdate.WorkPost = enrollee.WorkPost;
            enrolleeToUpdate.Seniority = enrollee.Seniority;
            enrolleeToUpdate.CurrentNumberCurs = enrollee.CurrentNumberCurs;
            enrolleeToUpdate.CurrentUniversity = enrollee.CurrentUniversity;
            enrolleeToUpdate.CurrentSpeciality = enrollee.CurrentSpeciality;
            enrolleeToUpdate.AttestatEstimationString = enrollee.AttestatEstimationString;
            enrolleeToUpdate.DiplomPtuEstimationString = enrollee.DiplomPtuEstimationString;
            enrolleeToUpdate.DiplomSusEstimationString = enrollee.DiplomSusEstimationString;
            enrolleeToUpdate.BeforeEnrollSpecialityId = enrollee.BeforeEnrollSpecialityId;
            enrolleeToUpdate.BeforeEnrollNumberOfDeal = enrollee.BeforeEnrollNumberOfDeal;
            context.SaveChanges();
            return enrolleeToUpdate;
        }
    }
}
