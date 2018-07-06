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
            string result = $"Код дисциплины = {this.DisciplineId}" + Environment.NewLine +
                   $"Код типа оценивания = {this.BasisForAssessingId}" + Environment.NewLine +
                   $"Наименование дисциплины = {this.Name.Trim()}" + Environment.NewLine +
                   $"Группа предметов? {this.IsGroup}" + Environment.NewLine +
                   $"Дисциплина группы предметов? {this.IsAlternative}" + Environment.NewLine;
            if (this.DisciplineGroupId.HasValue)
                result += $"Код группы специальностей = {this.DisciplineGroupId.Value}" + Environment.NewLine;
            if (!string.IsNullOrWhiteSpace(this.ConsultDate))
                result += $"Дата консультации = {this.ConsultDate.Trim()}" + Environment.NewLine;
            if (!string.IsNullOrWhiteSpace(this.EntryExamDate))
                result += $"Дата вступительного испытания = {this.EntryExamDate.Trim()}" + Environment.NewLine;
            if (this.StageCount.HasValue)
                result += $"Количество этапов = {this.StageCount.Value}" + Environment.NewLine;
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
