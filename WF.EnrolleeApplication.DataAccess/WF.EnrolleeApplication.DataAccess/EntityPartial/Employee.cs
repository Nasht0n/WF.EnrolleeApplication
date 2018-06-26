﻿using System;
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
            return $"Код пользователя = {this.EmployeeId}" +
                   $"\nКод группы пользователя = {this.PostId}" +
                   $"\nФИО пользователя = {this.Fullname.Trim()}" +
                   $"\nИмя входа пользователя = {this.Username.Trim()}" +
                   $"\nПароль пользователя = {this.Password.Trim()}" +
                   $"\nДата создания учетной записи = {this.CreateDate.ToShortDateString()}" +
                   $"\nУчетная запись активирована? = {this.Enabled}";
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
