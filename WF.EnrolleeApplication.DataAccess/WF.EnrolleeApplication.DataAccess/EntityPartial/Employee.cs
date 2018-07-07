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
    public partial class Employee
    {
        /// <summary>
        /// Переопределенный метод сравнения
        /// </summary>
        /// <param name="obj">Объект сущности</param>
        /// <returns>true - если объекты равны</returns>
        public override bool Equals(object obj)
        {
            if(obj is Employee && obj!=null)
            {
                Employee temp = (Employee)obj;
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
            return Environment.NewLine +
                   $"Информация об объекте: " + Environment.NewLine + 
                   $"Код пользователя = {this.EmployeeId} " + Environment.NewLine +
                   $"Код группы пользователя = {this.PostId}" + Environment.NewLine +
                   $"ФИО пользователя = {this.Fullname.Trim()}" + Environment.NewLine +
                   $"Имя входа пользователя = {this.Username.Trim()}" + Environment.NewLine +
                   $"Пароль пользователя = {this.Password.Trim()}" + Environment.NewLine +
                   $"Дата создания учетной записи = {this.CreateDate.ToShortDateString()}" + Environment.NewLine +
                   $"Учетная запись активирована? = {this.Enabled}";
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
