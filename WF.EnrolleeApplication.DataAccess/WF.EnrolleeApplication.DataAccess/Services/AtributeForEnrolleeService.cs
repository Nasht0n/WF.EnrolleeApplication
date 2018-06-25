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
    /// Класс доступа к данным таблицы "Атрибуты абитуриента"
    /// </summary>
    public class AtributeForEnrolleeService : IAtributeForEnrolleeService
    {
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public AtributeForEnrolleeService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="atributeForEnrollee">Удаляемые данные</param>
        public void DeleteAtributeForEnrollee(AtributeForEnrollee atributeForEnrollee)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к удалению данных атрибута абитуриента.");
            try
            {
                logger.Debug($"Поиск записи оценки для удаления. Удаляемый объект : {atributeForEnrollee.ToString()}.");
                AtributeForEnrollee atributeForEnrolleeToDelete = context.AtributeForEnrollee.FirstOrDefault(a => a.Id == atributeForEnrollee.Id);
                if (atributeForEnrolleeToDelete != null)
                {
                    context.AtributeForEnrollee.Remove(atributeForEnrolleeToDelete);
                    context.SaveChanges();
                    logger.Debug("Удаление атрибута абитуриента успешно завершено.");
                }            
            }
            catch(SqlException sqlEx)
            {
                logger.Error("Ошибка удаления данных атрибута абитуриента.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch(Exception ex)
            {
                logger.Error("Ошибка удаления данных атрибута абитуриента.");
                logger.Error($"Ошибка — {ex.Message}.");
            }
        }
        /// <summary>
        /// Получение атрибута абитуриента по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Искомая запись</returns>
        public AtributeForEnrollee GetAtributeForEnrollee(int id)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к получению данных атрибута абитуриента.");
            try
            {
                logger.Debug($"Поиск по уникальному идентификатору, id = {id}.");
                AtributeForEnrollee atributeForEnrollee = context.AtributeForEnrollee.AsNoTracking().FirstOrDefault(a => a.Id == id);
                if (atributeForEnrollee != null) logger.Debug($"Поиск окончен.\n Запись : {atributeForEnrollee.ToString()}.");
                return atributeForEnrollee;
            }
            catch(SqlException sqlEx)
            {
                logger.Error("Ошибка получения данных атрибута абитуриента.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch(Exception ex)
            {
                logger.Error("Ошибка получения данных атрибута абитуриента.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }            
        }
        /// <summary>
        /// Получение атрибута абитуриента по абитуриенту и атрибуту
        /// </summary>
        /// <param name="atribute">Фильтр атрибут</param>
        /// <param name="enrollee">Фильтр абитуриент</param>
        /// <returns>Искомая запись</returns>
        public AtributeForEnrollee GetAtributeForEnrollee(Atribute atribute, Enrollee enrollee)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к получению данных атрибута абитуриента.");
            try
            {
                logger.Debug($"Поиск по записи абитуриента(Код абитуриента = {enrollee.EnrolleeId}) и атрибута(Код атрибута = {atribute.AtributeId}).");
                AtributeForEnrollee atributeForEnrollee = context.AtributeForEnrollee.AsNoTracking().FirstOrDefault(a => a.AtributeId == atribute.AtributeId && a.EnrolleeId == enrollee.EnrolleeId);
                if (atributeForEnrollee != null) logger.Debug($"Поиск окончен.\n Запись : {atributeForEnrollee.ToString()}.");
                return atributeForEnrollee;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения данных атрибута абитуриента.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения данных атрибута абитуриента.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }            
        }
        /// <summary>
        /// Получение списка атрибутов абитуриентов
        /// </summary>
        /// <returns>Список атрибутов абитуриента</returns>
        public List<AtributeForEnrollee> GetAtributeForEnrollees()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к получению списка данных атрибутов абитуриентов.");
            try
            {
                logger.Debug($"Получение списка атрибутов абитуриентов");
                List<AtributeForEnrollee> atributesForEnrollee = context.AtributeForEnrollee.AsNoTracking().ToList();
                logger.Debug($"Поиск окончен. Количество записей : {atributesForEnrollee.Count}.");
                return atributesForEnrollee;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка данных атрибутов абитуриентов.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка данных атрибутов абитуриентов.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }            
        }
        /// <summary>
        /// Получение списка атрибутов абитуриента
        /// </summary>
        /// <param name="enrollee">Фильтр абитуриент</param>
        /// <returns>Отфильтрованный список</returns>
        public List<AtributeForEnrollee> GetAtributeForEnrollees(Enrollee enrollee)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к получению списка данных атрибутов абитуриента.");
            try
            {
                logger.Debug($"Получение списка атрибутов абитуриента (Код абитуриента = {enrollee.EnrolleeId}).");
                List<AtributeForEnrollee> atributesForEnrollee = context.AtributeForEnrollee.AsNoTracking().Where(a => a.EnrolleeId == enrollee.EnrolleeId).ToList();
                logger.Debug($"Поиск окончен. Количество записей : {atributesForEnrollee.Count}.");
                return atributesForEnrollee;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка данных атрибутов абитуриента.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка данных атрибутов абитуриента.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка атрибутов абитуриентов
        /// </summary>
        /// <param name="atribute">Фильтр атрибут</param>
        /// <returns>Отфильтрованный список</returns>
        public List<AtributeForEnrollee> GetAtributeForEnrollees(Atribute atribute)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к получению списка абитуриентов с атрибутом.");
            try
            {
                logger.Debug($"Получение списка абитуриентов с атрибутом (Код атрибута = {atribute.AtributeId}).");
                List<AtributeForEnrollee> atributesForEnrollee = context.AtributeForEnrollee.AsNoTracking().Where(a => a.AtributeId == atribute.AtributeId).ToList();
                logger.Debug($"Поиск окончен. Количество записей : {atributesForEnrollee.Count}.");
                return atributesForEnrollee;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка абитуриентов с атрибутом.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка абитуриентов с атрибутом.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Добавление новой записи
        /// </summary>
        /// <param name="atributeForEnrollee">Добавляемые данные</param>
        /// <returns>Запись атрибута абитуриента</returns>
        public AtributeForEnrollee InsertAtributeForEnrollee(AtributeForEnrollee atributeForEnrollee)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению атрибута абитуриенту.");
            try
            {
                logger.Debug($"Добавляемая запись: {atributeForEnrollee.ToString()}");
                context.AtributeForEnrollee.Add(atributeForEnrollee);
                context.SaveChanges();
                logger.Debug($"Атрибут успешно добавлен абитуриенту.");
                return atributeForEnrollee;
            }
            catch(SqlException sqlEx)
            {
                logger.Error("Ошибка добавления атрибута абитуриенту.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch(Exception ex)
            {
                logger.Error("Ошибка добавления атрибута абитуриенту.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }            
        }
        /// <summary>
        /// Обновление записи
        /// </summary>
        /// <param name="atributeForEnrollee">Редактируемые данные</param>
        /// <returns>Запись атрибута абитуриента</returns>
        public AtributeForEnrollee UpdateAtributeForEnrollee(AtributeForEnrollee atributeForEnrollee)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к обновлению атрибутов абитуриента.");
            try
            {
                AtributeForEnrollee atributeForEnrolleeToUpdate = context.AtributeForEnrollee.FirstOrDefault(a => a.Id == atributeForEnrollee.Id);
                logger.Debug($"Текущая запись: {atributeForEnrolleeToUpdate.ToString()}");
                atributeForEnrolleeToUpdate.AtributeId = atributeForEnrollee.AtributeId;
                atributeForEnrolleeToUpdate.EnrolleeId = atributeForEnrollee.EnrolleeId;
                logger.Debug($"Отредактированная запись: {atributeForEnrolleeToUpdate.ToString()}");
                return atributeForEnrolleeToUpdate;
            }
            catch(SqlException sqlEx)
            {
                logger.Error("Ошибка редактирования атрибутов абитуриента.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch(Exception ex)
            {
                logger.Error("Ошибка редактирования атрибутов абитуриента.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }            
        }
    }
}
