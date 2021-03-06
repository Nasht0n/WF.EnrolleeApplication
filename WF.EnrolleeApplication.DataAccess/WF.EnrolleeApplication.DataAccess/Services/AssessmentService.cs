﻿using NLog;
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
    /// Класс доступа к данным таблицы "Оценки"
    /// </summary>
    public class AssessmentService : IAssessmentService
    {
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public AssessmentService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление записи об оценке
        /// </summary>
        /// <param name="assessment">Удаляемая запись</param>
        public void DeleteAssessment(Assessment assessment)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к удалению данных об оценке.");
            try
            {
                var assessmentToDelete = context.Assessment.FirstOrDefault(a => a.AssessmentId == assessment.AssessmentId);
                if (assessmentToDelete != null)
                {
                    context.Assessment.Remove(assessmentToDelete);
                    context.SaveChanges();
                    logger.Debug("Удаление успешно завершено.");
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка удаления данных об оценке.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка удаления данных об оценке.");
                logger.Error($"Ошибка — {ex.Message}.");
            }
        }
        /// <summary>
        /// Получение оценки по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Искомая запись об оценке</returns>
        public Assessment GetAssessment(int id)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску данных об оценке по уникальному идентификатору.");
            try
            {
                var assessment = context.Assessment.AsNoTracking().FirstOrDefault(a => a.AssessmentId == id);
                if (assessment != null) logger.Debug($"Поиск окончен. Запись найдена {assessment.ToString()}.");
                else logger.Debug($"Поиск окончен. Запись не найдена.");
                return assessment;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска данных об оценке.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска данных об оценке.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение оценки по номеру сертификата
        /// </summary>
        /// <param name="sertcode">Номер сертификата</param>
        /// <returns>Искомая запись об оценке</returns>
        public Assessment GetAssessment(string sertcode)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску данных об оценке по номеру сертификата.");
            try
            {
                var assessment = context.Assessment.AsNoTracking().FirstOrDefault(a => a.SertCode == sertcode);
                if (assessment != null) logger.Debug($"Поиск окончен. Запись найдена {assessment.ToString()}.");
                else logger.Debug($"Поиск окончен. Запись не найдена.");
                return assessment;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска данных об оценке.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска данных об оценке.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение оценки по дисциплине и абитуриенту
        /// </summary>
        /// <param name="discipline">Дисциплина</param>
        /// <param name="enrollee">Абитуриент</param>
        /// <returns>Искомая запись об оценке</returns>
        public Assessment GetAssessment(Discipline discipline, Enrollee enrollee)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску данных об оценке по параметрам дисциплины и абитуриента.");
            try
            {
                var assessment = context.Assessment.AsNoTracking().FirstOrDefault(a => a.DisciplineId == discipline.DisciplineId && a.EnrolleeId == enrollee.EnrolleeId);
                if (assessment != null) logger.Debug($"Поиск окончен. Запись найдена {assessment.ToString()}.");
                else logger.Debug($"Поиск окончен. Запись не найдена.");
                return assessment;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска данных об оценке.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска данных об оценке.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение оценки по дисциплине, абитуриенту и типу оценивания
        /// </summary>
        /// <param name="discipline">Дисциплина</param>
        /// <param name="enrollee">Абитуриент</param>
        /// <param name="basisForAssessing">Фильтр по типу оценивания</param>
        /// <returns>Искомая запись об оценке</returns>
        public Assessment GetAssessment(Discipline discipline, Enrollee enrollee, BasisForAssessing basisForAssessing)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску данных об оценке по параметрам дисциплины, абитуриента и типа оценивания.");
            try
            {
                var assessment = context.Assessment.AsNoTracking().FirstOrDefault(a => a.DisciplineId == discipline.DisciplineId && a.EnrolleeId == enrollee.EnrolleeId && a.Discipline.BasisForAssessingId == basisForAssessing.BasisForAssessingId);
                if (assessment != null) logger.Debug($"Поиск окончен. Запись найдена {assessment.ToString()}.");
                else logger.Debug($"Поиск окончен. Запись не найдена.");
                return assessment;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска данных об оценке.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска данных об оценке.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка оценок
        /// </summary>
        /// <param name="discipline">Фильтр по дисциплине</param>
        /// <returns>Отфильтрованный список оценок</returns>
        public List<Assessment> GetAssessments(Discipline discipline)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка оценок по дисциплине.");
            try
            {
                var assessments = context.Assessment.AsNoTracking().Where(a => a.DisciplineId == discipline.DisciplineId).ToList();
                if (assessments.Count != 0) logger.Debug($"Поиск окончен. Количество записей: {assessments.Count}.");
                else logger.Debug($"Поиск окончен. Список оценок пуст.");
                return assessments;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска данных об оценке.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска данных об оценке.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка оценок
        /// </summary>
        /// <param name="discipline">Фильтр по дисциплине</param>
        /// <param name="basisForAssessing">Фильтр по типу оценивания</param>
        /// <returns>Отфильтрованный список оценок</returns>
        public List<Assessment> GetAssessments(Discipline discipline, BasisForAssessing basisForAssessing)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка оценок по параметрам дисциплины и типа оценивания.");
            try
            {
                var assessments = context.Assessment.AsNoTracking().Where(a => a.DisciplineId == discipline.DisciplineId && a.Discipline.BasisForAssessingId == basisForAssessing.BasisForAssessingId).ToList();
                if (assessments.Count != 0) logger.Trace($"Поиск окончен. Количество записей: {assessments.Count}.");
                else logger.Trace($"Поиск окончен. Список оценок пуст.");
                return assessments;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска данных об оценке.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска данных об оценке.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка оценок
        /// </summary>
        /// <param name="enrollee">Фильтр по абитуриенту</param>
        /// <returns>Отфильтрованный список оценок</returns>
        public List<Assessment> GetAssessments(Enrollee enrollee)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка оценок по параметрам абитуриента.");
            try
            {
                var assessments = context.Assessment.AsNoTracking().Where(a => a.EnrolleeId == enrollee.EnrolleeId).ToList();
                if (assessments.Count != 0) logger.Debug($"Поиск окончен. Количество записей: {assessments.Count}.");
                else logger.Debug($"Поиск окончен. Список оценок пуст.");
                return assessments;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска данных об оценке.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска данных об оценке.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка оценок
        /// </summary>
        /// <param name="enrollee">Фильтр по абитуриенту</param>
        /// <param name="basisForAssessing">Фильтр по типу оценивания</param>
        /// <returns>Отфильтрованный список оценок</returns>
        public List<Assessment> GetAssessments(Enrollee enrollee, BasisForAssessing basisForAssessing)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка оценок по параметрам абитуриента и типа оценивания.");
            try
            {
                var assessments = context.Assessment.AsNoTracking().Where(a => a.EnrolleeId == enrollee.EnrolleeId && a.Discipline.BasisForAssessingId == basisForAssessing.BasisForAssessingId).ToList();
                if(assessments.Count!=0) logger.Debug($"Поиск окончен. Количество записей: {assessments.Count}.");
                else logger.Debug($"Поиск окончен. Список оценок пуст.");
                return assessments;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска данных об оценке.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска данных об оценке.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка оценок
        /// </summary>
        /// <param name="basisForAssessing">Фильтр по типу оценивания</param>
        /// <returns>Отфильтрованный список оценок</returns>
        public List<Assessment> GetAssessments(BasisForAssessing basisForAssessing)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка оценок по типу оценивания.");
            try
            {
                var assessments = context.Assessment.AsNoTracking().Where(a => a.Discipline.BasisForAssessingId == basisForAssessing.BasisForAssessingId).ToList();
                if(assessments.Count!=0) logger.Debug($"Поиск окончен. Количество записей: {assessments.Count}.");
                else logger.Debug($"Поиск окончен. Список оценок пуст.");
                return assessments;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска данных об оценке.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска данных об оценке.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Добавление новой оценки
        /// </summary>
        /// <param name="assessment">Данные о текущей оценке</param>
        /// <returns>Запись о добавленной оценке в БД</returns>
        public Assessment InsertAssessment(Assessment assessment)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению оценки.");
            try
            {
                logger.Debug($"Добавляемая запись: {assessment.ToString()}");
                context.Assessment.Add(assessment);
                context.SaveChanges();
                logger.Debug($"Новая запись успешно добавлена.");
                return assessment;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка добавления записи об оценке.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка добавления записи об оценке.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Обновление данных текущей оценки
        /// </summary>
        /// <param name="assessment">Редактируемая запись об оценке</param>
        /// <returns>Отредактированный объект</returns>
        public Assessment UpdateAssessment(Assessment assessment)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к обновлению данных выбранной оценки.");
            try
            {
                var assessmentToUpdate = context.Assessment.FirstOrDefault(a => a.AssessmentId == assessment.AssessmentId);
                logger.Debug($"Текущая оценка: {assessmentToUpdate.ToString()}");
                assessmentToUpdate.ChangeDiscipline = assessment.ChangeDiscipline;
                assessmentToUpdate.DisciplineId = assessment.DisciplineId;
                assessmentToUpdate.EnrolleeId = assessment.EnrolleeId;
                assessmentToUpdate.Estimation = assessment.Estimation;
                assessmentToUpdate.SertCode = assessment.SertCode;
                assessmentToUpdate.SertDate = assessment.SertDate;
                context.SaveChanges();
                logger.Debug($"Отредактированная оценка: {assessmentToUpdate.ToString()}");
                return assessmentToUpdate;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка редактирования оценки.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка редактирования оценки.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
    }
}
