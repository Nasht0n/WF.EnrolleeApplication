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
    /// Класс доступа к данным таблицы "Типы оценивания"
    /// </summary>
    public class BasisForAssessingService : IBasisForAssessingService
    {
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public BasisForAssessingService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="basisForAssessing">Удаляемая запись</param>
        public void DeleteBasisForAssessing(BasisForAssessing basisForAssessing)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к удалению типа оценивания.");
            try
            {
                logger.Debug($"Поиск записи типа оценивания для удаления. Удаляемый объект : {basisForAssessing.ToString()}.");
                BasisForAssessing basisForAssessingToDelete = context.BasisForAssessing.FirstOrDefault(b => b.BasisForAssessingId == basisForAssessing.BasisForAssessingId);
                if (basisForAssessingToDelete != null)
                {
                    context.BasisForAssessing.Remove(basisForAssessingToDelete);
                    context.SaveChanges();
                    logger.Debug("Удаление типа оценивания успешно завершено.");
                }
            }
            catch(SqlException sqlEx)
            {
                logger.Error("Ошибка удаления типа оценивания.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch(Exception ex)
            {
                logger.Error("Ошибка удаления типа оценивания.");
                logger.Error($"Ошибка — {ex.Message}.");
            }            
        }
        /// <summary>
        /// Получение типа оценки по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Объект типа оценивания</returns>
        public BasisForAssessing GetBasisForAssessing(int id)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску типа оценивания.");
            try
            {
                logger.Debug($"Поиск типа оценивания по уникальному идентификатору = {id}.");
                BasisForAssessing basisForAssessingById = context.BasisForAssessing.AsNoTracking().FirstOrDefault(b => b.BasisForAssessingId == id);
                if (basisForAssessingById != null) logger.Debug($"Поиск окончен. Искомая запись: {basisForAssessingById.ToString()}.");
                return basisForAssessingById;
            }
            catch(SqlException sqlEx)
            {
                logger.Error("Ошибка поиска типа оценивания.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch(Exception ex)
            {
                logger.Error("Ошибка поиска типа оценивания.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }            
        }
        /// <summary>
        /// Получение типа оценки по наименованию
        /// </summary>
        /// <param name="name">Наименование типа</param>
        /// <returns>Объект типа оценивания</returns>
        public BasisForAssessing GetBasisForAssessing(string name)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску типа оценивания.");
            try
            {
                logger.Debug($"Поиск типа оценивания по наименованию = {name}.");
                BasisForAssessing basisForAssessingByName = context.BasisForAssessing.AsNoTracking().FirstOrDefault(b => b.Name == name);
                if (basisForAssessingByName != null) logger.Debug($"Поиск окончен. Искомая запись: {basisForAssessingByName.ToString()}.");
                return basisForAssessingByName;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска типа оценивания.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска типа оценивания.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }       
        }
        /// <summary>
        /// Получение списка типов оценивания
        /// </summary>
        /// <returns>Список типов оценивания</returns>
        public List<BasisForAssessing> GetBasisForAssessings()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка типов оценивания.");
            try
            {
                logger.Debug($"Получение списка типов оценивания.");
                List<BasisForAssessing> basisForAssessings = context.BasisForAssessing.AsNoTracking().ToList();
                logger.Debug($"Поиск окончен. Количество записей: {basisForAssessings.Count}.");
                return basisForAssessings;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка типов оценивания.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка типов оценивания.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }          
        }
        /// <summary>
        /// Добавление новой записи
        /// </summary>
        /// <param name="basisForAssessing">Тип оценивания</param>
        /// <returns>Объект добавленной записи</returns>
        public BasisForAssessing InsertBasisForAssessing(BasisForAssessing basisForAssessing)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению типа оценивания.");
            try
            {
                logger.Debug($"Добавляемая запись: {basisForAssessing.ToString()}");
                context.BasisForAssessing.Add(basisForAssessing);
                context.SaveChanges();
                logger.Debug($"Тип оценивания успешно добавлен.");
                return basisForAssessing;
            }
            catch(SqlException sqlEx)
            {
                logger.Error("Ошибка добавления типа оценивания.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch(Exception ex)
            {
                logger.Error("Ошибка добавления типа оценивания.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }            
        }
        /// <summary>
        /// Обновление записи
        /// </summary>
        /// <param name="basisForAssessing">Тип оценивания</param>
        /// <returns>Объект редактированной записи</returns>
        public BasisForAssessing UpdateBasisForAssessing(BasisForAssessing basisForAssessing)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к обновлению типа оценивания.");
            try
            {
                BasisForAssessing basisForAssessingToUpdate = context.BasisForAssessing.FirstOrDefault(b => b.BasisForAssessingId == basisForAssessing.BasisForAssessingId);
                logger.Debug($"Текущая запись: {basisForAssessingToUpdate.ToString()}");
                basisForAssessingToUpdate.Name = basisForAssessing.Name;
                context.SaveChanges();
                logger.Debug($"Новая запись: {basisForAssessingToUpdate.ToString()}");
                return basisForAssessingToUpdate;
            }
            catch(SqlException sqlEx)
            {
                logger.Error("Ошибка редактирования типа оценивания.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch(Exception ex)
            {
                logger.Error("Ошибка редактирования типа оценивания.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
    }
}
