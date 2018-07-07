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
    /// Класс доступа к данным представлений базы данных
    /// </summary>
    public class ViewService : IViewService
    {
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public ViewService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Получение списка оценок
        /// </summary>
        /// <returns>Список оценок</returns>
        public List<AssessmentView> GetAssessments()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка оценок.");
            try
            {
                 var assessments = context.AssessmentView.AsNoTracking().ToList();
                logger.Debug($"Поиск окончен. Количество записей: {assessments.Count}.");
                return assessments;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка оценок.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка оценок.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка оценок
        /// </summary>
        /// <param name="discipline">Фильтр дисциплина</param>
        /// <returns>Список оценок</returns>
        public List<AssessmentView> GetAssessments(Discipline discipline)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка оценок по дисциплине.");
            try
            {
                var assessments = context.AssessmentView.AsNoTracking().Where(a => a.DisciplineId == discipline.DisciplineId).ToList();
                logger.Debug($"Поиск окончен. Количество записей: {assessments.Count}.");
                return assessments;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка оценок по дисциплине.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка оценок по дисциплине.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка пользователей
        /// </summary>
        /// <returns>Список пользователей</returns>
        public List<EmployeeView> GetEmployees()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка пользователей.");
            try
            {
                var employees = context.EmployeeView.AsNoTracking().ToList();
                logger.Debug($"Поиск окончен. Количество записей: {employees.Count}.");
                return employees;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка пользователей.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка пользователей.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка абитуриентов
        /// </summary>
        /// <param name="employee">Фильтр пользователь (оператор)</param>
        /// <returns>Список абитуриентов</returns>
        public List<EnrolleeView> GetEnrollees(Employee employee)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка абитуриентов зарегистрированных оператором.");
            try
            {
                var enrollees = context.EnrolleeView.AsNoTracking().Where(e => e.EmployeeId == employee.EmployeeId).OrderByDescending(e => e.EnrolleeId).ToList();
                logger.Debug($"Поиск окончен. Количество записей: {enrollees.Count}.");
                return enrollees;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка абитуриентов зарегистрированных оператором.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка абитуриентов зарегистрированных оператором.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка абитуриентов
        /// </summary>
        /// <param name="speciality">Фильтр специальность</param>
        /// <returns>Список абитуриентов</returns>
        public List<EnrolleeView> GetEnrollees(Speciality speciality)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка абитуриентов специальности.");
            try
            {
                var enrollees = context.EnrolleeView.AsNoTracking().Where(e => e.SpecialityId == speciality.SpecialityId).OrderBy(e => e.NumberOfDeal).ToList();
                logger.Debug($"Поиск окончен. Количество записей: {enrollees.Count}.");
                return enrollees;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка абитуриентов специальности.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка абитуриентов специальности.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка приоритетов
        /// </summary>
        /// <param name="enrollee">Фильтр абитуриент</param>
        /// <returns>Список приоритетов</returns>
        public List<PriorityView> GetPriorities(Enrollee enrollee)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка приоритетов абитуриента.");
            try
            {
                var priorities = context.PriorityView.AsNoTracking().Where(p => p.EnrolleeId == enrollee.EnrolleeId).ToList();
                logger.Debug($"Поиск окончен. Количество записей: {priorities.Count}.");
                return priorities;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка приоритетов абитуриента.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка приоритетов абитуриента.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка специальностей
        /// </summary>
        /// <returns>Список специальностей</returns>
        public List<SpecialityView> GetSpecialities()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка специальностей.");
            try
            {
                var specialities = context.SpecialityView.AsNoTracking().ToList();
                logger.Debug($"Поиск окончен. Количество записей: {specialities.Count}.");
                return specialities;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка специальностей.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка специальностей.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
    }
}
