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
            var enrollee = context.Enrollee.AsNoTracking().FirstOrDefault(e => e.EnrolleeId == id);
            
            return enrollee;
        }

        public Enrollee GetEnrollee(string documentPersonalNumber)
        {
            Enrollee enrollee = context.Enrollee.AsNoTracking().FirstOrDefault(e => e.DocumentPersonalNumber == documentPersonalNumber);
            return enrollee;
        }

        public List<Enrollee> GetEnrollees()
        {
            List<Enrollee> enrollees = context.Enrollee.AsNoTracking().ToList();
            return enrollees;
        }

        public List<Enrollee> GetEnrollees(Speciality speciality)
        {
            List<Enrollee> enrollees = context.Enrollee.AsNoTracking().Where(e => e.SpecialityId == speciality.SpecialityId).ToList();
            return enrollees;
        }

        public List<Enrollee> GetEnrollees(Citizenship citizenship)
        {
            List<Enrollee> enrollees = context.Enrollee.AsNoTracking().Where(e => e.CitizenshipId == citizenship.CitizenshipId).ToList();
            return enrollees;
        }

        public List<Enrollee> GetEnrollees(Country country)
        {
            List<Enrollee> enrollees = context.Enrollee.AsNoTracking().Where(e => e.CountryId == country.CountryId).ToList();
            return enrollees;
        }

        public List<Enrollee> GetEnrollees(Area area)
        {
            List<Enrollee> enrollees = context.Enrollee.AsNoTracking().Where(e => e.AreaId == area.AreaId).ToList();
            return enrollees;
        }

        public List<Enrollee> GetEnrollees(District district)
        {
            List<Enrollee> enrollees = context.Enrollee.AsNoTracking().Where(e => e.DistrictId == district.DistrictId).ToList();
            return enrollees;
        }

        public List<Enrollee> GetEnrollees(TypeOfSchool typeOfSchool)
        {
            List<Enrollee> enrollees = context.Enrollee.AsNoTracking().Where(e => e.SchoolTypeId == typeOfSchool.SchoolTypeId).ToList();
            return enrollees;
        }

        public List<Enrollee> GetEnrollees(ReasonForAddmission reasonForAddmission)
        {
            List<Enrollee> enrollees = context.Enrollee.AsNoTracking().Where(e => e.ReasonForAddmissionId == reasonForAddmission.ReasonForAddmissionId).ToList();
            return enrollees;
        }

        public List<Enrollee> GetEnrollees(TypeOfState typeOfState)
        {
            List<Enrollee> enrollees = context.Enrollee.AsNoTracking().Where(e => e.StateTypeId == typeOfState.StateId).ToList();
            return enrollees;
        }

        public List<Enrollee> GetEnrollees(Employee employee)
        {
            List<Enrollee> enrollees = context.Enrollee.AsNoTracking().Where(e => e.EmployeeId == employee.EmployeeId).ToList();
            return enrollees;
        }

        public List<Enrollee> GetEnrollees(Decree decree)
        {
            List<Enrollee> enrollees = context.Enrollee.AsNoTracking().Where(e => e.DecreeId == decree.DecreeId).ToList();
            return enrollees;
        }

        public List<Enrollee> GetEnrollees(TypeOfFinance typeOfFinance)
        {
            List<Enrollee> enrollees = context.Enrollee.AsNoTracking().Where(e => e.FinanceTypeId == typeOfFinance.FinanceTypeId).ToList();
            return enrollees;
        }

        public Enrollee InsertEnrollee(Enrollee enrollee)
        {
                context.Enrollee.Add(enrollee);
                context.SaveChanges();
                return enrollee;
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
