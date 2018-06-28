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
                logger.Debug($"Поиск записи типа учебного заведения для удаления. Удаляемый объект : {typeOfSchool.ToString()}.");
                var typeOfSchoolToDelete = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == typeOfSchool.SchoolTypeId);
                if (typeOfSchoolToDelete != null)
                {
                    context.TypeOfSchool.Remove(typeOfSchoolToDelete);
                    context.SaveChanges();
                    logger.Debug("Удаление записи типа учебного заведения успешно завершено.");
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
                logger.Debug($"Поиск записи типа финансирования по уникальному идентификатору = {id}.");
                var typeOfSchoolById = context.TypeOfSchool.AsNoTracking().FirstOrDefault(ts => ts.SchoolTypeId == id);
                if (typeOfSchoolById != null) logger.Debug($"Поиск окончен. Искомая запись: {typeOfSchoolById.ToString()}.");
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
            TypeOfSchool typeOfSchool = context.TypeOfSchool.AsNoTracking().FirstOrDefault(ts => ts.Name == name);
            return typeOfSchool;
        }
        /// <summary>
        /// Получение списка типов учебных заведений
        /// </summary>
        /// <returns>Список типов учебных заведений</returns>
        public List<TypeOfSchool> GetTypeOfSchools()
        {
            List<TypeOfSchool> typeOfSchools = context.TypeOfSchool.AsNoTracking().ToList();
            return typeOfSchools;
        }
        /// <summary>
        /// Добавление нового типа учебного заведения
        /// </summary>
        /// <param name="typeOfSchool">Новый тип учебного заведения</param>
        /// <returns>Новая запись</returns>
        public TypeOfSchool InsertTypeOfSchool(TypeOfSchool typeOfSchool)
        {
            context.TypeOfSchool.Add(typeOfSchool);
            context.SaveChanges();
            return typeOfSchool;
        }
        /// <summary>
        /// Обновление типа учебного заведения
        /// </summary>
        /// <param name="typeOfSchool">Редактирование записи</param>
        /// <returns>Отредактированная запись</returns>
        public TypeOfSchool UpdateTypeOfSchool(TypeOfSchool typeOfSchool)
        {
            TypeOfSchool typeOfSchoolToUpdate = context.TypeOfSchool.FirstOrDefault(ts => ts.SchoolTypeId == typeOfSchool.SchoolTypeId);
            typeOfSchoolToUpdate.Name = typeOfSchool.Name;
            context.SaveChanges();
            return typeOfSchoolToUpdate;
        }
    }
}
