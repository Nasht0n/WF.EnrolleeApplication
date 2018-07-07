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
    /// Класс доступа к данным таблицы "Атрибуты"
    /// </summary>
    public class AtributeService : IAtributeService
    {
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public AtributeService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление атрибута
        /// </summary>
        /// <param name="atribute">Удаляемая запись</param>
        public void DeleteAtribute(Atribute atribute)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к удалению атрибута.");
            try
            {
                var atributeToDelete = context.Atribute.FirstOrDefault(a => a.AtributeId == atribute.AtributeId);
                if (atributeToDelete != null)
                {
                    context.Atribute.Remove(atributeToDelete);
                    context.SaveChanges();
                    logger.Debug("Удаление успешно завершено.");
                }
            }
            catch(SqlException sqlEx)
            {
                logger.Error("Ошибка удаления атрибута.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch(Exception ex)
            {
                logger.Error("Ошибка удаления атрибута.");
                logger.Error($"Ошибка — {ex.Message}.");
            }
        }
        /// <summary>
        /// Получение атрибута по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Искомая запись атрибута</returns>
        public Atribute GetAtribute(int id)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску атрибута по уникальному идентификатору.");
            try
            {
                var atributeById = context.Atribute.AsNoTracking().FirstOrDefault(a => a.AtributeId == id);
                if(atributeById !=null) logger.Debug($"Поиск окончен. Запись найдена {atributeById.ToString()}.");
                return atributeById;
            }
            catch(SqlException sqlEx)
            {
                logger.Error("Ошибка поиска атрибута.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch(Exception ex)
            {
                logger.Error("Ошибка поиска атрибута.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }            
        }
        /// <summary>
        /// Получение атрибута по наименованию
        /// </summary>
        /// <param name="fullname">Полное наименование атрибута</param>
        /// <returns>Искомая запись атрибута</returns>
        public Atribute GetAtribute(string fullname)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску атрибута по наименованию атрибута.");
            try
            {
                var atributeByFullname = context.Atribute.AsNoTracking().FirstOrDefault(a => a.Fullname == fullname);
                if (atributeByFullname != null) logger.Debug($"Поиск окончен. Запись найдена {atributeByFullname.ToString()}.");
                return atributeByFullname;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска атрибута.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска атрибута.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }         
        }
        /// <summary>
        /// Получение списка атрибутов
        /// </summary>
        /// <returns>Список атрибутов</returns>
        public List<Atribute> GetAtributes()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка атрибутов.");
            try
            {
                var atributes = context.Atribute.AsNoTracking().ToList();
                logger.Debug($"Поиск окончен. Количество записей: {atributes.Count}.");
                return atributes;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска списка атрибутов.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска списка атрибутов.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }            
        }
        /// <summary>
        /// Получение списка атрибутов
        /// </summary>
        /// <param name="IsDiscount">Фильтр льгот</param>
        /// <returns>Отфильтрованный список</returns>
        public List<Atribute> GetAtributes(bool IsDiscount)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка атрибутов, являющихся льготами.");
            try
            {
                var atributes = context.Atribute.AsNoTracking().Where(a => a.IsDiscount == IsDiscount).ToList();
                logger.Debug($"Поиск окончен. Количество записей: {atributes.Count}.");
                return atributes;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска списка атрибутов, являющихся льготами.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска списка атрибутов, являющихся льготами.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Добавление нового атрибута
        /// </summary>
        /// <param name="atribute">Добавляемый атрибут</param>
        /// <returns>Новая запись</returns>
        public Atribute InsertAtribute(Atribute atribute)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению атрибута.");
            try
            {
                logger.Debug($"Добавляемая запись: {atribute.ToString()}");
                context.Atribute.Add(atribute);
                context.SaveChanges();
                logger.Debug($"Новая запись успешно добавлена.");
                return atribute;
            }
            catch(SqlException sqlEx)
            {
                logger.Error("Ошибка добавления атрибута.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch(Exception ex)
            {
                logger.Error("Ошибка добавления атрибута.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }            
        }
        /// <summary>
        /// Обновление атрибута
        /// </summary>
        /// <param name="atribute">Редактируемый атритут</param>
        /// <returns>Обновленная запись</returns>
        public Atribute UpdateAtribute(Atribute atribute)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к обновлению атрибута.");
            try
            {
                var atributeToUpdate = context.Atribute.FirstOrDefault(a => a.AtributeId == atribute.AtributeId);
                logger.Debug($"Текущая запись: {atributeToUpdate.ToString()}");
                atributeToUpdate.Fullname = atribute.Fullname;
                atributeToUpdate.Shortname = atribute.Shortname;
                atributeToUpdate.IsDiscount = atribute.IsDiscount;
                context.SaveChanges();
                logger.Debug($"Новая запись: {atributeToUpdate.ToString()}");
                return atributeToUpdate;
            }
            catch(SqlException sqlEx)
            {
                logger.Error("Ошибка редактирования атрибута.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch(Exception ex)
            {
                logger.Error("Ошибка редактирования атрибута.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }            
        }
    }
}
