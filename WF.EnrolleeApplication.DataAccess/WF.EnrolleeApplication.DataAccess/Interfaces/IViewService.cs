using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс работы с данными представлений
    /// </summary>
    interface IViewService
    {     
        /// <summary>
        /// Получение списка пользователей
        /// </summary>
        /// <returns>Список пользователей</returns>
        List<EmployeeView> GetEmployees();
        /// <summary>
        /// Получение списка абитуриентов
        /// </summary>
        /// <param name="employee">Фильтр пользователь (оператор)</param>
        /// <returns>Список абитуриентов</returns>
        List<EnrolleeView> GetEnrollees(Employee employee);
        /// <summary>
        /// Получение списка абитуриентов
        /// </summary>
        /// <param name="speciality">Фильтр специальность</param>
        /// <returns>Список абитуриентов</returns>
        List<EnrolleeView> GetEnrollees(Speciality speciality);
        /// <summary>
        /// Получение списка специальностей
        /// </summary>
        /// <returns>Список специальностей</returns>
        List<SpecialityView> GetSpecialities();
        /// <summary>
        /// Получение списка оценок
        /// </summary>
        /// <returns>Список оценок</returns>
        List<AssessmentView> GetAssessments();
        /// <summary>
        /// Получение списка оценок
        /// </summary>
        /// <param name="discipline">Фильтр дисциплина</param>
        /// <returns>Список оценок</returns>
        List<AssessmentView> GetAssessments(Discipline discipline);
        /// <summary>
        /// Получение списка приоритетов
        /// </summary>
        /// <param name="enrollee">Фильтр абитуриент</param>
        /// <returns>Список приоритетов</returns>
        List<PriorityView> GetPriorities(Enrollee enrollee);
    }
}
