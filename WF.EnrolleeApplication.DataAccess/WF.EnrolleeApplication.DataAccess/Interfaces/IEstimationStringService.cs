using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс работы с данными таблицы "Представление оценок"
    /// </summary>
    interface IEstimationStringService
    {
        /// <summary>
        /// Новое представление оценок
        /// </summary>
        /// <param name="estimationString">Представление оценки</param>
        /// <returns>Новая запись</returns>
        EstimationString InsertEstimationString(EstimationString estimationString);
        /// <summary>
        /// Удаление представление оценки
        /// </summary>
        /// <param name="estimationString">Удаляемая оценка</param>
        void DeleteEstimationString(EstimationString estimationString);
        /// <summary>
        /// Получение списка оценок
        /// </summary>
        /// <returns>Список оценок</returns>
        List<EstimationString> GetEstimationStrings();
        /// <summary>
        /// Получение представления оценки
        /// </summary>
        /// <param name="number">Оценка цифрой</param>
        /// <param name="text">Оценка прописью</param>
        /// <returns>Запись представления оценки</returns>
        EstimationString GetEstimationString(int number, string text);
        /// <summary>
        /// Получение цифрого представления оценки
        /// </summary>
        /// <param name="estimation">Строковое представление оценки</param>
        /// <returns>Оценка цифрой</returns>
        int EstimationAsNumber(string estimation);
        /// <summary>
        /// Получение строкового представление оценки
        /// </summary>
        /// <param name="number">Цифровое представление оценки</param>
        /// <returns>Оценка прописью</returns>
        string EstimationAsText(int number);
    }
}
