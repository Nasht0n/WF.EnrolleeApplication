using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface IConversionSystemService
    {
        ConversionSystem InsertConversionSystem(ConversionSystem conversionSystem);
        void DeleteConversionSystem(ConversionSystem conversionSystem);
        List<ConversionSystem> GetConversions();
        ConversionSystem GetConversion(int id);
        ConversionSystem GetConversion(double five, double ten);
        double ConversionToFive(double ten);
        double ConversionToTen(double five);
    }
}
