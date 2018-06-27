using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс работы с данными таблицы "Тип учебного заведения"
    /// </summary>
    interface ITypeOfSchoolService
    {
        /// <summary>
        /// Добавление нового типа учебного заведения
        /// </summary>
        /// <param name="typeOfSchool">Новый тип учебного заведения</param>
        /// <returns>Новая запись</returns>
        TypeOfSchool InsertTypeOfSchool(TypeOfSchool typeOfSchool);
        /// <summary>
        /// Обновление типа учебного заведения
        /// </summary>
        /// <param name="typeOfSchool">Редактирование записи</param>
        /// <returns>Отредактированная запись</returns>
        TypeOfSchool UpdateTypeOfSchool(TypeOfSchool typeOfSchool);
        /// <summary>
        /// Удаление типа учебного заведения
        /// </summary>
        /// <param name="typeOfSchool">Удаляемый тип учебного заведения</param>
        void DeleteTypeOfSchool(TypeOfSchool typeOfSchool);
        /// <summary>
        /// Получение списка типов учебных заведений
        /// </summary>
        /// <returns>Список типов учебных заведений</returns>
        List<TypeOfSchool> GetTypeOfSchools();
        /// <summary>
        /// Получение типа учебного заведения по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись типа учебного заведения</returns>
        TypeOfSchool GetTypeOfSchool(int id);
        /// <summary>
        /// Получение типа учебного заведения по наименованию
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <returns>Запись типа учебного заведения</returns>
        TypeOfSchool GetTypeOfSchool(string name);
    }
}
