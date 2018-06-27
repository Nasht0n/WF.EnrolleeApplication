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
    /// Класс-репозиторий для управления должностями сотрудников
    /// </summary>
    public class EmployeePostService : IEmployeePostService
    {
        /// <summary>
        /// Объект для работы с данными
        /// </summary>
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public EmployeePostService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="post">Объект сотрудника</param>
        public void DeleteEmployeePost(EmployeePost post)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к удалению должности.");
            try
            {
                logger.Debug($"Поиск записи должности для удаления. Удаляемый объект : {post.ToString()}.");
                EmployeePost postToDelete = context.EmployeePost.FirstOrDefault(ep => ep.PostId == post.PostId);
                if (postToDelete != null)
                {
                    context.EmployeePost.Remove(postToDelete);
                    context.SaveChanges();
                    logger.Debug("Удаление записи должности успешно завершено.");
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка удаления записи должности.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка удаления записи должности.");
                logger.Error($"Ошибка — {ex.Message}.");
            }
        }
        /// <summary>
        /// Получение должности по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор записи</param>
        /// <returns>Возвращается объект должности сотрудника</returns>
        public EmployeePost GetEmployeePost(int id)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску должности.");
            try
            {
                logger.Debug($"Поиск записи должности по уникальному идентификатору = {id}.");
                EmployeePost postById = context.EmployeePost.AsNoTracking().FirstOrDefault(ep => ep.PostId == id);
                if (postById != null) logger.Debug($"Поиск окончен. Искомая запись: {postById.ToString()}.");
                return postById;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи должности.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи должности.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение должности по наименованию должности
        /// </summary>
        /// <param name="name">Наименование должности</param>
        /// <returns>Возвращается объект должности сотрудника</returns>
        public EmployeePost GetEmployeePost(string name)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску должности.");
            try
            {
                logger.Debug($"Поиск записи должности по наименованию = {name}.");
                EmployeePost postByName = context.EmployeePost.AsNoTracking().FirstOrDefault(ep => ep.Name == name);
                if (postByName != null) logger.Debug($"Поиск окончен. Искомая запись: {postByName.ToString()}.");
                return postByName;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи должности.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи должности.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка должностей
        /// </summary>
        /// <returns>Список должностей сотрудников</returns>
        public List<EmployeePost> GetEmployeePosts()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка должностей.");
            try
            {
                logger.Debug($"Получение списка должностей.");
                List<EmployeePost> posts = context.EmployeePost.AsNoTracking().ToList();
                logger.Debug($"Поиск окончен. Количество записей: {posts.Count}.");
                return posts;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка должностей.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка должностей.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Добавление новой должности сотрудников
        /// </summary>
        /// <param name="post">Объект новой должности</param>
        /// <returns>Возвращаем только что созданный объект</returns>
        public EmployeePost InsertEmployeePost(EmployeePost post)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению должности");
            try
            {
                logger.Debug($"Добавляемая запись: {post.ToString()}");
                context.EmployeePost.Add(post);
                context.SaveChanges();
                logger.Debug($"Должность успешно добавлена.");
                return post;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка добавления должности.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка добавления должности.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Обновление должности
        /// </summary>
        /// <param name="post">Редактируемая должность</param>
        /// <returns>Возвращаем объект должности, с обновленными данными</returns>
        public EmployeePost UpdateEmployeePost(EmployeePost post)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к обновлению должности.");
            try
            {
                EmployeePost postToUpdate = context.EmployeePost.FirstOrDefault(ep => ep.PostId == post.PostId);
                logger.Debug($"Текущая запись: {postToUpdate.ToString()}");
                postToUpdate.Name = post.Name;
                postToUpdate.Note = post.Note;
                postToUpdate.RegistrationAllow = post.RegistrationAllow;
                postToUpdate.EnrollAllow = post.EnrollAllow;
                postToUpdate.DictionaryAllow = post.DictionaryAllow;
                context.SaveChanges();
                logger.Debug($"Новая запись: {postToUpdate.ToString()}");
                return postToUpdate;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка редактирования должности.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка редактирования должности.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
    }
}
