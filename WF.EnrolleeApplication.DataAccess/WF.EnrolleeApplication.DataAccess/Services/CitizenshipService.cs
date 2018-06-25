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
    /// Класс доступа к данным таблицы "Виды гражданства"
    /// </summary>
    public class CitizenshipService: ICitizenshipService
    {
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public CitizenshipService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="citizenship">Удаляемая запись</param>
        public void DeleteCitizenship(Citizenship citizenship)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к удалению вида гражданства.");
            try
            {
                logger.Debug($"Поиск записи вида гражданства для удаления. Удаляемый объект : {citizenship.ToString()}.");
                Citizenship citizenshipToDelete = context.Citizenship.FirstOrDefault(c => c.CitizenshipId == citizenship.CitizenshipId);
                if (citizenshipToDelete != null)
                {
                    context.Citizenship.Remove(citizenship);
                    context.SaveChanges();
                    logger.Debug("Удаление вида гражданства успешно завершено.");
                }
            }
            catch(SqlException sqlEx)
            {
                logger.Error("Ошибка удаления вида гражданства.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch(Exception ex)
            {
                logger.Error("Ошибка удаления вида гражданства.");
                logger.Error($"Ошибка — {ex.Message}.");
            }            
        }
        /// <summary>
        /// Получение вида гражданства по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Найденная запись вида гражданства</returns>
        public Citizenship GetCitizenship(int id)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску вида гражданства.");
            try
            {
                logger.Debug($"Поиск вида гражданства по уникальному идентификатору = {id}.");
                Citizenship citizenshipById = context.Citizenship.AsNoTracking().FirstOrDefault(c => c.CitizenshipId == id);
                if (citizenshipById != null) logger.Debug($"Поиск окончен. Искомая запись: {citizenshipById.ToString()}.");
                return citizenshipById;
            }
            catch(SqlException sqlEx)
            {
                logger.Error("Ошибка поиска вида гражданства.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch(Exception ex)
            {
                logger.Error("Ошибка поиска вида гражданства.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }           
        }
        /// <summary>
        /// Получение вида гражданства по наименованию
        /// </summary>
        /// <param name="fullname">Наименование вида гражданства</param>
        /// <returns>Найденная запись вида гражданства</returns>
        public Citizenship GetCitizenship(string fullname)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску вида гражданства.");
            try
            {
                logger.Debug($"Поиск вида гражданства по наименованию = {fullname}.");
                Citizenship citizenshipByFullname = context.Citizenship.AsNoTracking().FirstOrDefault(c => c.Fullname == fullname);
                if (citizenshipByFullname != null) logger.Debug($"Поиск окончен. Искомая запись: {citizenshipByFullname.ToString()}.");
                return citizenshipByFullname;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска вида гражданства.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска вида гражданства.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }       
        }
        /// <summary>
        /// Получение списка видов гражданства
        /// </summary>
        /// <returns>Список видов гражданства</returns>
        public List<Citizenship> GetCitizenships()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка видов гражданства.");
            try
            {
                logger.Debug($"Получение списка типов оценивания.");
                List<Citizenship> citizenships = context.Citizenship.AsNoTracking().ToList();
                logger.Debug($"Поиск окончен. Количество записей: {citizenships.Count}.");
                return citizenships;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка видов гражданства.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка видов гражданства.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Добавление новой записи
        /// </summary>
        /// <param name="citizenship">Новый вид гражданства</param>
        /// <returns>Добавленная запись</returns>
        public Citizenship InsertCitizenship(Citizenship citizenship)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению вида гражданства.");
            try
            {
                logger.Debug($"Добавляемая запись: {citizenship.ToString()}");
                context.Citizenship.Add(citizenship);
                context.SaveChanges();
                logger.Debug($"Вид гражданства успешно добавлен.");
                return citizenship;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка добавления вида гражданства.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка добавления вида гражданства.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Обновление записи
        /// </summary>
        /// <param name="citizenship">Редактируемая запись вида гражданства</param>
        /// <returns>Отредактированная запись</returns>
        public Citizenship UpdateCitizenship(Citizenship citizenship)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к обновлению вида гражданства.");
            try
            {
                Citizenship citizenshipToUpdate = context.Citizenship.FirstOrDefault(c => c.CitizenshipId == citizenship.CitizenshipId);
                logger.Debug($"Текущая запись: {citizenshipToUpdate.ToString()}");
                citizenshipToUpdate.Fullname = citizenship.Fullname;
                citizenshipToUpdate.Shortname = citizenship.Shortname;
                context.SaveChanges();
                logger.Debug($"Новая запись: {citizenshipToUpdate.ToString()}");
                return citizenshipToUpdate;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка редактирования вида гражданства.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка редактирования вида гражданства.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
    }
}
