using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    /// <summary>
    /// Таблица "Оценки" 
    /// Частичный класс для переопределения внутренних методов
    /// </summary>
    public partial class Assessment
    {
        /// <summary>
        /// Переопределенный метод сравнения
        /// </summary>
        /// <param name="obj">Объект сущности</param>
        /// <returns>true - если объекты равны</returns>
        public override bool Equals(object obj)
        {
            if(obj is Assessment && obj!=null)
            {
                Assessment temp = (Assessment)obj;
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
            string result = $"Assessment - Entity" +
                            $"\nИдентификатор оценки = {this.AssessmentId}" +
                            $"\nИдентификатор дисциплины = {this.DisciplineId}" +
                            $"\nИдентификатор абитуриента = {this.EnrolleeId}" +
                            $"\nОценка абитуриента = {this.Estimation}";
            if (!string.IsNullOrWhiteSpace(this.SertCode))
            {
                result +=$"\n№ Сертификата - {this.SertCode}";
            }
            else if (!string.IsNullOrWhiteSpace(this.SertDate))
            {
                result += $"\nДата выдачи сертификата - {this.SertDate}";
            }
            else if (!string.IsNullOrWhiteSpace(this.ChangeDiscipline))
            {
                result += $"\nПроизведена замена предмета. Дисциплина по сертификату - {this.ChangeDiscipline}";
            }
            return result;
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
