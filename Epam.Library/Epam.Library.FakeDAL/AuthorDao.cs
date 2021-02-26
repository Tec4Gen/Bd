using Epam.Library.DAL.Interface;
using Epam.Library.Entities;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Library.FakeDAL
{
    public class AuthorDao : IAuthorDao
    {
        public void Add(AuthorDto author, ref ICollection<ValidationFailure> errorList)
        {
            try
            {
                author.Id = DataStore.AuthorStorage.Keys.LastOrDefault() + 1;
                DataStore.AuthorStorage.Add(author.Id, author);
            }
            catch (SystemException)
            {
                errorList.Add(new ValidationFailure(nameof(author), $"Unable to add {nameof(author)}"));
            }                
        }

        public IEnumerable<AbstractPrintedProducts> FindAllPatentsAndBooks(AuthorDto author)
        {
            var tempPrintProductsBook = DataStore.LibraryStorage.Values.Where(p => p is BookDto || p is PatentDto);

            foreach (var printProducts in tempPrintProductsBook)
            {
                if (printProducts is BookDto tempBook && tempBook.Authors.Any(p => p.Id == author.Id) ||
                    printProducts is PatentDto tempPatent && tempPatent.Authors.Any(p => p.Id == author.Id))
                {
                    yield return printProducts;
                }
            }
        }

        public IEnumerable<AuthorDto> GetAll()
        {
            foreach (var author  in DataStore.AuthorStorage.Values)
            {
                yield return author;
            }
        }

        public AuthorDto GetById(int id)
        {
            return DataStore.AuthorStorage.FirstOrDefault(p=> p.Key == id).Value;
        }

        public void Remove(int id, ref ICollection<ValidationFailure> errorList)
        {    
            try
            {
                if (!DataStore.AuthorStorage.Remove(id)) 
                {
                    errorList.Add(new ValidationFailure(nameof(id), $"Cannot be found author with such id"));
                }
            }
            catch (SystemException)
            {
                errorList.Add(new ValidationFailure(nameof(id), "Data entered incorrectly"));
            }
        }

        public void Update(AuthorDto book, ref ICollection<ValidationFailure> errorList)
        {
            throw new System.NotImplementedException();
        }
    }
}
