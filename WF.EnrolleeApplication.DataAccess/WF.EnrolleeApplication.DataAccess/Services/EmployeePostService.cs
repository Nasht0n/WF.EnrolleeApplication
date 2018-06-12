using System;
using System.Collections.Generic;
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
            EmployeePost postToDelete = context.EmployeePost.AsNoTracking().FirstOrDefault(ep => ep.PostId == post.PostId);
            context.EmployeePost.Remove(postToDelete);
            context.SaveChanges();
        }
        /// <summary>
        /// Получение должности по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор записи</param>
        /// <returns>Возвращается объект должности сотрудника</returns>
        public EmployeePost GetEmployeePost(int id)
        {
            EmployeePost postById = context.EmployeePost.AsNoTracking().FirstOrDefault(ep => ep.PostId == id);
            return postById;
        }
        /// <summary>
        /// Получение должности по наименованию должности
        /// </summary>
        /// <param name="name">Наименование должности</param>
        /// <returns>Возвращается объект должности сотрудника</returns>
        public EmployeePost GetEmployeePost(string name)
        {
            EmployeePost postByName = context.EmployeePost.AsNoTracking().FirstOrDefault(ep => ep.Name == name);
            return postByName;
        }
        /// <summary>
        /// Получение списка должностей
        /// </summary>
        /// <returns>Список должностей сотрудников</returns>
        public List<EmployeePost> GetEmployeePosts()
        {
            List<EmployeePost> posts = context.EmployeePost.AsNoTracking().ToList();
            return posts;
        }
        /// <summary>
        /// Добавление новой должности сотрудников
        /// </summary>
        /// <param name="post">Объект новой должности</param>
        /// <returns>Возвращаем только что созданный объект</returns>
        public EmployeePost InsertEmployeePost(EmployeePost post)
        {
            context.EmployeePost.Add(post);
            context.SaveChanges();
            return post;
        }
        /// <summary>
        /// Обновление должности
        /// </summary>
        /// <param name="post">Редактируемая должность</param>
        /// <returns>Возвращаем объект должности, с обновленными данными</returns>
        public EmployeePost UpdateEmployeePost(EmployeePost post)
        {
            EmployeePost postToUpdate = context.EmployeePost.FirstOrDefault(ep => ep.PostId == post.PostId);
            postToUpdate.Name = post.Name;
            postToUpdate.Note = post.Note;
            postToUpdate.RegistrationAllow = post.RegistrationAllow;
            postToUpdate.EnrollAllow = post.EnrollAllow;
            postToUpdate.DictionaryAllow = post.DictionaryAllow;
            context.SaveChanges();
            return postToUpdate;
        }
    }
}
