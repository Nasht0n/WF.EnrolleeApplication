﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    /// <summary>
    /// Представление "Приоритеты специальности" 
    /// Частичный класс для переопределения внутренних методов
    /// </summary>
    public partial class PriorityView
    {
        /// <summary>
        /// Переопределенный метод сравнения
        /// </summary>
        /// <param name="obj">Объект сущности</param>
        /// <returns>true - если объекты равны</returns>
        public override bool Equals(object obj)
        {
            if(obj is PriorityView && obj!=null)
            {
                PriorityView temp = (PriorityView)obj;
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
            return $"Код приоритета = {this.PriorityId}" +
                   $"\nКод абитуриента = {this.EnrolleeId}" +
                   $"\nКод специальности = {this.SpecialityId}" +
                   $"\nПолное наименование = {this.Fullname}" +
                   $"\nУровень приоритета = {this.PriorityLevel}";
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
