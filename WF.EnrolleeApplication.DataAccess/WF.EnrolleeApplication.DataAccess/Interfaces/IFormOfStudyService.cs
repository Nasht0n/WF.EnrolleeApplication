using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    /// <summary>
    /// Интерфейс работы с данными таблицы "Формы обучения"
    /// </summary>
    interface IFormOfStudyService
    {
        /// <summary>
        /// Добавление формы обучения
        /// </summary>
        /// <param name="formOfStudy">Новая форма обучения</param>
        /// <returns>Новая запись</returns>
        FormOfStudy InsertFormOfStudy(FormOfStudy formOfStudy);
        /// <summary>
        /// Обновление формы обучения
        /// </summary>
        /// <param name="formOfStudy">Редактируемая форма обучения</param>
        /// <returns>Отредактированная форма обучения</returns>
        FormOfStudy UpdateFormOfStudy(FormOfStudy formOfStudy);
        /// <summary>
        /// Удаление формы обучения
        /// </summary>
        /// <param name="formOfStudy">Удаляемая форма обучения</param>
        void DeleteFormOfStudy(FormOfStudy formOfStudy);
        /// <summary>
        /// Получение списка форм обучения
        /// </summary>
        /// <returns>Список форм обучения</returns>
        List<FormOfStudy> GetFormOfStudies();
        /// <summary>
        /// Получение формы обучения по уникальному идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Запись формы обучения</returns>
        FormOfStudy GetFormOfStudy(int id);
        /// <summary>
        /// Получение формы обучения по наименованию
        /// </summary>
        /// <param name="fullname">Наименование формы обучения</param>
        /// <returns>Запись формы обучения</returns>
        FormOfStudy GetFormOfStudy(string fullname);
    }
}
