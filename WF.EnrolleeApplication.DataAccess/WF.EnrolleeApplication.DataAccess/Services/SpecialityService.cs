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
    /// Класс доступа к данным таблицы "Специальность первой ступени"
    /// </summary>
    public class SpecialityService : ISpecialityService
    {
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public SpecialityService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление специальности первой ступени
        /// </summary>
        /// <param name="speciality">Удаляемая специальность первой ступени</param>
        public void DeleteSpeciality(Speciality speciality)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к удалению специальности первой ступени.");
            try
            {
                var specialityToDelete = context.Speciality.FirstOrDefault(s => s.SpecialityId == speciality.SpecialityId);
                if (specialityToDelete != null)
                {
                    context.Speciality.Remove(specialityToDelete);
                    context.SaveChanges();
                    logger.Debug("Удаление успешно завершено.");
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка удаления записи специальности первой ступени.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка удаления записи специальности первой ступени.");
                logger.Error($"Ошибка — {ex.Message}.");
            }
        }
        /// <summary>
        /// Получение списка специальностей первой ступени
        /// </summary>
        /// <returns>Список специальностей первой ступени</returns>
        public List<Speciality> GetSpecialities()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка специальностей первой ступени.");
            try
            {
                var specialities = context.Speciality.AsNoTracking().ToList();
                logger.Debug($"Поиск окончен. Количество записей: {specialities.Count}.");
                return specialities;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка специальностей первой ступени.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка специальностей первой ступени.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка специальностей первой ступени
        /// </summary>
        /// <param name="faculty">Фильтр - факультет</param>
        /// <returns>Список специальностей первой ступени</returns>
        public List<Speciality> GetSpecialities(Faculty faculty)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка специальностей первой ступени выбранного факультета.");
            try
            {
                var specialities = context.Speciality.AsNoTracking().Where(s => s.FacultyId == faculty.FacultyId).ToList();
                logger.Debug($"Поиск окончен. Количество записей: {specialities.Count}.");
                return specialities;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка специальностей первой ступени выбранного факультета.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка специальностей первой ступени выбранного факультета.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка специальностей первой ступени
        /// </summary>
        /// <param name="formOfStudy">Фильтр - форма обучения</param>
        /// <returns>Список специальностей первой ступени</returns>
        public List<Speciality> GetSpecialities(FormOfStudy formOfStudy)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка специальностей первой ступени выбранной формы обучения.");
            try
            {
                var specialities = context.Speciality.AsNoTracking().Where(s => s.FormOfStudyId == formOfStudy.FormOfStudyId).ToList();
                logger.Debug($"Поиск окончен. Количество записей: {specialities.Count}.");
                return specialities;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка специальностей первой ступени выбранной формы обучения.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка специальностей первой ступени выбранной формы обучения.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка специальностей первой ступени
        /// </summary>
        /// <param name="faculty">Фильтр - факультет</param>
        /// <param name="formOfStudy">Фильтр - форма обучения</param>
        /// <returns>Список специальностей первой ступени</returns>
        public List<Speciality> GetSpecialities(Faculty faculty, FormOfStudy formOfStudy)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка специальностей первой ступени по параметрам.");
            try
            {
                var specialities = context.Speciality.AsNoTracking().Where(s => s.FormOfStudyId == formOfStudy.FormOfStudyId && s.FacultyId == faculty.FacultyId).ToList();
                logger.Debug($"Поиск окончен. Количество записей: {specialities.Count}.");
                return specialities;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка специальностей первой ступени по параметрам.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка специальностей первой ступени по параметрам.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка специальностей первой ступени
        /// </summary>
        /// <param name="groupSpeciality">Фильтр - группа специальностей</param>
        /// <returns>Список специальностей первой ступени</returns>
        public List<Speciality> GetSpecialities(Speciality groupSpeciality)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка специальностей первой ступени группы специальностей.");
            try
            {
                var specialities = context.Speciality.AsNoTracking().Where(s => s.SpecialityGroupId == groupSpeciality.SpecialityId).ToList();
                logger.Debug($"Поиск окончен. Количество записей: {specialities.Count}.");
                return specialities;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка специальностей первой ступени группы специальностей.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка специальностей первой ступени группы специальностей.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение записи специальности первой ступени по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись специальности первой ступени</returns>
        public Speciality GetSpeciality(int id)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску специальности первой ступени по уникальному идентификатору.");
            try
            {
                var specialityById = context.Speciality.AsNoTracking().FirstOrDefault(s => s.SpecialityId == id);
                if (specialityById != null) logger.Debug($"Поиск окончен. Запись найдена {specialityById.ToString()}.");
                return specialityById;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи специальности первой ступени по уникальному идентификатору.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи специальности первой ступени по уникальному идентификатору.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение записи специальности первой ступени по шифру специальности
        /// </summary>
        /// <param name="cipher">Шифр специальности</param>
        /// <returns>Запись специальности первой ступени</returns>
        public Speciality GetSpeciality(string cipher)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску специальности первой ступени по шифру.");
            try
            {
                var specialityByCipher = context.Speciality.AsNoTracking().FirstOrDefault(s => s.Cipher == cipher);
                if (specialityByCipher != null) logger.Debug($"Поиск окончен. Запись найдена {specialityByCipher.ToString()}.");
                return specialityByCipher;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи специальности первой ступени по шифру.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи специальности первой ступени по шифру.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Добавление специальности первой ступени
        /// </summary>
        /// <param name="speciality">Новая специальность первой ступени</param>
        /// <returns>Новая запись</returns>
        public Speciality InsertSpeciality(Speciality speciality)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению специальности первой ступени");
            try
            {
                logger.Debug($"Добавляемая запись {speciality.ToString()}");
                context.Speciality.Add(speciality);
                context.SaveChanges();
                logger.Debug($"Новая запись успешно добавлена.");
                return speciality;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка добавления специальности первой ступени.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка добавления специальности первой ступени.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Обновление специальности первой ступени
        /// </summary>
        /// <param name="speciality">Редактируемая специальность первой ступени</param>
        /// <returns>Отредактированная специальность</returns>
        public Speciality UpdateSpeciality(Speciality speciality)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к обновлению специальности первой ступени.");
            try
            {
                var specialityToUpdate = context.Speciality.FirstOrDefault(s => s.SpecialityId == speciality.SpecialityId);
                logger.Debug($"Текущая запись {specialityToUpdate.ToString()}");
                specialityToUpdate.FacultyId = speciality.FacultyId;
                specialityToUpdate.FormOfStudyId = speciality.FormOfStudyId;
                specialityToUpdate.Fullname = speciality.Fullname;
                specialityToUpdate.Specialization = speciality.Specialization;
                specialityToUpdate.Cipher = speciality.Cipher;
                specialityToUpdate.BudgetCountPlace = speciality.BudgetCountPlace;
                specialityToUpdate.FeeCountPlace = speciality.FeeCountPlace;
                specialityToUpdate.TargetCountPlace = speciality.TargetCountPlace;
                specialityToUpdate.IsGroup = speciality.IsGroup;
                specialityToUpdate.IsAlternative = speciality.IsAlternative;
                specialityToUpdate.SpecialityGroupId = speciality.SpecialityGroupId;
                context.SaveChanges();
                logger.Debug($"Новая запись {specialityToUpdate.ToString()}");
                return specialityToUpdate;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка редактирования специальности первой ступени.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка редактирования специальности первой ступени.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
    }
}
