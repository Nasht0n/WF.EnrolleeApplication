using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class TargetWorkPlaceService : ITargetWorkPlaceService
    {
        private EnrolleeContext context;
        public TargetWorkPlaceService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }

        public void DeleteTargetWorkPlace(TargetWorkPlace targetWorkPlace)
        {
            TargetWorkPlace targetWorkPlaceToDelete = context.TargetWorkPlace.FirstOrDefault(tw => tw.TargetId == targetWorkPlace.TargetId);
            context.TargetWorkPlace.Remove(targetWorkPlaceToDelete);
            context.SaveChanges();
        }

        public TargetWorkPlace GetTargetWorkPlace(int id)
        {
            TargetWorkPlace targetWorkPlace = context.TargetWorkPlace.AsNoTracking().FirstOrDefault(tw => tw.TargetId == id);
            return targetWorkPlace;
        }

        public TargetWorkPlace GetTargetWorkPlace(string name)
        {
            TargetWorkPlace targetWorkPlace = context.TargetWorkPlace.AsNoTracking().FirstOrDefault(tw => tw.Name == name);
            return targetWorkPlace;
        }

        public List<TargetWorkPlace> GetTargetWorkPlaces()
        {
            List<TargetWorkPlace> targetWorkPlaces = context.TargetWorkPlace.AsNoTracking().ToList();
            return targetWorkPlaces;
        }

        public TargetWorkPlace InsertTargetWorkPlace(TargetWorkPlace targetWorkPlace)
        {
            context.TargetWorkPlace.Add(targetWorkPlace);
            context.SaveChanges();
            return targetWorkPlace;
        }

        public TargetWorkPlace UpdateTargetWorkPlace(TargetWorkPlace targetWorkPlace)
        {
            TargetWorkPlace targetWorkPlaceToUpdate = context.TargetWorkPlace.FirstOrDefault(tw => tw.TargetId == targetWorkPlace.TargetId);
            targetWorkPlaceToUpdate.Name = targetWorkPlace.Name;
            context.SaveChanges();
            return targetWorkPlaceToUpdate;
        }
    }
}
