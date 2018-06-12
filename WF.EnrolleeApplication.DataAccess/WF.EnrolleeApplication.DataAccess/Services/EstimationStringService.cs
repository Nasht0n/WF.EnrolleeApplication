using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class EstimationStringService : IEstimationStringService
    {
        private EnrolleeContext context;
        public EstimationStringService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }

        public void DeleteEstimationString(EstimationString estimationString)
        {
            EstimationString estimationStringToDelete = context.EstimationString.AsNoTracking().FirstOrDefault(es => es.EstimationNumber == estimationString.EstimationNumber && es.EstimationText == estimationString.EstimationText);
            context.EstimationString.Remove(estimationStringToDelete);
            context.SaveChanges();
        }

        public int EstimationAsNumber(string estimation)
        {
            EstimationString estimationString = context.EstimationString.AsNoTracking().FirstOrDefault(es => es.EstimationText == estimation);
            return estimationString.EstimationNumber;
        }

        public string EstimationAsText(int number)
        {
            EstimationString estimationString = context.EstimationString.AsNoTracking().FirstOrDefault(es => es.EstimationNumber == number);
            return estimationString.EstimationText;
        }

        public EstimationString GetEstimationString(int number, string text)
        {
            EstimationString estimationString = context.EstimationString.AsNoTracking().FirstOrDefault(es => es.EstimationNumber == number && es.EstimationText == text);
            return estimationString;
        }

        public List<EstimationString> GetEstimationStrings()
        {
            List<EstimationString> estimationStrings = context.EstimationString.AsNoTracking().ToList();
            return estimationStrings;
        }

        public EstimationString InsertEstimationString(EstimationString estimationString)
        {
            context.EstimationString.Add(estimationString);
            context.SaveChanges();
            return estimationString;
        }
    }
}
