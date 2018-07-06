using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    /// <summary>
    /// Таблица "Экзаменнационная схема" 
    /// Частичный класс для переопределения внутренних методов
    /// </summary>
    public partial class ExamSchema
    {
        /// <summary>
        /// Переопределенный метод сравнения
        /// </summary>
        /// <param name="obj">Объект сущности</param>
        /// <returns>true - если объекты равны</returns>
        public override bool Equals(object obj)
        {
            if(obj is ExamSchema && obj!=null)
            {
                ExamSchema temp = (ExamSchema)obj;
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
            return $"Код экзаменнационной схемы = {this.ExamSchemaId}" + Environment.NewLine +
                   $"Код специальности = {this.SpecialityId}" + Environment.NewLine +
                   $"Код дисциплины = {this.DisciplineId}" + Environment.NewLine +
                   $"Уровень дисциплины = {this.DisciplineRank}" + Environment.NewLine;
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
