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
            return Environment.NewLine +
                   $"Информация об объекте: " + Environment.NewLine + 
                   $"Код оценки = {this.AssessmentId}" + Environment.NewLine +
                   $"Код дисциплины = {this.DisciplineId}" + Environment.NewLine +
                   $"Код абитуриента = {this.EnrolleeId}" + Environment.NewLine +
                   $"Фамилия = {this.RuSurname.Trim()}" + Environment.NewLine +
                   $"Имя = {this.RuName.Trim()}" + Environment.NewLine +
                   $"Специальность = {this.Speciality.Trim()}" + Environment.NewLine +
                   $"Форма обучения = {this.FormOfStudy.Trim()}" + Environment.NewLine +
                   $"Номер личного дела = {this.NumberOfDeal}" + Environment.NewLine +
                   $"Оценка = {this.Estimation}";
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
