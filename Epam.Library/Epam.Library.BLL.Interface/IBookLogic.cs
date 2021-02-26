using Epam.Library.Entities;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Library.BLL.Interface
{
    public interface IBookLogic
    {
        void Add(BookDto book, out ICollection<ValidationFailure> errorList);
        void Remove(int id, out ICollection<ValidationFailure> errorList);
        void Update(BookDto book, out ICollection<ValidationFailure> errorList);
        BookDto GetById(int id);
        IEnumerable<BookDto> GetAll();
        BookDto FindByTitle(string title);
        IEnumerable<BookDto> SortedByYearForward();
        IEnumerable<BookDto> SortedByYearReverse();
        IEnumerable<BookDto> FindByAuthor(AuthorDto author);
        ILookup<string, BookDto> GetGroupsBooksStartingWithSetOfCharacters(string pattern);
        ILookup<int, BookDto> GroupByYear();

    }
}
