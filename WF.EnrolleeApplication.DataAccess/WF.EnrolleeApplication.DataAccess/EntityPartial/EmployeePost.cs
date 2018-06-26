using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    /// <summary>
    /// Таблица "Пользователи" 
    /// Частичный класс для переопределения внутренних методов
    /// </summary>
    public partial class EmployeePost
    {
        /// <summary>
        /// Переопределенный метод сравнения
        /// </summary>
        /// <param name="obj">Объект сущности</param>
        /// <returns>true - если объекты равны</returns>
        public override bool Equals(object obj)
        {
            if(obj is EmployeePost && obj != null)
            {
                EmployeePost temp = (EmployeePost)obj;
                if (temp.GetHashCode() == this.GetHashCode()) return true;
                else return false;
            }
            return false;
        }
        /// <summary>
        /// Переопределенный метод вывода данных класса
        /// </summary>
        /// <returns>Форматированная строка</returns>
        public override string ToString()
        {
            return $"Код группы пользователя = {this.PostId}" +
                   $"\nНаименование группы пользователя = {this.Name.Trim()}" +
                   $"\nОписание = {this.Note.Trim()}" +
                   $"\nРазрешение регистрации? - {this.RegistrationAllow}" +
                   $"\nРазрешение зачисления? - {this.EnrollAllow}" +
                   $"\nДоступ к справочникам системы? - {this.DictionaryAllow}";
        }
        /// <summary>
        /// Переопределенный метод получения хеш-кода объекта
        /// </summary>
        /// <returns>Хэш-код</returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }
}
