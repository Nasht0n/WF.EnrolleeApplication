using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс работы с данными таблицы "Специальность второй ступени"
    /// </summary>
    interface ISecondarySpecialityService
    {
        /// <summary>
        /// Добавление новой специальности второй ступени
        /// </summary>
        /// <param name="secondarySpeciality">Специальность второй ступени</param>
        /// <returns>Новая запись</returns>
        SecondarySpeciality InsertSecondarySpeciality(SecondarySpeciality secondarySpeciality);
        /// <summary>
        /// Обновление специальности второй ступени
        /// </summary>
        /// <param name="secondarySpeciality">Редактируемая специальность второй ступени</param>
        /// <returns>Отредактированная запись</returns>
        SecondarySpeciality UpdateSecondarySpeciality(SecondarySpeciality secondarySpeciality);
        /// <summary>
        /// Удаление специальности второй ступени
        /// </summary>
        /// <param name="secondarySpeciality">Удаляемая специальность второй ступени</param>
        void DeleteSecondarySpeciality(SecondarySpeciality secondarySpeciality);
        /// <summary>
        /// Получение списка специальностей второй ступени
        /// </summary>
        /// <returns>Список спецниальностей второй ступени</returns>
        List<SecondarySpeciality> GetSecondarySpecialities();
        /// <summary>
        /// Получение записи специальности второй ступени по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись специальности второй ступени</returns>
        SecondarySpeciality GetSecondarySpeciality(int id);
        /// <summary>
        /// Получение записи специальности второй ступени по шифру специальности
        /// </summary>
        /// <param name="cipher">Шифр специальности</param>
        /// <returns>Запись специальности второй ступени</returns>
        SecondarySpeciality GetSecondarySpeciality(string cipher);
    }
}
