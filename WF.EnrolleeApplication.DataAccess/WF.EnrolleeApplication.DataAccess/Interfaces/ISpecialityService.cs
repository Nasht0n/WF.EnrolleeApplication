using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс работы с данными таблицы "Специальность"
    /// </summary>
    interface ISpecialityService
    {
        /// <summary>
        /// Добавление специальности первой ступени
        /// </summary>
        /// <param name="speciality">Новая специальность первой ступени</param>
        /// <returns>Новая запись</returns>
        Speciality InsertSpeciality(Speciality speciality);
        /// <summary>
        /// Обновление специальности первой ступени
        /// </summary>
        /// <param name="speciality">Редактируемая специальность первой ступени</param>
        /// <returns>Отредактированная специальность</returns>
        Speciality UpdateSpeciality(Speciality speciality);
        /// <summary>
        /// Удаление специальности первой ступени
        /// </summary>
        /// <param name="speciality">Удаляемая специальность первой ступени</param>
        void DeleteSpeciality(Speciality speciality);
        /// <summary>
        /// Получение списка специальностей первой ступени
        /// </summary>
        /// <returns>Список специальностей первой ступени</returns>
        List<Speciality> GetSpecialities();
        /// <summary>
        /// Получение списка специальностей первой ступени
        /// </summary>
        /// <param name="faculty">Фильтр - факультет</param>
        /// <returns>Список специальностей первой ступени</returns>
        List<Speciality> GetSpecialities(Faculty faculty);
        /// <summary>
        /// Получение списка специальностей первой ступени
        /// </summary>
        /// <param name="formOfStudy">Фильтр - форма обучения</param>
        /// <returns>Список специальностей первой ступени</returns>
        List<Speciality> GetSpecialities(FormOfStudy formOfStudy);
        /// <summary>
        /// Получение списка специальностей первой ступени
        /// </summary>
        /// <param name="faculty">Фильтр - факультет</param>
        /// <param name="formOfStudy">Фильтр - форма обучения</param>
        /// <returns>Список специальностей первой ступени</returns>
        List<Speciality> GetSpecialities(Faculty faculty, FormOfStudy formOfStudy);
        /// <summary>
        /// Получение списка специальностей первой ступени
        /// </summary>
        /// <param name="groupSpeciality">Фильтр - группа специальностей</param>
        /// <returns>Список специальностей первой ступени</returns>
        List<Speciality> GetSpecialities(Speciality groupSpeciality);
        /// <summary>
        /// Получение записи специальности первой ступени по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись специальности первой ступени</returns>
        Speciality GetSpeciality(int id);
        /// <summary>
        /// Получение записи специальности первой ступени по шифру специальности
        /// </summary>
        /// <param name="cipher">Шифр специальности</param>
        /// <returns>Запись специальности первой ступени</returns>
        Speciality GetSpeciality(string cipher);
    }
}
