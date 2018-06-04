using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface ITargetWorkPlaceService
    {
        TargetWorkPlace InsertTargetWorkPlace(TargetWorkPlace targetWorkPlace);
        TargetWorkPlace UpdateTargetWorkPlace(TargetWorkPlace targetWorkPlace);
        void DeleteTargetWorkPlace(TargetWorkPlace targetWorkPlace);
        List<TargetWorkPlace> GetTargetWorkPlaces();
        TargetWorkPlace GetTargetWorkPlace(int id);
        TargetWorkPlace GetTargetWorkPlace(string name);
    }
}
