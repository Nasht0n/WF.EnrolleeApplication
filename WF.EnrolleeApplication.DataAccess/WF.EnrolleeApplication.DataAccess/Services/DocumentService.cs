using NLog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    /// <summary>
    /// Класс доступа к данным таблицы "Документы"
    /// </summary>
    public class DocumentService : IDocumentService
    {
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public DocumentService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="document">Удаляемая запись</param>
        public void DeleteDocument(Document document)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к удалению документа.");
            try
            {
                var documentToDelete = context.Document.FirstOrDefault(d => d.DocumentId == document.DocumentId);
                if (documentToDelete != null)
                {
                    context.Document.Remove(documentToDelete);
                    context.SaveChanges();
                    logger.Debug("Удаление успешно завершено.");
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка удаления записи документа.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка удаления записи документа.");
                logger.Error($"Ошибка — {ex.Message}.");
            }
        }
        /// <summary>
        /// Получение документа по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись документа</returns>
        public Document GetDocument(int id)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску документа по уникальному идентификатору.");
            try
            {
                var documentById = context.Document.AsNoTracking().FirstOrDefault(d => d.DocumentId == id);
                if (documentById != null) logger.Debug($"Поиск окончен. Запись найдена {documentById.ToString()}.");
                return documentById;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи документа.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи документа.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение документа по наименованию
        /// </summary>
        /// <param name="name">Наименование документа</param>
        /// <returns>Запись документа</returns>
        public Document GetDocument(string name)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску документа по наименованию.");
            try
            {
                var documentByName = context.Document.AsNoTracking().FirstOrDefault(d => d.Name == name);
                if (documentByName != null) logger.Debug($"Поиск окончен. Запись найдена {documentByName.ToString()}.");
                return documentByName;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи документа.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи документа.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка документов
        /// </summary>
        /// <returns>Список документов</returns>
        public List<Document> GetDocuments()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка документов.");
            try
            {
                var documents = context.Document.AsNoTracking().ToList();
                if(documents.Count!=0) logger.Debug($"Поиск окончен. Количество записей: {documents.Count}.");
                else logger.Debug($"Поиск окончен. Список пуст.");
                return documents;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка документов.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка документов.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Добавление новой записи
        /// </summary>
        /// <param name="document">Новый документ</param>
        /// <returns>Добавленная запись</returns>
        public Document InsertDocument(Document document)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению документа");
            try
            {
                logger.Debug($"Добавляемая запись {document.ToString()}");
                context.Document.Add(document);
                context.SaveChanges();
                logger.Debug($"Новая запись успешно добавлена.");
                return document;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка добавления документа.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка добавления документа.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Обновление записи
        /// </summary>
        /// <param name="document">Редактируемый документ</param>
        /// <returns>Отредактированная запись</returns>
        public Document UpdateDocument(Document document)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к обновлению документа.");
            try
            {
                var documentToUpdate = context.Document.FirstOrDefault(d => d.DocumentId == document.DocumentId);
                logger.Debug($"Текущая запись {documentToUpdate.ToString()}");
                documentToUpdate.Name = document.Name;
                context.SaveChanges();
                logger.Debug($"Новая запись {documentToUpdate.ToString()}");
                return documentToUpdate;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка редактирования документа.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка редактирования документа.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
    }
}
