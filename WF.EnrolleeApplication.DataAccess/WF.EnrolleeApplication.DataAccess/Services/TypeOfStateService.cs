using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class TypeOfStateService : ITypeOfStateService
    {
        private EnrolleeContext context;
        public TypeOfStateService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }

        public void DeleteTypeOfState(TypeOfState typeOfState)
        {
            TypeOfState typeOfStateToDelete = context.TypeOfState.FirstOrDefault(ts => ts.StateId == typeOfState.StateId);
            context.TypeOfState.Remove(typeOfStateToDelete);
            context.SaveChanges();
        }

        public TypeOfState GetTypeOfState(int id)
        {
            TypeOfState typeOfState = context.TypeOfState.FirstOrDefault(ts => ts.StateId == id);
            return typeOfState;
        }

        public TypeOfState GetTypeOfState(string name)
        {
            TypeOfState typeOfState = context.TypeOfState.FirstOrDefault(ts => ts.Name == name);
            return typeOfState;
        }

        public List<TypeOfState> GetTypeOfStates()
        {
            List<TypeOfState> typeOfStates = context.TypeOfState.ToList();
            return typeOfStates;
        }

        public TypeOfState InsertTypeOfState(TypeOfState typeOfState)
        {
            context.TypeOfState.Add(typeOfState);
            context.SaveChanges();
            return typeOfState;
        }

        public TypeOfState UpdateTypeOfState(TypeOfState typeOfState)
        {
            TypeOfState typeOfStateToUpdate = context.TypeOfState.FirstOrDefault(ts => ts.StateId == typeOfState.StateId);
            typeOfStateToUpdate.Name = typeOfState.Name;
            context.SaveChanges();
            return typeOfStateToUpdate;
        }
    }
}
