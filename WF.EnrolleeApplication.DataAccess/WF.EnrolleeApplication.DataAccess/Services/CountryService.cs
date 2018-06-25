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
    /// Класс доступа к данным таблицы "Страны"
    /// </summary>
    public class CountryService : ICountryService
    {
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public CountryService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="country">Удаляемая запись</param>
        public void DeleteCountry(Country country)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к удалению страны.");
            try
            {
                logger.Debug($"Поиск записи страны для удаления. Удаляемый объект : {country.ToString()}.");
                Country countryToDelete = context.Country.FirstOrDefault(c => c.CountryId == country.CountryId);
                if (countryToDelete != null)
                {
                    context.Country.Remove(countryToDelete);
                    context.SaveChanges();
                    logger.Debug("Удаление записи страны успешно завершено.");
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка удаления записи страны.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка удаления записи страны.");
                logger.Error($"Ошибка — {ex.Message}.");
            }
        }
        /// <summary>
        /// Получение списка стран
        /// </summary>
        /// <returns>Список стран</returns>
        public List<Country> GetCountries()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка стран.");
            try
            {
                logger.Debug($"Получение списка стран.");
                List<Country> countries = context.Country.AsNoTracking().ToList();
                logger.Debug($"Поиск окончен. Количество записей: {countries.Count}.");
                return countries;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка стран.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка стран.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение записи по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись страны</returns>
        public Country GetCountry(int id)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску страны.");
            try
            {
                logger.Debug($"Поиск записи страны по уникальному идентификатору = {id}.");
                Country countryById = context.Country.AsNoTracking().FirstOrDefault(c => c.CountryId == id);
                if (countryById != null) logger.Debug($"Поиск окончен. Искомая запись: {countryById.ToString()}.");
                return countryById;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи страны.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи страны.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }            
        }
        /// <summary>
        /// Получение записи по наименованию страны
        /// </summary>
        /// <param name="name">Наименование страны</param>
        /// <returns>Запись страны</returns>
        public Country GetCountry(string name)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску страны.");
            try
            {
                logger.Debug($"Поиск записи страны по наименованию = {name}.");
                Country countryByName = context.Country.AsNoTracking().FirstOrDefault(c => c.Name == name);
                if (countryByName != null) logger.Debug($"Поиск окончен. Искомая запись: {countryByName.ToString()}.");
                return countryByName;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи страны.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи страны.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Добавление новой записи
        /// </summary>
        /// <param name="country">Новая запись страны</param>
        /// <returns>Добавленная запись</returns>
        public Country InsertCountry(Country country)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению страны.");
            try
            {
                logger.Debug($"Добавляемая запись: {country.ToString()}");
                context.Country.Add(country);
                context.SaveChanges();
                logger.Debug($"Страна успешно добавлена.");
                return country;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка добавления страны.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка добавления страны.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }            
        }
        /// <summary>
        /// Обновление записи
        /// </summary>
        /// <param name="country">Редактируемая запись</param>
        /// <returns>Отредактированная запись</returns>
        public Country UpdateCountry(Country country)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к обновлению страны.");
            try
            {
                Country countryToUpdate = context.Country.FirstOrDefault(c => c.CountryId == country.CountryId);
                logger.Debug($"Текущая запись: {countryToUpdate.ToString()}");
                countryToUpdate.Name = country.Name;
                context.SaveChanges();
                logger.Debug($"Новая запись: {countryToUpdate.ToString()}");
                return countryToUpdate;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка редактирования страны.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка редактирования страны.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
    }
}
