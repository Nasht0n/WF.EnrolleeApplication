using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using WF.EnrolleeApplication.DataAccess.Services;

namespace WF.EnrolleeApplication.App.Services
{
    public class ReportManager
    {
        public static string ConnectionString;

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
                wordDocument.Variables["Район"].Value = enrollee.District.Name;
                wordDocument.Variables["Индекс"].Value = enrollee.SettlementIndex.ToString();
                wordDocument.Variables["Город"].Value = $"{enrollee.TypeOfSettlement.Shortname.Trim()} {enrollee.SettlementName.Trim()}";
                wordDocument.Variables["Улица"].Value = enrollee.StreetName.Trim();
                wordDocument.Variables["Номер дома"].Value = $"д. {enrollee.NumberHouse}";
                wordDocument.Variables["Номер квартиры"].Value = $"кв. {enrollee.NumberFlat}";
                wordDocument.Variables["Домашний телефон"].Value = $"{enrollee.HomePhone} (дом.)";
                wordDocument.Variables["Мобильный телефон"].Value = $"{enrollee.MobilePhone} (моб.)";
                wordDocument.Variables["Последнее УО"].Value = $"{enrollee.SchoolYear.Trim()}, {enrollee.SchoolName} г. {enrollee.SchoolAddress}";
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

                wordDocument.Variables["ФИО матери"].Value =$" {enrollee.MotherFullname} ";
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
                wordDocument.Variables["Район"].Value = enrollee.District.Name;
                wordDocument.Variables["Индекс"].Value = enrollee.SettlementIndex.ToString();
                wordDocument.Variables["Город"].Value = $"{enrollee.TypeOfSettlement.Shortname.Trim()} {enrollee.SettlementName.Trim()}";
                wordDocument.Variables["Улица"].Value = enrollee.StreetName.Trim();
                wordDocument.Variables["Номер дома"].Value = $"д. {enrollee.NumberHouse.Trim()}";
                wordDocument.Variables["Номер квартиры"].Value = $"кв. {enrollee.NumberFlat.Trim()}";
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
                        table.Cell(index + 2, 2).Range.Text = assessment.Discipline.Name.Trim();
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
                wordDocument.Variables["Район"].Value = enrollee.District.Name.Trim();
                wordDocument.Variables["Город"].Value = $"{enrollee.TypeOfSettlement.Shortname.Trim()} {enrollee.SettlementName.Trim()}";
                wordDocument.Variables["Улица"].Value = enrollee.StreetName.Trim();
                wordDocument.Variables["Номер дома"].Value = $"д. {enrollee.NumberHouse}";
                wordDocument.Variables["Номер квартиры"].Value = $"кв. {enrollee.NumberFlat} ";
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
    }
    }
