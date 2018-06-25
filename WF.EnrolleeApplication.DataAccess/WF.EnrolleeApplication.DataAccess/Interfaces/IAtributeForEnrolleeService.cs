using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс работы с данными таблицы "Атрибуты абитуриента"
    /// </summary>
    interface IAtributeForEnrolleeService
    {
        /// <summary>
        /// Добавление новой записи
        /// </summary>
        /// <param name="atributeForEnrollee">Добавляемые данные</param>
        /// <returns>Запись атрибута абитуриента</returns>
        AtributeForEnrollee InsertAtributeForEnrollee(AtributeForEnrollee atributeForEnrollee);
        /// <summary>
        /// Обновление записи
        /// </summary>
        /// <param name="atributeForEnrollee">Редактируемые данные</param>
        /// <returns>Запись атрибута абитуриента</returns>
        AtributeForEnrollee UpdateAtributeForEnrollee(AtributeForEnrollee atributeForEnrollee);
        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="atributeForEnrollee">Удаляемые данные</param>
        void DeleteAtributeForEnrollee(AtributeForEnrollee atributeForEnrollee);
        /// <summary>
        /// Получение списка атрибутов абитуриентов
        /// </summary>
        /// <returns>Список атрибутов абитуриента</returns>
        List<AtributeForEnrollee> GetAtributeForEnrollees();
        /// <summary>
        /// Получение списка атрибутов абитуриента
        /// </summary>
        /// <param name="enrollee">Фильтр абитуриент</param>
        /// <returns>Отфильтрованный список</returns>
        List<AtributeForEnrollee> GetAtributeForEnrollees(Enrollee enrollee);
        /// <summary>
        /// Получение списка атрибутов абитуриентов
        /// </summary>
        /// <param name="atribute">Фильтр атрибут</param>
        /// <returns>Отфильтрованный список</returns>
        List<AtributeForEnrollee> GetAtributeForEnrollees(Atribute atribute);
        /// <summary>
        /// Получение атрибута абитуриента по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Искомая запись</returns>
        AtributeForEnrollee GetAtributeForEnrollee(int id);
        /// <summary>
        /// Получение атрибута абитуриента по абитуриенту и атрибуту
        /// </summary>
        /// <param name="atribute">Фильтр атрибут</param>
        /// <param name="enrollee">Фильтр абитуриент</param>
        /// <returns>Искомая запись</returns>
        AtributeForEnrollee GetAtributeForEnrollee(Atribute atribute, Enrollee enrollee);
    }
}
