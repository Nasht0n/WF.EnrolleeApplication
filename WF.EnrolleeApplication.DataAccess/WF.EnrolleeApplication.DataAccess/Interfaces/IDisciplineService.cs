using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface IDisciplineService
    {
        Discipline InsertDiscipline(Discipline discipline);
        Discipline UpdateDiscipline(Discipline discipline);
        void DeleteDiscipline(Discipline discipline);
        List<Discipline> GetDisciplines();
        List<Discipline> GetDisciplines(bool IsGroup);
        List<Discipline> GetDisciplines(BasisForAssessing basisForAssessing, bool IsGroup);
        List<Discipline> GetDisciplines(Discipline discipline);
        Discipline GetDiscipline(int id);
        Discipline GetDiscipline(string name);
    }
}
