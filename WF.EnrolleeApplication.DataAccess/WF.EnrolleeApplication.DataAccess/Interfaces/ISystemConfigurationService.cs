using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс работы с данными таблицы "Настройки системы"
    /// </summary>
    interface ISystemConfigurationService
    {
        /// <summary>
        /// Добавление настройки системы
        /// </summary>
        /// <param name="systemConfiguration">Новая настройка системы</param>
        /// <returns>Новая запись</returns>
        SystemConfiguration InsertSystemConfiguration(SystemConfiguration systemConfiguration);
        /// <summary>
        /// Обновление настройки системы
        /// </summary>
        /// <param name="systemConfiguration">Редактируемая настройка системы</param>
        /// <returns>Отредактированная настройка системы</returns>
        SystemConfiguration UpdateSystemConfiguration(SystemConfiguration systemConfiguration);
        /// <summary>
        /// Удаление настройки системы
        /// </summary>
        /// <param name="systemConfiguration">Удаляемая настройка системы</param>
        void DeleteSystemConfiguration(SystemConfiguration systemConfiguration);
        /// <summary>
        /// Получение списка настроек системы
        /// </summary>
        /// <returns>Список настроек</returns>
        List<SystemConfiguration> GetSystemConfigurations();
        /// <summary>
        /// Получение настройки системы по наименованию
        /// </summary>
        /// <param name="name">Наименование параметра</param>
        /// <returns>Запись настройки</returns>
        SystemConfiguration GetSystemConfiguration(string name);
    }
}
