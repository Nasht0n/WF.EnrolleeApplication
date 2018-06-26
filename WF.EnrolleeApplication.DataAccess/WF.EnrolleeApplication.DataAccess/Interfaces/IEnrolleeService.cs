using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс работы с данными таблицы "Абитуриенты"
    /// </summary>
    interface IEnrolleeService
    {
        /// <summary>
        /// Добавление абитуриента
        /// </summary>
        /// <param name="enrollee">Новый абитуриент</param>
        /// <returns>Новая запись</returns>
        Enrollee InsertEnrollee(Enrollee enrollee);
        /// <summary>
        /// Обновление абитуриента
        /// </summary>
        /// <param name="enrollee">Редактируемый абитуриент</param>
        /// <returns>Отредактированная запись</returns>
        Enrollee UpdateEnrollee(Enrollee enrollee);
        /// <summary>
        /// Удаление абитуриента
        /// </summary>
        /// <param name="enrollee">Удаляемая запись</param>
        void DeleteEnrollee(Enrollee enrollee);
        /// <summary>
        /// Получение списка абитуриентов
        /// </summary>
        /// <returns>Список абитуриентов</returns>
        List<Enrollee> GetEnrollees();
        /// <summary>
        /// Получение списка абитуриентов
        /// </summary>
        /// <param name="speciality">Специальность</param>
        /// <returns>Список абитуриентов</returns>
        List<Enrollee> GetEnrollees(Speciality speciality);
        /// <summary>
        /// Получение списка абитуриентов
        /// </summary>
        /// <param name="citizenship">Гражданство</param>
        /// <returns>Список абитуриентов</returns>
        List<Enrollee> GetEnrollees(Citizenship citizenship);
        /// <summary>
        /// Получение списка абитуриентов
        /// </summary>
        /// <param name="country">Страна</param>
        /// <returns>Список абитуриентов</returns>
        List<Enrollee> GetEnrollees(Country country);
        /// <summary>
        /// Получение списка абитуриентов
        /// </summary>
        /// <param name="area">Область</param>
        /// <returns>Список абитуриентов</returns>
        List<Enrollee> GetEnrollees(Area area);
        /// <summary>
        /// Получение списка абитуриентов
        /// </summary>
        /// <param name="district">Район</param>
        /// <returns>Список абитуриентов</returns>
        List<Enrollee> GetEnrollees(District district);
        /// <summary>
        /// Получение списка абитуриентов
        /// </summary>
        /// <param name="typeOfSchool">Тип учебного заведения</param>
        /// <returns>Список абитуриентов</returns>
        List<Enrollee> GetEnrollees(TypeOfSchool typeOfSchool);
        /// <summary>
        /// Получение списка абитуриентов
        /// </summary>
        /// <param name="reasonForAddmission">Основание зачисления</param>
        /// <returns>Список абитуриентов</returns>
        List<Enrollee> GetEnrollees(ReasonForAddmission reasonForAddmission);
        /// <summary>
        /// Получение списка абитуриентов
        /// </summary>
        /// <param name="typeOfState">Статус</param>
        /// <returns>Список абитуриентов</returns>
        List<Enrollee> GetEnrollees(TypeOfState typeOfState);
        /// <summary>
        /// Получение списка абитуриентов
        /// </summary>
        /// <param name="employee">Пользователь</param>
        /// <returns>Список абитуриентов</returns>
        List<Enrollee> GetEnrollees(Employee employee);
        /// <summary>
        /// Получение списка абитуриентов
        /// </summary>
        /// <param name="decree">Приказ</param>
        /// <returns>Список абитуриентов</returns>
        List<Enrollee> GetEnrollees(Decree decree);
        /// <summary>
        /// Получение списка абитуриентов
        /// </summary>
        /// <param name="typeOfFinance">Тип финансирования</param>
        /// <returns>Список абитуриентов</returns>
        List<Enrollee> GetEnrollees(TypeOfFinance typeOfFinance);
        /// <summary>
        /// Получение абитуриента по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись абитуриента</returns>
        Enrollee GetEnrollee(int id);
        /// <summary>
        /// Получение абитуриента по номеру документа
        /// </summary>
        /// <param name="documentPersonalNumber">Личный номер документа</param>
        /// <returns>Запись абитуриента</returns>
        Enrollee GetEnrollee(string documentPersonalNumber);
    }
}
