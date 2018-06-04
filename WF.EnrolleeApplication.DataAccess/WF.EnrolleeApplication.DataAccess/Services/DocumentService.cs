using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Interfaces;

namespace WF.EnrolleeApplication.DataAccess.Services
{
    public class DocumentService : IDocumentService
    {
        private EnrolleeContext context;
        public DocumentService(string connectionString)
        {
            context = new EnrolleeContext(connectionString);
        }
        public void DeleteDocument(Document document)
        {
            Document documentToDelete = context.Document.FirstOrDefault(d => d.DocumentId == document.DocumentId);
            context.Document.Remove(documentToDelete);
            context.SaveChanges();
        }

        public Document GetDocument(int id)
        {
            Document documentById = context.Document.FirstOrDefault(d => d.DocumentId == id);
            return documentById;
        }

        public Document GetDocument(string name)
        {
            Document documentByName = context.Document.FirstOrDefault(d => d.Name == name);
            return documentByName;
        }

        public List<Document> GetDocuments()
        {
            List<Document> documents = context.Document.ToList();
            return documents;
        }

        public Document InsertDocument(Document document)
        {
            context.Document.Add(document);
            context.SaveChanges();
            return document;
        }

        public Document UpdateDocument(Document document)
        {
            Document documentToUpdate = context.Document.FirstOrDefault(d => d.DocumentId == document.DocumentId);
            documentToUpdate.Name = document.Name;
            context.SaveChanges();
            return documentToUpdate;
        }
    }
}
