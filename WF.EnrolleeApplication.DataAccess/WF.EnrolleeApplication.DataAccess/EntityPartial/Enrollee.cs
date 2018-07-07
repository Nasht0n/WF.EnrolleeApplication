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
            string result = Environment.NewLine +
                            $"Информация об объекте: " + Environment.NewLine + 
                            $"Код абитуриента = {this.EnrolleeId}" + Environment.NewLine +
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
                            $"Код типа финансирования = {this.FinanceTypeId}";
            if (this.DecreeId.HasValue) result += Environment.NewLine + $"Код приказа о зачислении = {this.DecreeId.Value}";
            if (this.SecondarySpecialityId.HasValue) result += Environment.NewLine + $"Код специальности второй ступени = {this.SecondarySpecialityId}";
            if (this.TargetWorkPlaceId.HasValue) result += Environment.NewLine + $"Код места целевого направления = {this.TargetWorkPlaceId}";
            result += Environment.NewLine + 
                      $"Фамилия абитуриента (на русском языке) = {this.RuSurname.Trim()}" + Environment.NewLine +
                      $"Имя абитуриента (на русском языке) = {this.RuName.Trim()}" + Environment.NewLine +
                      $"Отчество абитуриента (на русском языке) = {this.RuPatronymic.Trim()}" + Environment.NewLine +
                      $"Фамилия абитуриента (на белорусском языке) = {this.BlrSurname.Trim()}" + Environment.NewLine +
                      $"Имя абитуриента (на белорусском языке) = {this.BlrName.Trim()}" + Environment.NewLine +
                      $"Отчество абитуриента (на белорусском языке) = {this.BlrPatronymic.Trim()}" + Environment.NewLine +
                      $"Пол абитуриента = {this.Gender.Trim()}" + Environment.NewLine +
                      $"Дата рождения = {this.DateOfBirthday.ToShortDateString()}";
            if (!string.IsNullOrWhiteSpace(this.FatherFullname)) result += Environment.NewLine + $"ФИО отца абитуриента = {this.FatherFullname.Trim()}";
            if (!string.IsNullOrWhiteSpace(this.FatherAddress)) result += Environment.NewLine + $"Адрес проживания отца абитуриента = {this.FatherAddress.Trim()}";
            if (!string.IsNullOrWhiteSpace(this.MotherFullname)) result += Environment.NewLine + $"ФИО матери абитуриента = {this.MotherFullname.Trim()}";
            if (!string.IsNullOrWhiteSpace(this.MotherAddress)) result += Environment.NewLine + $"Адрес проживания матери абитуриента = {this.MotherAddress.Trim()}";
            result += Environment.NewLine +
                      $"Наименование населенного пункта = {this.SettlementName.Trim()}" + Environment.NewLine +
                      $"Индекс населенного пункта = {this.SettlementIndex}" + Environment.NewLine +
                      $"Наименование улицы = {this.StreetName}" + Environment.NewLine +
                      $"Номер дома = {this.NumberHouse}";
            if (!string.IsNullOrWhiteSpace(this.NumberFlat)) result += Environment.NewLine + $"Номер квартиры = {this.NumberFlat.Trim()}";
            result += Environment.NewLine + 
                      $"Номер домашнего телефона = {this.HomePhone.Trim()}" + Environment.NewLine +
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
                      $"Лицо ответственное за прием документов = {this.PersonInCharge.Trim()}";
            if (!string.IsNullOrWhiteSpace(this.WorkPlace)) result += Environment.NewLine + $"Место работы = {this.WorkPlace.Trim()}";
            if (!string.IsNullOrWhiteSpace(this.WorkPost)) result += Environment.NewLine + $"Должность = {this.WorkPost.Trim()}";
            if (!string.IsNullOrWhiteSpace(this.Seniority)) result += Environment.NewLine + $"Стаж работы = {this.Seniority.Trim()}";
            if (!string.IsNullOrWhiteSpace(this.CurrentNumberCurs)) result += Environment.NewLine + $"Текущий курс = {this.CurrentNumberCurs.Trim()}";
            if (!string.IsNullOrWhiteSpace(this.CurrentUniversity)) result += Environment.NewLine + $"Текущий университет = {this.CurrentUniversity.Trim()}";
            if (!string.IsNullOrWhiteSpace(this.CurrentSpeciality)) result += Environment.NewLine + $"Текущая специальность = {this.CurrentSpeciality.Trim()}";
            if (!string.IsNullOrWhiteSpace(this.AttestatEstimationString)) result += Environment.NewLine + $"Оценки аттестата = {this.AttestatEstimationString.Trim()}";
            if (!string.IsNullOrWhiteSpace(this.DiplomPtuEstimationString)) result += Environment.NewLine + $"Оценки диплома ПТУ = {this.DiplomPtuEstimationString.Trim()}";
            if (!string.IsNullOrWhiteSpace(this.DiplomSusEstimationString)) result += Environment.NewLine + $"Оценки диплома ССУЗ = {this.DiplomSusEstimationString.Trim()}";
            if (this.BeforeEnrollSpecialityId.HasValue) result += Environment.NewLine + $"Код специальности подачи документа (до зачисления) = {this.BeforeEnrollSpecialityId.Value}";
            if (this.BeforeEnrollNumberOfDeal.HasValue) result += Environment.NewLine + $"Номер личного дела специальности подачи документа (до зачисления) = {this.BeforeEnrollNumberOfDeal.Value}";
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
