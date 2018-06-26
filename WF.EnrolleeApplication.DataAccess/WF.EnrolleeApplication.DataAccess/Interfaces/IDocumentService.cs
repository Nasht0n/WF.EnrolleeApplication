using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс работы с данными таблицы "Документы"
    /// </summary>
    interface IDocumentService
    {
        /// <summary>
        /// Добавление новой записи
        /// </summary>
        /// <param name="document">Новый документ</param>
        /// <returns>Добавленная запись</returns>
        Document InsertDocument(Document document);
        /// <summary>
        /// Обновление записи
        /// </summary>
        /// <param name="document">Редактируемый документ</param>
        /// <returns>Отредактированная запись</returns>
        Document UpdateDocument(Document document);
        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="document">Удаляемая запись</param>
        void DeleteDocument(Document document);
        /// <summary>
        /// Получение списка документов
        /// </summary>
        /// <returns>Список документов</returns>
        List<Document> GetDocuments();
        /// <summary>
        /// Получение документа по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись документа</returns>
        Document GetDocument(int id);
        /// <summary>
        /// Получение документа по наименованию
        /// </summary>
        /// <param name="fullname">Наименование документа</param>
        /// <returns>Запись документа</returns>
        Document GetDocument(string fullname);
    }
}
