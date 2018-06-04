using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    /// <summary>
    /// Часть класса-сущности таблицы "Атрибуты абитуриента" базы данных АИС "Абитуриент"
    /// Реализованны переопределенные методы класса
    /// </summary>
    public partial class AtributeForEnrollee
    {
        /// <summary>
        /// Переопределенный метод строкового представления объекта
        /// </summary>
        /// <returns>Строка формата {[Фамилия абитуриента] [Имя абитуриента] — [Наименование льготы]}</returns>
        public override string ToString()
        {
            return $"{this.Enrollee.RuSurname} {this.Enrollee.RuName} — {this.Atribute.Fullname}"; 
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
            if(obj is AtributeForEnrollee && obj != null)
            {
                AtributeForEnrollee temp = (AtributeForEnrollee)obj;
                if (temp.Id == this.Id && temp.AtributeId == this.AtributeId && temp.EnrolleeId == this.EnrolleeId) return true;
                else return false;
            }
            return false;
        }
    }
}
