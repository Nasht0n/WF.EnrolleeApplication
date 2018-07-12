using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using WF.EnrolleeApplication.DataAccess.Services;
using NLog;

namespace WF.EnrolleeApplication.App.Services
{
    public class ExamDiscipline
    {
        public Discipline discipline;
        public int ColumnIndex;
    }

    public class ReportManager
    {
        public static string ConnectionString;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        // Заявление
        public static void PrintStatement(Enrollee enrollee)
        {     
            string path = Environment.CurrentDirectory + "\\Templates\\Заявление абитуриента.dotx";
            Word.Application wordApp = new Word.Application();
            wordApp.Visible = false;
            try
            {
                Object template = path; // - имя шаблона, по которому создается новый документ
                Object newTemplate = false; // при true новый документ открывается как шаблон.
                Object documentType = Word.WdNewDocumentType.wdNewBlankDocument; // документ Word (по умолчанию)
                Object visible = true;//видимость документа. При true (по умолчанию) документ отображается.
                Word.Document wordDocument = wordApp.Documents.Add(ref template, ref newTemplate, ref documentType, ref visible);
                SystemConfigurationService configurationService = new SystemConfigurationService(ConnectionString);
                SystemConfiguration systemConfiguration = configurationService.GetSystemConfiguration("V_UNI_NAME");
                wordDocument.Variables["Наименование УО"].Value = systemConfiguration.Value;
                wordDocument.Variables["ФИО абитуриента"].Value = $"{enrollee.RuSurname.Trim()} {enrollee.RuName.Trim()} {enrollee.RuPatronymic.Trim()}";
                wordDocument.Variables["Страна"].Value = enrollee.Country.Name;
                wordDocument.Variables["Область"].Value = enrollee.Area.Name;
                wordDocument.Variables["Район"].Value = $"{enrollee.District.Name} р-н";
                wordDocument.Variables["Индекс"].Value = enrollee.SettlementIndex.ToString();
                wordDocument.Variables["Город"].Value = $"{enrollee.TypeOfSettlement.Shortname.Trim()} {enrollee.SettlementName.Trim()}";
                wordDocument.Variables["Улица"].Value = $"{enrollee.TypeOfStreet.Shortname.Trim()} {enrollee.StreetName.Trim()}";
                wordDocument.Variables["Номер дома"].Value = $"д.{enrollee.NumberHouse}";
                wordDocument.Variables["Номер квартиры"].Value = $"кв.{enrollee.NumberFlat}";
                wordDocument.Variables["Домашний телефон"].Value = $"{enrollee.HomePhone} (дом.)";
                wordDocument.Variables["Мобильный телефон"].Value = $"{enrollee.MobilePhone} (моб.)";
                wordDocument.Variables["Последнее УО"].Value = $"{enrollee.SchoolYear.Trim()}, {enrollee.SchoolName} {enrollee.SchoolAddress}";
                wordDocument.Variables["Текущий курс"].Value = $"{enrollee.CurrentNumberCurs.ToString()} ";
                wordDocument.Variables["Текущее учреждение образования"].Value = $"{enrollee.CurrentUniversity.Trim()} ";
                wordDocument.Variables["Текущая специальность"].Value = $"{enrollee.CurrentSpeciality.Trim()} ";
                wordDocument.Variables["Иностранный язык"].Value = enrollee.ForeignLanguage.Name;
                wordDocument.Variables["Факультет"].Value = enrollee.Speciality.Faculty.Fullname;
                PriorityOfSpecialityService priorityOfSpecialityService = new PriorityOfSpecialityService(ConnectionString);
                List<PriorityOfSpeciality> priorities = priorityOfSpecialityService.GetPriorityOfSpecialities(enrollee);
                if (priorities.Count != 0)
                {
                    int i = 1;
                    foreach (var priority in priorities)
                    {
                        string field = string.Format("Приоритет {0}", i);
                        if (!string.IsNullOrWhiteSpace(priority.Speciality.FormOfStudy.Shortname)) wordDocument.Variables[field].Value = $"{priority.Speciality.Cipher} {priority.Speciality.Fullname}({priority.Speciality.Specialization}) -{priority.Speciality.FormOfStudy.Shortname.Trim()}";
                        else wordDocument.Variables[field].Value = $"{priority.Speciality.Cipher} {priority.Speciality.Fullname}({priority.Speciality.Specialization})";
                        i++;
                    }
                    for (int j = priorities.Count + 1; j < 10; j++)
                    {
                        string field = string.Format("Приоритет {0}", j);
                        wordDocument.Variables[field].Value = " ";
                    }
                }
                wordDocument.Variables["Дата рождения"].Value = enrollee.DateOfBirthday.ToShortDateString();
                wordDocument.Variables["Место работы"].Value = $"{enrollee.WorkPlace} ";
                wordDocument.Variables["Должность"].Value = $"{enrollee.WorkPost} ";
                if (!string.IsNullOrWhiteSpace(enrollee.Seniority))
                {
                    int year = Int32.Parse(enrollee.Seniority) / 12;
                    int mounts = Int32.Parse(enrollee.Seniority) - year * 12;
                    wordDocument.Variables["Лет стажа"].Value = $"{year.ToString()} ";
                    wordDocument.Variables["Месяцев стажа"].Value = $"{mounts.ToString()} ";
                }
                else
                {
                    wordDocument.Variables["Лет стажа"].Value = $" ";
                    wordDocument.Variables["Месяцев стажа"].Value = $" ";
                }
                AtributeForEnrolleeService atributeService = new AtributeForEnrolleeService(ConnectionString);
                List<AtributeForEnrollee> atributes = atributeService.GetAtributeForEnrollees(enrollee);
                bool flag = false;
                if (atributes.Count != 0)
                {
                    foreach (var atribute in atributes)
                        if (atribute.AtributeId == 13) flag = true;
                }
                if (flag) wordDocument.Variables["Нуждаюсь в общежитии"].Value = "Да";
                else wordDocument.Variables["Нуждаюсь в общежитии"].Value = "Нет";


                wordDocument.Variables["ФИО отца"].Value = $" {enrollee.FatherFullname} ";
                wordDocument.Variables["Местожительство отца"].Value = $" {enrollee.FatherAddress} ";

                wordDocument.Variables["ФИО матери"].Value = $" {enrollee.MotherFullname} ";
                wordDocument.Variables["Местожительство матери"].Value = $" {enrollee.MotherAddress} ";

                string atributeString = " ";
                if (atributes.Count != 0)
                {
                    foreach (var atribute in atributes)
                        if (atribute.Atribute.IsDiscount)
                        {
                            atributeString += $"{atribute.Atribute.Fullname} ";
                        }
                }
                wordDocument.Variables["Список льгот"].Value = atributeString;
                wordDocument.Variables["Серия документа"].Value = enrollee.DocumentSeria.Trim();
                wordDocument.Variables["Номер документа"].Value = enrollee.DocumentNumber.Trim();
                wordDocument.Variables["Дата выдачи документа"].Value = enrollee.DocumentDate.ToShortDateString();
                wordDocument.Variables["Орган выдавший документ"].Value = enrollee.DocumentWhoGave.Trim();
                wordDocument.Variables["Личный номер"].Value = enrollee.DocumentPersonalNumber.Trim();
                wordDocument.Variables["Вид конкурса"].Value = enrollee.ReasonForAddmission.Contest.Name.Trim();
                wordDocument.Variables["Дополнительные сведения"].Value = $" ";
                wordDocument.Fields.Update();
                wordApp.Visible = true;
                wordApp.Activate();
            }
            catch (Exception ex)
            {
                Object saveChanges = Word.WdSaveOptions.wdPromptToSaveChanges;
                Object originalFormat = Word.WdOriginalFormat.wdWordDocument;
                Object routeDocument = Type.Missing;
                ((Word._Application)wordApp).Quit(ref saveChanges, ref originalFormat, ref routeDocument);
                wordApp = null;
            }
            finally
            {
                //wordApp.Quit(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        // Титульный лист
        public static void PrintTitle(Enrollee enrollee)
        {
            string path = Environment.CurrentDirectory + "\\Templates\\Титульный лист.dotx";
            Word.Application wordApp = new Word.Application();
            wordApp.Visible = false;
            try
            {
                Object template = path; // - имя шаблона, по которому создается новый документ
                Object newTemplate = false; // при true новый документ открывается как шаблон.
                Object documentType = Word.WdNewDocumentType.wdNewBlankDocument; // документ Word (по умолчанию)
                Object visible = true;//видимость документа. При true (по умолчанию) документ отображается.
                Word.Document wordDocument = wordApp.Documents.Add(ref template, ref newTemplate, ref documentType, ref visible);
                wordDocument.Variables["Факультет"].Value = enrollee.Speciality.Faculty.Fullname.Trim();
                wordDocument.Variables["Форма обучения"].Value = enrollee.Speciality.FormOfStudy.Fullname.Trim();
                wordDocument.Variables["Специальность"].Value = enrollee.Speciality.Fullname.Trim();
                string numberOfDeal = null;
                if (string.IsNullOrWhiteSpace(enrollee.Speciality.FormOfStudy.Shortname)) numberOfDeal = $"{enrollee.Speciality.Shortname.Trim()}-{enrollee.NumberOfDeal}";
                else numberOfDeal = $"{enrollee.Speciality.Shortname.Trim()}{enrollee.Speciality.FormOfStudy.Shortname.Trim()}-{enrollee.NumberOfDeal}";
                wordDocument.Variables["Номер личного дела"].Value = numberOfDeal;
                wordDocument.Variables["Тип финансирования"].Value = enrollee.TypeOfFinance.Fullname.Trim();
                wordDocument.Variables["ФИО Абитуриента"].Value = $"{enrollee.RuSurname.Trim()} {enrollee.RuName.Trim()} {enrollee.RuPatronymic.Trim()}";
                wordDocument.Variables["Вид конкурса"].Value = enrollee.ReasonForAddmission.Contest.Name.Trim();
                wordDocument.Variables["Основание"].Value = enrollee.ReasonForAddmission.Fullname.Trim();
                wordDocument.Variables["Статус"].Value = enrollee.TypeOfState.Name.Trim();

                if (!enrollee.TargetWorkPlaceId.HasValue) wordDocument.Variables["Целевое направление"].Value = "Нет";
                else wordDocument.Variables["Целевое направление"].Value = enrollee.TargetWorkPlace.Name.Trim();
                wordDocument.Variables["Страна"].Value = enrollee.Country.Name;
                wordDocument.Variables["Область"].Value = enrollee.Area.Name;
                wordDocument.Variables["Район"].Value = $"{enrollee.District.Name} р-н";
                wordDocument.Variables["Индекс"].Value = enrollee.SettlementIndex.ToString();
                wordDocument.Variables["Город"].Value = $"{enrollee.TypeOfSettlement.Shortname.Trim()} {enrollee.SettlementName.Trim()}";
                wordDocument.Variables["Улица"].Value = $"{enrollee.TypeOfStreet.Shortname.Trim()} {enrollee.StreetName.Trim()}";
                wordDocument.Variables["Номер дома"].Value = $"д.{enrollee.NumberHouse.Trim()}";
                wordDocument.Variables["Номер квартиры"].Value = $"кв.{enrollee.NumberFlat.Trim()}";
                wordDocument.Variables["Телефон домашний"].Value = $"{enrollee.HomePhone} (дом.)";
                wordDocument.Variables["Телефон мобильный"].Value = $"{enrollee.MobilePhone} (моб.)";
                wordDocument.Variables["Иностранный язык"].Value = $"{enrollee.ForeignLanguage.Name} ";
                AssessmentService assessmentService = new AssessmentService(ConnectionString);
                List<Assessment> assessments = assessmentService.GetAssessments(enrollee);

                if (assessments.Count != 0)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        string fieldSubject = string.Format("Предмет {0}", j);
                        string fieldEstimation = string.Format("Оценка {0}", j);
                        string fieldSertcode = string.Format("Сертификат {0}", j);
                        wordDocument.Variables[fieldSubject].Value = " ";
                        wordDocument.Variables[fieldEstimation].Value = " ";
                        wordDocument.Variables[fieldSertcode].Value = " ";
                    }

                    int index = 1;
                    foreach (var assessment in assessments)
                    {
                        if (assessment.Discipline.BasisForAssessingId == 1)
                        {
                            wordDocument.Variables["Средний балл аттестата"].Value = assessment.Estimation.ToString();
                        }
                        else if (assessment.Discipline.BasisForAssessingId == 3)
                        {
                            string fieldSubject = string.Format("Предмет {0}", index);
                            string fieldEstimation = string.Format("Оценка {0}", index);
                            string fieldSertcode = string.Format("Сертификат {0}", index);
                            wordDocument.Variables[fieldSubject].Value = assessment.Discipline.Name.Trim();
                            wordDocument.Variables[fieldEstimation].Value = assessment.Estimation.ToString();
                            wordDocument.Variables[fieldSertcode].Value = assessment.SertCode.Trim();
                            index++;
                        }
                        else
                        {
                            string fieldSubject = string.Format("Предмет {0}", index);
                            wordDocument.Variables[fieldSubject].Value = assessment.Discipline.Name.Trim();
                            index++;
                        }
                    }

                    wordDocument.Variables["Сумма баллов"].Value = assessments.Sum(a => a.Estimation).ToString();
                }
                // Оценки аттестата
                if (!string.IsNullOrWhiteSpace(enrollee.AttestatEstimationString))
                {
                    wordDocument.Variables["Аттестат"].Value = "Оценки аттестата";
                    string estimation_string = "";
                    foreach (char c in enrollee.AttestatEstimationString)
                    {
                        if (c == '0') estimation_string += "10";
                        else estimation_string += c;
                    }
                    wordDocument.Variables["Оценки аттестата"].Value = estimation_string;
                }
                else
                {
                    wordDocument.Variables["Аттестат"].Value = " ";
                    wordDocument.Variables["Оценки аттестата"].Value = " ";
                }

                // Оценки диплома ПТУ
                if (!string.IsNullOrWhiteSpace(enrollee.DiplomPtuEstimationString))
                {
                    wordDocument.Variables["Диплом ПТУ"].Value = "Оценки диплома ПТУ";
                    string estimation_string = "";
                    foreach (char c in enrollee.DiplomPtuEstimationString)
                    {
                        if (c == '0') estimation_string += "10";
                        else estimation_string += c;
                    }
                    wordDocument.Variables["Оценки диплома ПТУ"].Value = estimation_string;
                }
                else
                {
                    wordDocument.Variables["Диплом ПТУ"].Value = " ";
                    wordDocument.Variables["Оценки диплома ПТУ"].Value = " ";
                }

                // Оценки диплома СУЗ
                if (!string.IsNullOrWhiteSpace(enrollee.DiplomSusEstimationString))
                {
                    wordDocument.Variables["Диплом СУЗ"].Value = "Оценки диплома ССУЗа";
                    string estimation_string = "";
                    foreach (char c in enrollee.DiplomSusEstimationString)
                    {
                        if (c == '0') estimation_string += "10";
                        else estimation_string += c;
                    }
                    wordDocument.Variables["Оценки диплома СУЗ"].Value = estimation_string;
                }
                else
                {
                    wordDocument.Variables["Диплом СУЗ"].Value = " ";
                    wordDocument.Variables["Оценки диплома СУЗ"].Value = " ";
                }

                wordDocument.Variables["Дата приёма документов"].Value = enrollee.DateDeal.ToShortDateString();
                wordDocument.Variables["Гражданство"].Value = enrollee.Citizenship.Fullname.Trim();
                AtributeForEnrolleeService atributeService = new AtributeForEnrolleeService(ConnectionString);
                List<AtributeForEnrollee> atributes = atributeService.GetAtributeForEnrollees(enrollee);
                string atributeString = " ";
                if (atributes.Count != 0)
                {
                    foreach (var atribute in atributes)
                    {
                        if (atribute.Atribute.IsDiscount)
                        {
                            atributeString += string.Format(" {0} ", atribute.Atribute.Fullname.Trim());
                        }
                    }
                }
                if (string.IsNullOrWhiteSpace(atributeString)) wordDocument.Variables["Список льгот"].Value = "Не имеет";
                else wordDocument.Variables["Список льгот"].Value = atributeString;
                wordDocument.Variables["Ответственное лицо"].Value = enrollee.PersonInCharge;
                wordDocument.Fields.Update();
                wordApp.Visible = true;
                wordApp.Activate();
            }
            catch (Exception ex)
            {
                Object saveChanges = Word.WdSaveOptions.wdPromptToSaveChanges;
                Object originalFormat = Word.WdOriginalFormat.wdWordDocument;
                Object routeDocument = Type.Missing;
                ((Word._Application)wordApp).Quit(ref saveChanges, ref originalFormat, ref routeDocument);
                wordApp = null;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        // Экзаменнационный лист
        public static void PrintExamSheet(Enrollee enrollee)
        {
            string path = Environment.CurrentDirectory + "\\Templates\\Экзаменнационный лист.dotx";
            Word.Application wordApp = new Word.Application();
            wordApp.Visible = false;
            try
            {
                Object template = path; // - имя шаблона, по которому создается новый документ
                Object newTemplate = false; // при true новый документ открывается как шаблон.
                Object documentType = Word.WdNewDocumentType.wdNewBlankDocument; // документ Word (по умолчанию)
                Object visible = true;//видимость документа. При true (по умолчанию) документ отображается.
                Word.Document wordDocument = wordApp.Documents.Add(ref template, ref newTemplate, ref documentType, ref visible);
                string numberOfDeal = null;
                if (string.IsNullOrWhiteSpace(enrollee.Speciality.FormOfStudy.Shortname.Trim())) numberOfDeal = $"{enrollee.Speciality.Shortname.Trim()}-{enrollee.NumberOfDeal}";
                else numberOfDeal = $"{enrollee.Speciality.Shortname.Trim()}{enrollee.Speciality.FormOfStudy.Shortname.Trim()}-{enrollee.NumberOfDeal}";
                wordDocument.Variables["Номер личного дела"].Value = numberOfDeal;
                wordDocument.Variables["Фамилия"].Value = enrollee.RuSurname.Trim();
                wordDocument.Variables["Имя"].Value = enrollee.RuName.Trim();
                wordDocument.Variables["Отчество"].Value = enrollee.RuPatronymic.Trim();
                wordDocument.Variables["Факультет"].Value = enrollee.Speciality.Faculty.Fullname.Trim();
                wordDocument.Variables["Специальность"].Value = enrollee.Speciality.Fullname.Trim();
                wordDocument.Variables["Группа"].Value = $"{enrollee.Speciality.Shortname.Trim()}{enrollee.Speciality.FormOfStudy.Shortname.Trim()}";
                SystemConfigurationService configurationService = new SystemConfigurationService(ConnectionString);
                SystemConfiguration systemConfiguration = configurationService.GetSystemConfiguration("SOKR_SEKR");
                wordDocument.Variables["Секретарь приемной комиссии"].Value = systemConfiguration.Value;
                Word.Table table = wordDocument.Tables[4];
                AssessmentService assessmentService = new AssessmentService(ConnectionString);
                List<Assessment> assessments = assessmentService.GetAssessments(enrollee);
                int index = 1;
                foreach (var assessment in assessments)
                {
                    table.Rows.Add();
                    // index
                    table.Cell(index + 2, 1).Range.Text = index.ToString();
                    BasisForAssessingService basisForAssessingService = new BasisForAssessingService(ConnectionString);
                    BasisForAssessing basisForAssessing = basisForAssessingService.GetBasisForAssessing(assessment.Discipline.BasisForAssessingId.Value);
                    if (basisForAssessing.BasisForAssessingId == 1)
                    {
                        table.Cell(index + 2, 2).Range.Text = "Средний балл";
                        table.Cell(index + 2, 4).Range.Text = assessment.Estimation.ToString();
                        EstimationStringService estimationService = new EstimationStringService(ConnectionString);
                        string estimationString = " ";
                        if (assessment.Estimation.HasValue) estimationString = estimationService.EstimationAsText(assessment.Estimation.Value);
                        if (!string.IsNullOrWhiteSpace(estimationString)) table.Cell(index + 2, 5).Range.Text = estimationString;
                    }
                    else
                    {
                        string stages = "";
                        if (assessment.Discipline.StageCount.HasValue)
                        {
                            int count = assessment.Discipline.StageCount.Value;
                            stages += $" ({assessment.Discipline.StageCount.Value} - этапа)";
                        }

                        table.Cell(index + 2, 2).Range.Text = assessment.Discipline.Name.Trim()+stages;
                        table.Cell(index + 2, 3).Range.Text = assessment.SertDate;
                        if (assessment.Estimation == 0)
                        {
                            table.Cell(index + 2, 4).Range.Text = " ";
                            table.Cell(index + 2, 5).Range.Text = " ";
                        }
                        else
                        {
                            table.Cell(index + 2, 4).Range.Text = assessment.Estimation.ToString();
                            EstimationStringService estimationService = new EstimationStringService(ConnectionString);
                            string estimationString = " ";
                            if (assessment.Estimation.HasValue) estimationString = estimationService.EstimationAsText(assessment.Estimation.Value);
                            table.Cell(index + 2, 5).Range.Text = estimationString;
                        }
                    }
                    index++;
                }
                wordDocument.Fields.Update();
                wordApp.Visible = true;
                wordApp.Activate();
            }
            catch (Exception ex)
            {
                Object saveChanges = Word.WdSaveOptions.wdPromptToSaveChanges;
                Object originalFormat = Word.WdOriginalFormat.wdWordDocument;
                Object routeDocument = Type.Missing;
                ((Word._Application)wordApp).Quit(ref saveChanges, ref originalFormat, ref routeDocument);
                wordApp = null;
            }
            finally
            {
                //  wordApp.Quit(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        // Расписка
        public static void PrintReceipt(Enrollee enrollee, string documentOfStudy, string documentOfDiscount, string documentOther)
        {
            string path = Environment.CurrentDirectory + "\\Templates\\Расписка.dotx";
            Word.Application wordApp = new Word.Application();
            wordApp.Visible = false;
            try
            {
                Object template = path; // - имя шаблона, по которому создается новый документ
                Object newTemplate = false; // при true новый документ открывается как шаблон.
                Object documentType = Word.WdNewDocumentType.wdNewBlankDocument; // документ Word (по умолчанию)
                Object visible = true;//видимость документа. При true (по умолчанию) документ отображается.
                Word.Document wordDocument = wordApp.Documents.Add(ref template, ref newTemplate, ref documentType, ref visible);
                string numberOfDeal = null;
                if (string.IsNullOrWhiteSpace(enrollee.Speciality.FormOfStudy.Shortname.Trim())) numberOfDeal = $"{enrollee.Speciality.Shortname.Trim()}-{enrollee.NumberOfDeal}";
                else numberOfDeal = $"{enrollee.Speciality.Shortname.Trim()}{enrollee.Speciality.FormOfStudy.Shortname.Trim()}-{enrollee.NumberOfDeal}";
                wordDocument.Variables["Номер личного дела"].Value = numberOfDeal;
                wordDocument.Variables["Факультет"].Value = enrollee.Speciality.Faculty.Fullname.Trim();
                wordDocument.Variables["Шифр"].Value = $" {enrollee.Speciality.Cipher.Trim()} ";
                wordDocument.Variables["Специальность"].Value = $"{enrollee.Speciality.Fullname.Trim()}";
                wordDocument.Variables["ФИО Абитуриента"].Value = string.Format("{0} {1} {2}", enrollee.RuSurname.ToUpper().Trim(), enrollee.RuName.Trim(), enrollee.RuPatronymic.Trim());
                wordDocument.Variables["Секретарь приемной комиссии"].Value = enrollee.PersonInCharge.Trim();
                wordDocument.Variables["Документ об образовании"].Value = $" {documentOfStudy} ";
                AssessmentService assessmentService = new AssessmentService(ConnectionString);
                List<Assessment> assessments = assessmentService.GetAssessments(enrollee);
                for (int j = 1; j < 4; j++)
                {
                    string field = string.Format("Сертификат {0}", j);
                    wordDocument.Variables[field].Value = " ";
                }
                int index = 1;
                foreach (var assessment in assessments)
                {
                    string field = string.Format("Сертификат {0}", index);
                    if (assessment.Discipline.BasisForAssessingId.Value == 3)
                    {
                        wordDocument.Variables[field].Value = $"Сертификат {assessment.SertCode} {assessment.Discipline.Name.Trim()}";
                        index++;
                    }
                }
                if (enrollee.TargetWorkPlaceId.HasValue)
                {
                    wordDocument.Variables["Договор целевой"].Value = enrollee.TargetWorkPlace.Name.Trim();
                    wordDocument.Variables["Направление"].Value = "Да";
                }
                else
                {
                    wordDocument.Variables["Договор целевой"].Value = " ";
                    wordDocument.Variables["Направление"].Value = "Нет";
                }
                wordDocument.Variables["Документы на льготы"].Value = $" {documentOfDiscount} ";
                wordDocument.Variables["Иные документы"].Value = $" {documentOther} ";
                wordDocument.Fields.Update();
                wordApp.Visible = true;
                wordApp.Activate();
            }
            catch (Exception ex)
            {
                Object saveChanges = Word.WdSaveOptions.wdPromptToSaveChanges;
                Object originalFormat = Word.WdOriginalFormat.wdWordDocument;
                Object routeDocument = Type.Missing;
                ((Word._Application)wordApp).Quit(ref saveChanges, ref originalFormat, ref routeDocument);
                wordApp = null;
            }
            finally
            {
                //  wordApp.Quit(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        // Извещение
        public static void PrintNotice(Enrollee enrollee)
        {
            string path = Environment.CurrentDirectory + "\\Templates\\Извещение.dotx";
            Word.Application wordApp = new Word.Application();
            wordApp.Visible = false;
            try
            {
                Object template = path; // - имя шаблона, по которому создается новый документ
                Object newTemplate = false; // при true новый документ открывается как шаблон.
                Object documentType = Word.WdNewDocumentType.wdNewBlankDocument; // документ Word (по умолчанию)
                Object visible = true;//видимость документа. При true (по умолчанию) документ отображается.
                Word.Document wordDocument = wordApp.Documents.Add(ref template, ref newTemplate, ref documentType, ref visible);
                string numberOfDeal = null;
                if (string.IsNullOrWhiteSpace(enrollee.Speciality.FormOfStudy.Shortname.Trim())) numberOfDeal = $"{enrollee.Speciality.Shortname.Trim()}-{enrollee.NumberOfDeal}";
                else numberOfDeal = $"{enrollee.Speciality.Shortname.Trim()}{enrollee.Speciality.FormOfStudy.Shortname.Trim()}-{enrollee.NumberOfDeal}";
                wordDocument.Variables["Номер личного дела"].Value = numberOfDeal;
                wordDocument.Variables["ФИО Абитуриента"].Value = string.Format("{0} {1} {2}", enrollee.RuSurname.ToUpper().Trim(), enrollee.RuName.Trim(), enrollee.RuPatronymic.Trim());
                wordDocument.Variables["Страна"].Value = enrollee.Country.Name.Trim();
                wordDocument.Variables["Область"].Value = enrollee.Area.Name.Trim();
                wordDocument.Variables["Район"].Value = $"{enrollee.District.Name} р-н";
                wordDocument.Variables["Город"].Value = $"{enrollee.TypeOfSettlement.Shortname.Trim()} {enrollee.SettlementName.Trim()}";
                wordDocument.Variables["Улица"].Value = $"{enrollee.TypeOfStreet.Shortname.Trim()} {enrollee.StreetName.Trim()}";
                wordDocument.Variables["Номер дома"].Value = $"д.{enrollee.NumberHouse}";
                wordDocument.Variables["Номер квартиры"].Value = $"кв.{enrollee.NumberFlat} ";
                wordDocument.Variables["Номер протокола"].Value = "1";
                for (int j = 1; j < 4; j++)
                {
                    string field = string.Format("Вступительное испытание {0}", j);
                    wordDocument.Variables[field].Value = " ";
                }
                AssessmentService assessmentService = new AssessmentService(ConnectionString);
                List<Assessment> assessments = assessmentService.GetAssessments(enrollee);
                int index = 1;
                foreach (var assessment in assessments)
                {
                    if (assessment.Discipline.BasisForAssessingId == 2)
                    {
                        string field = string.Format("Вступительное испытание {0}", index);
                        string text = string.Format("{0} — {1}", assessment.Discipline.Name.Trim(), assessment.Discipline.EntryExamDate.Trim());
                        wordDocument.Variables[field].Value = text;
                        wordDocument.Variables["Дата консультации"].Value = assessment.Discipline.ConsultDate.Trim();
                        index++;
                    }
                }
                wordDocument.Variables["Вид конкурса"].Value = enrollee.ReasonForAddmission.Contest.Name.Trim();
                SystemConfigurationService configurationService = new SystemConfigurationService(ConnectionString);
                SystemConfiguration systemConfiguration = configurationService.GetSystemConfiguration("SOKR_SEKR");
                wordDocument.Variables["Секретарь приемной комиссии"].Value = systemConfiguration.Value;
                wordDocument.Fields.Update();
                wordApp.Visible = true;
                wordApp.Activate();
            }
            catch (Exception ex)
            {
                Object saveChanges = Word.WdSaveOptions.wdPromptToSaveChanges;
                Object originalFormat = Word.WdOriginalFormat.wdWordDocument;
                Object routeDocument = Type.Missing;
                ((Word._Application)wordApp).Quit(ref saveChanges, ref originalFormat, ref routeDocument);
                wordApp = null;
            }
            finally
            {
                //  wordApp.Quit(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        // Сводная экзаменнационная ведомость
        public static void PrintSummaryExaminationSheet(List<Enrollee> enrollees, Speciality speciality)
        {
            ExamSchemaService examSchemaService = new ExamSchemaService(ConnectionString);
            AssessmentService assessmentService = new AssessmentService(ConnectionString);
            DisciplineService disciplineService = new DisciplineService(ConnectionString);
            string path = Environment.CurrentDirectory + "\\Templates\\Сводная экзаменнационная ведомость.dotx";
            Word.Application wordApp = new Word.Application();
            wordApp.Visible = false;
            try
            {
                Object template = path; // - имя шаблона, по которому создается новый документ
                Object newTemplate = false; // при true новый документ открывается как шаблон.
                Object documentType = Word.WdNewDocumentType.wdNewBlankDocument; // документ Word (по умолчанию)
                Object visible = true;//видимость документа. При true (по умолчанию) документ отображается.
                Word.Document wordDocument = wordApp.Documents.Add(ref template, ref newTemplate, ref documentType, ref visible);
                SystemConfigurationService systemConfigurationService = new SystemConfigurationService(ConnectionString);
                wordDocument.Variables["Наименование учреждения образования"].Value = systemConfigurationService.GetSystemConfiguration("V_UNI_NAME").Value;
                wordDocument.Variables["Факультет"].Value = $" {speciality.Faculty.Fullname.Trim()} ";
                wordDocument.Variables["Специальность"].Value = $" {speciality.Cipher.Trim()} {speciality.Fullname.Trim()}";
                wordDocument.Variables["Форма получения образования"].Value = $"{speciality.FormOfStudy.Fullname.Trim() }";
                if (speciality.FormOfStudyId == 6 || speciality.FormOfStudyId == 7)
                {
                    wordDocument.Variables["Основание"].Value = "среднего специального образования";
                }
                else
                {
                    wordDocument.Variables["Основание"].Value = "общего среднего, профессионально-технического с общим средним, среднего специального образования";
                }

                int index = 1;
                int indexTable = 3;
                Word.Table table = wordDocument.Tables[indexTable];
                foreach (var enrollee in enrollees)
                {
                    table.Rows.Add();
                    table.Cell(index + 4, 1).Range.Text = index.ToString(); // № п/п
                    string numberOfDeal = null;
                    if (string.IsNullOrWhiteSpace(enrollee.Speciality.FormOfStudy.Shortname.Trim())) numberOfDeal = $"{enrollee.Speciality.Shortname.Trim()}-{enrollee.NumberOfDeal}";
                    else numberOfDeal = $"{enrollee.Speciality.Shortname.Trim()}{enrollee.Speciality.FormOfStudy.Shortname.Trim()}-{enrollee.NumberOfDeal}";
                    table.Cell(index + 4, 2).Range.Text = numberOfDeal; // № экз.листа
                    string fullname = null;
                    if (string.IsNullOrWhiteSpace(enrollee.RuPatronymic)) fullname = $"{enrollee.RuSurname.Trim()} {enrollee.RuName.Trim()}";
                    else fullname = $"{enrollee.RuSurname.Trim()} {enrollee.RuName.Trim()} {enrollee.RuPatronymic.Trim()}";
                    table.Cell(index + 4, 3).Range.Text = fullname; // ФИО абитуриента


                    var exams = examSchemaService.GetExamSchemas(speciality);
                    var assessments = assessmentService.GetAssessments(enrollee);

                    string disciplineList = "";
                    string averageDocumentEstimation = "";
                    int indexAssessment = 4;

                    List<ExamDiscipline> examDisciplines = new List<ExamDiscipline>();
                    foreach (var exam in exams)
                    {

                        if (exam.Discipline.BasisForAssessingId == 1)
                        {
                            ExamDiscipline examDiscipline = new ExamDiscipline();
                            examDiscipline.ColumnIndex = indexAssessment;
                            examDiscipline.discipline = exam.Discipline;
                            examDisciplines.Add(examDiscipline);
                            continue;
                        }
                        if (exam.Discipline.IsGroup)
                        {
                            var disciplines = disciplineService.GetDisciplines(exam.Discipline);
                            foreach (var discipline in disciplines)
                            {
                                ExamDiscipline examDiscipline = new ExamDiscipline();
                                examDiscipline.ColumnIndex = indexAssessment;
                                examDiscipline.discipline = discipline;
                                disciplineList += $"{indexAssessment - 3}) {discipline.Name.Trim()}; ";
                                examDisciplines.Add(examDiscipline);
                                indexAssessment++;
                            }
                        }
                        else
                        {
                            ExamDiscipline examDiscipline = new ExamDiscipline();
                            examDiscipline.ColumnIndex = indexAssessment;
                            examDiscipline.discipline = exam.Discipline;
                            disciplineList += $"{indexAssessment - 3}) {exam.Discipline.Name.Trim()}; ";
                            examDisciplines.Add(examDiscipline);
                            indexAssessment++;
                        }
                    }


                    foreach (var discipline in examDisciplines)
                    {

                        table.Cell(index + 4, discipline.ColumnIndex).Range.Text = "0";
                        foreach (var assessment in assessments)
                        {
                            if (assessment.Discipline.BasisForAssessingId == 1)
                            {
                                averageDocumentEstimation = assessment.Estimation.ToString();
                            }
                            else if (discipline.discipline.DisciplineId == assessment.DisciplineId)
                            {
                                if (!string.IsNullOrWhiteSpace(assessment.ChangeDiscipline))
                                    table.Cell(index + 4, discipline.ColumnIndex).Range.Text = assessment.Estimation.ToString() + "*";
                                else table.Cell(index + 4, discipline.ColumnIndex).Range.Text = assessment.Estimation.ToString();
                            }
                        }
                    }


                    wordDocument.Variables["Предметы"].Value = disciplineList; // Список предметов
                    table.Cell(index + 4, 11).Range.Text = averageDocumentEstimation; // Средний балл документа об образованиии
                                                                                      // Сумма баллов
                    int? sum = assessments.Sum(a => a.Estimation);
                    if (sum.HasValue) table.Cell(index + 4, 12).Range.Text = sum.Value.ToString();
                    else table.Cell(index + 4, 12).Range.Text = "0";
                    // Конкурс
                    char contestFirstChar = enrollee.ReasonForAddmission.Contest.Name[0];
                    table.Cell(index + 4, 13).Range.Text = contestFirstChar.ToString();
                    // Город/Село
                    if (enrollee.TypeOfSettlement.IsTown) table.Cell(index + 4, 14).Range.Text = "Г";
                    else table.Cell(index + 4, 14).Range.Text = "C";
                    AtributeForEnrolleeService atributeForEnrolleeService = new AtributeForEnrolleeService(ConnectionString);
                    var atributes = atributeForEnrolleeService.GetAtributeForEnrollees(enrollee);
                    string atributeList = "";
                    foreach (var atribute in atributes)
                    {
                        atributeList += $"{atribute.Atribute.Shortname.Trim()} ";
                    }
                    table.Cell(index + 4, 15).Range.Text = atributeList;
                    PriorityOfSpecialityService priorityOfSpecialityService = new PriorityOfSpecialityService(ConnectionString);
                    var priorities = priorityOfSpecialityService.GetPriorityOfSpecialities(enrollee).OrderBy(p=>p.PriorityLevel).ToList();
                    string priorityList = "";
                    foreach (var priority in priorities)
                    {
                        priorityList += $"{priority.PriorityLevel} — {priority.Speciality.Shortname.Trim()}({priority.Speciality.FormOfStudy.Shortname.Trim()}); ";
                    }
                    table.Cell(index + 4, 16).Range.Text = priorityList;
                    index++;
                }
                wordDocument.Variables["Секретарь приемной комиссии"].Value = systemConfigurationService.GetSystemConfiguration("SOKR_SEKR").Value;
                wordDocument.Fields.Update();
                wordApp.Visible = true;
                wordApp.Activate();
            }
            catch (Exception ex)
            {
                Object saveChanges = Word.WdSaveOptions.wdPromptToSaveChanges;
                Object originalFormat = Word.WdOriginalFormat.wdWordDocument;
                Object routeDocument = Type.Missing;
                ((Word._Application)wordApp).Quit(ref saveChanges, ref originalFormat, ref routeDocument);
                wordApp = null;
            }
            finally
            {
                //  wordApp.Quit(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        // Экзаменнационная ведомость
        public static void PrintExaminationSheet(List<ExamSchema> exams, List<Enrollee> enrollees)
        {
            string path = Environment.CurrentDirectory + "\\Templates\\Экзаменнационная ведомость.dotx";
            Word.Application wordApp = new Word.Application();
            wordApp.Visible = true;
            try
            {
                Object template = path; // - имя шаблона, по которому создается новый документ
                Object newTemplate = false; // при true новый документ открывается как шаблон.
                Object documentType = Word.WdNewDocumentType.wdNewBlankDocument; // документ Word (по умолчанию)
                Object visible = false;//видимость документа. При true (по умолчанию) документ отображается.
                SystemConfigurationService systemConfigurationService = new SystemConfigurationService(ConnectionString);
                ExamSchemaService examSchemaService = new ExamSchemaService(ConnectionString);
                EnrolleeService enrolleeService = new EnrolleeService(ConnectionString);
                AssessmentService assessmentService = new AssessmentService(ConnectionString);
                EstimationStringService estimationStringService = new EstimationStringService(ConnectionString);       
                foreach (var exam in exams)
                {
                    if (exam.Discipline.BasisForAssessingId == 2)
                    {
                        Word.Document wordDocument = wordApp.Documents.Add(ref template, ref newTemplate, ref documentType, ref visible);
                        wordDocument.Variables["Наименование учреждения образования"].Value = systemConfigurationService.GetSystemConfiguration("V_UNI_NAME").Value;
                        wordDocument.Variables["Дисциплина"].Value = exam.Discipline.Name.Trim();
                        wordDocument.Variables["Факультет"].Value = exam.Speciality.Faculty.Fullname.Trim();
                        wordDocument.Variables["№ группы"].Value = exam.Speciality.Shortname.Trim();
                        int index = 1;
                        int indexTable = 4;
                        Word.Table table = wordDocument.Tables[indexTable];
                        foreach (var enrollee in enrollees)
                        {
                            var assessment = assessmentService.GetAssessment(exam.Discipline, enrollee);
                            if (assessment != null)
                            {
                                table.Rows.Add();
                                table.Cell(index + 2, 1).Range.Text = index.ToString();
                                if (string.IsNullOrWhiteSpace(enrollee.RuPatronymic)) table.Cell(index + 2, 2).Range.Text = $"{enrollee.RuSurname.Trim()} {enrollee.RuName[0]}.";
                                else table.Cell(index + 2, 2).Range.Text = $"{enrollee.RuSurname.Trim()} {enrollee.RuName[0]}.{enrollee.RuPatronymic[0]}.";
                                table.Cell(index + 2, 3).Range.Text = $"{enrollee.Speciality.Shortname.Trim()}{enrollee.Speciality.FormOfStudy.Shortname.Trim()}—{enrollee.NumberOfDeal}";
                                //table.Cell(index + 2, 5).Range.Text = assessment.Estimation.Value.ToString();
                                //table.Cell(index + 2, 6).Range.Text = estimationStringService.EstimationAsText(assessment.Estimation.Value).Trim();
                            }
                            index++;
                        }
                        wordDocument.Variables["Секретарь приемной комиссии"].Value = systemConfigurationService.GetSystemConfiguration("SOKR_SEKR").Value;
                        wordDocument.Fields.Update();
                    }
                }

                wordApp.Visible = true;
                wordApp.Activate();
            }
            catch (Exception ex)
            {
                Object saveChanges = Word.WdSaveOptions.wdPromptToSaveChanges;
                Object originalFormat = Word.WdOriginalFormat.wdWordDocument;
                Object routeDocument = Type.Missing;
                ((Word._Application)wordApp).Quit(ref saveChanges, ref originalFormat, ref routeDocument);
                wordApp = null;
            }
            finally
            {
                //  wordApp.Quit(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        // Функция замены текста
        private static void ReplaceText(Word.Application app, string findText, string replacementText)
        {
            Word.Find find = app.Selection.Find;
            find.Text = findText;
            find.Replacement.Text = replacementText;
            Object wrap = Word.WdFindWrap.wdFindContinue;
            Object replace = Word.WdReplace.wdReplaceAll;
            find.Execute(FindText: Type.Missing,
            MatchCase: false,
            MatchWholeWord: false,
            MatchWildcards: false,
            MatchSoundsLike: Type.Missing,
            MatchAllWordForms: false,
            Forward: true,
            Wrap: wrap,
            Format: false,
            ReplaceWith: Type.Missing, Replace: replace);
        }
        // Выписка
        public static void PrintExtract(List<Enrollee> enrollees)
        {
            string path = Environment.CurrentDirectory + "\\Templates\\Выписка.dotx";
            Word.Application wordApp = new Word.Application();
            wordApp.Visible = false;
            try
            {
                Object template = path; // - имя шаблона, по которому создается новый документ
                Object newTemplate = false; // при true новый документ открывается как шаблон.
                Object documentType = Word.WdNewDocumentType.wdNewBlankDocument; // документ Word (по умолчанию)
                Object visible = true;//видимость документа. При true (по умолчанию) документ отображается.
                Word.Document wordDocument = wordApp.Documents.Add(ref template, ref newTemplate, ref documentType, ref visible);
                object oMissing = System.Reflection.Missing.Value;
                SystemConfigurationService systemConfigurationService = new SystemConfigurationService(ConnectionString);
                Word.Selection selection = wordApp.Selection;
                var tableToUse = wordDocument.Tables[1];
                Word.Range range = tableToUse.Range;
                range.Copy();

                int index = 1;
                int count = enrollees.Count;
                foreach(var enrollee in enrollees)
                {
                    // fill template
                    ReplaceText(wordApp, "@@DATE_ORD ", enrollee.Decree.DecreeDate.ToShortDateString().Trim());
                    ReplaceText(wordApp, "@@NUMBER_ORD", enrollee.Decree.DecreeNumber.Trim());
                    ReplaceText(wordApp, "@@DESCRIPTION", enrollee.Decree.Content.Trim());
                    ReplaceText(wordApp, "@@NUMBER_PRT", enrollee.Decree.ProtocolNumber.Trim());
                    ReplaceText(wordApp, "@@DATE_PRT", enrollee.Decree.ProtocolDate.ToShortDateString().Trim());
                    DateTime now = DateTime.Now;
                    ReplaceText(wordApp, "@@CUR_YEAR", now.Year.ToString());
                    string formOfStudy = "";
                    switch (enrollee.Speciality.FormOfStudyId)
                    {
                        case 1:
                            {
                                formOfStudy = "дневной";
                                break;
                            }
                        case 2:
                            {
                                formOfStudy = "дневной сокращенной";
                                break;
                            }
                        case 3:
                            {
                                formOfStudy = "заочной";
                                break;
                            }
                        case 4:
                            {
                                formOfStudy = "заочной дистанционной";
                                break;
                            }
                        case 5:
                            {
                                formOfStudy = "заочной дистанционно-сокращенной";
                                break;
                            }
                        case 6:
                            {
                                formOfStudy = "заочной сокращенной";
                                break;
                            }
                    }
                    ReplaceText(wordApp, "@@FOS", formOfStudy);


                    ReplaceText(wordApp, "@@FAC", enrollee.Speciality.Faculty.Fullname.Trim());
                    ReplaceText(wordApp, "@@CIFER", enrollee.Speciality.Cipher.Trim());
                    ReplaceText(wordApp, "@@SPC", enrollee.Speciality.Fullname.Trim());
                    string fullname = $"";
                    if (string.IsNullOrWhiteSpace(enrollee.RuPatronymic)) fullname = $"{enrollee.RuSurname.Trim()} {enrollee.RuName[0]}.";
                    else fullname = $"{enrollee.RuSurname.Trim()} {enrollee.RuName[0]}. {enrollee.RuPatronymic[0]}.";
                    ReplaceText(wordApp, "@@FIO", fullname);
                    ReplaceText(wordApp, "@@SHORT_REKTOR", systemConfigurationService.GetSystemConfiguration("SHORT_REKTOR").Value.Trim());
                    ReplaceText(wordApp, "@@SOKR_SEKR", systemConfigurationService.GetSystemConfiguration("SOKR_SEKR").Value.Trim());
                    ReplaceText(wordApp, "@@CUR_DATE", now.Date.ToString("dd.MM.yyyy"));

                    if (index < count)
                    {
                        //inserting a page break: first go to end of document
                        selection.EndKey(Word.WdUnits.wdStory, Word.WdMovementType.wdMove);
                        //insert a page break
                        object breakType = Word.WdBreakType.wdPageBreak;
                        selection.InsertBreak(ref breakType);

                        //paste the template table in new page
                        //add a new table (initially with 1 row and one column) at the end of the document
                        Word.Table tableCopy = wordDocument.Tables.Add(selection.Range, 1, 1, ref oMissing, ref oMissing);
                        //paste the original template table over the new one
                        tableCopy.Range.Paste();
                    }
                    index++;
                }               

                wordDocument.Fields.Update();
                wordApp.Visible = true;
                wordApp.Activate();
            }
            catch (Exception ex)
            {
                Object saveChanges = Word.WdSaveOptions.wdPromptToSaveChanges;
                Object originalFormat = Word.WdOriginalFormat.wdWordDocument;
                Object routeDocument = Type.Missing;
                ((Word._Application)wordApp).Quit(ref saveChanges, ref originalFormat, ref routeDocument);
                wordApp = null;
            }
            finally
            {
                //  wordApp.Quit(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        // Мониторинг бюджет
        public static void PrintBudgetMonitoring(List<Enrollee> enrollees)
        {
            SpecialityService specialityService = new SpecialityService(ConnectionString);
            string path = string.Format(Environment.CurrentDirectory + "\\Templates\\Мониторинг (Бюджетная форма).xlsx");
            Excel.Application excelApp = new Excel.Application();
            try
            {
                Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(path, 1, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                Excel.Sheets excelSheets = excelWorkbook.Worksheets;
                Excel.Worksheet excelWorkSheet = (Excel.Worksheet)excelSheets.get_Item("DATA");
                int row = 2;
                enrollees = enrollees.Where(e=>e.FinanceTypeId != 2 && e.StateTypeId == 1)
                    .ToList();
                var specialities = specialityService.GetSpecialities()
                    .Where(s=>s.BudgetCountPlace>0)
                    .OrderBy(s => s.FormOfStudy.Fullname).ThenBy(s => s.Faculty.Fullname).ToList();
                foreach(var speciality in specialities)
                {
                    if(speciality.IsGroup)
                    {
                        var specialitiesInGroup = specialityService.GetSpecialities(speciality);
                        foreach(var specialityInGroup in specialitiesInGroup)
                        {
                            excelWorkSheet.Cells[row, 1] = speciality.Faculty.Fullname.Trim();
                            excelWorkSheet.Cells[row, 2] = speciality.FormOfStudy.Fullname.Trim();
                            excelWorkSheet.Cells[row, 3] = speciality.Fullname.Trim();
                            excelWorkSheet.Cells[row, 4] = speciality.BudgetCountPlace;
                            int countRecord = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId);
                            excelWorkSheet.Cells[row, 5] = countRecord;

                            if (string.IsNullOrWhiteSpace(specialityInGroup.FormOfStudy.Shortname.Trim())) excelWorkSheet.Cells[row, 6] = specialityInGroup.Fullname.Trim();
                            else excelWorkSheet.Cells[row, 6] = $"{specialityInGroup.Fullname.Trim()} -{specialityInGroup.FormOfStudy.Shortname.Trim()}";
                            excelWorkSheet.Cells[row, 7] = specialityInGroup.BudgetCountPlace;
                            excelWorkSheet.Cells[row, 8] = specialityInGroup.TargetCountPlace;
                            int countDealRecord = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId));
                            excelWorkSheet.Cells[row, 9] = countDealRecord;
                            int countTargetDealRecord = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && e.TargetWorkPlaceId.HasValue && e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId));
                            excelWorkSheet.Cells[row, 10] = countTargetDealRecord;
                            int countWithoutExamDealRecord = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && e.ReasonForAddmission.ContestId ==2 && e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId));
                            excelWorkSheet.Cells[row, 11] = countWithoutExamDealRecord;
                            int countOutOfContestDealRecord = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && e.ReasonForAddmission.ContestId == 3 && e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId));
                            excelWorkSheet.Cells[row, 12] = countOutOfContestDealRecord;
                            int countInContestDealRecord = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && e.ReasonForAddmission.ContestId == 1 && e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId));
                            excelWorkSheet.Cells[row, 13] = countInContestDealRecord;

                            #region Получение оценок
                            int more340 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                            //e.ReasonForAddmission.ContestId == 1 && 
                                                          e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                          e.Assessment.Sum(a=>a.Estimation)>340);
                            excelWorkSheet.Cells[row, 14] = more340;

                            int between331to340 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                          e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                          e.Assessment.Sum(a => a.Estimation) >= 331 && e.Assessment.Sum(a => a.Estimation) <= 340);
                            excelWorkSheet.Cells[row, 15] = between331to340;

                            int between321to330 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                          e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                          e.Assessment.Sum(a => a.Estimation) >= 321 && e.Assessment.Sum(a => a.Estimation) <= 330);
                            excelWorkSheet.Cells[row, 16] = between321to330;

                            int between311to320 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                          e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                          e.Assessment.Sum(a => a.Estimation) >= 311 && e.Assessment.Sum(a => a.Estimation) <= 320);
                            excelWorkSheet.Cells[row, 17] = between311to320;

                            int between301to310 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                          e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                          e.Assessment.Sum(a => a.Estimation) >= 301 && e.Assessment.Sum(a => a.Estimation) <= 310);
                            excelWorkSheet.Cells[row, 18] = between301to310;

                            int between291to300 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                          e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                          e.Assessment.Sum(a => a.Estimation) >= 291 && e.Assessment.Sum(a => a.Estimation) <= 300);
                            excelWorkSheet.Cells[row, 19] = between291to300;

                            int between281to290 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                          e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                          e.Assessment.Sum(a => a.Estimation) >= 281 && e.Assessment.Sum(a => a.Estimation) <= 290);
                            excelWorkSheet.Cells[row, 20] = between281to290;

                            int between271to280 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                          e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                          e.Assessment.Sum(a => a.Estimation) >= 271 && e.Assessment.Sum(a => a.Estimation) <= 280);
                            excelWorkSheet.Cells[row, 21] = between271to280;

                            int between261to270 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                          e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                          e.Assessment.Sum(a => a.Estimation) >= 261 && e.Assessment.Sum(a => a.Estimation) <= 270);
                            excelWorkSheet.Cells[row, 22] = between261to270;

