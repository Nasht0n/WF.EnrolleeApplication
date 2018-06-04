using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class FacultyService : IFacultyService
    {
        private EnrolleeContext context;
        public FacultyService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }

        public void DeleteFaculty(Faculty faculty)
        {
            Faculty facultyToDelete = context.Faculty.FirstOrDefault(f => f.FacultyId == faculty.FacultyId);
            context.Faculty.Remove(facultyToDelete);
            context.SaveChanges();
        }

        public List<Faculty> GetFaculties()
        {
            List<Faculty> faculties = context.Faculty.ToList();
            return faculties;
        }

        public Faculty GetFaculty(int id)
        {
            Faculty faculty = context.Faculty.FirstOrDefault(f => f.FacultyId == id);
            return faculty;
        }

        public Faculty GetFaculty(string fullname)
        {
            Faculty faculty = context.Faculty.FirstOrDefault(f => f.Fullname == fullname);
            return faculty;
        }

        public Faculty InsertFaculty(Faculty faculty)
        {
            context.Faculty.Add(faculty);
            context.SaveChanges();
            return faculty;
        }

        public Faculty UpdateFaculty(Faculty faculty)
        {
            Faculty facultyToUpdate = context.Faculty.FirstOrDefault(f => f.FacultyId == faculty.FacultyId);
            facultyToUpdate.Fullname = faculty.Fullname;
            facultyToUpdate.Shortname = faculty.Shortname;
            context.SaveChanges();
            return facultyToUpdate;
        }
    }
}
