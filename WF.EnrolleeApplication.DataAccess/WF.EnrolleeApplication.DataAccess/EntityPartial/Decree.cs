using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    /// <summary>
    /// Таблица "Приказы" 
    /// Частичный класс для переопределения внутренних методов
    /// </summary>
    public partial class Decree
    {
        /// <summary>
        /// Переопределенный метод сравнения
        /// </summary>
        /// <param name="obj">Объект сущности</param>
        /// <returns>true - если объекты равны</returns>
        public override bool Equals(object obj)
        {
            if (obj is Decree && obj != null)
            {
                Decree temp = (Decree)obj;
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
            return $"Код приказа = {this.DecreeId}" +
                   $"\nНомер приказа = {this.DecreeNumber.Trim()}" +
                   $"\nДата приказа = {this.DecreeDate.ToShortDateString().Trim()}" +
                   $"\nНомер протокола = {this.ProtocolNumber.Trim()}" +
                   $"\nДата протокола = {this.ProtocolDate.ToShortDateString().Trim()}" +
                   $"\nСодержание приказа = {this.Content.Trim()}";
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
