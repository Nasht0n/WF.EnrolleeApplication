using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс работы с данными таблицы "Интеграция специальностей"
    /// </summary>
    interface IIntegrationOfSpecialitiesService
    {
        /// <summary>
        /// Добавление интеграции специальностей
        /// </summary>
        /// <param name="integrationOfSpecialities">Новая запись об интеграции специальностей</param>
        /// <returns>Новая запись</returns>
        IntegrationOfSpecialities InsertIntegrationOfSpecialities(IntegrationOfSpecialities integrationOfSpecialities);
        /// <summary>
        /// Обновление интеграции специальностей
        /// </summary>
        /// <param name="integrationOfSpecialities">Редактируемая запись об интеграции специальностей</param>
        /// <returns>Отредактированная запись</returns>
        IntegrationOfSpecialities UpdateIntegrationOfSpecialities(IntegrationOfSpecialities integrationOfSpecialities);
        /// <summary>
        /// Удаление интеграции специальностей
        /// </summary>
        /// <param name="integrationOfSpecialities">Удаляемая запись об интеграции специальностей</param>
        void DeleteIntegrationOfSpecialities(IntegrationOfSpecialities integrationOfSpecialities);
        /// <summary>
        /// Получение списка интеграций специальностей
        /// </summary>
        /// <returns>Список интеграций специальностей</returns>
        List<IntegrationOfSpecialities> GetIntegrationOfSpecialities();
        /// <summary>
        ///  Получение списка интеграций специальностей
        /// </summary>
        /// <param name="speciality">Фильтр по специальности первой ступени</param>
        /// <returns>Список интеграций специальностей</returns>
        List<IntegrationOfSpecialities> GetIntegrationOfSpecialities(Speciality speciality);
        /// <summary>
        /// Получение списка интеграций специальностей
        /// </summary>
        /// <param name="secondarySpeciality">Фильтр по специальности второй ступени</param>
        /// <returns>Список интеграций специальностей</returns>
        List<IntegrationOfSpecialities> GetIntegrationOfSpecialities(SecondarySpeciality secondarySpeciality);
        /// <summary>
        /// Получение записи об интеграции специальностей по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись интеграции</returns>
        IntegrationOfSpecialities GetIntegrationOfSpecialities(int id);
        /// <summary>
        /// Получение записи об интеграции специальностей по параметрам
        /// </summary>
        /// <param name="speciality">Фильтр по специальности первой ступени</param>
        /// <param name="secondarySpeciality">Фильтр по специальности второй ступени</param>
        /// <returns>Запись интеграции</returns>
        IntegrationOfSpecialities GetIntegrationOfSpecialities(Speciality speciality, SecondarySpeciality secondarySpeciality);
    }
}
