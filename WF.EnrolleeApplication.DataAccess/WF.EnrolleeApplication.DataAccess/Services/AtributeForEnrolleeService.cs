using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class AtributeForEnrolleeService : IAtributeForEnrolleeService
    {
        private EnrolleeContext context;

        public AtributeForEnrolleeService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        public void DeleteAtributeForEnrollee(AtributeForEnrollee atributeForEnrollee)
        {
            AtributeForEnrollee atributeForEnrolleeToDelete = context.AtributeForEnrollee.FirstOrDefault(a => a.Id == atributeForEnrollee.Id);
            context.AtributeForEnrollee.Remove(atributeForEnrolleeToDelete);
            context.SaveChanges();
        }

        public AtributeForEnrollee GetAtributeForEnrollee(int id)
        {
            AtributeForEnrollee atributeForEnrollee = context.AtributeForEnrollee.FirstOrDefault(a => a.Id == id);
            if (atributeForEnrollee != null)
            {
                atributeForEnrollee.Atribute = context.Atribute.FirstOrDefault(a => a.AtributeId == atributeForEnrollee.AtributeId);
                atributeForEnrollee.Enrollee = context.Enrollee.FirstOrDefault(e => e.EnrolleeId == atributeForEnrollee.EnrolleeId);
                atributeForEnrollee.Enrollee = context.Enrollee.FirstOrDefault(e => e.EnrolleeId == atributeForEnrollee.EnrolleeId);
                atributeForEnrollee.Enrollee.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == atributeForEnrollee.Enrollee.SpecialityId);
                atributeForEnrollee.Enrollee.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == atributeForEnrollee.Enrollee.Speciality.FacultyId);
                atributeForEnrollee.Enrollee.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == atributeForEnrollee.Enrollee.Speciality.FormOfStudyId);
                atributeForEnrollee.Enrollee.Citizenship = context.Citizenship.FirstOrDefault(e => e.CitizenshipId == atributeForEnrollee.Enrollee.CitizenshipId);
                atributeForEnrollee.Enrollee.Country = context.Country.FirstOrDefault(c => c.CountryId == atributeForEnrollee.Enrollee.CountryId);
                atributeForEnrollee.Enrollee.Area = context.Area.FirstOrDefault(a => a.AreaId == atributeForEnrollee.Enrollee.AreaId);
                atributeForEnrollee.Enrollee.District = context.District.FirstOrDefault(d => d.DistrictId == atributeForEnrollee.Enrollee.DistrictId);
                atributeForEnrollee.Enrollee.TypeOfSettlement = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == atributeForEnrollee.Enrollee.SettlementTypeId);
                atributeForEnrollee.Enrollee.TypeOfStreet = context.TypeOfStreet.FirstOrDefault(ts => ts.SteetTypeId == atributeForEnrollee.Enrollee.SteetTypeId);
                atributeForEnrollee.Enrollee.Document = context.Document.FirstOrDefault(d => d.DocumentId == atributeForEnrollee.Enrollee.DocumentId);
                atributeForEnrollee.Enrollee.TypeOfSchool = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == atributeForEnrollee.Enrollee.SchoolTypeId);
                atributeForEnrollee.Enrollee.ForeignLanguage = context.ForeignLanguage.FirstOrDefault(fl => fl.LanguageId == atributeForEnrollee.Enrollee.ForeignLanguageId);
                atributeForEnrollee.Enrollee.ReasonForAddmission = context.ReasonForAddmission.FirstOrDefault(rfa => rfa.ReasonForAddmissionId == atributeForEnrollee.Enrollee.ReasonForAddmissionId);
                atributeForEnrollee.Enrollee.ReasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == atributeForEnrollee.Enrollee.ReasonForAddmission.ContestId);
                atributeForEnrollee.Enrollee.TypeOfState = context.TypeOfState.FirstOrDefault(ts => ts.StateId == atributeForEnrollee.Enrollee.StateTypeId);
                atributeForEnrollee.Enrollee.Employee = context.Employee.FirstOrDefault(e => e.EmployeeId == atributeForEnrollee.Enrollee.EmployeeId);
                atributeForEnrollee.Enrollee.Employee.EmployeePost = context.EmployeePost.FirstOrDefault(e => e.PostId == atributeForEnrollee.Enrollee.Employee.PostId);
                atributeForEnrollee.Enrollee.TypeOfFinance = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == atributeForEnrollee.Enrollee.FinanceTypeId);
                if (atributeForEnrollee.Enrollee.DecreeId.HasValue) atributeForEnrollee.Enrollee.Decree = context.Decree.FirstOrDefault(d => d.DecreeId == atributeForEnrollee.Enrollee.DecreeId);
                if (atributeForEnrollee.Enrollee.SecondarySpecialityId.HasValue) atributeForEnrollee.Enrollee.SecondarySpeciality = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == atributeForEnrollee.Enrollee.SecondarySpecialityId);
                if (atributeForEnrollee.Enrollee.TargetWorkPlaceId.HasValue) atributeForEnrollee.Enrollee.TargetWorkPlace = context.TargetWorkPlace.FirstOrDefault(tw => tw.TargetId == atributeForEnrollee.Enrollee.TargetWorkPlaceId);
            }
            return atributeForEnrollee;
        }

        public AtributeForEnrollee GetAtributeForEnrollee(Atribute atribute, Enrollee enrollee)
        {
            AtributeForEnrollee atributeForEnrollee = context.AtributeForEnrollee.FirstOrDefault(a => a.AtributeId == atribute.AtributeId && a.EnrolleeId == enrollee.EnrolleeId);
            if (atributeForEnrollee != null)
            {
                atributeForEnrollee.Atribute = context.Atribute.FirstOrDefault(a => a.AtributeId == atributeForEnrollee.AtributeId);
                atributeForEnrollee.Enrollee = context.Enrollee.FirstOrDefault(e => e.EnrolleeId == atributeForEnrollee.EnrolleeId);
                atributeForEnrollee.Enrollee = context.Enrollee.FirstOrDefault(e => e.EnrolleeId == atributeForEnrollee.EnrolleeId);
                atributeForEnrollee.Enrollee.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == atributeForEnrollee.Enrollee.SpecialityId);
                atributeForEnrollee.Enrollee.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == atributeForEnrollee.Enrollee.Speciality.FacultyId);
                atributeForEnrollee.Enrollee.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == atributeForEnrollee.Enrollee.Speciality.FormOfStudyId);
                atributeForEnrollee.Enrollee.Citizenship = context.Citizenship.FirstOrDefault(e => e.CitizenshipId == atributeForEnrollee.Enrollee.CitizenshipId);
                atributeForEnrollee.Enrollee.Country = context.Country.FirstOrDefault(c => c.CountryId == atributeForEnrollee.Enrollee.CountryId);
                atributeForEnrollee.Enrollee.Area = context.Area.FirstOrDefault(a => a.AreaId == atributeForEnrollee.Enrollee.AreaId);
                atributeForEnrollee.Enrollee.District = context.District.FirstOrDefault(d => d.DistrictId == atributeForEnrollee.Enrollee.DistrictId);
                atributeForEnrollee.Enrollee.TypeOfSettlement = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == atributeForEnrollee.Enrollee.SettlementTypeId);
                atributeForEnrollee.Enrollee.TypeOfStreet = context.TypeOfStreet.FirstOrDefault(ts => ts.SteetTypeId == atributeForEnrollee.Enrollee.SteetTypeId);
                atributeForEnrollee.Enrollee.Document = context.Document.FirstOrDefault(d => d.DocumentId == atributeForEnrollee.Enrollee.DocumentId);
                atributeForEnrollee.Enrollee.TypeOfSchool = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == atributeForEnrollee.Enrollee.SchoolTypeId);
                atributeForEnrollee.Enrollee.ForeignLanguage = context.ForeignLanguage.FirstOrDefault(fl => fl.LanguageId == atributeForEnrollee.Enrollee.ForeignLanguageId);
                atributeForEnrollee.Enrollee.ReasonForAddmission = context.ReasonForAddmission.FirstOrDefault(rfa => rfa.ReasonForAddmissionId == atributeForEnrollee.Enrollee.ReasonForAddmissionId);
                atributeForEnrollee.Enrollee.ReasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == atributeForEnrollee.Enrollee.ReasonForAddmission.ContestId);
                atributeForEnrollee.Enrollee.TypeOfState = context.TypeOfState.FirstOrDefault(ts => ts.StateId == atributeForEnrollee.Enrollee.StateTypeId);
                atributeForEnrollee.Enrollee.Employee = context.Employee.FirstOrDefault(e => e.EmployeeId == atributeForEnrollee.Enrollee.EmployeeId);
                atributeForEnrollee.Enrollee.Employee.EmployeePost = context.EmployeePost.FirstOrDefault(e => e.PostId == atributeForEnrollee.Enrollee.Employee.PostId);
                atributeForEnrollee.Enrollee.TypeOfFinance = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == atributeForEnrollee.Enrollee.FinanceTypeId);
                if (atributeForEnrollee.Enrollee.DecreeId.HasValue) atributeForEnrollee.Enrollee.Decree = context.Decree.FirstOrDefault(d => d.DecreeId == atributeForEnrollee.Enrollee.DecreeId);
                if (atributeForEnrollee.Enrollee.SecondarySpecialityId.HasValue) atributeForEnrollee.Enrollee.SecondarySpeciality = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == atributeForEnrollee.Enrollee.SecondarySpecialityId);
                if (atributeForEnrollee.Enrollee.TargetWorkPlaceId.HasValue) atributeForEnrollee.Enrollee.TargetWorkPlace = context.TargetWorkPlace.FirstOrDefault(tw => tw.TargetId == atributeForEnrollee.Enrollee.TargetWorkPlaceId);
            }
            return atributeForEnrollee;
        }

        public List<AtributeForEnrollee> GetAtributeForEnrollees()
        {
            List<AtributeForEnrollee> atributesForEnrollee = context.AtributeForEnrollee.ToList();
            if (atributesForEnrollee.Count != 0)
            {
                foreach (var atributeForEnrollee in atributesForEnrollee)
                {
                    if (atributeForEnrollee != null)
                    {
                        atributeForEnrollee.Atribute = context.Atribute.FirstOrDefault(a => a.AtributeId == atributeForEnrollee.AtributeId);
                        atributeForEnrollee.Enrollee = context.Enrollee.FirstOrDefault(e => e.EnrolleeId == atributeForEnrollee.EnrolleeId);
                        atributeForEnrollee.Enrollee = context.Enrollee.FirstOrDefault(e => e.EnrolleeId == atributeForEnrollee.EnrolleeId);
                        atributeForEnrollee.Enrollee.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == atributeForEnrollee.Enrollee.SpecialityId);
                        atributeForEnrollee.Enrollee.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == atributeForEnrollee.Enrollee.Speciality.FacultyId);
                        atributeForEnrollee.Enrollee.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == atributeForEnrollee.Enrollee.Speciality.FormOfStudyId);
                        atributeForEnrollee.Enrollee.Citizenship = context.Citizenship.FirstOrDefault(e => e.CitizenshipId == atributeForEnrollee.Enrollee.CitizenshipId);
                        atributeForEnrollee.Enrollee.Country = context.Country.FirstOrDefault(c => c.CountryId == atributeForEnrollee.Enrollee.CountryId);
                        atributeForEnrollee.Enrollee.Area = context.Area.FirstOrDefault(a => a.AreaId == atributeForEnrollee.Enrollee.AreaId);
                        atributeForEnrollee.Enrollee.District = context.District.FirstOrDefault(d => d.DistrictId == atributeForEnrollee.Enrollee.DistrictId);
                        atributeForEnrollee.Enrollee.TypeOfSettlement = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == atributeForEnrollee.Enrollee.SettlementTypeId);
                        atributeForEnrollee.Enrollee.TypeOfStreet = context.TypeOfStreet.FirstOrDefault(ts => ts.SteetTypeId == atributeForEnrollee.Enrollee.SteetTypeId);
                        atributeForEnrollee.Enrollee.Document = context.Document.FirstOrDefault(d => d.DocumentId == atributeForEnrollee.Enrollee.DocumentId);
                        atributeForEnrollee.Enrollee.TypeOfSchool = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == atributeForEnrollee.Enrollee.SchoolTypeId);
                        atributeForEnrollee.Enrollee.ForeignLanguage = context.ForeignLanguage.FirstOrDefault(fl => fl.LanguageId == atributeForEnrollee.Enrollee.ForeignLanguageId);
                        atributeForEnrollee.Enrollee.ReasonForAddmission = context.ReasonForAddmission.FirstOrDefault(rfa => rfa.ReasonForAddmissionId == atributeForEnrollee.Enrollee.ReasonForAddmissionId);
                        atributeForEnrollee.Enrollee.ReasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == atributeForEnrollee.Enrollee.ReasonForAddmission.ContestId);
                        atributeForEnrollee.Enrollee.TypeOfState = context.TypeOfState.FirstOrDefault(ts => ts.StateId == atributeForEnrollee.Enrollee.StateTypeId);
                        atributeForEnrollee.Enrollee.Employee = context.Employee.FirstOrDefault(e => e.EmployeeId == atributeForEnrollee.Enrollee.EmployeeId);
                        atributeForEnrollee.Enrollee.Employee.EmployeePost = context.EmployeePost.FirstOrDefault(e => e.PostId == atributeForEnrollee.Enrollee.Employee.PostId);
                        atributeForEnrollee.Enrollee.TypeOfFinance = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == atributeForEnrollee.Enrollee.FinanceTypeId);
                        if (atributeForEnrollee.Enrollee.DecreeId.HasValue) atributeForEnrollee.Enrollee.Decree = context.Decree.FirstOrDefault(d => d.DecreeId == atributeForEnrollee.Enrollee.DecreeId);
                        if (atributeForEnrollee.Enrollee.SecondarySpecialityId.HasValue) atributeForEnrollee.Enrollee.SecondarySpeciality = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == atributeForEnrollee.Enrollee.SecondarySpecialityId);
                        if (atributeForEnrollee.Enrollee.TargetWorkPlaceId.HasValue) atributeForEnrollee.Enrollee.TargetWorkPlace = context.TargetWorkPlace.FirstOrDefault(tw => tw.TargetId == atributeForEnrollee.Enrollee.TargetWorkPlaceId);
                    }
                }
            }
            return atributesForEnrollee;
        }

        public List<AtributeForEnrollee> GetAtributeForEnrollees(Enrollee enrollee)
        {
            List<AtributeForEnrollee> atributesForEnrollee = null;
            atributesForEnrollee = context.AtributeForEnrollee.Where(a => a.EnrolleeId == enrollee.EnrolleeId).ToList();
            if (atributesForEnrollee.Count != 0)
            {
                foreach (var atributeForEnrollee in atributesForEnrollee)
                {
                    if (atributeForEnrollee != null)
                    {
                        atributeForEnrollee.Atribute = context.Atribute.FirstOrDefault(a => a.AtributeId == atributeForEnrollee.AtributeId);
                        atributeForEnrollee.Enrollee = context.Enrollee.FirstOrDefault(e => e.EnrolleeId == atributeForEnrollee.EnrolleeId);
                        atributeForEnrollee.Enrollee = context.Enrollee.FirstOrDefault(e => e.EnrolleeId == atributeForEnrollee.EnrolleeId);
                        atributeForEnrollee.Enrollee.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == atributeForEnrollee.Enrollee.SpecialityId);
                        atributeForEnrollee.Enrollee.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == atributeForEnrollee.Enrollee.Speciality.FacultyId);
                        atributeForEnrollee.Enrollee.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == atributeForEnrollee.Enrollee.Speciality.FormOfStudyId);
                        atributeForEnrollee.Enrollee.Citizenship = context.Citizenship.FirstOrDefault(e => e.CitizenshipId == atributeForEnrollee.Enrollee.CitizenshipId);
                        atributeForEnrollee.Enrollee.Country = context.Country.FirstOrDefault(c => c.CountryId == atributeForEnrollee.Enrollee.CountryId);
                        atributeForEnrollee.Enrollee.Area = context.Area.FirstOrDefault(a => a.AreaId == atributeForEnrollee.Enrollee.AreaId);
                        atributeForEnrollee.Enrollee.District = context.District.FirstOrDefault(d => d.DistrictId == atributeForEnrollee.Enrollee.DistrictId);
                        atributeForEnrollee.Enrollee.TypeOfSettlement = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == atributeForEnrollee.Enrollee.SettlementTypeId);
                        atributeForEnrollee.Enrollee.TypeOfStreet = context.TypeOfStreet.FirstOrDefault(ts => ts.SteetTypeId == atributeForEnrollee.Enrollee.SteetTypeId);
                        atributeForEnrollee.Enrollee.Document = context.Document.FirstOrDefault(d => d.DocumentId == atributeForEnrollee.Enrollee.DocumentId);
                        atributeForEnrollee.Enrollee.TypeOfSchool = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == atributeForEnrollee.Enrollee.SchoolTypeId);
                        atributeForEnrollee.Enrollee.ForeignLanguage = context.ForeignLanguage.FirstOrDefault(fl => fl.LanguageId == atributeForEnrollee.Enrollee.ForeignLanguageId);
                        atributeForEnrollee.Enrollee.ReasonForAddmission = context.ReasonForAddmission.FirstOrDefault(rfa => rfa.ReasonForAddmissionId == atributeForEnrollee.Enrollee.ReasonForAddmissionId);
                        atributeForEnrollee.Enrollee.ReasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == atributeForEnrollee.Enrollee.ReasonForAddmission.ContestId);
                        atributeForEnrollee.Enrollee.TypeOfState = context.TypeOfState.FirstOrDefault(ts => ts.StateId == atributeForEnrollee.Enrollee.StateTypeId);
                        atributeForEnrollee.Enrollee.Employee = context.Employee.FirstOrDefault(e => e.EmployeeId == atributeForEnrollee.Enrollee.EmployeeId);
                        atributeForEnrollee.Enrollee.Employee.EmployeePost = context.EmployeePost.FirstOrDefault(e => e.PostId == atributeForEnrollee.Enrollee.Employee.PostId);
                        atributeForEnrollee.Enrollee.TypeOfFinance = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == atributeForEnrollee.Enrollee.FinanceTypeId);
                        if (atributeForEnrollee.Enrollee.DecreeId.HasValue) atributeForEnrollee.Enrollee.Decree = context.Decree.FirstOrDefault(d => d.DecreeId == atributeForEnrollee.Enrollee.DecreeId);
                        if (atributeForEnrollee.Enrollee.SecondarySpecialityId.HasValue) atributeForEnrollee.Enrollee.SecondarySpeciality = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == atributeForEnrollee.Enrollee.SecondarySpecialityId);
                        if (atributeForEnrollee.Enrollee.TargetWorkPlaceId.HasValue) atributeForEnrollee.Enrollee.TargetWorkPlace = context.TargetWorkPlace.FirstOrDefault(tw => tw.TargetId == atributeForEnrollee.Enrollee.TargetWorkPlaceId);
                    }
                }
            }
            return atributesForEnrollee;
        }

        public List<AtributeForEnrollee> GetAtributeForEnrollees(Atribute atribute)
        {
            List<AtributeForEnrollee> atributesForEnrollee = null;
            atributesForEnrollee = context.AtributeForEnrollee.Where(a => a.AtributeId == atribute.AtributeId).ToList();
            if (atributesForEnrollee.Count != 0)
            {
                foreach (var atributeForEnrollee in atributesForEnrollee)
                {
                    if (atributeForEnrollee != null)
                    {
                        atributeForEnrollee.Atribute = context.Atribute.FirstOrDefault(a => a.AtributeId == atributeForEnrollee.AtributeId);
                        atributeForEnrollee.Enrollee = context.Enrollee.FirstOrDefault(e => e.EnrolleeId == atributeForEnrollee.EnrolleeId);
                        atributeForEnrollee.Enrollee = context.Enrollee.FirstOrDefault(e => e.EnrolleeId == atributeForEnrollee.EnrolleeId);
                        atributeForEnrollee.Enrollee.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == atributeForEnrollee.Enrollee.SpecialityId);
                        atributeForEnrollee.Enrollee.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == atributeForEnrollee.Enrollee.Speciality.FacultyId);
                        atributeForEnrollee.Enrollee.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == atributeForEnrollee.Enrollee.Speciality.FormOfStudyId);
                        atributeForEnrollee.Enrollee.Citizenship = context.Citizenship.FirstOrDefault(e => e.CitizenshipId == atributeForEnrollee.Enrollee.CitizenshipId);
                        atributeForEnrollee.Enrollee.Country = context.Country.FirstOrDefault(c => c.CountryId == atributeForEnrollee.Enrollee.CountryId);
                        atributeForEnrollee.Enrollee.Area = context.Area.FirstOrDefault(a => a.AreaId == atributeForEnrollee.Enrollee.AreaId);
                        atributeForEnrollee.Enrollee.District = context.District.FirstOrDefault(d => d.DistrictId == atributeForEnrollee.Enrollee.DistrictId);
                        atributeForEnrollee.Enrollee.TypeOfSettlement = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == atributeForEnrollee.Enrollee.SettlementTypeId);
                        atributeForEnrollee.Enrollee.TypeOfStreet = context.TypeOfStreet.FirstOrDefault(ts => ts.SteetTypeId == atributeForEnrollee.Enrollee.SteetTypeId);
                        atributeForEnrollee.Enrollee.Document = context.Document.FirstOrDefault(d => d.DocumentId == atributeForEnrollee.Enrollee.DocumentId);
                        atributeForEnrollee.Enrollee.TypeOfSchool = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == atributeForEnrollee.Enrollee.SchoolTypeId);
                        atributeForEnrollee.Enrollee.ForeignLanguage = context.ForeignLanguage.FirstOrDefault(fl => fl.LanguageId == atributeForEnrollee.Enrollee.ForeignLanguageId);
                        atributeForEnrollee.Enrollee.ReasonForAddmission = context.ReasonForAddmission.FirstOrDefault(rfa => rfa.ReasonForAddmissionId == atributeForEnrollee.Enrollee.ReasonForAddmissionId);
                        atributeForEnrollee.Enrollee.ReasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == atributeForEnrollee.Enrollee.ReasonForAddmission.ContestId);
                        atributeForEnrollee.Enrollee.TypeOfState = context.TypeOfState.FirstOrDefault(ts => ts.StateId == atributeForEnrollee.Enrollee.StateTypeId);
                        atributeForEnrollee.Enrollee.Employee = context.Employee.FirstOrDefault(e => e.EmployeeId == atributeForEnrollee.Enrollee.EmployeeId);
                        atributeForEnrollee.Enrollee.Employee.EmployeePost = context.EmployeePost.FirstOrDefault(e => e.PostId == atributeForEnrollee.Enrollee.Employee.PostId);
                        atributeForEnrollee.Enrollee.TypeOfFinance = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == atributeForEnrollee.Enrollee.FinanceTypeId);
                        if (atributeForEnrollee.Enrollee.DecreeId.HasValue) atributeForEnrollee.Enrollee.Decree = context.Decree.FirstOrDefault(d => d.DecreeId == atributeForEnrollee.Enrollee.DecreeId);
                        if (atributeForEnrollee.Enrollee.SecondarySpecialityId.HasValue) atributeForEnrollee.Enrollee.SecondarySpeciality = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == atributeForEnrollee.Enrollee.SecondarySpecialityId);
                        if (atributeForEnrollee.Enrollee.TargetWorkPlaceId.HasValue) atributeForEnrollee.Enrollee.TargetWorkPlace = context.TargetWorkPlace.FirstOrDefault(tw => tw.TargetId == atributeForEnrollee.Enrollee.TargetWorkPlaceId);
                    }
                }
            }
            return atributesForEnrollee;
        }

        public AtributeForEnrollee InsertAtributeForEnrollee(AtributeForEnrollee atributeForEnrollee)
        {
            context.AtributeForEnrollee.Add(atributeForEnrollee);
            context.SaveChanges();
            return atributeForEnrollee;
        }

        public AtributeForEnrollee UpdateAtributeForEnrollee(AtributeForEnrollee atributeForEnrollee)
        {
            AtributeForEnrollee atributeForEnrolleeToUpdate = context.AtributeForEnrollee.FirstOrDefault(a => a.Id == atributeForEnrollee.Id);
            atributeForEnrolleeToUpdate.AtributeId = atributeForEnrollee.AtributeId;
            atributeForEnrolleeToUpdate.EnrolleeId = atributeForEnrollee.EnrolleeId;
            return atributeForEnrolleeToUpdate;
        }
    }
}
