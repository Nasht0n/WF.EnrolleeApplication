using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface IAreaService
    {
        Area InsertArea(Area area);
        Area UpdateArea(Area area);
        void DeleteArea(Area area);
        List<Area> GetAreas();
        Area GetArea(int id);
        Area GetArea(string name);
    }
}
