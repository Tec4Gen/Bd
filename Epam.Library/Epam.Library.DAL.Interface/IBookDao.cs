using Epam.Library.Entities;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Library.DAL.Interface
{
    public interface IBookDao
    {
        void Add(BookDto book, ref ICollection<ValidationFailure> errorList);
        void Remove(int id, ref ICollection<ValidationFailure> errorList);
        void Update(BookDto book, ref ICollection<ValidationFailure> errorList);
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
