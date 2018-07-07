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
    /// Класс доступа к данным таблицы "Области"
    /// </summary>
    public class AreaService : IAreaService
    {
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public AreaService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление области 
        /// </summary>
        /// <param name="area">Удаляемая область</param>
        public void DeleteArea(Area area)
        {
            logger.Trace("Попытка подключения к источнику данных");
            logger.Trace("Подготовка к удалению области");
            try
            {
                var areaToDelete = context.Area.FirstOrDefault(a => a.AreaId == area.AreaId);
                if (areaToDelete != null)
                {
                    context.Area.Remove(areaToDelete);
                    context.SaveChanges();
                    logger.Debug("Удаление успешно завершено");
                }
            }            
            catch(SqlException sqlEx)
            {
                logger.Error("Ошибка удаления области");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка удаления области");
                logger.Error($"Ошибка — {ex.Message}.");
            }
        }
        /// <summary>
        /// Получение области по уникальному идентификатору
        /// </summary>
        /// <param name="id">УИД Области</param>
        /// <returns>Объект области</returns>
        public Area GetArea(int id)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску области по уникальному идентификатору.");
            try
            {
                var areaById = context.Area.AsNoTracking().FirstOrDefault(a => a.AreaId == id);
                if(areaById!=null) logger.Debug($"Запись найдена {areaById.ToString()}");
                return areaById;
            }
            catch(SqlException sqlEx)
            {
                logger.Error("Ошибка поиска области");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch(Exception ex)
            {
                logger.Error("Ошибка поиска области");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение области по наименованию
        /// </summary>
        /// <param name="name">Наименование области</param>
        /// <returns>Объект области</returns>
        public Area GetArea(string name)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску области по наименованию.");
            try
            {
                var areaByName = context.Area.AsNoTracking().FirstOrDefault(a => a.Name == name);
                if (areaByName != null) logger.Debug($"Запись найдена {areaByName.ToString()}");
                return areaByName;
            }
            catch(SqlException sqlEx)
            {
                logger.Error("Ошибка поиска области");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch(Exception ex)
            {
                logger.Error("Ошибка поиска области");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }            
        }
        /// <summary>
        /// Получение списка областей
        /// </summary>
        /// <returns>Список областей</returns>
        public List<Area> GetAreas()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к получению списка областей.");
            try
            {
                var areas = context.Area.AsNoTracking().ToList();
                if (areas.Count != 0) logger.Debug($"Список получен. Количество записей: [{areas.Count}].");
                else logger.Debug($"Поиск окончен. Список пуст.");
                return areas;
            }
            catch(SqlException sqlEx)
            {
                logger.Error("Ошибка поиска области");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch(Exception ex)
            {
                logger.Error("Ошибка поиска области");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Вставка новой области
        /// </summary>
        /// <param name="area">Новая область</param>
        /// <returns>Объект новой области</returns>
        public Area InsertArea(Area area)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению новой области.");
            try
            {
                logger.Debug($"Добавляемая область: {area.ToString()}");
                context.Area.Add(area);
                context.SaveChanges();
                logger.Debug($"Новая запись успешно добавлена.");
                return area;
            }
            catch(SqlException sqlEx)
            {
                logger.Error("Ошибка добавления области");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch(Exception ex)
            {
                logger.Error("Ошибка добавления области");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }     
        }
        /// <summary>
        /// Редактирование области
        /// </summary>
        /// <param name="area">Редактируемая область</param>
        /// <returns>Отредактированная область</returns>
        public Area UpdateArea(Area area)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к редактированию данных выбранной области.");
            try
            {                
                var areaToUpdate = context.Area.FirstOrDefault(a => a.AreaId == area.AreaId);
                logger.Debug($"Запись до редактирования области: {areaToUpdate.ToString()}");              
                areaToUpdate.Name = area.Name;
                context.SaveChanges();
                logger.Debug($"Запись после редактирования области: {areaToUpdate.ToString()}");
                return areaToUpdate;
            }
            catch(SqlException sqlEx)
            {
                logger.Error("Ошибка поиска области");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch(Exception ex)
            {
                logger.Error("Ошибка поиска области");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
    }
}
