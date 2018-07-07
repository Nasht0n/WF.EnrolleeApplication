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
    /// Класс доступа к данным таблицы "Специальность второй ступени"
    /// </summary>
    public class SecondarySpecialityService : ISecondarySpecialityService
    {
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public SecondarySpecialityService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление специальности второй ступени
        /// </summary>
        /// <param name="secondarySpeciality">Удаляемая специальность второй ступени</param>
        public void DeleteSecondarySpeciality(SecondarySpeciality secondarySpeciality)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к удалению специальности второй ступени.");
            try
            {
                var secondarySpecialityToDelete = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == secondarySpeciality.SecondarySpecialityId);
                if (secondarySpecialityToDelete != null)
                {
                    context.SecondarySpeciality.Remove(secondarySpecialityToDelete);
                    context.SaveChanges();
                    logger.Debug("Удаление записи успешно завершено.");
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка удаления записи специальности второй ступени.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка удаления записи специальности второй ступени.");
                logger.Error($"Ошибка — {ex.Message}.");
            }
        }
        /// <summary>
        /// Получение списка специальностей второй ступени
        /// </summary>
        /// <returns>Список спецниальностей второй ступени</returns>
        public List<SecondarySpeciality> GetSecondarySpecialities()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка специальностей второй ступени.");
            try
            {
                var secondarySpecialities = context.SecondarySpeciality.AsNoTracking().ToList();
                logger.Debug($"Поиск окончен. Количество записей: {secondarySpecialities.Count}.");
                return secondarySpecialities;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка специальностей второй ступени.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка специальностей второй ступени.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение записи специальности второй ступени по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись специальности второй ступени</returns>
        public SecondarySpeciality GetSecondarySpeciality(int id)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску специальности второй ступени по уникальному идентификатору.");
            try
            {
                var secondarySpecialityById = context.SecondarySpeciality.AsNoTracking().FirstOrDefault(ss => ss.SecondarySpecialityId == id);
                if (secondarySpecialityById != null) logger.Debug($"Поиск окончен. Запись найдена {secondarySpecialityById.ToString()}.");
                return secondarySpecialityById;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи специальности второй ступени по уникальному идентификатору.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи специальности второй ступени по уникальному идентификатору.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение записи специальности второй ступени по шифру специальности
        /// </summary>
        /// <param name="cipher">Шифр специальности</param>
        /// <returns>Запись специальности второй ступени</returns>
        public SecondarySpeciality GetSecondarySpeciality(string cipher)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску специальности второй ступени по шифру специальности.");
            try
            {
                var secondarySpecialityByCipher = context.SecondarySpeciality.AsNoTracking().FirstOrDefault(ss => ss.Cipher == cipher);
                if (secondarySpecialityByCipher != null) logger.Debug($"Поиск окончен. Запись найдена {secondarySpecialityByCipher.ToString()}.");
                return secondarySpecialityByCipher;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи специальности второй ступени по шифру специальности.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи специальности второй ступени по шифру специальности.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Добавление новой специальности второй ступени
        /// </summary>
        /// <param name="secondarySpeciality">Специальность второй ступени</param>
        /// <returns>Новая запись</returns>
        public SecondarySpeciality InsertSecondarySpeciality(SecondarySpeciality secondarySpeciality)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению специальности второй ступени");
            try
            {
                logger.Debug($"Добавляемая запись {secondarySpeciality.ToString()}");
                context.SecondarySpeciality.Add(secondarySpeciality);
                context.SaveChanges();
                logger.Debug($"Новая запись успешно добавлена.");
                return secondarySpeciality;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка добавления специальности второй ступени.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка добавления специальности второй ступени.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Обновление специальности второй ступени
        /// </summary>
        /// <param name="secondarySpeciality">Редактируемая специальность второй ступени</param>
        /// <returns>Отредактированная запись</returns>
        public SecondarySpeciality UpdateSecondarySpeciality(SecondarySpeciality secondarySpeciality)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к обновлению специальности второй ступени.");
            try
            {
                var secondarySpecialityToUpdate = context.SecondarySpeciality.FirstOrDefault(ss => ss.SecondarySpecialityId == secondarySpeciality.SecondarySpecialityId);
                logger.Debug($"Текущая запись {secondarySpecialityToUpdate.ToString()}");
                secondarySpecialityToUpdate.Fullname = secondarySpeciality.Fullname;
                secondarySpecialityToUpdate.Cipher = secondarySpeciality.Cipher;
                context.SaveChanges();
                logger.Debug($"Новая запись {secondarySpecialityToUpdate.ToString()}");
                return secondarySpecialityToUpdate;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка редактирования специальности второй ступени.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка редактирования специальности второй ступени.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
    }
}
