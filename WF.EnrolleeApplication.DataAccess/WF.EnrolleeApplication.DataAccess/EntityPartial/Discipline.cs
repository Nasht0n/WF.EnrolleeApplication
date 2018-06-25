using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    /// <summary>
    /// Таблица "Дисциплины" 
    /// Частичный класс для переопределения внутренних методов
    /// </summary>
    public partial class Discipline
    {
        /// <summary>
        /// Переопределенный метод сравнения
        /// </summary>
        /// <param name="obj">Объект сущности</param>
        /// <returns>true - если объекты равны</returns>
        public override bool Equals(object obj)
        {
            if (obj is Discipline && obj != null)
            {
                Discipline temp = (Discipline)obj;
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
            string result = $"Код дисциплины = {this.DisciplineId}" +
                   $"\nКод типа оценивания = {this.BasisForAssessingId}" +
                   $"\nНаименование дисциплины = {this.Name.Trim()}" +
                   $"\nГруппа предметов? {this.IsGroup}" +
                   $"\nДисциплина группы предметов? {this.IsAlternative}";
            if (this.DisciplineGroupId.HasValue)
                result += $"\nКод группы специальностей = {this.DisciplineGroupId.Value}";
            if (!string.IsNullOrWhiteSpace(this.ConsultDate))
                result += $"\nДата консультации = {this.ConsultDate.Trim()}";
            if (!string.IsNullOrWhiteSpace(this.EntryExamDate))
                result += $"\nДата вступительного испытания = {this.EntryExamDate.Trim()}";
            if (this.StageCount.HasValue)
                result += $"\nКоличество этапов = {this.StageCount.Value}";
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
