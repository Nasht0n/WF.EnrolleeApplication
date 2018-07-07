using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    /// <summary>
    /// Таблица "Области" 
    /// Частичный класс для переопределения внутренних методов
    /// </summary>
    public partial class Area
    {        
        /// <summary>
        /// Переопределенный метод сравнения
        /// </summary>
        /// <param name="obj">Объект сущности</param>
        /// <returns>true - если объекты равны</returns>
        public override bool Equals(object obj)
        {
            if(obj is Area && obj!=null)
            {
                Area temp = (Area)obj;
                if (temp.GetHashCode() == this.GetHashCode()) return true;
                else return false;
            }
            return false;
        }
        /// <summary>
        /// Переопределенный метод вывода данных класса
        /// </summary>
        /// <returns>Строку формата</returns>
        public override string ToString()
        {
            return Environment.NewLine +
                   $"Информация об объекте: " + Environment.NewLine +
                   $"Код области = {this.AreaId}" + Environment.NewLine +
                   $"Наименование области = {this.Name})";
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
