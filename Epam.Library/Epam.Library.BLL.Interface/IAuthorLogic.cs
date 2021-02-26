using Epam.Library.Entities;
using FluentValidation.Results;
using System.Collections.Generic;

namespace Epam.Library.BLL.Interface
{
    public interface IAuthorLogic
    {
        void Add(AuthorDto book, out ICollection<ValidationFailure> errorList);
        void Remove(int id, out ICollection<ValidationFailure> errorList);
        void Update(AuthorDto book, out ICollection<ValidationFailure> errorList);
        AuthorDto GetById(int id);
        IEnumerable<AuthorDto> GetAll();
        IEnumerable<AbstractPrintedProducts> FindAllPatentsAndBooks(AuthorDto author);
    }
}
