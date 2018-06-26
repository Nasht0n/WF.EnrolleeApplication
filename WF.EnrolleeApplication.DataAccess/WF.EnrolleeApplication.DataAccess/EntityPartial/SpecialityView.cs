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
            return $"Код специальности = {this.SpecialityId}" +
                    $"\nКод факультета = {this.FacultyId}" +
                    $"\nНаименование факультета = {this.Faculty.Trim()}" +
                    $"\nКод формы обучения = {this.FormOfStudyId}" +
                    $"\nНаименование формы обучения = {this.FormOfStudy}" +
                    $"\nШифр специальности = {this.Cipher.Trim()}" +
                    $"\nНаименование специальности = {this.Speciality.Trim()}" +
                    $"\nГруппа специальностей? {this.IsGroup}" +
                    $"\nПлан набора (Бюджет) = {this.BudgetCountPlace}" +
                    $"\nПлан набора (Платно) = {this.FeeCountPlace}" +
                    $"\nПлан набора (Целевые места) = {this.TargetCountPlace}";
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
