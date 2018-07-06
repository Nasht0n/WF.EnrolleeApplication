using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.App.Views
{
    /// <summary>
    /// Класс-формы подготовка отчёта "Расписка"
    /// </summary>
    public partial class frmReceipt : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        // Документы об образовании
        public string DocumentOfStudy;
        // Документы подтверждающие льготу
        public string DocumentOfDiscount;
        // Другие документы
        public string DocumentOther;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="enrollee">Абитуриент</param>
        public frmReceipt()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Обработчик завершения подготовки отчета "Расписки"
        /// </summary>
        /// <param name="sender">Кнопка "Отмена"</param>
        /// <param name="e"></param>
        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        /// <summary>
        /// Подготовка печати расписки
        /// </summary>
        /// <param name="sender">Кнопка "Печать"</param>
        /// <param name="e"></param>
        private void btPrint_Click(object sender, EventArgs e)
        {
            DocumentOfStudy = tbDocumentOfStudy.Text;
            DocumentOfDiscount = tbDocumentForDiscount.Text;
            DocumentOther = tbOtherDocument.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
