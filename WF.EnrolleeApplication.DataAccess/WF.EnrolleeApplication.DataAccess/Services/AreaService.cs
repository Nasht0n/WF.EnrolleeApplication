using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    /// <summary>
    /// Класс доступа к данным таблицы "Области"
    /// </summary>
    public class AreaService : IAreaService
    {
        private EnrolleeContext context;
        public AreaService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        /// <summary>
        /// Удаление области 
        /// </summary>
        /// <param name="area">Удаляемая область</param>
        public void DeleteArea(Area area)
        {
            Area areaToDelete = context.Area.AsNoTracking().FirstOrDefault(a => a.AreaId == area.AreaId);
            if (areaToDelete != null)
            {
                context.Area.Remove(areaToDelete);
                context.SaveChanges();
            }
        }
        /// <summary>
        /// Получение области по уникальному идентификатору
        /// </summary>
        /// <param name="id">УИД Области</param>
        /// <returns>Объект области</returns>
        public Area GetArea(int id)
        {
            Area areaById = context.Area.AsNoTracking().FirstOrDefault(a => a.AreaId == id);
            return areaById;
        }
        /// <summary>
        /// Получение области по наименованию
        /// </summary>
        /// <param name="name">Наименование области</param>
        /// <returns>Объект области</returns>
        public Area GetArea(string name)
        {
            Area areaById = context.Area.AsNoTracking().FirstOrDefault(a => a.Name == name);
            return areaById;
        }
        /// <summary>
        /// Получение списка областей
        /// </summary>
        /// <returns>Список областей</returns>
        public List<Area> GetAreas()
        {
            List<Area> areas = context.Area.AsNoTracking().ToList();
            return areas;
        }
        /// <summary>
        /// Вставка новой области
        /// </summary>
        /// <param name="area">Новая область</param>
        /// <returns>Объект новой области</returns>
        public Area InsertArea(Area area)
        {
             context.Area.Add(area);
             context.SaveChanges();
             return area;       
        }
        /// <summary>
        /// Редактирование области
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public Area UpdateArea(Area area)
        {
            Area areaToUpdate = context.Area.FirstOrDefault(a => a.AreaId == area.AreaId);
            areaToUpdate.Name = area.Name;
            context.SaveChanges();
            return areaToUpdate;
        }
    }
}
