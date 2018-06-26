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
            return $"Код специальности = {this.SpecialityId}" +
                   $"\nКод факультета = {this.FacultyId}" +
                   $"\nКод формы обучения = {this.FormOfStudyId}" +
                   $"\nНаименование специальности = {this.Fullname.Trim()}" +
                   $"\nСпециализация = {this.Specialization.Trim()}" +
                   $"\nШифр специальности = {this.Cipher.Trim()}" +
                   $"\nСокращенное наименование специальности = {this.Shortname.Trim()}" +
                   $"\nПлан набора (Бюджет) = {this.BudgetCountPlace}" +
                   $"\nПлан набора (Платно) = {this.FeeCountPlace}" +
                   $"\nПлан набора (Целевые места) = {this.TargetCountPlace}" +
                   $"\nГруппа специальностей? {this.IsGroup}" +
                   $"\nСпециальность группы специальностей? {this.IsAlternative}" +
                   $"\nКод группы специальностей = {this.SpecialityGroupId}";
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
