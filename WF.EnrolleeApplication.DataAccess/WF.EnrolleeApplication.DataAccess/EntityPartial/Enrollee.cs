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
            string result = $"Код абитуриента = {this.EnrolleeId}" + Environment.NewLine +
                            $"Код специальности = {this.SpecialityId}" + Environment.NewLine +
                            $"Код гражданство = {this.CitizenshipId}" + Environment.NewLine +
                            $"Код страны = {this.CountryId}" + Environment.NewLine +
                            $"Код области = {this.AreaId}" + Environment.NewLine +
                            $"Код района = {this.DistrictId}" + Environment.NewLine +
                            $"Код типа населенного пункта = {this.SettlementTypeId}" + Environment.NewLine +
                            $"Код типа улицы = {this.StreetTypeId}" + Environment.NewLine +
                            $"Код документа = {this.DocumentId}" + Environment.NewLine +
                            $"Код последнего учреждения образования = {this.SchoolTypeId}" + Environment.NewLine +
                            $"Код иностранного языка = {this.ForeignLanguageId}" + Environment.NewLine +
                            $"Код основания зачисления = {this.ReasonForAddmissionId}" + Environment.NewLine +
                            $"Код статуса абитуриента = {this.StateTypeId}" + Environment.NewLine +
                            $"Код оператора, регистрирующего абитуриента = {this.EmployeeId}" + Environment.NewLine +
                            $"Код типа финансирования = {this.FinanceTypeId}" + Environment.NewLine ;
            if (this.DecreeId.HasValue) result += $"Код приказа о зачислении = {this.DecreeId.Value}" + Environment.NewLine;
            if (this.SecondarySpecialityId.HasValue) result += $"Код специальности второй ступени = {this.SecondarySpecialityId}" + Environment.NewLine;
            if (this.TargetWorkPlaceId.HasValue) result += $"Код места целевого направления = {this.TargetWorkPlaceId}" + Environment.NewLine;
            result += $"Фамилия абитуриента (на русском языке) = {this.RuSurname.Trim()}" + Environment.NewLine +
                      $"Имя абитуриента (на русском языке) = {this.RuName.Trim()}" + Environment.NewLine +
                      $"Отчество абитуриента (на русском языке) = {this.RuPatronymic.Trim()}" + Environment.NewLine +
                      $"Фамилия абитуриента (на белорусском языке) = {this.BlrSurname.Trim()}" + Environment.NewLine +
                      $"Имя абитуриента (на белорусском языке) = {this.BlrName.Trim()}" + Environment.NewLine +
                      $"Отчество абитуриента (на белорусском языке) = {this.BlrPatronymic.Trim()}" + Environment.NewLine +
                      $"Пол абитуриента = {this.Gender.Trim()}" + Environment.NewLine +
                      $"Дата рождения = {this.DateOfBirthday.ToShortDateString()}" + Environment.NewLine;
            if (!string.IsNullOrWhiteSpace(this.FatherFullname)) result += $"ФИО отца абитуриента = {this.FatherFullname.Trim()}" + Environment.NewLine;
            if (!string.IsNullOrWhiteSpace(this.FatherAddress)) result += $"Адрес проживания отца абитуриента = {this.FatherAddress.Trim()}" + Environment.NewLine;
            if (!string.IsNullOrWhiteSpace(this.MotherFullname)) result += $"ФИО матери абитуриента = {this.MotherFullname.Trim()}" + Environment.NewLine;
            if (!string.IsNullOrWhiteSpace(this.MotherAddress)) result += $"Адрес проживания матери абитуриента = {this.MotherAddress.Trim()}" + Environment.NewLine;
            result += $"Наименование населенного пункта = {this.SettlementName.Trim()}" + Environment.NewLine +
                      $"Индекс населенного пункта = {this.SettlementIndex}" + Environment.NewLine +
                      $"Наименование улицы = {this.StreetName}" + Environment.NewLine +
                      $"Номер дома = {this.NumberHouse}" + Environment.NewLine;
            if (!string.IsNullOrWhiteSpace(this.NumberFlat)) result += $"Номер квартиры = {this.NumberFlat.Trim()}" + Environment.NewLine;
            result += $"Номер домашнего телефона = {this.HomePhone.Trim()}" + Environment.NewLine +
                      $"Номер мобильного телефона = {this.MobilePhone.Trim()}" + Environment.NewLine +
                      $"Серия документа = {this.DocumentSeria.Trim()}" + Environment.NewLine +
                      $"Номер документа = {this.DocumentNumber.Trim()}" + Environment.NewLine +
                      $"Дата выдачи документа = {this.DocumentDate.ToShortDateString()}" + Environment.NewLine +
                      $"Орган выдавший документ = {this.DocumentWhoGave.Trim()}" + Environment.NewLine +
                      $"Личный номер документа абитуриента = {this.DocumentPersonalNumber.Trim()}" + Environment.NewLine +
                      $"Наименование последнего учреждения об образовании = {this.SchoolName.Trim()}" + Environment.NewLine +
                      $"Год окончания последнего учреждения об образовании = {this.SchoolYear.Trim()}" + Environment.NewLine +
                      $"Адрес последнего учреждения об образовании = {this.SchoolAddress.Trim()}" + Environment.NewLine +
                      $"Является членом БРСМ? {this.IsBRSM}" + Environment.NewLine +
                      $"Номер личного дела = {this.NumberOfDeal}" + Environment.NewLine +
                      $"Дата регистрации абитуриента = {this.DateDeal.ToShortDateString()}" + Environment.NewLine +
                      $"Дата изменения статуса абитуриента = {this.StateDateChange.ToShortDateString()}" + Environment.NewLine +
                      $"Лицо ответственное за прием документов = {this.PersonInCharge.Trim()}" + Environment.NewLine;
            if (!string.IsNullOrWhiteSpace(this.WorkPlace)) result += $"Место работы = {this.WorkPlace.Trim()}" + Environment.NewLine;
            if (!string.IsNullOrWhiteSpace(this.WorkPost)) result += $"Должность = {this.WorkPost.Trim()}" + Environment.NewLine;
            if (!string.IsNullOrWhiteSpace(this.Seniority)) result += $"Стаж работы = {this.Seniority.Trim()}" + Environment.NewLine;
            if (!string.IsNullOrWhiteSpace(this.CurrentNumberCurs)) result += $"Текущий курс = {this.CurrentNumberCurs.Trim()}" + Environment.NewLine;
            if (!string.IsNullOrWhiteSpace(this.CurrentUniversity)) result += $"Текущий университет = {this.CurrentUniversity.Trim()}" + Environment.NewLine;
            if (!string.IsNullOrWhiteSpace(this.CurrentSpeciality)) result += $"Текущая специальность = {this.CurrentSpeciality.Trim()}" + Environment.NewLine;
            if (!string.IsNullOrWhiteSpace(this.AttestatEstimationString)) result += $"Оценки аттестата = {this.AttestatEstimationString.Trim()}" + Environment.NewLine;
            if (!string.IsNullOrWhiteSpace(this.DiplomPtuEstimationString)) result += $"Оценки диплома ПТУ = {this.DiplomPtuEstimationString.Trim()}" + Environment.NewLine;
            if (!string.IsNullOrWhiteSpace(this.DiplomSusEstimationString)) result += $"Оценки диплома ССУЗ = {this.DiplomSusEstimationString.Trim()}" + Environment.NewLine;
            if (this.BeforeEnrollSpecialityId.HasValue) result += $"Код специальности подачи документа (до зачисления) = {this.BeforeEnrollSpecialityId.Value}" + Environment.NewLine;
            if (this.BeforeEnrollNumberOfDeal.HasValue) result += $"Номер личного дела специальности подачи документа (до зачисления) = {this.BeforeEnrollNumberOfDeal.Value}" + Environment.NewLine;
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
