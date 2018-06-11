using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF.EnrolleeApplication.App.Services
{
    public class ChangeInputLanguage
    {
        public void ChangeLanguage()
        {
            // Если язык только один ничего не делаем
            if (InputLanguage.InstalledInputLanguages.Count == 1)
                return;
            // Текущий индекс языка
            int currentLang = InputLanguage.InstalledInputLanguages.IndexOf(InputLanguage.CurrentInputLanguage);
            // Следующий язык
            InputLanguage nextLang = ++currentLang == InputLanguage.InstalledInputLanguages.Count ? InputLanguage.InstalledInputLanguages[0] : InputLanguage.InstalledInputLanguages[currentLang];
            ChangeLanguage(nextLang);
        }

        public void ChangeLanguage(string ISOLang)
        {
            // Convert ISO Culture name to InputLanguage object. Be aware: if ISO is not supported
            // ArgumentException will be invoked here
            InputLanguage nextLang = InputLanguage.FromCulture(new System.Globalization.CultureInfo(ISOLang));
            ChangeLanguage(nextLang);
        }

        public void ChangeLanguage(int LangID)
        {
            // Convert Integer Culture code to InputLanguage object. Be aware: if Culture code is not supported
            // ArgumentException will be invoked here
            InputLanguage nextLang = InputLanguage.FromCulture(new System.Globalization.CultureInfo(LangID));
            ChangeLanguage(nextLang);
        }

        public void ChangeLanguage(InputLanguage InputLang)
        {
            // Check is this Language really installed. Raise exception to warn if it is not:
            if (InputLanguage.InstalledInputLanguages.IndexOf(InputLang) == -1)
                throw new ArgumentOutOfRangeException();
            // InputLAnguage changes here:
            InputLanguage.CurrentInputLanguage = InputLang;
        }
    }
}
