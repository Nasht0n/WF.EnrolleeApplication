using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;

namespace WF.EnrolleeApplication.DataAccess.Interfaces
{
    interface IDocumentService
    {
        Document InsertDocument(Document document);
        Document UpdateDocument(Document document);
        void DeleteDocument(Document document);
        List<Document> GetDocuments();
        Document GetDocument(int id);
        Document GetDocument(string fullname);
    }
}
