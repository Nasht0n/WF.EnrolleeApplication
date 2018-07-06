using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    /// <summary>
    /// Представление "Пользователи" 
    /// Частичный класс для переопределения внутренних методов
    /// </summary>
    public partial class EmployeeView
    {
        /// <summary>
        /// Переопределенный метод сравнения
        /// </summary>
        /// <param name="obj">Объект сущности</param>
        /// <returns>true - если объекты равны</returns>
        public override bool Equals(object obj)
        {
            if(obj is EmployeeView && obj!=null)
            {
                EmployeeView temp = (EmployeeView)obj;
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
            return $"Код пользователя = {this.EmployeeId}"  + Environment.NewLine +
                   $"Код должности пользователя = {this.PostId}" + Environment.NewLine +
                   $"Должность пользователя = {this.Name.Trim()}" + Environment.NewLine +
                   $"ФИО пользователя = {this.Fullname.Trim()}" + Environment.NewLine +
                   $"Имя входа пользователя = {this.Username.Trim()}" + Environment.NewLine +
                   $"Дата регистрации = {this.CreateDate.ToShortDateString()}" + Environment.NewLine +
                   $"Учетная запись активирована? = {this.Enabled}" + Environment.NewLine ;
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
