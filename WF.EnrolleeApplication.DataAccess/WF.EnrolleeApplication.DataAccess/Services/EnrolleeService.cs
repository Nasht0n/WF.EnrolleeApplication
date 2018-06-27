using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    /// <summary>
    /// Класс доступа к данным таблицы "Абитуриенты"
    /// </summary>
    public class EnrolleeService : IEnrolleeService
    {
        private EnrolleeContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public EnrolleeService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление абитуриента
        /// </summary>
        /// <param name="enrollee">Удаляемая запись</param>
        public void DeleteEnrollee(Enrollee enrollee)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к удалению абитуриента.");
            try
            {
                logger.Debug($"Поиск записи пользователя для удаления. Удаляемый объект : {enrollee.ToString()}.");
                Enrollee enrolleeToDelete = context.Enrollee.FirstOrDefault(e => e.EnrolleeId == enrollee.EnrolleeId);
                if (enrolleeToDelete != null)
                {
                    context.Enrollee.Remove(enrolleeToDelete);
                    context.SaveChanges();
                    logger.Debug("Удаление записи пользователя успешно завершено.");
                }
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка удаления записи пользователя.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка удаления записи пользователя.");
                logger.Error($"Ошибка — {ex.Message}.");
            }
        }
        /// <summary>
        /// Получение абитуриента по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись абитуриента</returns>
        public Enrollee GetEnrollee(int id)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску абитуриента.");
            try
            {
                logger.Debug($"Поиск записи абитуриента по уникальному идентификатору = {id}.");
                var enrolleeById = context.Enrollee.AsNoTracking().FirstOrDefault(e => e.EnrolleeId == id);
                if (enrolleeById != null) logger.Debug($"Поиск окончен. Искомая запись: {enrolleeById.ToString()}.");
                return enrolleeById;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи абитуриента.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи абитуриента.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение абитуриента по номеру документа
        /// </summary>
        /// <param name="documentPersonalNumber">Личный номер документа</param>
        /// <returns>Запись абитуриента</returns>
        public Enrollee GetEnrollee(string documentPersonalNumber)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску абитуриента.");
            try
            {
                logger.Debug($"Поиск записи абитуриента по уникальному идентификатору = {documentPersonalNumber}.");
                var enrolleeByPersonalNumber = context.Enrollee.AsNoTracking().FirstOrDefault(e => e.DocumentPersonalNumber == documentPersonalNumber);
                if (enrolleeByPersonalNumber != null) logger.Debug($"Поиск окончен. Искомая запись: {enrolleeByPersonalNumber.ToString()}.");
                return enrolleeByPersonalNumber;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка поиска записи абитуриента.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка поиска записи абитуриента.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка абитуриентов
        /// </summary>
        /// <returns>Список абитуриентов</returns>
        public List<Enrollee> GetEnrollees()
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка абитуриентов.");
            try
            {
                logger.Debug($"Получение списка абитуриентов.");
                var enrollees = context.Enrollee.AsNoTracking().ToList();
                logger.Debug($"Поиск окончен. Количество записей: {enrollees.Count}.");
                return enrollees;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка абитуриентов.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка абитуриентов.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка абитуриентов
        /// </summary>
        /// <param name="speciality">Специальность</param>
        /// <returns>Список абитуриентов</returns>
        public List<Enrollee> GetEnrollees(Speciality speciality)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка абитуриентов.");
            try
            {
                logger.Debug($"Получение списка абитуриентов. Специальность = [{speciality.ToString()}]");
                var enrollees = context.Enrollee.AsNoTracking().Where(e => e.SpecialityId == speciality.SpecialityId).ToList();
                logger.Debug($"Поиск окончен. Количество записей: {enrollees.Count}.");
                return enrollees;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка абитуриентов выбранной специальности.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка абитуриентов выбранной специальности.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка абитуриентов
        /// </summary>
        /// <param name="citizenship">Гражданство</param>
        /// <returns>Список абитуриентов</returns>
        public List<Enrollee> GetEnrollees(Citizenship citizenship)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка абитуриентов.");
            try
            {
                logger.Debug($"Получение списка абитуриентов. Вид гражданства = [{citizenship.ToString()}]");
                var enrollees = context.Enrollee.AsNoTracking().Where(e => e.CitizenshipId == citizenship.CitizenshipId).ToList();
                logger.Debug($"Поиск окончен. Количество записей: {enrollees.Count}.");
                return enrollees;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка абитуриентов выбранного гражданства.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка абитуриентов выбранного гражданства.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка абитуриентов
        /// </summary>
        /// <param name="country">Страна</param>
        /// <returns>Список абитуриентов</returns>
        public List<Enrollee> GetEnrollees(Country country)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка абитуриентов.");
            try
            {
                logger.Debug($"Получение списка абитуриентов. Страна = [{country.ToString()}]");
                var enrollees = context.Enrollee.AsNoTracking().Where(e => e.CountryId == country.CountryId).ToList();
                logger.Debug($"Поиск окончен. Количество записей: {enrollees.Count}.");
                return enrollees;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка абитуриентов, по местожительству выбранной страны.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка абитуриентов, по местожительству выбранной страны.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка абитуриентов
        /// </summary>
        /// <param name="area">Область</param>
        /// <returns>Список абитуриентов</returns>
        public List<Enrollee> GetEnrollees(Area area)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка абитуриентов.");
            try
            {
                logger.Debug($"Получение списка абитуриентов. Область = [{area.ToString()}]");
                var enrollees = context.Enrollee.AsNoTracking().Where(e => e.AreaId == area.AreaId).ToList();
                logger.Debug($"Поиск окончен. Количество записей: {enrollees.Count}.");
                return enrollees;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка абитуриентов, по местожительству выбранной области.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка абитуриентов, по местожительству выбранной области.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка абитуриентов
        /// </summary>
        /// <param name="district">Район</param>
        /// <returns>Список абитуриентов</returns>
        public List<Enrollee> GetEnrollees(District district)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка абитуриентов.");
            try
            {
                logger.Debug($"Получение списка абитуриентов. Район = [{district.ToString()}]");
                var enrollees = context.Enrollee.AsNoTracking().Where(e => e.DistrictId == district.DistrictId).ToList();
                logger.Debug($"Поиск окончен. Количество записей: {enrollees.Count}.");
                return enrollees;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка абитуриентов, по местожительству выбранного района.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка абитуриентов, по местожительству выбранного района.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка абитуриентов
        /// </summary>
        /// <param name="typeOfSchool">Тип учебного заведения</param>
        /// <returns>Список абитуриентов</returns>
        public List<Enrollee> GetEnrollees(TypeOfSchool typeOfSchool)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка абитуриентов.");
            try
            {
                logger.Debug($"Получение списка абитуриентов. Тип учебного заведения = [{typeOfSchool.ToString()}]");
                var enrollees = context.Enrollee.AsNoTracking().Where(e => e.SchoolTypeId == typeOfSchool.SchoolTypeId).ToList();
                logger.Debug($"Поиск окончен. Количество записей: {enrollees.Count}.");
                return enrollees;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка абитуриентов, по типу оконченного учебного заведения.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка абитуриентов, по типу оконченного учебного заведения.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка абитуриентов
        /// </summary>
        /// <param name="reasonForAddmission">Основание зачисления</param>
        /// <returns>Список абитуриентов</returns>
        public List<Enrollee> GetEnrollees(ReasonForAddmission reasonForAddmission)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка абитуриентов.");
            try
            {
                logger.Debug($"Получение списка абитуриентов. Основание зачисления = [{reasonForAddmission.ToString()}]");
                var enrollees = context.Enrollee.AsNoTracking().Where(e => e.ReasonForAddmissionId == reasonForAddmission.ReasonForAddmissionId).ToList();
                logger.Debug($"Поиск окончен. Количество записей: {enrollees.Count}.");
                return enrollees;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка абитуриентов, по типу основания зачисления.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка абитуриентов, по типу основания зачисления.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка абитуриентов
        /// </summary>
        /// <param name="typeOfState">Статус</param>
        /// <returns>Список абитуриентов</returns>
        public List<Enrollee> GetEnrollees(TypeOfState typeOfState)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка абитуриентов.");
            try
            {
                logger.Debug($"Получение списка абитуриентов. Тип состояния (статуса) абитуриента = [{typeOfState.ToString()}]");
                var enrollees = context.Enrollee.AsNoTracking().Where(e => e.StateTypeId == typeOfState.StateId).ToList();
                logger.Debug($"Поиск окончен. Количество записей: {enrollees.Count}.");
                return enrollees;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка абитуриентов, по типу состояния (статуса).");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка абитуриентов,  по типу состояния (статуса).");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка абитуриентов
        /// </summary>
        /// <param name="employee">Пользователь</param>
        /// <returns>Список абитуриентов</returns>
        public List<Enrollee> GetEnrollees(Employee employee)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка абитуриентов.");
            try
            {
                logger.Debug($"Получение списка абитуриентов. Оператор, проводящий регистрацию = [{employee.ToString()}]");
                var enrollees = context.Enrollee.AsNoTracking().Where(e => e.EmployeeId == employee.EmployeeId).ToList();
                logger.Debug($"Поиск окончен. Количество записей: {enrollees.Count}.");
                return enrollees;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка абитуриентов, зарегистрированных выбранным оператором.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка абитуриентов, зарегистрированных выбранным оператором.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка абитуриентов
        /// </summary>
        /// <param name="decree">Приказ</param>
        /// <returns>Список абитуриентов</returns>
        public List<Enrollee> GetEnrollees(Decree decree)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка абитуриентов.");
            try
            {
                logger.Debug($"Получение списка зачисленных абитуриентов. Приказ = [{decree.ToString()}]");
                var enrollees = context.Enrollee.AsNoTracking().Where(e => e.DecreeId == decree.DecreeId).ToList();
                logger.Debug($"Поиск окончен. Количество записей: {enrollees.Count}.");
                return enrollees;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка зачисленных абитуриентов.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка зачисленных абитуриентов.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Получение списка абитуриентов
        /// </summary>
        /// <param name="typeOfFinance">Тип финансирования</param>
        /// <returns>Список абитуриентов</returns>
        public List<Enrollee> GetEnrollees(TypeOfFinance typeOfFinance)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к поиску списка абитуриентов.");
            try
            {
                logger.Debug($"Получение списка зачисленных абитуриентов. Тип финансирования = [{typeOfFinance.ToString()}]");
                var enrollees = context.Enrollee.AsNoTracking().Where(e => e.FinanceTypeId == typeOfFinance.FinanceTypeId).ToList();
                logger.Debug($"Поиск окончен. Количество записей: {enrollees.Count}.");
                return enrollees;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка получения списка зачисленных абитуриентов, выбранной формы финансирования.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка получения списка зачисленных абитуриентов, выбранной формы финансирования.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
        /// <summary>
        /// Добавление абитуриента
        /// </summary>
        /// <param name="enrollee">Новый абитуриент</param>
        /// <returns>Новая запись</returns>
        public Enrollee InsertEnrollee(Enrollee enrollee)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к добавлению абитуриента");
            try
            {
                logger.Debug($"Добавляемая запись: {enrollee.ToString()}");
                context.Enrollee.Add(enrollee);
                context.SaveChanges();
                logger.Debug($"Пользователь успешно добавлена.");
                return enrollee;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка добавления абитуриента.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка добавления абитуриента.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }   
        }
        /// <summary>
        /// Обновление абитуриента
        /// </summary>
        /// <param name="enrollee">Редактируемый абитуриент</param>
        /// <returns>Отредактированная запись</returns>
        public Enrollee UpdateEnrollee(Enrollee enrollee)
        {
            logger.Trace("Попытка подключения к источнику данных.");
            logger.Trace("Подготовка к обновлению данных абитуриента.");
            try
            {
                Enrollee enrolleeToUpdate = context.Enrollee.FirstOrDefault(e => e.EnrolleeId == enrollee.EnrolleeId);
                logger.Debug($"Текущая запись: {enrolleeToUpdate.ToString()}");
                enrolleeToUpdate.SpecialityId = enrollee.SpecialityId;
                enrolleeToUpdate.CitizenshipId = enrollee.CitizenshipId;
                enrolleeToUpdate.CountryId = enrollee.CountryId;
                enrolleeToUpdate.AreaId = enrollee.AreaId;
                enrolleeToUpdate.DistrictId = enrollee.DistrictId;
                enrolleeToUpdate.SettlementTypeId = enrollee.SettlementTypeId;
                enrolleeToUpdate.StreetTypeId = enrollee.StreetTypeId;
                enrolleeToUpdate.DocumentId = enrollee.DocumentId;
                enrolleeToUpdate.SchoolTypeId = enrollee.SchoolTypeId;
                enrolleeToUpdate.ForeignLanguageId = enrollee.ForeignLanguageId;
                enrolleeToUpdate.ReasonForAddmissionId = enrollee.ReasonForAddmissionId;
                enrolleeToUpdate.StateTypeId = enrollee.StateTypeId;
                enrolleeToUpdate.EmployeeId = enrollee.EmployeeId;
                enrolleeToUpdate.FinanceTypeId = enrollee.FinanceTypeId;
                enrolleeToUpdate.DecreeId = enrollee.DecreeId;
                enrolleeToUpdate.SecondarySpecialityId = enrollee.SecondarySpecialityId;
                enrolleeToUpdate.TargetWorkPlaceId = enrollee.TargetWorkPlaceId;
                enrolleeToUpdate.RuSurname = enrollee.RuSurname;
                enrolleeToUpdate.RuName = enrollee.RuName;
                enrolleeToUpdate.RuPatronymic = enrollee.RuPatronymic;
                enrolleeToUpdate.BlrSurname = enrollee.BlrSurname;
                enrolleeToUpdate.BlrName = enrollee.BlrName;
                enrolleeToUpdate.BlrPatronymic = enrollee.BlrPatronymic;
                enrolleeToUpdate.Gender = enrollee.Gender;
                enrolleeToUpdate.DateOfBirthday = enrollee.DateOfBirthday;
                enrolleeToUpdate.FatherFullname = enrollee.FatherFullname;
                enrolleeToUpdate.FatherAddress = enrollee.FatherAddress;
                enrolleeToUpdate.MotherFullname = enrollee.MotherFullname;
                enrolleeToUpdate.MotherAddress = enrollee.MotherAddress;
                enrolleeToUpdate.SettlementName = enrollee.SettlementName;
                enrolleeToUpdate.SettlementIndex = enrollee.SettlementIndex;
                enrolleeToUpdate.StreetName = enrollee.StreetName;
                enrolleeToUpdate.NumberHouse = enrollee.NumberHouse;
                enrolleeToUpdate.NumberFlat = enrollee.NumberFlat;
                enrolleeToUpdate.HomePhone = enrollee.HomePhone;
                enrolleeToUpdate.MobilePhone = enrollee.MobilePhone;
                enrolleeToUpdate.DocumentSeria = enrollee.DocumentSeria;
                enrolleeToUpdate.DocumentNumber = enrollee.DocumentNumber;
                enrolleeToUpdate.DocumentDate = enrollee.DocumentDate;
                enrolleeToUpdate.DocumentWhoGave = enrollee.DocumentWhoGave;
                enrolleeToUpdate.DocumentPersonalNumber = enrollee.DocumentPersonalNumber;
                enrolleeToUpdate.SchoolName = enrollee.SchoolName;
                enrolleeToUpdate.SchoolYear = enrollee.SchoolYear;
                enrolleeToUpdate.SchoolAddress = enrollee.SchoolAddress;
                enrolleeToUpdate.IsBRSM = enrollee.IsBRSM;
                enrolleeToUpdate.NumberOfDeal = enrollee.NumberOfDeal;
                enrolleeToUpdate.DateDeal = enrollee.DateDeal;
                enrolleeToUpdate.StateDateChange = enrollee.StateDateChange;
                enrolleeToUpdate.PersonInCharge = enrollee.PersonInCharge;
                enrolleeToUpdate.WorkPlace = enrollee.WorkPlace;
                enrolleeToUpdate.WorkPost = enrollee.WorkPost;
                enrolleeToUpdate.Seniority = enrollee.Seniority;
                enrolleeToUpdate.CurrentNumberCurs = enrollee.CurrentNumberCurs;
                enrolleeToUpdate.CurrentUniversity = enrollee.CurrentUniversity;
                enrolleeToUpdate.CurrentSpeciality = enrollee.CurrentSpeciality;
                enrolleeToUpdate.AttestatEstimationString = enrollee.AttestatEstimationString;
                enrolleeToUpdate.DiplomPtuEstimationString = enrollee.DiplomPtuEstimationString;
                enrolleeToUpdate.DiplomSusEstimationString = enrollee.DiplomSusEstimationString;
                enrolleeToUpdate.BeforeEnrollSpecialityId = enrollee.BeforeEnrollSpecialityId;
                enrolleeToUpdate.BeforeEnrollNumberOfDeal = enrollee.BeforeEnrollNumberOfDeal;
                context.SaveChanges();
                logger.Debug($"Новая запись: {enrolleeToUpdate.ToString()}");
                return enrolleeToUpdate;
            }
            catch (SqlException sqlEx)
            {
                logger.Error("Ошибка редактирования данных абитуриента.");
                logger.Error($"Ошибка SQL Server — {sqlEx.Number}.");
                logger.Error($"Сообщение об ошибке: {sqlEx.Message}.");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка редактирования данных абитуриента.");
                logger.Error($"Ошибка — {ex.Message}.");
                return null;
            }
        }
    }
}
