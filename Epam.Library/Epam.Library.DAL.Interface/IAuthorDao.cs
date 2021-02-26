using Epam.Library.Entities;
using FluentValidation.Results;
using System.Collections.Generic;

namespace Epam.Library.DAL.Interface
{
    public interface IAuthorDao
    {
        void Add(AuthorDto author, ref ICollection<ValidationFailure> errorList);
        void Remove(int id, ref ICollection<ValidationFailure> errorList);
        void Update(AuthorDto author, ref ICollection<ValidationFailure> errorList);
        AuthorDto GetById(int id);
        IEnumerable<AuthorDto> GetAll();
        IEnumerable<AbstractPrintedProducts> FindAllPatentsAndBooks(AuthorDto author);
    }
}
