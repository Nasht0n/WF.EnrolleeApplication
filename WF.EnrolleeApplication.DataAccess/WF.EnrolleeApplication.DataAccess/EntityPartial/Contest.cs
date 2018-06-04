using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    /// <summary>
    /// Часть класса-сущности таблицы "Тип конкурса" базы данных АИС "Абитуриент"
    /// Реализованны переопределенные методы класса
    /// </summary>
    public partial class Contest
    {
        /// <summary>
        /// Переопределенный метод строкового представления объекта
        /// </summary>
        /// <returns>Строка формата {[Уникальный идентификатор]. [Наименование типа конкурса]}</returns>
        public override string ToString()
        {
            return $"{this.ContestId}. {this.Name}";
        }
        /// <summary>
        /// Переопределенный метод получения хеш-кода объекта
        /// </summary>
        /// <returns>Хеш-код объекта</returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        /// <summary>
        /// Переопределенный метод сравнения двух объектов
        /// </summary>
        /// <param name="obj">Объекта сравнения</param>
        /// <returns>True — Если объекты одинаковы; False — Если объекты различны.</returns>
        public override bool Equals(object obj)
        {
            if(obj is Contest && obj != null)
            {
                Contest temp = (Contest)obj;
                if (temp.ContestId == this.ContestId && temp.Name == this.Name) return true;
                else return false;
            }
            return false;
        }
    }
}
