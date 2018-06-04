using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    /// <summary>
    /// Часть класса-сущности таблицы "Виды гражданства" базы данных АИС "Абитуриент"
    /// Реализованны переопределенные методы класса
    /// </summary>
    public partial class Citizenship
    {
        /// <summary>
        /// Переопределенный метод строкового представления объекта
        /// </summary>
        /// <returns>Строка формата {[Уникальный идентификатор]. [Полное наименование](Сокращенное наименование).}</returns>
        public override string ToString()
        {
            return $"{this.CitizenshipId}. {this.Fullname}({this.Shortname}).";
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
            if(obj != null && obj is Citizenship)
            {
                Citizenship temp = (Citizenship)obj;
                if (temp.CitizenshipId == this.CitizenshipId && temp.Fullname == this.Fullname && temp.Shortname == this.Shortname) return true;
                else return false;
            }
            return false;
        }
    }
}
