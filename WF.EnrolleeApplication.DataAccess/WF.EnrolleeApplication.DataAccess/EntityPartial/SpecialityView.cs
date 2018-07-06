using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    /// <summary>
    /// Представление "Специальности первой ступени" 
    /// Частичный класс для переопределения внутренних методов
    /// </summary>
    public partial class SpecialityView
    {
        /// <summary>
        /// Переопределенный метод сравнения
        /// </summary>
        /// <param name="obj">Объект сущности</param>
        /// <returns>true - если объекты равны</returns>
        public override bool Equals(object obj)
        {
            if(obj is SpecialityView && obj!=null)
            {
                SpecialityView temp = (SpecialityView)obj;
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
            return $"Код специальности = {this.SpecialityId}" + Environment.NewLine +
                    $"Код факультета = {this.FacultyId}" + Environment.NewLine +
                    $"Наименование факультета = {this.Faculty.Trim()}" + Environment.NewLine +
                    $"Код формы обучения = {this.FormOfStudyId}" + Environment.NewLine +
                    $"Наименование формы обучения = {this.FormOfStudy}" + Environment.NewLine +
                    $"Шифр специальности = {this.Cipher.Trim()}" + Environment.NewLine +
                    $"Наименование специальности = {this.Speciality.Trim()}" + Environment.NewLine +
                    $"Группа специальностей? {this.IsGroup}" + Environment.NewLine +
                    $"План набора (Бюджет) = {this.BudgetCountPlace}" + Environment.NewLine +
                    $"План набора (Платно) = {this.FeeCountPlace}" + Environment.NewLine +
                    $"План набора (Целевые места) = {this.TargetCountPlace}" + Environment.NewLine;
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
