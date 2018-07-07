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
    /// Класс доступа к данным таблицы "Типы финансирования"
    /// </summary>
    public class TypeOfSchoolService : ITypeOfSchoolService
    {
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public TypeOfSchoolService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление типа учебного заведения
        /// </summary>
        /// <param name="typeOfSchool">Удаляемый тип учебного заведения</param>
        public void DeleteTypeOfSchool(TypeOfSchool typeOfSchool)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к удалению типа учебного заведения.");
            try
            {
                var typeOfSchoolToDelete = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == typeOfSchool.SchoolTypeId);
                if (typeOfSchoolToDelete != null)
                {
                    context.TypeOfSchool.Remove(typeOfSchoolToDelete);
                    context.SaveChanges();
                    logger.Debug("Удаление успешно завершено.");
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка удаления записи типа учебного заведения.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка удаления записи типа учебного заведения.");
                logger.Error($"Ошибка — {ex.Message}.");
            }
        }
        /// <summary>
        /// Получение типа учебного заведения по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись типа учебного заведения</returns>
        public TypeOfSchool GetTypeOfSchool(int id)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску типа финансирования по уникальному идентификатору.");
            try
            {
                var typeOfSchoolById = context.TypeOfSchool.AsNoTracking().FirstOrDefault(ts => ts.SchoolTypeId == id);
                if (typeOfSchoolById != null) logger.Debug($"Поиск окончен. Запись найдена {typeOfSchoolById.ToString()}.");
                return typeOfSchoolById;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи типа финансирования по уникальному идентификатору.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи типа финансирования по уникальному идентификатору.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение типа учебного заведения по наименованию
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <returns>Запись типа учебного заведения</returns>
        public TypeOfSchool GetTypeOfSchool(string name)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску типа учебного заведения по наименованию.");
            try
            {
                var typeOfSchoolByName = context.TypeOfSchool.AsNoTracking().FirstOrDefault(ts => ts.Name == name);
                if (typeOfSchoolByName != null) logger.Debug($"Поиск окончен. Запись найдена {typeOfSchoolByName.ToString()}.");
                return typeOfSchoolByName;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи типа учебного заведения по наименованию.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи типа финансирования по наименованию.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка типов учебных заведений
        /// </summary>
        /// <returns>Список типов учебных заведений</returns>
        public List<TypeOfSchool> GetTypeOfSchools()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка типов учебных заведений.");
            try
            {
                var typeOfSchools = context.TypeOfSchool.AsNoTracking().ToList();
                logger.Debug($"Поиск окончен. Количество записей: {typeOfSchools.Count}.");
                return typeOfSchools;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка типов учебных заведений.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка типов учебных заведений.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Добавление нового типа учебного заведения
        /// </summary>
        /// <param name="typeOfSchool">Новый тип учебного заведения</param>
        /// <returns>Новая запись</returns>
        public TypeOfSchool InsertTypeOfSchool(TypeOfSchool typeOfSchool)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению типа учебного заведения");
            try
            {
                logger.Debug($"Добавляемая запись {typeOfSchool.ToString()}");
                context.TypeOfSchool.Add(typeOfSchool);
                context.SaveChanges();
                logger.Debug($"Новая запись успешно добавлена.");
                return typeOfSchool;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка добавления типа учебного заведения.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка добавления типа учебного заведения.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Обновление типа учебного заведения
        /// </summary>
        /// <param name="typeOfSchool">Редактирование записи</param>
        /// <returns>Отредактированная запись</returns>
        public TypeOfSchool UpdateTypeOfSchool(TypeOfSchool typeOfSchool)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к обновлению типа учебного заведения.");
            try
            {
                var typeOfSchoolToUpdate = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == typeOfSchool.SchoolTypeId);
                logger.Debug($"Текущая запись {typeOfSchoolToUpdate.ToString()}");
                typeOfSchoolToUpdate.Name = typeOfSchool.Name;
                context.SaveChanges();
                logger.Debug($"Новая запись {typeOfSchoolToUpdate.ToString()}");
                return typeOfSchoolToUpdate;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка редактирования типа учебного заведения.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка редактирования типа учебного заведения.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
    }
}
