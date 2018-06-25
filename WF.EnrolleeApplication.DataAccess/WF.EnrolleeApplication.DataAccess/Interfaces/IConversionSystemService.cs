using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс работы с данными таблицы "Системы перевода"
    /// </summary>
    interface IConversionSystemService
    {
        /// <summary>
        /// Добавление новой записи
        /// </summary>
        /// <param name="conversionSystem">Новая запись системы перевода</param>
        /// <returns>Добавленная запись</returns>
        ConversionSystem InsertConversionSystem(ConversionSystem conversionSystem);
        /// <summary>
        /// Удаление новой записи
        /// </summary>
        /// <param name="conversionSystem">Удаляемая запись системы перевода</param>
        void DeleteConversionSystem(ConversionSystem conversionSystem);
        /// <summary>
        /// Получение списка оценок системы перевода
        /// </summary>
        /// <returns>Список системы перевода оценок</returns>
        List<ConversionSystem> GetConversions();
        /// <summary>
        /// Получение записи системы перевода по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный сертификат</param>
        /// <returns>Запись системы перевода</returns>
        ConversionSystem GetConversion(int id);
        /// <summary>
        /// Получение записи системы перевода по оценкам
        /// </summary>
        /// <param name="five">Оценка в пятибалльной системе</param>
        /// <param name="ten">Оценка в десятибалльной системе</param>
        /// <returns>Запись системы перевода</returns>
        ConversionSystem GetConversion(double five, double ten);
        /// <summary>
        /// Получение оценки в пятибалльной системе
        /// </summary>
        /// <param name="ten">Оценка в десятибалльной системе</param>
        /// <returns>Оценка в пятибалльной системе</returns>
        double ConversionToFive(double ten);
        /// <summary>
        /// Получение оценки в десятибалльной системе
        /// </summary>
        /// <param name="five">Оценка в пятибалльной системе</param>
        /// <returns>Оценка в десятибалльной системе</returns>
        double ConversionToTen(double five);
    }
}
