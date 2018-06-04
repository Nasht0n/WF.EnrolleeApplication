using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface IEstimationStringService
    {
        EstimationString InsertEstimationString(EstimationString estimationString);
        void DeleteEstimationString(EstimationString estimationString);
        List<EstimationString> GetEstimationStrings();
        EstimationString GetEstimationString(int number, string text);
        int EstimationAsNumber(string estimation);
        string EstimationAsText(int number);
    }
}
