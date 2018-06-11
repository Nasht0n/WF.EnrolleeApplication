using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class AssessmentService : IAssessmentService
    {
        private EnrolleeContext context;
        public AssessmentService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        public void DeleteAssessment(Assessment assessment)
        {
            Assessment assessmentToDelete = context.Assessment.FirstOrDefault(a => a.AssessmentId == assessment.AssessmentId);
            context.Assessment.Remove(assessmentToDelete);
            context.SaveChanges();
        }

        public Assessment GetAssessment(int id)
        {
            Assessment assessment = context.Assessment.FirstOrDefault(a => a.AssessmentId == id);
            if (assessment != null)
            {
                assessment.Discipline = context.Discipline.FirstOrDefault(d => d.DisciplineId == assessment.DisciplineId);
                assessment.Discipline.BasisForAssessing = context.BasisForAssessing.FirstOrDefault(b => b.BasisForAssessingId == assessment.Discipline.BasisForAssessingId);
                assessment.Enrollee = context.Enrollee.FirstOrDefault(e => e.EnrolleeId == assessment.EnrolleeId);
                assessment.Enrollee.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == assessment.Enrollee.SpecialityId);
                assessment.Enrollee.Speciality.Faculty = context.Faculty.FirstOrDefault(f=>f.FacultyId == assessment.Enrollee.Speciality.FacultyId);
                assessment.Enrollee.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f=>f.FormOfStudyId == assessment.Enrollee.Speciality.FormOfStudyId);
                assessment.Enrollee.Citizenship = context.Citizenship.FirstOrDefault(e=>e.CitizenshipId == assessment.Enrollee.CitizenshipId);
                assessment.Enrollee.Country = context.Country.FirstOrDefault(c=>c.CountryId == assessment.Enrollee.CountryId);
                assessment.Enrollee.Area = context.Area.FirstOrDefault(a=>a.AreaId == assessment.Enrollee.AreaId);
                assessment.Enrollee.District = context.District.FirstOrDefault(d=>d.DistrictId == assessment.Enrollee.DistrictId);
                assessment.Enrollee.TypeOfSettlement = context.TypeOfSettlement.FirstOrDefault(ts=>ts.SettlementTypeId == assessment.Enrollee.SettlementTypeId);
                assessment.Enrollee.TypeOfStreet = context.TypeOfStreet.FirstOrDefault(ts=>ts.StreetTypeId == assessment.Enrollee.StreetTypeId);
                assessment.Enrollee.Document = context.Document.FirstOrDefault(d=>d.DocumentId == assessment.Enrollee.DocumentId);
                assessment.Enrollee.TypeOfSchool = context.TypeOfSchool.FirstOrDefault(ts=>ts.SchoolTypeId == assessment.Enrollee.SchoolTypeId);
                assessment.Enrollee.ForeignLanguage = context.ForeignLanguage.FirstOrDefault(fl=>fl.LanguageId == assessment.Enrollee.ForeignLanguageId);
                assessment.Enrollee.ReasonForAddmission = context.ReasonForAddmission.FirstOrDefault(rfa=>rfa.ReasonForAddmissionId == assessment.Enrollee.ReasonForAddmissionId);
                assessment.Enrollee.ReasonForAddmission.Contest = context.Contest.FirstOrDefault(c=>c.ContestId == assessment.Enrollee.ReasonForAddmission.ContestId);
                assessment.Enrollee.TypeOfState = context.TypeOfState.FirstOrDefault(ts=>ts.StateId == assessment.Enrollee.StateTypeId);
                assessment.Enrollee.Employee = context.Employee.FirstOrDefault(e=>e.EmployeeId == assessment.Enrollee.EmployeeId);
                assessment.Enrollee.Employee.EmployeePost = context.EmployeePost.FirstOrDefault(e=>e.PostId == assessment.Enrollee.Employee.PostId);
                assessment.Enrollee.TypeOfFinance = context.TypeOfFinance.FirstOrDefault(tf=>tf.FinanceTypeId == assessment.Enrollee.FinanceTypeId);
                if (assessment.Enrollee.DecreeId.HasValue) assessment.Enrollee.Decree = context.Decree.FirstOrDefault(d=>d.DecreeId == assessment.Enrollee.DecreeId);
                if (assessment.Enrollee.SecondarySpecialityId.HasValue) assessment.Enrollee.SecondarySpeciality = context.SecondarySpeciality.FirstOrDefault(ss=>ss.SecondarySpecialityId == assessment.Enrollee.SecondarySpecialityId);
                if (assessment.Enrollee.TargetWorkPlaceId.HasValue) assessment.Enrollee.TargetWorkPlace = context.TargetWorkPlace.FirstOrDefault(tw=>tw.TargetId == assessment.Enrollee.TargetWorkPlaceId);
            }
            return assessment;
        }

        public Assessment GetAssessment(string sertcode)
        {
            Assessment assessment = context.Assessment.FirstOrDefault(a => a.SertCode == sertcode);
            if (assessment != null)
            {
                assessment.Discipline = context.Discipline.FirstOrDefault(d => d.DisciplineId == assessment.DisciplineId);
                assessment.Discipline.BasisForAssessing = context.BasisForAssessing.FirstOrDefault(b => b.BasisForAssessingId == assessment.Discipline.BasisForAssessingId);
                assessment.Enrollee = context.Enrollee.FirstOrDefault(e => e.EnrolleeId == assessment.EnrolleeId);
                assessment.Enrollee.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == assessment.Enrollee.SpecialityId);
                assessment.Enrollee.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == assessment.Enrollee.Speciality.FacultyId);
                assessment.Enrollee.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == assessment.Enrollee.Speciality.FormOfStudyId);
                assessment.Enrollee.Citizenship = context.Citizenship.FirstOrDefault(e => e.CitizenshipId == assessment.Enrollee.CitizenshipId);
                assessment.Enrollee.Country = context.Country.FirstOrDefault(c => c.CountryId == assessment.Enrollee.CountryId);
                assessment.Enrollee.Area = context.Area.FirstOrDefault(a => a.AreaId == assessment.Enrollee.AreaId);
                assessment.Enrollee.District = context.District.FirstOrDefault(d => d.DistrictId == assessment.Enrollee.DistrictId);
                assessment.Enrollee.TypeOfSettlement = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == assessment.Enrollee.SettlementTypeId);
                assessment.Enrollee.TypeOfStreet = context.TypeOfStreet.FirstOrDefault(ts => ts.StreetTypeId == assessment.Enrollee.StreetTypeId);
                assessment.Enrollee.Document = context.Document.FirstOrDefault(d => d.DocumentId == assessment.Enrollee.DocumentId);
                assessment.Enrollee.TypeOfSchool = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == assessment.Enrollee.SchoolTypeId);
                assessment.Enrollee.ForeignLanguage = context.ForeignLanguage.FirstOrDefault(fl => fl.LanguageId == assessment.Enrollee.ForeignLanguageId);
                assessment.Enrollee.ReasonForAddmission = context.ReasonForAddmission.FirstOrDefault(rfa => rfa.ReasonForAddmissionId == assessment.Enrollee.ReasonForAddmissionId);
                assessment.Enrollee.ReasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == assessment.Enrollee.ReasonForAddmission.ContestId);
                assessment.Enrollee.TypeOfState = context.TypeOfState.FirstOrDefault(ts => ts.StateId == assessment.Enrollee.StateTypeId);
                assessment.Enrollee.Employee = context.Employee.FirstOrDefault(e => e.EmployeeId == assessment.Enrollee.EmployeeId);
                assessment.Enrollee.Employee.EmployeePost = context.EmployeePost.FirstOrDefault(e => e.PostId == assessment.Enrollee.Employee.PostId);
                assessment.Enrollee.TypeOfFinance = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == assessment.Enrollee.FinanceTypeId);
                if (assessment.Enrollee.DecreeId.HasValue) assessment.Enrollee.Decree = context.Decree.FirstOrDefault(d => d.DecreeId == assessment.Enrollee.DecreeId);
                if (assessment.Enrollee.SecondarySpecialityId.HasValue) assessment.Enrollee.SecondarySpeciality = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == assessment.Enrollee.SecondarySpecialityId);
                if (assessment.Enrollee.TargetWorkPlaceId.HasValue) assessment.Enrollee.TargetWorkPlace = context.TargetWorkPlace.FirstOrDefault(tw => tw.TargetId == assessment.Enrollee.TargetWorkPlaceId);
            }
            return assessment;
        }

        public Assessment GetAssessment(Discipline discipline, Enrollee enrollee)
        {
            Assessment assessment = context.Assessment.FirstOrDefault(a => a.DisciplineId == discipline.DisciplineId && a.EnrolleeId == enrollee.EnrolleeId);
            if (assessment != null)
            {
                assessment.Discipline = context.Discipline.FirstOrDefault(d => d.DisciplineId == assessment.DisciplineId);
                assessment.Discipline.BasisForAssessing = context.BasisForAssessing.FirstOrDefault(b => b.BasisForAssessingId == assessment.Discipline.BasisForAssessingId);
                assessment.Enrollee = context.Enrollee.FirstOrDefault(e => e.EnrolleeId == assessment.EnrolleeId);
                assessment.Enrollee.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == assessment.Enrollee.SpecialityId);
                assessment.Enrollee.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == assessment.Enrollee.Speciality.FacultyId);
                assessment.Enrollee.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == assessment.Enrollee.Speciality.FormOfStudyId);
                assessment.Enrollee.Citizenship = context.Citizenship.FirstOrDefault(e => e.CitizenshipId == assessment.Enrollee.CitizenshipId);
                assessment.Enrollee.Country = context.Country.FirstOrDefault(c => c.CountryId == assessment.Enrollee.CountryId);
                assessment.Enrollee.Area = context.Area.FirstOrDefault(a => a.AreaId == assessment.Enrollee.AreaId);
                assessment.Enrollee.District = context.District.FirstOrDefault(d => d.DistrictId == assessment.Enrollee.DistrictId);
                assessment.Enrollee.TypeOfSettlement = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == assessment.Enrollee.SettlementTypeId);
                assessment.Enrollee.TypeOfStreet = context.TypeOfStreet.FirstOrDefault(ts => ts.StreetTypeId == assessment.Enrollee.StreetTypeId);
                assessment.Enrollee.Document = context.Document.FirstOrDefault(d => d.DocumentId == assessment.Enrollee.DocumentId);
                assessment.Enrollee.TypeOfSchool = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == assessment.Enrollee.SchoolTypeId);
                assessment.Enrollee.ForeignLanguage = context.ForeignLanguage.FirstOrDefault(fl => fl.LanguageId == assessment.Enrollee.ForeignLanguageId);
                assessment.Enrollee.ReasonForAddmission = context.ReasonForAddmission.FirstOrDefault(rfa => rfa.ReasonForAddmissionId == assessment.Enrollee.ReasonForAddmissionId);
                assessment.Enrollee.ReasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == assessment.Enrollee.ReasonForAddmission.ContestId);
                assessment.Enrollee.TypeOfState = context.TypeOfState.FirstOrDefault(ts => ts.StateId == assessment.Enrollee.StateTypeId);
                assessment.Enrollee.Employee = context.Employee.FirstOrDefault(e => e.EmployeeId == assessment.Enrollee.EmployeeId);
                assessment.Enrollee.Employee.EmployeePost = context.EmployeePost.FirstOrDefault(e => e.PostId == assessment.Enrollee.Employee.PostId);
                assessment.Enrollee.TypeOfFinance = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == assessment.Enrollee.FinanceTypeId);
                if (assessment.Enrollee.DecreeId.HasValue) assessment.Enrollee.Decree = context.Decree.FirstOrDefault(d => d.DecreeId == assessment.Enrollee.DecreeId);
                if (assessment.Enrollee.SecondarySpecialityId.HasValue) assessment.Enrollee.SecondarySpeciality = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == assessment.Enrollee.SecondarySpecialityId);
                if (assessment.Enrollee.TargetWorkPlaceId.HasValue) assessment.Enrollee.TargetWorkPlace = context.TargetWorkPlace.FirstOrDefault(tw => tw.TargetId == assessment.Enrollee.TargetWorkPlaceId);
            }
            return assessment;
        }

        public Assessment GetAssessment(Discipline discipline, Enrollee enrollee, BasisForAssessing basisForAssessing)
        {
            Assessment assessment = context.Assessment.FirstOrDefault(a => a.DisciplineId == discipline.DisciplineId && a.EnrolleeId == enrollee.EnrolleeId && a.Discipline.BasisForAssessingId == basisForAssessing.BasisForAssessingId);
            if (assessment != null)
            {
                assessment.Discipline = context.Discipline.FirstOrDefault(d => d.DisciplineId == assessment.DisciplineId);
                assessment.Discipline.BasisForAssessing = context.BasisForAssessing.FirstOrDefault(b => b.BasisForAssessingId == assessment.Discipline.BasisForAssessingId);
                assessment.Enrollee = context.Enrollee.FirstOrDefault(e => e.EnrolleeId == assessment.EnrolleeId);
                assessment.Enrollee.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == assessment.Enrollee.SpecialityId);
                assessment.Enrollee.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == assessment.Enrollee.Speciality.FacultyId);
                assessment.Enrollee.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == assessment.Enrollee.Speciality.FormOfStudyId);
                assessment.Enrollee.Citizenship = context.Citizenship.FirstOrDefault(e => e.CitizenshipId == assessment.Enrollee.CitizenshipId);
                assessment.Enrollee.Country = context.Country.FirstOrDefault(c => c.CountryId == assessment.Enrollee.CountryId);
                assessment.Enrollee.Area = context.Area.FirstOrDefault(a => a.AreaId == assessment.Enrollee.AreaId);
                assessment.Enrollee.District = context.District.FirstOrDefault(d => d.DistrictId == assessment.Enrollee.DistrictId);
                assessment.Enrollee.TypeOfSettlement = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == assessment.Enrollee.SettlementTypeId);
                assessment.Enrollee.TypeOfStreet = context.TypeOfStreet.FirstOrDefault(ts => ts.StreetTypeId == assessment.Enrollee.StreetTypeId);
                assessment.Enrollee.Document = context.Document.FirstOrDefault(d => d.DocumentId == assessment.Enrollee.DocumentId);
                assessment.Enrollee.TypeOfSchool = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == assessment.Enrollee.SchoolTypeId);
                assessment.Enrollee.ForeignLanguage = context.ForeignLanguage.FirstOrDefault(fl => fl.LanguageId == assessment.Enrollee.ForeignLanguageId);
                assessment.Enrollee.ReasonForAddmission = context.ReasonForAddmission.FirstOrDefault(rfa => rfa.ReasonForAddmissionId == assessment.Enrollee.ReasonForAddmissionId);
                assessment.Enrollee.ReasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == assessment.Enrollee.ReasonForAddmission.ContestId);
                assessment.Enrollee.TypeOfState = context.TypeOfState.FirstOrDefault(ts => ts.StateId == assessment.Enrollee.StateTypeId);
                assessment.Enrollee.Employee = context.Employee.FirstOrDefault(e => e.EmployeeId == assessment.Enrollee.EmployeeId);
                assessment.Enrollee.Employee.EmployeePost = context.EmployeePost.FirstOrDefault(e => e.PostId == assessment.Enrollee.Employee.PostId);
                assessment.Enrollee.TypeOfFinance = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == assessment.Enrollee.FinanceTypeId);
                if (assessment.Enrollee.DecreeId.HasValue) assessment.Enrollee.Decree = context.Decree.FirstOrDefault(d => d.DecreeId == assessment.Enrollee.DecreeId);
                if (assessment.Enrollee.SecondarySpecialityId.HasValue) assessment.Enrollee.SecondarySpeciality = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == assessment.Enrollee.SecondarySpecialityId);
                if (assessment.Enrollee.TargetWorkPlaceId.HasValue) assessment.Enrollee.TargetWorkPlace = context.TargetWorkPlace.FirstOrDefault(tw => tw.TargetId == assessment.Enrollee.TargetWorkPlaceId);
            }
            return assessment;
        }

        public List<Assessment> GetAssessments(Discipline discipline)
        {
            List<Assessment> assessments = null;
            assessments = context.Assessment.Where(a => a.DisciplineId == discipline.DisciplineId).ToList();
            if (assessments.Count != 0)
            {
                foreach (var assessment in assessments)
                {
                        assessment.Discipline = context.Discipline.FirstOrDefault(d => d.DisciplineId == assessment.DisciplineId);
                        assessment.Discipline.BasisForAssessing = context.BasisForAssessing.FirstOrDefault(b => b.BasisForAssessingId == assessment.Discipline.BasisForAssessingId);
                        assessment.Enrollee = context.Enrollee.FirstOrDefault(e => e.EnrolleeId == assessment.EnrolleeId);
                        assessment.Enrollee.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == assessment.Enrollee.SpecialityId);
                        assessment.Enrollee.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == assessment.Enrollee.Speciality.FacultyId);
                        assessment.Enrollee.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == assessment.Enrollee.Speciality.FormOfStudyId);
                        assessment.Enrollee.Citizenship = context.Citizenship.FirstOrDefault(e => e.CitizenshipId == assessment.Enrollee.CitizenshipId);
                        assessment.Enrollee.Country = context.Country.FirstOrDefault(c => c.CountryId == assessment.Enrollee.CountryId);
                        assessment.Enrollee.Area = context.Area.FirstOrDefault(a => a.AreaId == assessment.Enrollee.AreaId);
                        assessment.Enrollee.District = context.District.FirstOrDefault(d => d.DistrictId == assessment.Enrollee.DistrictId);
                        assessment.Enrollee.TypeOfSettlement = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == assessment.Enrollee.SettlementTypeId);
                        assessment.Enrollee.TypeOfStreet = context.TypeOfStreet.FirstOrDefault(ts => ts.StreetTypeId == assessment.Enrollee.StreetTypeId);
                        assessment.Enrollee.Document = context.Document.FirstOrDefault(d => d.DocumentId == assessment.Enrollee.DocumentId);
                        assessment.Enrollee.TypeOfSchool = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == assessment.Enrollee.SchoolTypeId);
                        assessment.Enrollee.ForeignLanguage = context.ForeignLanguage.FirstOrDefault(fl => fl.LanguageId == assessment.Enrollee.ForeignLanguageId);
                        assessment.Enrollee.ReasonForAddmission = context.ReasonForAddmission.FirstOrDefault(rfa => rfa.ReasonForAddmissionId == assessment.Enrollee.ReasonForAddmissionId);
                        assessment.Enrollee.ReasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == assessment.Enrollee.ReasonForAddmission.ContestId);
                        assessment.Enrollee.TypeOfState = context.TypeOfState.FirstOrDefault(ts => ts.StateId == assessment.Enrollee.StateTypeId);
                        assessment.Enrollee.Employee = context.Employee.FirstOrDefault(e => e.EmployeeId == assessment.Enrollee.EmployeeId);
                        assessment.Enrollee.Employee.EmployeePost = context.EmployeePost.FirstOrDefault(e => e.PostId == assessment.Enrollee.Employee.PostId);
                        assessment.Enrollee.TypeOfFinance = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == assessment.Enrollee.FinanceTypeId);
                        if (assessment.Enrollee.DecreeId.HasValue) assessment.Enrollee.Decree = context.Decree.FirstOrDefault(d => d.DecreeId == assessment.Enrollee.DecreeId);
                        if (assessment.Enrollee.SecondarySpecialityId.HasValue) assessment.Enrollee.SecondarySpeciality = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == assessment.Enrollee.SecondarySpecialityId);
                        if (assessment.Enrollee.TargetWorkPlaceId.HasValue) assessment.Enrollee.TargetWorkPlace = context.TargetWorkPlace.FirstOrDefault(tw => tw.TargetId == assessment.Enrollee.TargetWorkPlaceId);
                }
            }
            return assessments;
        }

        public List<Assessment> GetAssessments(Discipline discipline, BasisForAssessing basisForAssessing)
        {
            List<Assessment> assessments = null;
            assessments = context.Assessment.Where(a => a.DisciplineId == discipline.DisciplineId && a.Discipline.BasisForAssessingId == basisForAssessing.BasisForAssessingId).ToList();
            if (assessments.Count != 0)
            {
                foreach (var assessment in assessments)
                {
                    assessment.Discipline = context.Discipline.FirstOrDefault(d => d.DisciplineId == assessment.DisciplineId);
                    assessment.Discipline.BasisForAssessing = context.BasisForAssessing.FirstOrDefault(b => b.BasisForAssessingId == assessment.Discipline.BasisForAssessingId);
                    assessment.Enrollee = context.Enrollee.FirstOrDefault(e => e.EnrolleeId == assessment.EnrolleeId);
                    assessment.Enrollee.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == assessment.Enrollee.SpecialityId);
                    assessment.Enrollee.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == assessment.Enrollee.Speciality.FacultyId);
                    assessment.Enrollee.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == assessment.Enrollee.Speciality.FormOfStudyId);
                    assessment.Enrollee.Citizenship = context.Citizenship.FirstOrDefault(e => e.CitizenshipId == assessment.Enrollee.CitizenshipId);
                    assessment.Enrollee.Country = context.Country.FirstOrDefault(c => c.CountryId == assessment.Enrollee.CountryId);
                    assessment.Enrollee.Area = context.Area.FirstOrDefault(a => a.AreaId == assessment.Enrollee.AreaId);
                    assessment.Enrollee.District = context.District.FirstOrDefault(d => d.DistrictId == assessment.Enrollee.DistrictId);
                    assessment.Enrollee.TypeOfSettlement = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == assessment.Enrollee.SettlementTypeId);
                    assessment.Enrollee.TypeOfStreet = context.TypeOfStreet.FirstOrDefault(ts => ts.StreetTypeId == assessment.Enrollee.StreetTypeId);
                    assessment.Enrollee.Document = context.Document.FirstOrDefault(d => d.DocumentId == assessment.Enrollee.DocumentId);
                    assessment.Enrollee.TypeOfSchool = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == assessment.Enrollee.SchoolTypeId);
                    assessment.Enrollee.ForeignLanguage = context.ForeignLanguage.FirstOrDefault(fl => fl.LanguageId == assessment.Enrollee.ForeignLanguageId);
                    assessment.Enrollee.ReasonForAddmission = context.ReasonForAddmission.FirstOrDefault(rfa => rfa.ReasonForAddmissionId == assessment.Enrollee.ReasonForAddmissionId);
                    assessment.Enrollee.ReasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == assessment.Enrollee.ReasonForAddmission.ContestId);
                    assessment.Enrollee.TypeOfState = context.TypeOfState.FirstOrDefault(ts => ts.StateId == assessment.Enrollee.StateTypeId);
                    assessment.Enrollee.Employee = context.Employee.FirstOrDefault(e => e.EmployeeId == assessment.Enrollee.EmployeeId);
                    assessment.Enrollee.Employee.EmployeePost = context.EmployeePost.FirstOrDefault(e => e.PostId == assessment.Enrollee.Employee.PostId);
                    assessment.Enrollee.TypeOfFinance = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == assessment.Enrollee.FinanceTypeId);
                    if (assessment.Enrollee.DecreeId.HasValue) assessment.Enrollee.Decree = context.Decree.FirstOrDefault(d => d.DecreeId == assessment.Enrollee.DecreeId);
                    if (assessment.Enrollee.SecondarySpecialityId.HasValue) assessment.Enrollee.SecondarySpeciality = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == assessment.Enrollee.SecondarySpecialityId);
                    if (assessment.Enrollee.TargetWorkPlaceId.HasValue) assessment.Enrollee.TargetWorkPlace = context.TargetWorkPlace.FirstOrDefault(tw => tw.TargetId == assessment.Enrollee.TargetWorkPlaceId);
                }
            }
            return assessments;
        }

        public List<Assessment> GetAssessments(Enrollee enrollee)
        {
            List<Assessment> assessments = null;
            assessments = context.Assessment.Where(a => a.EnrolleeId == enrollee.EnrolleeId).ToList();
            if (assessments.Count != 0)
            {
                foreach (var assessment in assessments)
                {
                    assessment.Discipline = context.Discipline.FirstOrDefault(d => d.DisciplineId == assessment.DisciplineId);
                    assessment.Discipline.BasisForAssessing = context.BasisForAssessing.FirstOrDefault(b => b.BasisForAssessingId == assessment.Discipline.BasisForAssessingId);
                    assessment.Enrollee = context.Enrollee.FirstOrDefault(e => e.EnrolleeId == assessment.EnrolleeId);
                    assessment.Enrollee.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == assessment.Enrollee.SpecialityId);
                    assessment.Enrollee.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == assessment.Enrollee.Speciality.FacultyId);
                    assessment.Enrollee.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == assessment.Enrollee.Speciality.FormOfStudyId);
                    assessment.Enrollee.Citizenship = context.Citizenship.FirstOrDefault(e => e.CitizenshipId == assessment.Enrollee.CitizenshipId);
                    assessment.Enrollee.Country = context.Country.FirstOrDefault(c => c.CountryId == assessment.Enrollee.CountryId);
                    assessment.Enrollee.Area = context.Area.FirstOrDefault(a => a.AreaId == assessment.Enrollee.AreaId);
                    assessment.Enrollee.District = context.District.FirstOrDefault(d => d.DistrictId == assessment.Enrollee.DistrictId);
                    assessment.Enrollee.TypeOfSettlement = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == assessment.Enrollee.SettlementTypeId);
                    assessment.Enrollee.TypeOfStreet = context.TypeOfStreet.FirstOrDefault(ts => ts.StreetTypeId == assessment.Enrollee.StreetTypeId);
                    assessment.Enrollee.Document = context.Document.FirstOrDefault(d => d.DocumentId == assessment.Enrollee.DocumentId);
                    assessment.Enrollee.TypeOfSchool = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == assessment.Enrollee.SchoolTypeId);
                    assessment.Enrollee.ForeignLanguage = context.ForeignLanguage.FirstOrDefault(fl => fl.LanguageId == assessment.Enrollee.ForeignLanguageId);
                    assessment.Enrollee.ReasonForAddmission = context.ReasonForAddmission.FirstOrDefault(rfa => rfa.ReasonForAddmissionId == assessment.Enrollee.ReasonForAddmissionId);
                    assessment.Enrollee.ReasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == assessment.Enrollee.ReasonForAddmission.ContestId);
                    assessment.Enrollee.TypeOfState = context.TypeOfState.FirstOrDefault(ts => ts.StateId == assessment.Enrollee.StateTypeId);
                    assessment.Enrollee.Employee = context.Employee.FirstOrDefault(e => e.EmployeeId == assessment.Enrollee.EmployeeId);
                    assessment.Enrollee.Employee.EmployeePost = context.EmployeePost.FirstOrDefault(e => e.PostId == assessment.Enrollee.Employee.PostId);
                    assessment.Enrollee.TypeOfFinance = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == assessment.Enrollee.FinanceTypeId);
                    if (assessment.Enrollee.DecreeId.HasValue) assessment.Enrollee.Decree = context.Decree.FirstOrDefault(d => d.DecreeId == assessment.Enrollee.DecreeId);
                    if (assessment.Enrollee.SecondarySpecialityId.HasValue) assessment.Enrollee.SecondarySpeciality = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == assessment.Enrollee.SecondarySpecialityId);
                    if (assessment.Enrollee.TargetWorkPlaceId.HasValue) assessment.Enrollee.TargetWorkPlace = context.TargetWorkPlace.FirstOrDefault(tw => tw.TargetId == assessment.Enrollee.TargetWorkPlaceId);
                }
            }
            return assessments;
        }

        public List<Assessment> GetAssessments(Enrollee enrollee, BasisForAssessing basisForAssessing)
        {
            List<Assessment> assessments = null;
            assessments = context.Assessment.Where(a => a.EnrolleeId == enrollee.EnrolleeId && a.Discipline.BasisForAssessingId == basisForAssessing.BasisForAssessingId).ToList();
            if (assessments.Count != 0)
            {
                foreach (var assessment in assessments)
                {
                    assessment.Discipline = context.Discipline.FirstOrDefault(d => d.DisciplineId == assessment.DisciplineId);
                    assessment.Discipline.BasisForAssessing = context.BasisForAssessing.FirstOrDefault(b => b.BasisForAssessingId == assessment.Discipline.BasisForAssessingId);
                    assessment.Enrollee = context.Enrollee.FirstOrDefault(e => e.EnrolleeId == assessment.EnrolleeId);
                    assessment.Enrollee.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == assessment.Enrollee.SpecialityId);
                    assessment.Enrollee.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == assessment.Enrollee.Speciality.FacultyId);
                    assessment.Enrollee.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == assessment.Enrollee.Speciality.FormOfStudyId);
                    assessment.Enrollee.Citizenship = context.Citizenship.FirstOrDefault(e => e.CitizenshipId == assessment.Enrollee.CitizenshipId);
                    assessment.Enrollee.Country = context.Country.FirstOrDefault(c => c.CountryId == assessment.Enrollee.CountryId);
                    assessment.Enrollee.Area = context.Area.FirstOrDefault(a => a.AreaId == assessment.Enrollee.AreaId);
                    assessment.Enrollee.District = context.District.FirstOrDefault(d => d.DistrictId == assessment.Enrollee.DistrictId);
                    assessment.Enrollee.TypeOfSettlement = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == assessment.Enrollee.SettlementTypeId);
                    assessment.Enrollee.TypeOfStreet = context.TypeOfStreet.FirstOrDefault(ts => ts.StreetTypeId == assessment.Enrollee.StreetTypeId);
                    assessment.Enrollee.Document = context.Document.FirstOrDefault(d => d.DocumentId == assessment.Enrollee.DocumentId);
                    assessment.Enrollee.TypeOfSchool = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == assessment.Enrollee.SchoolTypeId);
                    assessment.Enrollee.ForeignLanguage = context.ForeignLanguage.FirstOrDefault(fl => fl.LanguageId == assessment.Enrollee.ForeignLanguageId);
                    assessment.Enrollee.ReasonForAddmission = context.ReasonForAddmission.FirstOrDefault(rfa => rfa.ReasonForAddmissionId == assessment.Enrollee.ReasonForAddmissionId);
                    assessment.Enrollee.ReasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == assessment.Enrollee.ReasonForAddmission.ContestId);
                    assessment.Enrollee.TypeOfState = context.TypeOfState.FirstOrDefault(ts => ts.StateId == assessment.Enrollee.StateTypeId);
                    assessment.Enrollee.Employee = context.Employee.FirstOrDefault(e => e.EmployeeId == assessment.Enrollee.EmployeeId);
                    assessment.Enrollee.Employee.EmployeePost = context.EmployeePost.FirstOrDefault(e => e.PostId == assessment.Enrollee.Employee.PostId);
                    assessment.Enrollee.TypeOfFinance = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == assessment.Enrollee.FinanceTypeId);
                    if (assessment.Enrollee.DecreeId.HasValue) assessment.Enrollee.Decree = context.Decree.FirstOrDefault(d => d.DecreeId == assessment.Enrollee.DecreeId);
                    if (assessment.Enrollee.SecondarySpecialityId.HasValue) assessment.Enrollee.SecondarySpeciality = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == assessment.Enrollee.SecondarySpecialityId);
                    if (assessment.Enrollee.TargetWorkPlaceId.HasValue) assessment.Enrollee.TargetWorkPlace = context.TargetWorkPlace.FirstOrDefault(tw => tw.TargetId == assessment.Enrollee.TargetWorkPlaceId);
                }
            }
            return assessments;
        }

        public List<Assessment> GetAssessments(BasisForAssessing basisForAssessing)
        {
            List<Assessment> assessments = null;
            assessments = context.Assessment.Where(a => a.Discipline.BasisForAssessingId == basisForAssessing.BasisForAssessingId).ToList();
            if (assessments.Count != 0)
            {
                foreach (var assessment in assessments)
                {
                    assessment.Discipline = context.Discipline.FirstOrDefault(d => d.DisciplineId == assessment.DisciplineId);
                    assessment.Discipline.BasisForAssessing = context.BasisForAssessing.FirstOrDefault(b => b.BasisForAssessingId == assessment.Discipline.BasisForAssessingId);
                    assessment.Enrollee = context.Enrollee.FirstOrDefault(e => e.EnrolleeId == assessment.EnrolleeId);
                    assessment.Enrollee.Speciality = context.Speciality.FirstOrDefault(s => s.SpecialityId == assessment.Enrollee.SpecialityId);
                    assessment.Enrollee.Speciality.Faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == assessment.Enrollee.Speciality.FacultyId);
                    assessment.Enrollee.Speciality.FormOfStudy = context.FormOfStudy.FirstOrDefault(f => f.FormOfStudyId == assessment.Enrollee.Speciality.FormOfStudyId);
                    assessment.Enrollee.Citizenship = context.Citizenship.FirstOrDefault(e => e.CitizenshipId == assessment.Enrollee.CitizenshipId);
                    assessment.Enrollee.Country = context.Country.FirstOrDefault(c => c.CountryId == assessment.Enrollee.CountryId);
                    assessment.Enrollee.Area = context.Area.FirstOrDefault(a => a.AreaId == assessment.Enrollee.AreaId);
                    assessment.Enrollee.District = context.District.FirstOrDefault(d => d.DistrictId == assessment.Enrollee.DistrictId);
                    assessment.Enrollee.TypeOfSettlement = context.TypeOfSettlement.FirstOrDefault(ts => ts.SettlementTypeId == assessment.Enrollee.SettlementTypeId);
                    assessment.Enrollee.TypeOfStreet = context.TypeOfStreet.FirstOrDefault(ts => ts.StreetTypeId == assessment.Enrollee.StreetTypeId);
                    assessment.Enrollee.Document = context.Document.FirstOrDefault(d => d.DocumentId == assessment.Enrollee.DocumentId);
                    assessment.Enrollee.TypeOfSchool = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == assessment.Enrollee.SchoolTypeId);
                    assessment.Enrollee.ForeignLanguage = context.ForeignLanguage.FirstOrDefault(fl => fl.LanguageId == assessment.Enrollee.ForeignLanguageId);
                    assessment.Enrollee.ReasonForAddmission = context.ReasonForAddmission.FirstOrDefault(rfa => rfa.ReasonForAddmissionId == assessment.Enrollee.ReasonForAddmissionId);
                    assessment.Enrollee.ReasonForAddmission.Contest = context.Contest.FirstOrDefault(c => c.ContestId == assessment.Enrollee.ReasonForAddmission.ContestId);
                    assessment.Enrollee.TypeOfState = context.TypeOfState.FirstOrDefault(ts => ts.StateId == assessment.Enrollee.StateTypeId);
                    assessment.Enrollee.Employee = context.Employee.FirstOrDefault(e => e.EmployeeId == assessment.Enrollee.EmployeeId);
                    assessment.Enrollee.Employee.EmployeePost = context.EmployeePost.FirstOrDefault(e => e.PostId == assessment.Enrollee.Employee.PostId);
                    assessment.Enrollee.TypeOfFinance = context.TypeOfFinance.FirstOrDefault(tf => tf.FinanceTypeId == assessment.Enrollee.FinanceTypeId);
                    if (assessment.Enrollee.DecreeId.HasValue) assessment.Enrollee.Decree = context.Decree.FirstOrDefault(d => d.DecreeId == assessment.Enrollee.DecreeId);
                    if (assessment.Enrollee.SecondarySpecialityId.HasValue) assessment.Enrollee.SecondarySpeciality = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == assessment.Enrollee.SecondarySpecialityId);
                    if (assessment.Enrollee.TargetWorkPlaceId.HasValue) assessment.Enrollee.TargetWorkPlace = context.TargetWorkPlace.FirstOrDefault(tw => tw.TargetId == assessment.Enrollee.TargetWorkPlaceId);
                }
            }
            return assessments;
        }

        public Assessment InsertAssessment(Assessment assessment)
        {
            context.Assessment.Add(assessment);
            context.SaveChanges();
            return assessment;
        }

        public Assessment UpdateAssessment(Assessment assessment)
        {
            Assessment assessmentToUpdate = context.Assessment.FirstOrDefault(a => a.AssessmentId == assessment.AssessmentId);
            assessmentToUpdate.ChangeDiscipline = assessment.ChangeDiscipline;
            assessmentToUpdate.DisciplineId = assessment.DisciplineId;
            assessmentToUpdate.EnrolleeId = assessment.EnrolleeId;
            assessmentToUpdate.Estimation = assessment.Estimation;
            assessmentToUpdate.SertCode = assessment.SertCode;
            assessmentToUpdate.SertDate = assessment.SertDate;
            context.SaveChanges();
            return assessmentToUpdate;
        }
    }
}
