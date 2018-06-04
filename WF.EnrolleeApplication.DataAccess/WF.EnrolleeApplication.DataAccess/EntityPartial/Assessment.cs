using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    /// <summary>
    /// Часть класса-сущности таблицы "Оценки" базы данных АИС "Абитуриент"
    /// Реализованны переопределенные методы класса
    /// </summary>
    public partial class Assessment
    {
        /// <summary>
        /// Переопределенный метод строкового представления объекта
        /// </summary>
        /// <returns>Строка формата {[Сокращенное наименование специальности][Сокращенное наименование формы обучения] | [Фамилия абитуриента] [Имя абитуриента] | [Дисциплина] | [Оценка]}</returns>
        public override string ToString()
        {
            return $"{this.Enrollee.Speciality.Shortname}{this.Enrollee.Speciality.FormOfStudy.Shortname} | {this.Enrollee.RuSurname} {this.Enrollee.RuName} | {this.Discipline.Name} | {this.Estimation}";
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
            if (obj is Assessment && obj != null)
            {
                Assessment temp = (Assessment)obj;
                if (temp.AssessmentId == this.AssessmentId && temp.DisciplineId == this.DisciplineId &&
                     temp.EnrolleeId == this.EnrolleeId && temp.Estimation == this.Estimation &&
                       temp.SertCode == this.SertCode && temp.SertDate == this.SertDate &&
                         temp.ChangeDiscipline == this.ChangeDiscipline) return true;
                else return false;
            }
            return false;
        }
    }
}
