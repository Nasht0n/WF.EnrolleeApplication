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
            string result = Environment.NewLine +
                            $"Информация об объекте: " + Environment.NewLine + 
                            $"Код абитуриента = {this.EnrolleeId}" + Environment.NewLine+
                            $"Код специальности = {this.SpecialityId}" + Environment.NewLine +
                            $"Код конкурса = {this.ContestId}" + Environment.NewLine +
                            $"Код основания зачисления = {this.ReasonForAddmissionId}" + Environment.NewLine +
                            $"Код типа финансирования = {this.FinanceTypeId}" + Environment.NewLine +
                            $"Код статуса абитуриента = {this.StateId}" + Environment.NewLine +
                            $"Код оператора = {this.EmployeeId}" + Environment.NewLine +
                            $"Наименование формы обучения = {this.FormOfStudy}" + Environment.NewLine +
                            $"Наименование специальности = {this.Speciality}" + Environment.NewLine +
                            $"Фамилия = {this.Surname}" + Environment.NewLine +
                            $"Имя = {this.Name}" + Environment.NewLine +
                            $"Тип конкурса = {this.Contest}" + Environment.NewLine +
                            $"Вид основания зачисления  = {this.ReasonForAddmission}" + Environment.NewLine +
                            $"Тип финансирования = {this.Finance}" + Environment.NewLine +
                            $"Номер личного дела = {this.NumberOfDeal}" + Environment.NewLine +
                            $"Статус абитуриента = {this.Status.Trim()}" + Environment.NewLine +
                            $"Сокращенное наименование специальности = {this.SpecialityShortname.Trim()}" + Environment.NewLine +
                            $"Сокращенное наименование формы обучения = {this.FormOfStudyShortname.Trim()}";
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
