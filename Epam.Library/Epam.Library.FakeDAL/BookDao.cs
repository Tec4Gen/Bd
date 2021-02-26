using Epam.Library.DAL.Interface;
using Epam.Library.Entities;
using Epam.Library.Exceprions;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Library.FakeDAL
{
    public class BookDao : IBookDao
    {
        public void Add(BookDto book, ref ICollection<ValidationFailure> errorList)
        {
            try
            {
                CheckingForUniqueness(book);

                book.Id = DataStore.LibraryStorage.Keys.LastOrDefault() + 1;
                DataStore.LibraryStorage.Add(book.Id, book);
            }
            catch (ArgumentException)
            {
                errorList.Add(new ValidationFailure(nameof(book), "Cannot be found book with such id"));
            }
            catch (UniqueIdentifierException ex) 
            {
                errorList.Add(new ValidationFailure(nameof(book), ex.Message));
            }
           
        }

        public IEnumerable<BookDto> FindByAuthor(AuthorDto author)
        {
            foreach (var printedProduct in DataStore.LibraryStorage.Values)
            {
                if (printedProduct is BookDto book && book.Authors.Any(p=>p.Id == author.Id)) 
                {
                    yield return book;
                }
            }
        }

        public BookDto FindByTitle(string title)
        {
            return DataStore.LibraryStorage.Values
                .FirstOrDefault(p => p is BookDto book && book.Title == title) as BookDto;
        }

        public IEnumerable<BookDto> GetAll()
        {
            foreach (var printedProduct in DataStore.LibraryStorage.Values)
            {
                if (printedProduct is BookDto book) 
                {
                    yield return book;
                }
            };
        }

        public ILookup<string, BookDto> GetGroupsBooksStartingWithSetOfCharacters(string pattern)
        {           
            return DataStore.LibraryStorage.Values
                .Where(p => p is BookDto book && book.PublishingHouse.StartsWith(pattern))
                .Cast<BookDto>()
                .ToLookup(p => p.PublishingHouse);
        }
        
        public BookDto GetById(int id)
        {
            return DataStore.LibraryStorage.FirstOrDefault(p=>p.Key==id).Value as BookDto;
        }

        public void Remove(int id, ref ICollection<ValidationFailure> errorList)
        {
            try
            {
                DataStore.LibraryStorage.Remove(id);
            }
            catch (ArgumentException)
            {
                errorList.Add(new ValidationFailure(nameof(id), "Cannot be found book with such id"));
            }
      
        }

        public IEnumerable<BookDto> SortedByYearForward()
        {  
            return DataStore.LibraryStorage.Values
                .Where(p=>p is BookDto)
                .Cast<BookDto>()
                .OrderBy(p => p.ReliseDate.Year);
        }

        public IEnumerable<BookDto> SortedByYearReverse()
        {
            return DataStore.LibraryStorage.Values
                .Where(p => p is BookDto)
                .Cast<BookDto>()
                .OrderByDescending(p => p.ReliseDate.Year);
        }

        public void Update(BookDto book, ref ICollection<ValidationFailure> errorList)
        {
            throw new System.NotImplementedException();
        }

        public ILookup<int,BookDto> GroupByYear()
        {
            return DataStore.LibraryStorage.Values
                .Where(p => p is BookDto)
                .Cast<BookDto>()
                .ToLookup(p => p.ReliseDate.Year);
        }

        private void CheckingForUniqueness(BookDto book)
        {
            var tempData = DataStore.LibraryStorage.Values.Where(p => p is BookDto).Cast<BookDto>();
          
            if (tempData != null) 
            {
                if (book.ISBN != null && tempData.FirstOrDefault(p => p.ISBN == book.ISBN) != null)
                {
                    throw new UniqueIdentifierException($"A {nameof(book)} with this ISBN already exists");
                }
                else
                {
                    var findBook = tempData.FirstOrDefault(p => p.Authors.SequenceEqual(book.Authors, new AuthorsCompaper()));

                    if (findBook != null &&
                        book.Title == findBook.Title &&
                        book.ReliseDate == findBook.ReliseDate)
                    {
                        throw new UniqueIdentifierException($"A {nameof(book)} with this title, authors and date publication already exists");
                    }
                }
            }
            
        }
        
    }
}
