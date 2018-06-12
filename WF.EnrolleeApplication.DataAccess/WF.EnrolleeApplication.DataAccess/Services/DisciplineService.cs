using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class DisciplineService : IDisciplineService
    {
        private EnrolleeContext context;

        public DisciplineService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        public void DeleteDiscipline(Discipline discipline)
        {
            Discipline disciplineToDelete = context.Discipline.AsNoTracking().FirstOrDefault(d => d.DisciplineId == discipline.DisciplineId);
            context.Discipline.Remove(disciplineToDelete);
            context.SaveChanges();
        }

        public Discipline GetDiscipline(int id)
        {
            Discipline discipline = context.Discipline.AsNoTracking().FirstOrDefault(d => d.DisciplineId == id);
            return discipline;
        }

        public Discipline GetDiscipline(string name)
        {
            Discipline discipline = context.Discipline.AsNoTracking().FirstOrDefault(d => d.Name == name);
            return discipline;
        }

        public List<Discipline> GetDisciplines()
        {
            List<Discipline> disciplines = context.Discipline.AsNoTracking().ToList();
            return disciplines;
        }

        public List<Discipline> GetDisciplines(bool IsGroup)
        {
            List<Discipline> disciplines = context.Discipline.AsNoTracking().Where(d => d.IsGroup == IsGroup).ToList();
            return disciplines;
        }

        public List<Discipline> GetDisciplines(BasisForAssessing basisForAssessing, bool IsGroup)
        {
            List<Discipline> disciplines = context.Discipline.AsNoTracking().Where(d => d.IsGroup == IsGroup && d.BasisForAssessingId == basisForAssessing.BasisForAssessingId).ToList();
            return disciplines;
        }

        public List<Discipline> GetDisciplines(Discipline disciplineGroup)
        {
            List<Discipline> disciplines = context.Discipline.AsNoTracking().Where(d => d.DisciplineGroupId == disciplineGroup.DisciplineId).ToList();
            return disciplines;
        }

        public Discipline InsertDiscipline(Discipline discipline)
        {
            context.Discipline.Add(discipline);
            context.SaveChanges();
            return discipline;
        }

        public Discipline UpdateDiscipline(Discipline discipline)
        {
            Discipline disciplineToUpdate = context.Discipline.FirstOrDefault(d => d.DisciplineId == discipline.DisciplineId);
            disciplineToUpdate.BasisForAssessingId = discipline.BasisForAssessingId;
            disciplineToUpdate.Name = discipline.Name;
            disciplineToUpdate.IsGroup = discipline.IsGroup;
            disciplineToUpdate.IsAlternative = discipline.IsAlternative;
            disciplineToUpdate.DisciplineGroupId = discipline.DisciplineGroupId;
            disciplineToUpdate.ConsultDate = discipline.ConsultDate;
            disciplineToUpdate.EntryExamDate = discipline.EntryExamDate;
            disciplineToUpdate.StageCount = discipline.StageCount;
            context.SaveChanges();
            return disciplineToUpdate;
        }
    }
}
