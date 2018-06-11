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
            Discipline disciplineToDelete = context.Discipline.FirstOrDefault(d => d.DisciplineId == discipline.DisciplineId);
            context.Discipline.Remove(disciplineToDelete);
            context.SaveChanges();
        }

        public Discipline GetDiscipline(int id)
        {
            Discipline discipline = context.Discipline.FirstOrDefault(d => d.DisciplineId == id);
            if(discipline != null)
            {
                discipline.BasisForAssessing = context.BasisForAssessing.FirstOrDefault(bfa=>bfa.BasisForAssessingId == discipline.BasisForAssessingId);
            }
            return discipline;
        }

        public Discipline GetDiscipline(string name)
        {
            Discipline discipline = context.Discipline.FirstOrDefault(d => d.Name == name);
            if (discipline != null)
            {
                discipline.BasisForAssessing = context.BasisForAssessing.FirstOrDefault(bfa => bfa.BasisForAssessingId == discipline.BasisForAssessingId);
            }
            return discipline;
        }

        public List<Discipline> GetDisciplines()
        {
            List<Discipline> disciplines = context.Discipline.ToList();
            if(disciplines.Count!= 0)
            {
                foreach(var discipline in disciplines)
                    discipline.BasisForAssessing = context.BasisForAssessing.FirstOrDefault(bfa => bfa.BasisForAssessingId == discipline.BasisForAssessingId);
            }
            return disciplines;
        }

        public List<Discipline> GetDisciplines(bool IsGroup)
        {
            List<Discipline> disciplines = context.Discipline.Where(d => d.IsGroup == IsGroup).ToList();
            if (disciplines.Count != 0)
            {
                foreach (var discipline in disciplines)
                    discipline.BasisForAssessing = context.BasisForAssessing.FirstOrDefault(bfa => bfa.BasisForAssessingId == discipline.BasisForAssessingId);
            }
            return disciplines;
        }

        public List<Discipline> GetDisciplines(BasisForAssessing basisForAssessing, bool IsGroup)
        {
            List<Discipline> disciplines = context.Discipline.Where(d => d.IsGroup == IsGroup && d.BasisForAssessingId == basisForAssessing.BasisForAssessingId).ToList();
            if (disciplines.Count != 0)
            {
                foreach (var discipline in disciplines)
                    discipline.BasisForAssessing = context.BasisForAssessing.FirstOrDefault(bfa => bfa.BasisForAssessingId == discipline.BasisForAssessingId);
            }
            return disciplines;
        }

        public List<Discipline> GetDisciplines(Discipline disciplineGroup)
        {
            List<Discipline> disciplines = context.Discipline.Where(d => d.DisciplineGroupId == disciplineGroup.DisciplineId).ToList();
            if (disciplines.Count != 0)
            {
                foreach (var discipline in disciplines)
                    discipline.BasisForAssessing = context.BasisForAssessing.FirstOrDefault(bfa => bfa.BasisForAssessingId == discipline.BasisForAssessingId);
            }
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
