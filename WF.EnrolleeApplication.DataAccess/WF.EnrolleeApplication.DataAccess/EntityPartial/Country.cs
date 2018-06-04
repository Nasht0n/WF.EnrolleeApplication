using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    /// <summary>
    /// Часть класса-сущности таблицы Страны" базы данных АИС "Абитуриент"
    /// Реализованны переопределенные методы класса
    /// </summary>
    public partial class Country
    {
        /// <summary>
        /// Переопределенный метод строкового представления объекта
        /// </summary>
        /// <returns>Строка формата {[Уникальный идентификатор]. [Наименование страны]}</returns>
        public override string ToString()
        {
            return $"{this.CountryId}. {this.Name}";
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
            if(obj is Country && obj != null)
            {
                Country temp = (Country)obj;
                if (temp.CountryId == this.CountryId && temp.Name == this.Name) return true;
                else return false;
            }
            return false;
        }
    }
}
