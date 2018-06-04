using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface IAtributeForEnrolleeService
    {
        AtributeForEnrollee InsertAtributeForEnrollee(AtributeForEnrollee atributeForEnrollee);
        AtributeForEnrollee UpdateAtributeForEnrollee(AtributeForEnrollee atributeForEnrollee);
        void DeleteAtributeForEnrollee(AtributeForEnrollee atributeForEnrollee);
        List<AtributeForEnrollee> GetAtributeForEnrollees();
        List<AtributeForEnrollee> GetAtributeForEnrollees(Enrollee enrollee);
        List<AtributeForEnrollee> GetAtributeForEnrollees(Atribute atribute);
        AtributeForEnrollee GetAtributeForEnrollee(int id);
        AtributeForEnrollee GetAtributeForEnrollee(Atribute atribute, Enrollee enrollee);
    }
}
