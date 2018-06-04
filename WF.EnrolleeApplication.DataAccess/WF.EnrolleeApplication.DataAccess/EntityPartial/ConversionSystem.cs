using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    /// <summary>
    /// Часть класса-сущности таблицы "Система перевода" базы данных АИС "Абитуриент"
    /// Реализованны переопределенные методы класса
    /// </summary>
    public partial class ConversionSystem
    {
        /// <summary>
        /// Переопределенный метод строкового представления объекта
        /// </summary>
        /// <returns>Строка формата {[Оценка в пятибальной системе] = [Оценка в десятибальной]}</returns>
        public override string ToString()
        {
            return $"{this.Five} = {this.Ten}";
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
            if(obj is ConversionSystem && obj!=null)
            {
                ConversionSystem temp = (ConversionSystem)obj;
                if (temp.ConversionSystemId == this.ConversionSystemId && temp.Five == this.Five && temp.Ten == this.Ten) return true;
                else return false;
            }
            return false;
        }
    }
}
