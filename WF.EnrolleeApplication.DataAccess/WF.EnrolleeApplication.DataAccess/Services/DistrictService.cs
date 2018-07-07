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
    /// Класс доступа к данным таблицы "Районы"
    /// </summary>
    public class DistrictService : IDistrictService
    {
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public DistrictService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="district">Удаляемый район</param>
        public void DeleteDistrict(District district)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к удалению района.");
            try
            {
                var districtToDelete = context.District.FirstOrDefault(d => d.DistrictId == district.DistrictId);
                if (districtToDelete != null)
                {
                    context.District.Remove(districtToDelete);
                    context.SaveChanges();
                    logger.Debug("Удаление успешно завершено.");
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка удаления записи района.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка удаления записи района.");
                logger.Error($"Ошибка — {ex.Message}.");
            }
        }
        /// <summary>
        /// Получение района по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись района</returns>
        public District GetDistrict(int id)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску района по уникальному идентификатору.");
            try
            {
                var districtById = context.District.AsNoTracking().FirstOrDefault(d => d.DistrictId == id);
                if (districtById != null) logger.Debug($"Поиск окончен. Запись найдена {districtById.ToString()}.");
                return districtById;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи района.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи района.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение района по наименованию
        /// </summary>
        /// <param name="name">Наименование района</param>
        /// <returns>Запись района</returns>
        public District GetDistrict(string name)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску района по наименованию.");
            try
            {
                var districtByName = context.District.AsNoTracking().FirstOrDefault(d => d.Name == name);
                if (districtByName != null) logger.Debug($"Поиск окончен. Запись найдена {districtByName.ToString()}.");
                return districtByName;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи района.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи района.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка районов
        /// </summary>
        /// <returns>Список районов</returns>
        public List<District> GetDistricts()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка районов.");
            try
            {
                var districts = context.District.AsNoTracking().ToList();
                if (districts.Count != 0) logger.Debug($"Поиск окончен. Количество записей: {districts.Count}.");
                else logger.Debug($"Поиск окончен. Список пуст.");
                return districts;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка районов.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка районов.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Добавление новой записи
        /// </summary>
        /// <param name="district">Новый район</param>
        /// <returns>Добавленная запись</returns>
        public District InsertDistrict(District district)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению района");
            try
            {
                logger.Debug($"Добавляемая запись {district.ToString()}");
                context.District.Add(district);
                context.SaveChanges();
                logger.Debug($"Новая запись успешно добавлена.");
                return district;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка добавления района.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка добавления района.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Обновление записи
        /// </summary>
        /// <param name="district">Редактируемый район</param>
        /// <returns>Отредактированная запись</returns>
        public District UpdateDistrict(District district)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к обновлению района.");
            try
            {
                var districtToUpdate = context.District.FirstOrDefault(d => d.DistrictId == district.DistrictId);
                logger.Debug($"Текущая запись {districtToUpdate.ToString()}");
                districtToUpdate.Name = district.Name;
                context.SaveChanges();
                logger.Debug($"Новая запись {districtToUpdate.ToString()}");
                return districtToUpdate;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка редактирования района.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка редактирования района.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
    }
}
