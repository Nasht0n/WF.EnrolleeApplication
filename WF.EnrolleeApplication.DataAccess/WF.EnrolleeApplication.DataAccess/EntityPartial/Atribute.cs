using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    /// <summary>
    /// Часть класса-сущности таблицы "Льготы" базы данных АИС "Абитуриент"
    /// Реализованны переопределенные методы класса
    /// </summary>
    public partial class Atribute
    {
        /// <summary>
        /// Переопределенный метод строкового представления объекта
        /// </summary>
        /// <returns>Строка формата {[Уникальный идентификатор]. [Наименование области]}</returns>
        public override string ToString()
        {
            if (this.IsDiscount) return $"{this.AtributeId}. {this.Fullname}({this.Shortname}). Являеется льготой? Да.";
            else return $"{this.AtributeId}. {this.Fullname}({this.Shortname}). Являеется льготой? Нет.";
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
            if(obj!= null && obj is Atribute)
            {
                Atribute temp = (Atribute)obj;
                if (temp.AtributeId == this.AtributeId && temp.Fullname == this.Fullname && temp.Shortname == this.Shortname && temp.IsDiscount == this.IsDiscount) return true;
                else return false;
            }
            return false;
        }
    }
}
