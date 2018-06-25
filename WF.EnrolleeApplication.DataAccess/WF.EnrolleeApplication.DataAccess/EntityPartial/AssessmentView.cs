using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    /// <summary>
    /// Представление "Оценки" 
    /// Частичный класс для переопределения внутренних методов
    /// </summary>
    public partial class AssessmentView
    {
        /// <summary>
        /// Переопределенный метод сравнения
        /// </summary>
        /// <param name="obj">Объект сущности</param>
        /// <returns>true - если объекты равны</returns>
        public override bool Equals(object obj)
        {
            if(obj is AssessmentView && obj!=null)
            {
                AssessmentView temp = (AssessmentView)obj;
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
            return $"Код оценки = {this.AssessmentId}" +
                   $"\nКод дисциплины = {this.DisciplineId}" +
                   $"\nКод абитуриента = {this.EnrolleeId}" +
                   $"\nФамилия = {this.RuSurname.Trim()}" +
                   $"\nИмя = {this.RuName.Trim()}" +
                   $"\nСпециальность = {this.Speciality.Trim()}" +
                   $"\nФорма обучения = {this.FormOfStudy.Trim()}" +
                   $"\nНомер личного дела = {this.NumberOfDeal}" +
                   $"\nОценка = {this.Estimation}";
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
