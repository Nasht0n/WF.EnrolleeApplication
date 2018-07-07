using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    /// <summary>
    /// Таблица "Специальности первой ступени" 
    /// Частичный класс для переопределения внутренних методов
    /// </summary>
    public partial class Speciality
    {
        /// <summary>
        /// Переопределенный метод сравнения
        /// </summary>
        /// <param name="obj">Объект сущности</param>
        /// <returns>true - если объекты равны</returns>
        public override bool Equals(object obj)
        {
            if(obj is Speciality && obj!=null)
            {
                Speciality temp = (Speciality)obj;
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
            return Environment.NewLine +
                   $"Информация об объекте: " + Environment.NewLine + 
                   $"Код специальности = {this.SpecialityId}" + Environment.NewLine +
                   $"Код факультета = {this.FacultyId}" + Environment.NewLine +
                   $"Код формы обучения = {this.FormOfStudyId}" + Environment.NewLine +
                   $"Наименование специальности = {this.Fullname.Trim()}" + Environment.NewLine +
                   $"Специализация = {this.Specialization.Trim()}" + Environment.NewLine +
                   $"Шифр специальности = {this.Cipher.Trim()}" + Environment.NewLine +
                   $"Сокращенное наименование специальности = {this.Shortname.Trim()}" + Environment.NewLine +
                   $"План набора (Бюджет) = {this.BudgetCountPlace}" + Environment.NewLine +
                   $"План набора (Платно) = {this.FeeCountPlace}" + Environment.NewLine +
                   $"План набора (Целевые места) = {this.TargetCountPlace}" + Environment.NewLine +
                   $"Группа специальностей? {this.IsGroup}" + Environment.NewLine +
                   $"Специальность группы специальностей? {this.IsAlternative}" + Environment.NewLine +
                   $"Код группы специальностей = {this.SpecialityGroupId}";
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
