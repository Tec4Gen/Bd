using Epam.Library.BLL.Interface;
using Epam.Library.BLL.Validations;
using Epam.Library.DAL.Interface;
using Epam.Library.Entities;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Library.BLL
{
    public class BookLogic : IBookLogic
    {
        private IBookDao _bookDao;
        IValidator<BookDto> _validationBook;

        public BookLogic(IBookDao bookDao, IValidator<BookDto> validationBook)
        {
            _validationBook = validationBook;
            _bookDao = bookDao;
        }
        public void Add(BookDto book, out ICollection<ValidationFailure> errorList)
        {
            if (!_validationBook.Validate(book).IsValid)
            {
                errorList = _validationBook.Validate(book).Errors;

                return;
            }

            errorList = new List<ValidationFailure>();

            _bookDao.Add(book, ref errorList);   
        }

        public IEnumerable<BookDto> FindByAuthor(AuthorDto author)
        {
            foreach (var book in _bookDao.FindByAuthor(author))
            {
                yield return book;
            }
        }

        public BookDto FindByTitle(string title)
        {
            return _bookDao.FindByTitle(title);
        }

        public IEnumerable<BookDto> GetAll()
        {
            foreach (var book in _bookDao.GetAll())
            {
                yield return book;
            }
        }

        public ILookup<string, BookDto> GetGroupsBooksStartingWithSetOfCharacters(string pattern)
        {
          return _bookDao.GetGroupsBooksStartingWithSetOfCharacters(pattern);     
        }

        public BookDto GetById(int id)
        {
            return _bookDao.GetById(id);
        }

        public void Remove(int id, out ICollection<ValidationFailure> errorList)
        {
            errorList = new List<ValidationFailure>();

            _bookDao.Remove(id, ref errorList);
        }

        public IEnumerable<BookDto> SortedByYearForward()
        {
            foreach (var book in _bookDao.SortedByYearForward())
            {
                yield return book;
            }
        }

        public IEnumerable<BookDto> SortedByYearReverse()
        {
            foreach (var book in _bookDao.SortedByYearReverse())
            {
                yield return book;
            }
        }

        public void Update(BookDto book, out ICollection<ValidationFailure> errorList)
        {
            throw new NotImplementedException();
        }

        public ILookup<int, BookDto> GroupByYear()
        {
            return _bookDao.GroupByYear();
        }
    }
}