                            int between251to260 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                          e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                          e.Assessment.Sum(a => a.Estimation) >= 251 && e.Assessment.Sum(a => a.Estimation) <= 260);
                            excelWorkSheet.Cells[row, 23] = between251to260;

                            int between241to250 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                          e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                          e.Assessment.Sum(a => a.Estimation) >= 241 && e.Assessment.Sum(a => a.Estimation) <= 250);
                            excelWorkSheet.Cells[row, 24] = between241to250;

                            int between231to240 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                          e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                          e.Assessment.Sum(a => a.Estimation) >= 231 && e.Assessment.Sum(a => a.Estimation) <= 240);
                            excelWorkSheet.Cells[row, 25] = between231to240;

                            int between221to230 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                          e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                          e.Assessment.Sum(a => a.Estimation) >= 221 && e.Assessment.Sum(a => a.Estimation) <= 230);
                            excelWorkSheet.Cells[row, 26] = between221to230;

                            int between211to220 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                          e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                          e.Assessment.Sum(a => a.Estimation) >= 211 && e.Assessment.Sum(a => a.Estimation) <= 220);
                            excelWorkSheet.Cells[row, 27] = between211to220;

                            int between201to210 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                          e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                          e.Assessment.Sum(a => a.Estimation) >= 201 && e.Assessment.Sum(a => a.Estimation) <= 210);
                            excelWorkSheet.Cells[row, 28] = between201to210;

                            int between191to200 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                         e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                         e.Assessment.Sum(a => a.Estimation) >= 191 && e.Assessment.Sum(a => a.Estimation) <= 200);
                            excelWorkSheet.Cells[row, 29] = between191to200;

                            int between181to190 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                         e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                         e.Assessment.Sum(a => a.Estimation) >= 181 && e.Assessment.Sum(a => a.Estimation) <= 190);
                            excelWorkSheet.Cells[row, 30] = between181to190;

                            int between171to180 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                        e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                        e.Assessment.Sum(a => a.Estimation) >= 171 && e.Assessment.Sum(a => a.Estimation) <= 180);
                            excelWorkSheet.Cells[row, 31] = between171to180;

                            int between161to170 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                        e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                        e.Assessment.Sum(a => a.Estimation) >= 161 && e.Assessment.Sum(a => a.Estimation) <= 170);
                            excelWorkSheet.Cells[row, 32] = between161to170;

                            int between151to160 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                        e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                        e.Assessment.Sum(a => a.Estimation) >= 151 && e.Assessment.Sum(a => a.Estimation) <= 160);
                            excelWorkSheet.Cells[row, 33] = between151to160;

                            int between141to150 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                        e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                        e.Assessment.Sum(a => a.Estimation) >= 141 && e.Assessment.Sum(a => a.Estimation) <= 150);
                            excelWorkSheet.Cells[row, 34] = between141to150;

                            int between131to140 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                        e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                        e.Assessment.Sum(a => a.Estimation) >= 131 && e.Assessment.Sum(a => a.Estimation) <= 140);
                            excelWorkSheet.Cells[row, 35] = between131to140;

                            int between121to130 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                        e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                        e.Assessment.Sum(a => a.Estimation) >= 121 && e.Assessment.Sum(a => a.Estimation) <= 130);
                            excelWorkSheet.Cells[row, 36] = between121to130;

                            int between111to120 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                        e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                        e.Assessment.Sum(a => a.Estimation) >= 111 && e.Assessment.Sum(a => a.Estimation) <= 120);
                            excelWorkSheet.Cells[row, 37] = between111to120;

                            int between101to110 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                        e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                        e.Assessment.Sum(a => a.Estimation) >= 101 && e.Assessment.Sum(a => a.Estimation) <= 110);
                            excelWorkSheet.Cells[row, 38] = between101to110;

                            int between91to100 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                        e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                        e.Assessment.Sum(a => a.Estimation) >= 91 && e.Assessment.Sum(a => a.Estimation) <= 100);
                            excelWorkSheet.Cells[row, 39] = between91to100;

                            int between81to90 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                        e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                        e.Assessment.Sum(a => a.Estimation) >= 81 && e.Assessment.Sum(a => a.Estimation) <= 90);
                            excelWorkSheet.Cells[row, 40] = between81to90;

                            int between71to80 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                       e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                       e.Assessment.Sum(a => a.Estimation) >= 71 && e.Assessment.Sum(a => a.Estimation) <= 80);
                            excelWorkSheet.Cells[row, 41] = between71to80;

                            int between61to70 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                       e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                       e.Assessment.Sum(a => a.Estimation) >= 61 && e.Assessment.Sum(a => a.Estimation) <= 70);
                            excelWorkSheet.Cells[row, 42] = between61to70;

                            int between51to60 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 51 && e.Assessment.Sum(a => a.Estimation) <= 60);
                            excelWorkSheet.Cells[row, 43] = between51to60;

                            int between41to50 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 41 && e.Assessment.Sum(a => a.Estimation) <= 50);
                            excelWorkSheet.Cells[row, 44] = between41to50;

                            int less41 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) < 41);
                            excelWorkSheet.Cells[row, 45] = less41;
                            #endregion
                            row++;
                        }
                    }
                    else
                    {
                        excelWorkSheet.Cells[row, 1] = speciality.Faculty.Fullname.Trim();
                        excelWorkSheet.Cells[row, 2] = speciality.FormOfStudy.Fullname.Trim();
                        excelWorkSheet.Cells[row, 3] = speciality.Fullname.Trim();
                        excelWorkSheet.Cells[row, 4] = speciality.BudgetCountPlace;
                        int countRecord = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId);
                        excelWorkSheet.Cells[row, 5] = countRecord;
                        excelWorkSheet.Cells[row, 6] = speciality.Fullname.Trim();
                        excelWorkSheet.Cells[row, 7] = speciality.BudgetCountPlace;
                        excelWorkSheet.Cells[row, 8] = speciality.TargetCountPlace;
                        int countDealRecord = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId));
                        excelWorkSheet.Cells[row, 9] = countDealRecord;
                        int countTargetDealRecord = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && e.TargetWorkPlaceId.HasValue && e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId));
                        excelWorkSheet.Cells[row, 10] = countTargetDealRecord;
                        int countWithoutExamDealRecord = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && e.ReasonForAddmission.ContestId == 2 && e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId));
                        excelWorkSheet.Cells[row, 11] = countWithoutExamDealRecord;
                        int countOutOfContestDealRecord = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && e.ReasonForAddmission.ContestId == 3 && e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId));
                        excelWorkSheet.Cells[row, 12] = countOutOfContestDealRecord;
                        int countInContestDealRecord = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && e.ReasonForAddmission.ContestId == 1 && e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId));
                        excelWorkSheet.Cells[row, 13] = countInContestDealRecord;

                        #region Получение оценок
                        int more340 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 340);
                        excelWorkSheet.Cells[row, 14] = more340;

                        int between331to340 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 331 && e.Assessment.Sum(a => a.Estimation) <= 340);
                        excelWorkSheet.Cells[row, 15] = between331to340;

                        int between321to330 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 321 && e.Assessment.Sum(a => a.Estimation) <= 330);
                        excelWorkSheet.Cells[row, 16] = between321to330;

                        int between311to320 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 311 && e.Assessment.Sum(a => a.Estimation) <= 320);
                        excelWorkSheet.Cells[row, 17] = between311to320;

                        int between301to310 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 301 && e.Assessment.Sum(a => a.Estimation) <= 310);
                        excelWorkSheet.Cells[row, 18] = between301to310;

                        int between291to300 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 291 && e.Assessment.Sum(a => a.Estimation) <= 300);
                        excelWorkSheet.Cells[row, 19] = between291to300;

                        int between281to290 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 281 && e.Assessment.Sum(a => a.Estimation) <= 290);
                        excelWorkSheet.Cells[row, 20] = between281to290;

                        int between271to280 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 271 && e.Assessment.Sum(a => a.Estimation) <= 280);
                        excelWorkSheet.Cells[row, 21] = between271to280;

                        int between261to270 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 261 && e.Assessment.Sum(a => a.Estimation) <= 270);
                        excelWorkSheet.Cells[row, 22] = between261to270;

                        int between251to260 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 251 && e.Assessment.Sum(a => a.Estimation) <= 260);
                        excelWorkSheet.Cells[row, 23] = between251to260;

                        int between241to250 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 241 && e.Assessment.Sum(a => a.Estimation) <= 250);
                        excelWorkSheet.Cells[row, 24] = between241to250;

                        int between231to240 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 231 && e.Assessment.Sum(a => a.Estimation) <= 240);
                        excelWorkSheet.Cells[row, 25] = between231to240;

                        int between221to230 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 221 && e.Assessment.Sum(a => a.Estimation) <= 230);
                        excelWorkSheet.Cells[row, 26] = between221to230;

                        int between211to220 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 211 && e.Assessment.Sum(a => a.Estimation) <= 220);
                        excelWorkSheet.Cells[row, 27] = between211to220;

                        int between201to210 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 201 && e.Assessment.Sum(a => a.Estimation) <= 210);
                        excelWorkSheet.Cells[row, 28] = between201to210;

                        int between191to200 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                     e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                     e.Assessment.Sum(a => a.Estimation) >= 191 && e.Assessment.Sum(a => a.Estimation) <= 200);
                        excelWorkSheet.Cells[row, 29] = between191to200;

                        int between181to190 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                     e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                     e.Assessment.Sum(a => a.Estimation) >= 181 && e.Assessment.Sum(a => a.Estimation) <= 190);
                        excelWorkSheet.Cells[row, 30] = between181to190;

                        int between171to180 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                    e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                    e.Assessment.Sum(a => a.Estimation) >= 171 && e.Assessment.Sum(a => a.Estimation) <= 180);
                        excelWorkSheet.Cells[row, 31] = between171to180;

                        int between161to170 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                    e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                    e.Assessment.Sum(a => a.Estimation) >= 161 && e.Assessment.Sum(a => a.Estimation) <= 170);
                        excelWorkSheet.Cells[row, 32] = between161to170;

                        int between151to160 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                    e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                    e.Assessment.Sum(a => a.Estimation) >= 151 && e.Assessment.Sum(a => a.Estimation) <= 160);
                        excelWorkSheet.Cells[row, 33] = between151to160;

                        int between141to150 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                    e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                    e.Assessment.Sum(a => a.Estimation) >= 141 && e.Assessment.Sum(a => a.Estimation) <= 150);
                        excelWorkSheet.Cells[row, 34] = between141to150;

                        int between131to140 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                    e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                    e.Assessment.Sum(a => a.Estimation) >= 131 && e.Assessment.Sum(a => a.Estimation) <= 140);
                        excelWorkSheet.Cells[row, 35] = between131to140;

                        int between121to130 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                    e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                    e.Assessment.Sum(a => a.Estimation) >= 121 && e.Assessment.Sum(a => a.Estimation) <= 130);
                        excelWorkSheet.Cells[row, 36] = between121to130;

                        int between111to120 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                    e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                    e.Assessment.Sum(a => a.Estimation) >= 111 && e.Assessment.Sum(a => a.Estimation) <= 120);
                        excelWorkSheet.Cells[row, 37] = between111to120;

                        int between101to110 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                    e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                    e.Assessment.Sum(a => a.Estimation) >= 101 && e.Assessment.Sum(a => a.Estimation) <= 110);
                        excelWorkSheet.Cells[row, 38] = between101to110;

                        int between91to100 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                    e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                    e.Assessment.Sum(a => a.Estimation) >= 91 && e.Assessment.Sum(a => a.Estimation) <= 100);
                        excelWorkSheet.Cells[row, 39] = between91to100;

                        int between81to90 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                    e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                    e.Assessment.Sum(a => a.Estimation) >= 81 && e.Assessment.Sum(a => a.Estimation) <= 90);
                        excelWorkSheet.Cells[row, 40] = between81to90;

                        int between71to80 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                   e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                   e.Assessment.Sum(a => a.Estimation) >= 71 && e.Assessment.Sum(a => a.Estimation) <= 80);
                        excelWorkSheet.Cells[row, 41] = between71to80;

                        int between61to70 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                   e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                   e.Assessment.Sum(a => a.Estimation) >= 61 && e.Assessment.Sum(a => a.Estimation) <= 70);
                        excelWorkSheet.Cells[row, 42] = between61to70;

                        int between51to60 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                  e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                  e.Assessment.Sum(a => a.Estimation) >= 51 && e.Assessment.Sum(a => a.Estimation) <= 60);
                        excelWorkSheet.Cells[row, 43] = between51to60;

                        int between41to50 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                  e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                  e.Assessment.Sum(a => a.Estimation) >= 41 && e.Assessment.Sum(a => a.Estimation) <= 50);
                        excelWorkSheet.Cells[row, 44] = between41to50;

                        int less41 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                  e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                  e.Assessment.Sum(a => a.Estimation) < 41);
                        excelWorkSheet.Cells[row, 45] = less41;
                        #endregion
                        row++;
                    }                                                         
                }
                excelApp.Visible = true;
            }
            catch (Exception ex)
            {
                ((Excel._Application)excelApp).Quit();
                excelApp = null;
            }
            finally
            {
                //  wordApp.Quit(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        // Мониторинг платное
        public static void PrintFeeMonitoring(List<Enrollee> enrollees)
        {
            SpecialityService specialityService = new SpecialityService(ConnectionString);
            string path = string.Format(Environment.CurrentDirectory + "\\Templates\\Мониторинг (Платная форма).xlsx");
            Excel.Application excelApp = new Excel.Application();
            try
            {
                Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(path, 1, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                Excel.Sheets excelSheets = excelWorkbook.Worksheets;
                Excel.Worksheet excelWorkSheet = (Excel.Worksheet)excelSheets.get_Item("DATA");
                int row = 2;
                enrollees = enrollees.Where(e => e.FinanceTypeId == 2 && e.StateTypeId == 1)
                    .ToList();
                var specialities = specialityService.GetSpecialities()
                    .Where(s => s.FeeCountPlace > 0)
                    .OrderBy(s => s.FormOfStudy.Fullname).ThenBy(s => s.Faculty.Fullname).ToList();
                foreach (var speciality in specialities)
                {
                    if (speciality.IsGroup)
                    {
                        var specialitiesInGroup = specialityService.GetSpecialities(speciality);
                        foreach (var specialityInGroup in specialitiesInGroup)
                        {
                            excelWorkSheet.Cells[row, 1] = speciality.Faculty.Fullname.Trim();
                            excelWorkSheet.Cells[row, 2] = speciality.FormOfStudy.Fullname.Trim();
                            excelWorkSheet.Cells[row, 3] = speciality.Fullname.Trim();
                            excelWorkSheet.Cells[row, 4] = speciality.FeeCountPlace;
                            int countRecord = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId);
                            excelWorkSheet.Cells[row, 5] = countRecord;

                            if (string.IsNullOrWhiteSpace(specialityInGroup.FormOfStudy.Shortname.Trim())) excelWorkSheet.Cells[row, 6] = specialityInGroup.Fullname.Trim();
                            else excelWorkSheet.Cells[row, 6] = $"{specialityInGroup.Fullname.Trim()} -{specialityInGroup.FormOfStudy.Shortname.Trim()}";
                            excelWorkSheet.Cells[row, 7] = specialityInGroup.FeeCountPlace;
                            excelWorkSheet.Cells[row, 8] = specialityInGroup.TargetCountPlace;
                            int countDealRecord = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId));
                            excelWorkSheet.Cells[row, 9] = countDealRecord;
                            int countTargetDealRecord = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && e.TargetWorkPlaceId.HasValue && e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId));
                            excelWorkSheet.Cells[row, 10] = countTargetDealRecord;
                            int countWithoutExamDealRecord = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && e.ReasonForAddmission.ContestId == 2 && e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId));
                            excelWorkSheet.Cells[row, 11] = countWithoutExamDealRecord;
                            int countOutOfContestDealRecord = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && e.ReasonForAddmission.ContestId == 3 && e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId));
                            excelWorkSheet.Cells[row, 12] = countOutOfContestDealRecord;
                            int countInContestDealRecord = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && e.ReasonForAddmission.ContestId == 1 && e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId));
                            excelWorkSheet.Cells[row, 13] = countInContestDealRecord;

                            #region Получение оценок
                            int more340 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                          e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                          e.Assessment.Sum(a => a.Estimation) > 340);
                            excelWorkSheet.Cells[row, 14] = more340;

                            int between331to340 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                          e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                          e.Assessment.Sum(a => a.Estimation) >= 331 && e.Assessment.Sum(a => a.Estimation) <= 340);
                            excelWorkSheet.Cells[row, 15] = between331to340;

                            int between321to330 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                          e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                          e.Assessment.Sum(a => a.Estimation) >= 321 && e.Assessment.Sum(a => a.Estimation) <= 330);
                            excelWorkSheet.Cells[row, 16] = between321to330;

                            int between311to320 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                          e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                          e.Assessment.Sum(a => a.Estimation) >= 311 && e.Assessment.Sum(a => a.Estimation) <= 320);
                            excelWorkSheet.Cells[row, 17] = between311to320;

                            int between301to310 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                          e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                          e.Assessment.Sum(a => a.Estimation) >= 301 && e.Assessment.Sum(a => a.Estimation) <= 310);
                            excelWorkSheet.Cells[row, 18] = between301to310;

                            int between291to300 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                          e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                          e.Assessment.Sum(a => a.Estimation) >= 291 && e.Assessment.Sum(a => a.Estimation) <= 300);
                            excelWorkSheet.Cells[row, 19] = between291to300;

                            int between281to290 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                          e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                          e.Assessment.Sum(a => a.Estimation) >= 281 && e.Assessment.Sum(a => a.Estimation) <= 290);
                            excelWorkSheet.Cells[row, 20] = between281to290;

                            int between271to280 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                          e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                          e.Assessment.Sum(a => a.Estimation) >= 271 && e.Assessment.Sum(a => a.Estimation) <= 280);
                            excelWorkSheet.Cells[row, 21] = between271to280;

                            int between261to270 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                          e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                          e.Assessment.Sum(a => a.Estimation) >= 261 && e.Assessment.Sum(a => a.Estimation) <= 270);
                            excelWorkSheet.Cells[row, 22] = between261to270;

                            int between251to260 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                          e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                          e.Assessment.Sum(a => a.Estimation) >= 251 && e.Assessment.Sum(a => a.Estimation) <= 260);
                            excelWorkSheet.Cells[row, 23] = between251to260;

                            int between241to250 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                          e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                          e.Assessment.Sum(a => a.Estimation) >= 241 && e.Assessment.Sum(a => a.Estimation) <= 250);
                            excelWorkSheet.Cells[row, 24] = between241to250;

                            int between231to240 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                          e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                          e.Assessment.Sum(a => a.Estimation) >= 231 && e.Assessment.Sum(a => a.Estimation) <= 240);
                            excelWorkSheet.Cells[row, 25] = between231to240;

                            int between221to230 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && e.ReasonForAddmission.ContestId == 1 &&
                                                          e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                          e.Assessment.Sum(a => a.Estimation) >= 221 && e.Assessment.Sum(a => a.Estimation) <= 230);
                            excelWorkSheet.Cells[row, 26] = between221to230;

                            int between211to220 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                          e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                          e.Assessment.Sum(a => a.Estimation) >= 211 && e.Assessment.Sum(a => a.Estimation) <= 220);
                            excelWorkSheet.Cells[row, 27] = between211to220;

                            int between201to210 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                          e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                          e.Assessment.Sum(a => a.Estimation) >= 201 && e.Assessment.Sum(a => a.Estimation) <= 210);
                            excelWorkSheet.Cells[row, 28] = between201to210;

                            int between191to200 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                         e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                         e.Assessment.Sum(a => a.Estimation) >= 191 && e.Assessment.Sum(a => a.Estimation) <= 200);
                            excelWorkSheet.Cells[row, 29] = between191to200;

                            int between181to190 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                         e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                         e.Assessment.Sum(a => a.Estimation) >= 181 && e.Assessment.Sum(a => a.Estimation) <= 190);
                            excelWorkSheet.Cells[row, 30] = between181to190;

                            int between171to180 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                        e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                        e.Assessment.Sum(a => a.Estimation) >= 171 && e.Assessment.Sum(a => a.Estimation) <= 180);
                            excelWorkSheet.Cells[row, 31] = between171to180;

                            int between161to170 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                        e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                        e.Assessment.Sum(a => a.Estimation) >= 161 && e.Assessment.Sum(a => a.Estimation) <= 170);
                            excelWorkSheet.Cells[row, 32] = between161to170;

                            int between151to160 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                        e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                        e.Assessment.Sum(a => a.Estimation) >= 151 && e.Assessment.Sum(a => a.Estimation) <= 160);
                            excelWorkSheet.Cells[row, 33] = between151to160;

                            int between141to150 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                        e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                        e.Assessment.Sum(a => a.Estimation) >= 141 && e.Assessment.Sum(a => a.Estimation) <= 150);
                            excelWorkSheet.Cells[row, 34] = between141to150;

                            int between131to140 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                        e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                        e.Assessment.Sum(a => a.Estimation) >= 131 && e.Assessment.Sum(a => a.Estimation) <= 140);
                            excelWorkSheet.Cells[row, 35] = between131to140;

                            int between121to130 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                        e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                        e.Assessment.Sum(a => a.Estimation) >= 121 && e.Assessment.Sum(a => a.Estimation) <= 130);
                            excelWorkSheet.Cells[row, 36] = between121to130;

                            int between111to120 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                        e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                        e.Assessment.Sum(a => a.Estimation) >= 111 && e.Assessment.Sum(a => a.Estimation) <= 120);
                            excelWorkSheet.Cells[row, 37] = between111to120;

                            int between101to110 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                        e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                        e.Assessment.Sum(a => a.Estimation) >= 101 && e.Assessment.Sum(a => a.Estimation) <= 110);
                            excelWorkSheet.Cells[row, 38] = between101to110;

                            int between91to100 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                        e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                        e.Assessment.Sum(a => a.Estimation) >= 91 && e.Assessment.Sum(a => a.Estimation) <= 100);
                            excelWorkSheet.Cells[row, 39] = between91to100;

                            int between81to90 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                        e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                        e.Assessment.Sum(a => a.Estimation) >= 81 && e.Assessment.Sum(a => a.Estimation) <= 90);
                            excelWorkSheet.Cells[row, 40] = between81to90;

                            int between71to80 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                       e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                       e.Assessment.Sum(a => a.Estimation) >= 71 && e.Assessment.Sum(a => a.Estimation) <= 80);
                            excelWorkSheet.Cells[row, 41] = between71to80;

                            int between61to70 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                       e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                       e.Assessment.Sum(a => a.Estimation) >= 61 && e.Assessment.Sum(a => a.Estimation) <= 70);
                            excelWorkSheet.Cells[row, 42] = between61to70;

                            int between51to60 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 51 && e.Assessment.Sum(a => a.Estimation) <= 60);
                            excelWorkSheet.Cells[row, 43] = between51to60;

                            int between41to50 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 41 && e.Assessment.Sum(a => a.Estimation) <= 50);
                            excelWorkSheet.Cells[row, 44] = between41to50;

                            int less41 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                            //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == specialityInGroup.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) < 41);
                            excelWorkSheet.Cells[row, 45] = less41;
                            #endregion
                            row++;
                        }
                    }
                    else
                    {
                        excelWorkSheet.Cells[row, 1] = speciality.Faculty.Fullname.Trim();
                        excelWorkSheet.Cells[row, 2] = speciality.FormOfStudy.Fullname.Trim();
                        excelWorkSheet.Cells[row, 3] = speciality.Fullname.Trim();
                        excelWorkSheet.Cells[row, 4] = speciality.FeeCountPlace;
                        int countRecord = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId);
                        excelWorkSheet.Cells[row, 5] = countRecord;
                        excelWorkSheet.Cells[row, 6] = speciality.Fullname.Trim();
                        excelWorkSheet.Cells[row, 7] = speciality.FeeCountPlace;
                        excelWorkSheet.Cells[row, 8] = speciality.TargetCountPlace;
                        int countDealRecord = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId));
                        excelWorkSheet.Cells[row, 9] = countDealRecord;
                        int countTargetDealRecord = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && e.TargetWorkPlaceId.HasValue && e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId));
                        excelWorkSheet.Cells[row, 10] = countTargetDealRecord;
                        int countWithoutExamDealRecord = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && e.ReasonForAddmission.ContestId == 2 && e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId));
                        excelWorkSheet.Cells[row, 11] = countWithoutExamDealRecord;
                        int countOutOfContestDealRecord = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && e.ReasonForAddmission.ContestId == 3 && e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId));
                        excelWorkSheet.Cells[row, 12] = countOutOfContestDealRecord;
                        int countInContestDealRecord = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && e.ReasonForAddmission.ContestId == 1 && e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId));
                        excelWorkSheet.Cells[row, 13] = countInContestDealRecord;

                        #region Получение оценок
                        int more340 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) > 340);
                        excelWorkSheet.Cells[row, 14] = more340;

                        int between331to340 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 331 && e.Assessment.Sum(a => a.Estimation) <= 340);
                        excelWorkSheet.Cells[row, 15] = between331to340;

                        int between321to330 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 321 && e.Assessment.Sum(a => a.Estimation) <= 330);
                        excelWorkSheet.Cells[row, 16] = between321to330;

                        int between311to320 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 311 && e.Assessment.Sum(a => a.Estimation) <= 320);
                        excelWorkSheet.Cells[row, 17] = between311to320;

                        int between301to310 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 301 && e.Assessment.Sum(a => a.Estimation) <= 310);
                        excelWorkSheet.Cells[row, 18] = between301to310;

                        int between291to300 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 291 && e.Assessment.Sum(a => a.Estimation) <= 300);
                        excelWorkSheet.Cells[row, 19] = between291to300;

                        int between281to290 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 281 && e.Assessment.Sum(a => a.Estimation) <= 290);
                        excelWorkSheet.Cells[row, 20] = between281to290;

                        int between271to280 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 271 && e.Assessment.Sum(a => a.Estimation) <= 280);
                        excelWorkSheet.Cells[row, 21] = between271to280;

                        int between261to270 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 261 && e.Assessment.Sum(a => a.Estimation) <= 270);
                        excelWorkSheet.Cells[row, 22] = between261to270;

                        int between251to260 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 251 && e.Assessment.Sum(a => a.Estimation) <= 260);
                        excelWorkSheet.Cells[row, 23] = between251to260;

                        int between241to250 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 241 && e.Assessment.Sum(a => a.Estimation) <= 250);
                        excelWorkSheet.Cells[row, 24] = between241to250;

                        int between231to240 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 231 && e.Assessment.Sum(a => a.Estimation) <= 240);
                        excelWorkSheet.Cells[row, 25] = between231to240;

                        int between221to230 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 221 && e.Assessment.Sum(a => a.Estimation) <= 230);
                        excelWorkSheet.Cells[row, 26] = between221to230;

                        int between211to220 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 211 && e.Assessment.Sum(a => a.Estimation) <= 220);
                        excelWorkSheet.Cells[row, 27] = between211to220;

                        int between201to210 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                      e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                      e.Assessment.Sum(a => a.Estimation) >= 201 && e.Assessment.Sum(a => a.Estimation) <= 210);
                        excelWorkSheet.Cells[row, 28] = between201to210;

                        int between191to200 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                     e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                     e.Assessment.Sum(a => a.Estimation) >= 191 && e.Assessment.Sum(a => a.Estimation) <= 200);
                        excelWorkSheet.Cells[row, 29] = between191to200;

                        int between181to190 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                     e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                     e.Assessment.Sum(a => a.Estimation) >= 181 && e.Assessment.Sum(a => a.Estimation) <= 190);
                        excelWorkSheet.Cells[row, 30] = between181to190;

                        int between171to180 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                    e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                    e.Assessment.Sum(a => a.Estimation) >= 171 && e.Assessment.Sum(a => a.Estimation) <= 180);
                        excelWorkSheet.Cells[row, 31] = between171to180;

                        int between161to170 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                    e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                    e.Assessment.Sum(a => a.Estimation) >= 161 && e.Assessment.Sum(a => a.Estimation) <= 170);
                        excelWorkSheet.Cells[row, 32] = between161to170;

                        int between151to160 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                    e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                    e.Assessment.Sum(a => a.Estimation) >= 151 && e.Assessment.Sum(a => a.Estimation) <= 160);
                        excelWorkSheet.Cells[row, 33] = between151to160;

                        int between141to150 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                    e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                    e.Assessment.Sum(a => a.Estimation) >= 141 && e.Assessment.Sum(a => a.Estimation) <= 150);
                        excelWorkSheet.Cells[row, 34] = between141to150;

                        int between131to140 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                    e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                    e.Assessment.Sum(a => a.Estimation) >= 131 && e.Assessment.Sum(a => a.Estimation) <= 140);
                        excelWorkSheet.Cells[row, 35] = between131to140;

                        int between121to130 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                    e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                    e.Assessment.Sum(a => a.Estimation) >= 121 && e.Assessment.Sum(a => a.Estimation) <= 130);
                        excelWorkSheet.Cells[row, 36] = between121to130;

                        int between111to120 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                    e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                    e.Assessment.Sum(a => a.Estimation) >= 111 && e.Assessment.Sum(a => a.Estimation) <= 120);
                        excelWorkSheet.Cells[row, 37] = between111to120;

                        int between101to110 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                    e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                    e.Assessment.Sum(a => a.Estimation) >= 101 && e.Assessment.Sum(a => a.Estimation) <= 110);
                        excelWorkSheet.Cells[row, 38] = between101to110;

                        int between91to100 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                    e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                    e.Assessment.Sum(a => a.Estimation) >= 91 && e.Assessment.Sum(a => a.Estimation) <= 100);
                        excelWorkSheet.Cells[row, 39] = between91to100;

                        int between81to90 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                    e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                    e.Assessment.Sum(a => a.Estimation) >= 81 && e.Assessment.Sum(a => a.Estimation) <= 90);
                        excelWorkSheet.Cells[row, 40] = between81to90;

                        int between71to80 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                   e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                   e.Assessment.Sum(a => a.Estimation) >= 71 && e.Assessment.Sum(a => a.Estimation) <= 80);
                        excelWorkSheet.Cells[row, 41] = between71to80;

                        int between61to70 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                   e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                   e.Assessment.Sum(a => a.Estimation) >= 61 && e.Assessment.Sum(a => a.Estimation) <= 70);
                        excelWorkSheet.Cells[row, 42] = between61to70;

                        int between51to60 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                  e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                  e.Assessment.Sum(a => a.Estimation) >= 51 && e.Assessment.Sum(a => a.Estimation) <= 60);
                        excelWorkSheet.Cells[row, 43] = between51to60;

                        int between41to50 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                  e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                  e.Assessment.Sum(a => a.Estimation) >= 41 && e.Assessment.Sum(a => a.Estimation) <= 50);
                        excelWorkSheet.Cells[row, 44] = between41to50;

                        int less41 = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                        //e.ReasonForAddmission.ContestId == 1 &&
                                                  e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && p.SpecialityId == speciality.SpecialityId) &&
                                                  e.Assessment.Sum(a => a.Estimation) < 41);
                        excelWorkSheet.Cells[row, 45] = less41;
                        #endregion
                        row++;
                    }
                }
                excelApp.Visible = true;
            }
            catch (Exception ex)
            {
                ((Excel._Application)excelApp).Quit();
                excelApp = null;
            }
            finally
            {
                //  wordApp.Quit(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        // Информация о ходе приема
        public static void PrintInformationReport(List<Enrollee> enrollees)
        {
            SpecialityService specialityService = new SpecialityService(ConnectionString);
            string path = string.Format(Environment.CurrentDirectory + "\\Templates\\Информация о ходе приема.xlsx");
            Excel.Application excelApp = new Excel.Application();
            try
            {
                Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(path, 1, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                Excel.Sheets excelSheets = excelWorkbook.Worksheets;
                Excel.Worksheet excelWorkSheet = (Excel.Worksheet)excelSheets.get_Item("DATA");
                int row = 2;
                enrollees = enrollees.Where(e => e.StateTypeId == 1).ToList();
                var specialities = specialityService.GetSpecialities()
                    .Where(s => s.IsAlternative == false)
                    .OrderBy(s => s.FormOfStudy.Fullname).ThenBy(s => s.Faculty.Fullname).ToList();
                foreach (var speciality in specialities)
                {
                    if (speciality.IsGroup)
                    {
                        excelWorkSheet.Cells[row, 1] = speciality.Faculty.Fullname.Trim();
                        excelWorkSheet.Cells[row, 2] = speciality.Faculty.Shortname.Trim();
                        excelWorkSheet.Cells[row, 3] = speciality.FormOfStudy.Fullname.Trim();
                        excelWorkSheet.Cells[row, 4] = speciality.FormOfStudy.Shortname.Trim();
                        excelWorkSheet.Cells[row, 5] = speciality.Cipher.Trim();
                        excelWorkSheet.Cells[row, 6] = speciality.Fullname.Trim();

                        excelWorkSheet.Cells[row, 7] = speciality.BudgetCountPlace;
                        excelWorkSheet.Cells[row, 8] = speciality.FeeCountPlace;

                        int countRecordBudget = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                                                                e.FinanceTypeId != 2);
                        excelWorkSheet.Cells[row, 9] = countRecordBudget;

                        int countRecordFee = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                                                                e.FinanceTypeId == 2);
                        excelWorkSheet.Cells[row, 10] = countRecordFee;

                        int countRecordBudgetToday = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                                                                e.FinanceTypeId != 2 &&
                                                                e.DateDeal.Date == DateTime.Now.Date &&
                                                                e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 &&
                                                                p.SpecialityId == speciality.SpecialityId));
                        excelWorkSheet.Cells[row, 11] = countRecordBudgetToday;

                        int countRecordFeeToday = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                                                                e.FinanceTypeId == 2 &&
                                                                e.DateDeal.Date == DateTime.Now.Date &&
                                                                e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 &&
                                                                p.SpecialityId == speciality.SpecialityId));
                        excelWorkSheet.Cells[row, 12] = countRecordFeeToday;
                        row++;

                        var specialitiesInGroup = specialityService.GetSpecialities(speciality);
                        foreach (var specialityInGroup in specialitiesInGroup)
                        {
                            excelWorkSheet.Cells[row, 1] = specialityInGroup.Faculty.Fullname.Trim();
                            excelWorkSheet.Cells[row, 2] = specialityInGroup.Faculty.Shortname.Trim();
                            excelWorkSheet.Cells[row, 3] = specialityInGroup.FormOfStudy.Fullname.Trim();
                            excelWorkSheet.Cells[row, 4] = specialityInGroup.FormOfStudy.Shortname.Trim();
                            excelWorkSheet.Cells[row, 5] = specialityInGroup.Cipher.Trim();
                            excelWorkSheet.Cells[row, 6] = specialityInGroup.Fullname.Trim();

                            excelWorkSheet.Cells[row, 7] = specialityInGroup.BudgetCountPlace;
                            excelWorkSheet.Cells[row, 8] = specialityInGroup.FeeCountPlace;

                            countRecordBudget = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId && 
                                                                    e.FinanceTypeId !=2 &&
                                                                    e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 && 
                                                                    p.SpecialityId == specialityInGroup.SpecialityId));
                            excelWorkSheet.Cells[row, 9] = countRecordBudget;

                            countRecordFee = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                                                                    e.FinanceTypeId == 2 &&
                                                                    e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 &&
                                                                    p.SpecialityId == specialityInGroup.SpecialityId));
                            excelWorkSheet.Cells[row, 10] = countRecordFee;

                            countRecordBudgetToday = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                                                                    e.FinanceTypeId != 2 &&
                                                                    e.DateDeal.Date == DateTime.Now.Date &&
                                                                    e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 &&
                                                                    p.SpecialityId == specialityInGroup.SpecialityId));
                            excelWorkSheet.Cells[row, 11] = countRecordBudgetToday;

                            countRecordFeeToday = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                                                                    e.FinanceTypeId == 2 &&
                                                                    e.DateDeal.Date == DateTime.Now.Date &&
                                                                    e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 &&
                                                                    p.SpecialityId == specialityInGroup.SpecialityId));
                            excelWorkSheet.Cells[row, 12] = countRecordFeeToday;
                            row++;
                        }
                    }
                    else
                    {
                        excelWorkSheet.Cells[row, 1] = speciality.Faculty.Fullname.Trim();
                        excelWorkSheet.Cells[row, 2] = speciality.Faculty.Shortname.Trim();
                        excelWorkSheet.Cells[row, 3] = speciality.FormOfStudy.Fullname.Trim();
                        excelWorkSheet.Cells[row, 4] = speciality.FormOfStudy.Shortname.Trim();
                        excelWorkSheet.Cells[row, 5] = speciality.Cipher.Trim();
                        excelWorkSheet.Cells[row, 6] = speciality.Fullname.Trim();

                        excelWorkSheet.Cells[row, 7] = speciality.BudgetCountPlace;
                        excelWorkSheet.Cells[row, 8] = speciality.FeeCountPlace;

                        int countRecordBudget = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                                                                e.FinanceTypeId != 2 &&
                                                                e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 &&
                                                                p.SpecialityId == speciality.SpecialityId));
                        excelWorkSheet.Cells[row, 9] = countRecordBudget;

                        int countRecordFee = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                                                                e.FinanceTypeId == 2 &&
                                                                e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 &&
                                                                p.SpecialityId == speciality.SpecialityId));
                        excelWorkSheet.Cells[row, 10] = countRecordFee;

                        int countRecordBudgetToday = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                                                                e.FinanceTypeId != 2 &&
                                                                e.DateDeal.Date == DateTime.Now.Date &&
                                                                e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 &&
                                                                p.SpecialityId == speciality.SpecialityId));
                        excelWorkSheet.Cells[row, 11] = countRecordBudgetToday;

                        int countRecordFeeToday = enrollees.Count(e => e.SpecialityId == speciality.SpecialityId &&
                                                                e.FinanceTypeId == 2 &&
                                                                e.DateDeal.Date == DateTime.Now.Date &&
                                                                e.PriorityOfSpeciality.Any(p => p.PriorityLevel == 1 &&
                                                                p.SpecialityId == speciality.SpecialityId));
                        excelWorkSheet.Cells[row, 12] = countRecordFeeToday;
                        row++;
                    }
                }
                excelApp.Visible = true;
            }
            catch (Exception ex)
            {
                ((Excel._Application)excelApp).Quit();
                excelApp = null;
            }
            finally
            {
                //  wordApp.Quit(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

    }

}


