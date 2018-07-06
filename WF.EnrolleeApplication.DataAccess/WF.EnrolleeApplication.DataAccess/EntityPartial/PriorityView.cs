using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    /// <summary>
    /// Представление "Приоритеты специальности" 
    /// Частичный класс для переопределения внутренних методов
    /// </summary>
    public partial class PriorityView
    {
        /// <summary>
        /// Переопределенный метод сравнения
        /// </summary>
        /// <param name="obj">Объект сущности</param>
        /// <returns>true - если объекты равны</returns>
        public override bool Equals(object obj)
        {
            if(obj is PriorityView && obj!=null)
            {
                PriorityView temp = (PriorityView)obj;
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
            return $"Код приоритета = {this.PriorityId}" + Environment.NewLine +
                   $"Код абитуриента = {this.EnrolleeId}" + Environment.NewLine +
                   $"Код специальности = {this.SpecialityId}" + Environment.NewLine +
                   $"Полное наименование = {this.Fullname}" + Environment.NewLine +
                   $"Уровень приоритета = {this.PriorityLevel}" + Environment.NewLine;
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
