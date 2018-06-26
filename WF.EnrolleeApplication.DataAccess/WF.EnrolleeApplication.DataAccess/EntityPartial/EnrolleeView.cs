using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    /// <summary>
    /// Представление "Абитуриенты" 
    /// Частичный класс для переопределения внутренних методов
    /// </summary>
    public partial class EnrolleeView
    {
        /// <summary>
        /// Переопределенный метод сравнения
        /// </summary>
        /// <param name="obj">Объект сущности</param>
        /// <returns>true - если объекты равны</returns>
        public override bool Equals(object obj)
        {
            if (obj is EnrolleeView && obj != null)
            {
                EnrolleeView temp = (EnrolleeView)obj;
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
            string result = $"Код абитуриента = {this.EnrolleeId}" +
                            $"\nКод специальности = {this.SpecialityId}" +
                            $"\nКод конкурса = {this.ContestId}" +
                            $"\nКод основания зачисления = {this.ReasonForAddmissionId}" +
                            $"\nКод типа финансирования = {this.FinanceTypeId}" +
                            $"\nКод статуса абитуриента = {this.StateId}" +
                            $"\nКод оператора = {this.EmployeeId}" +
                            $"\nНаименование формы обучения = {this.FormOfStudy}" +
                            $"\nНаименование специальности = {this.Speciality}" +
                            $"\nФамилия = {this.Surname}" +
                            $"\nИмя = {this.Name}" +
                            $"\nТип конкурса = {this.Contest}" +
                            $"\nВид основания зачисления  = {this.ReasonForAddmission}" +
                            $"\nТип финансирования = {this.Finance}" +
                            $"\nНомер личного дела = {this.NumberOfDeal}" +
                            $"\nСтатус абитуриента = {this.Status.Trim()}" +
                            $"\nСокращенное наименование специальности = {this.SpecialityShortname.Trim()}" +
                            $"\nСокращенное наименование формы обучения = {this.FormOfStudyShortname.Trim()}";
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
