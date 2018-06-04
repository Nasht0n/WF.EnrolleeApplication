using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    public partial class Enrollee
    {
        public override string ToString()
        {
            return $"{this.EnrolleeId}. {this.RuSurname} {this.RuName}";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj is Enrollee && obj != null)
            {
                Enrollee temp = (Enrollee)obj;
                if (temp.EnrolleeId == this.EnrolleeId && temp.SpecialityId == this.SpecialityId && temp.CitizenshipId == this.CitizenshipId && temp.CountryId == this.CountryId &&
                    temp.AreaId == this.AreaId && temp.DistrictId == this.DistrictId && temp.SettlementTypeId == this.SettlementTypeId && temp.SteetTypeId == this.SteetTypeId &&
                    temp.DocumentId == this.DocumentId && temp.SchoolTypeId == this.SchoolTypeId && temp.ForeignLanguageId == this.ForeignLanguageId && temp.ReasonForAddmissionId == this.ReasonForAddmissionId &&
                    temp.StateTypeId == this.StateTypeId && temp.EmployeeId == this.EmployeeId && temp.DecreeId == this.DecreeId && temp.SecondarySpecialityId == this.SecondarySpecialityId &&
                    temp.TargetWorkPlaceId == this.TargetWorkPlaceId && temp.RuSurname == this.RuSurname && temp.RuName == this.RuName && temp.RuPatronymic == this.RuPatronymic &&
                    temp.BlrSurname == this.BlrSurname && temp.BlrName == this.BlrName && temp.BlrPatronymic == this.BlrPatronymic && temp.Gender == this.Gender && temp.DateOfBirthday == this.DateOfBirthday &&
                    temp.FatherFullname == this.FatherFullname && temp.FatherAddress == this.FatherAddress && temp.MotherFullname == this.MotherFullname && temp.MotherAddress == this.MotherAddress &&
                    temp.SettlementName == this.SettlementName && temp.SettlementIndex == this.SettlementIndex && temp.StreetName == this.StreetName && temp.NumberHouse == this.NumberHouse &&
                    temp.NumberFlat == this.NumberFlat && temp.HomePhone == this.HomePhone && temp.MobilePhone == this.MobilePhone && temp.DocumentSeria == this.DocumentSeria &&
                    temp.DocumentNumber == this.DocumentNumber && temp.DocumentDate == this.DocumentDate && temp.DocumentWhoGave == this.DocumentWhoGave && temp.DocumentPersonalNumber == this.DocumentPersonalNumber &&
                    temp.SchoolName == this.SchoolName && temp.SchoolYear == this.SchoolYear && temp.SchoolAddress == this.SchoolAddress && temp.IsBRSM == this.IsBRSM && temp.NumberOfDeal == this.NumberOfDeal &&
                    temp.DateDeal == this.DateDeal && temp.StateDateChange == this.StateDateChange && temp.PersonInCharge == this.PersonInCharge && temp.WorkPlace == this.WorkPlace &&
                    temp.WorkPost == this.WorkPost && temp.Seniority == this.Seniority && temp.CurrentNumberCurs == this.CurrentNumberCurs && temp.CurrentSpeciality == this.CurrentSpeciality &&
                    temp.CurrentUniversity == this.CurrentUniversity && temp.AttestatEstimationString == this.AttestatEstimationString && temp.DiplomPtuEstimationString == this.DiplomPtuEstimationString &&
                    temp.DiplomSusEstimationString == this.DiplomSusEstimationString && temp.BeforeEnrollSpecialityId == this.BeforeEnrollSpecialityId && temp.BeforeEnrollNumberOfDeal == temp.BeforeEnrollNumberOfDeal) return true;
                else return false;
            }
            return false;
        }
    }
}
