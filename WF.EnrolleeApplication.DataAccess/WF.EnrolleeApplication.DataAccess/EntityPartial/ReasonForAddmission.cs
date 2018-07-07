﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    /// <summary>
    /// Таблица "Основания зачисления" 
    /// Частичный класс для переопределения внутренних методов
    /// </summary>
    public partial class ReasonForAddmission
    {
        /// <summary>
        /// Переопределенный метод сравнения
        /// </summary>
        /// <param name="obj">Объект сущности</param>
        /// <returns>true - если объекты равны</returns>
        public override bool Equals(object obj)
        {
            if(obj is ReasonForAddmission && obj!=null)
            {
                ReasonForAddmission temp = (ReasonForAddmission)obj;
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
                   $"Код основания зачисления = {this.ReasonForAddmissionId}" + Environment.NewLine +
                   $"Код конкурса = {this.ContestId}" + Environment.NewLine +
                   $"Полное наименование = {this.Fullname.Trim()}" + Environment.NewLine +
                   $"Сокращенное наименование = {this.Shortname.Trim()}";
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
