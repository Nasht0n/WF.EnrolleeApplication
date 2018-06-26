using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    /// <summary>
    /// Таблица "Абитуриенты" 
    /// Частичный класс для переопределения внутренних методов
    /// </summary>
    public partial class Enrollee
    {
        /// <summary>
        /// Переопределенный метод сравнения
        /// </summary>
        /// <param name="obj">Объект сущности</param>
        /// <returns>true - если объекты равны</returns>
        public override bool Equals(object obj)
        {
            if(obj is Enrollee && obj !=null)
            {
                Enrollee temp = (Enrollee)obj;
                if (temp.GetHashCode() == this.GetHashCode()) return true;
                else return false;
            }
            return false;
        }
        /// <summary>
        /// Переопределенный метод вывода данных класса
        /// </summary>
        /// <returns>Форматированная строка</returns>
        public override string ToString()
        {
            string result = $"Код абитуриента = {this.EnrolleeId}" +
                            $"\nКод специальности = {this.SpecialityId}" +
                            $"\nКод гражданство = {this.CitizenshipId}" +
                            $"\nКод страны = {this.CountryId}" +
                            $"\nКод области = {this.AreaId}" +
                            $"\nКод района = {this.DistrictId}" +
                            $"\nКод типа населенного пункта = {this.SettlementTypeId}" +
                            $"\nКод типа улицы = {this.StreetTypeId}" +
                            $"\nКод документа = {this.DocumentId}" +
                            $"\nКод последнего учреждения образования = {this.SchoolTypeId}" +
                            $"\nКод иностранного языка = {this.ForeignLanguageId}" +
                            $"\nКод основания зачисления = {this.ReasonForAddmissionId}" +
                            $"\nКод статуса абитуриента = {this.StateTypeId}" +
                            $"\nКод оператора, регистрирующего абитуриента = {this.EmployeeId}" +
                            $"\nКод типа финансирования = {this.FinanceTypeId}";
            if (this.DecreeId.HasValue) result += $"\nКод приказа о зачислении = {this.DecreeId.Value}";
            if (this.SecondarySpecialityId.HasValue) result += $"\nКод специальности второй ступени = {this.SecondarySpecialityId}";
            if (this.TargetWorkPlaceId.HasValue) result += $"\nКод места целевого направления = {this.TargetWorkPlaceId}";
            result += $"\nФамилия абитуриента (на русском языке) = {this.RuSurname.Trim()}" +
                      $"\nИмя абитуриента (на русском языке) = {this.RuName.Trim()}" +
                      $"\nОтчество абитуриента (на русском языке) = {this.RuPatronymic.Trim()}" +
                      $"\nФамилия абитуриента (на белорусском языке) = {this.BlrSurname.Trim()}" +
                      $"\nИмя абитуриента (на белорусском языке) = {this.BlrName.Trim()}" +
                      $"\nОтчество абитуриента (на белорусском языке) = {this.BlrPatronymic.Trim()}" +
                      $"\nПол абитуриента = {this.Gender.Trim()}" +
                      $"\nДата рождения = {this.DateOfBirthday.ToShortDateString()}";
            if (!string.IsNullOrWhiteSpace(this.FatherFullname)) result += $"\nФИО отца абитуриента = {this.FatherFullname.Trim()}";
            if (!string.IsNullOrWhiteSpace(this.FatherAddress)) result += $"\nАдрес проживания отца абитуриента = {this.FatherAddress.Trim()}";
            if (!string.IsNullOrWhiteSpace(this.MotherFullname)) result += $"\nФИО матери абитуриента = {this.MotherFullname.Trim()}";
            if (!string.IsNullOrWhiteSpace(this.MotherAddress)) result += $"\nАдрес проживания матери абитуриента = {this.MotherAddress.Trim()}";
            result += $"\nНаименование населенного пункта = {this.SettlementName.Trim()}" +
                      $"\nИндекс населенного пункта = {this.SettlementIndex}" +
                      $"\nНаименование улицы = {this.StreetName}" +
                      $"\nНомер дома = {this.NumberHouse}";
            if (!string.IsNullOrWhiteSpace(this.NumberFlat)) result += $"\nНомер квартиры = {this.NumberFlat.Trim()}";
            result += $"\nНомер домашнего телефона = {this.HomePhone.Trim()}" +
                      $"\nНомер мобильного телефона = {this.MobilePhone.Trim()}" +
                      $"\nСерия документа = {this.DocumentSeria.Trim()}" +
                      $"\nНомер документа = {this.DocumentNumber.Trim()}" +
                      $"\nДата выдачи документа = {this.DocumentDate.ToShortDateString()}" +
                      $"\nОрган выдавший документ = {this.DocumentWhoGave.Trim()}" +
                      $"\nЛичный номер документа абитуриента = {this.DocumentPersonalNumber.Trim()}" +
                      $"\nНаименование последнего учреждения об образовании = {this.SchoolName.Trim()}" +
                      $"\nГод окончания последнего учреждения об образовании = {this.SchoolYear.Trim()}" +
                      $"\nАдрес последнего учреждения об образовании = {this.SchoolAddress.Trim()}" +
                      $"\nЯвляется членом БРСМ? {this.IsBRSM}" +
                      $"\nНомер личного дела = {this.NumberOfDeal}" +
                      $"\nДата регистрации абитуриента = {this.DateDeal.ToShortDateString()}" +
                      $"\nДата изменения статуса абитуриента = {this.StateDateChange.ToShortDateString()}" +
                      $"\nЛицо ответственное за прием документов = {this.PersonInCharge.Trim()}";
            if (!string.IsNullOrWhiteSpace(this.WorkPlace)) result += $"\nМесто работы = {this.WorkPlace.Trim()}";
            if (!string.IsNullOrWhiteSpace(this.WorkPost)) result += $"\nДолжность = {this.WorkPost.Trim()}";
            if (!string.IsNullOrWhiteSpace(this.Seniority)) result += $"\nСтаж работы = {this.Seniority.Trim()}";
            if (!string.IsNullOrWhiteSpace(this.CurrentNumberCurs)) result += $"\nТекущий курс = {this.CurrentNumberCurs.Trim()}";
            if (!string.IsNullOrWhiteSpace(this.CurrentUniversity)) result += $"\nТекущий университет = {this.CurrentUniversity.Trim()}";
            if (!string.IsNullOrWhiteSpace(this.CurrentSpeciality)) result += $"\nТекущая специальность = {this.CurrentSpeciality.Trim()}";
            if (!string.IsNullOrWhiteSpace(this.AttestatEstimationString)) result += $"\nОценки аттестата = {this.AttestatEstimationString.Trim()}";
            if (!string.IsNullOrWhiteSpace(this.DiplomPtuEstimationString)) result += $"\nОценки диплома ПТУ = {this.DiplomPtuEstimationString.Trim()}";
            if (!string.IsNullOrWhiteSpace(this.DiplomSusEstimationString)) result += $"\nОценки диплома ССУЗ = {this.DiplomSusEstimationString.Trim()}";
            if (this.BeforeEnrollSpecialityId.HasValue) result += $"\nКод специальности подачи документа (до зачисления) = {this.BeforeEnrollSpecialityId.Value}";
            if (this.BeforeEnrollNumberOfDeal.HasValue) result += $"\nНомер личного дела специальности подачи документа (до зачисления) = {this.BeforeEnrollNumberOfDeal.Value}";
            return result;
        }
        /// <summary>
        /// Переопределенный метод получения хеш-кода объекта
        /// </summary>
        /// <returns>Хэш-код</returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }
}
