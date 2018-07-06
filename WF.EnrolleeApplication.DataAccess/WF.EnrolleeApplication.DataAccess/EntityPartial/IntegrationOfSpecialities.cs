using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    /// <summary>
    /// Таблица "Интеграция специальностей" 
    /// Частичный класс для переопределения внутренних методов
    /// </summary>
    public partial class IntegrationOfSpecialities
    {
        /// <summary>
        /// Переопределенный метод сравнения
        /// </summary>
        /// <param name="obj">Объект сущности</param>
        /// <returns>true - если объекты равны</returns>
        public override bool Equals(object obj)
        {
            if(obj is IntegrationOfSpecialities && obj!=null)
            {
                IntegrationOfSpecialities temp = (IntegrationOfSpecialities)obj;
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
            return $"Код интеграции = {this.IntegrationId}" + Environment.NewLine +
                   $"Специальность первой ступени = {this.FirstSpecialityId}" + Environment.NewLine +
                   $"Специальность второй ступени = {this.SecondarySpecialityId}" + Environment.NewLine;
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
