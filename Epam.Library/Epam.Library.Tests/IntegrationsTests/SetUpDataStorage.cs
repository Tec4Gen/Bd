using Epam.Library.Entities;
using Epam.Library.FakeDAL;
using FluentValidation.Results;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Epam.Library.Tests.IntegrationsTests
{
    [SetUpFixture]
    public class SetUpDataStorage
    {
        private AuthorDao _authorDao;
        private BookDao _bookDao;
        private NewspaperDao _newspaperDao;
        private PatentDao _patentDao;

        [OneTimeSetUp]
        public void Setup()
        {
            _authorDao = new AuthorDao();
            _bookDao = new BookDao();
            _newspaperDao = new NewspaperDao();
            _patentDao = new PatentDao();

            AddingAuthor();
            AddningBook();
            AddingNewspaper();
            AddingPatent();
        }
        private void AddingAuthor()
        {
            ICollection<ValidationFailure> listError = new List<ValidationFailure>();

            _authorDao.Add(new AuthorDto
            {
                Id = 1,
                FirstName = "Robert",
                LastName = "Hailain"
            }, ref listError); ;

            _authorDao.Add(new AuthorDto
            {
                Id = 2,
                FirstName = "Lev",
                LastName = "Tolstoi"
            }, ref listError);

            _authorDao.Add(new AuthorDto
            {
                Id = 3,
                FirstName = "Agata",
                LastName = "Kristi"
            }, ref listError);

            _authorDao.Add(new AuthorDto
            {
                Id = 4,
                FirstName = "Test",
                LastName = "Test"
            }, ref listError);

            _authorDao.Add(new AuthorDto
            {
                Id = 5,
                FirstName = "Test",
                LastName = "Test"
            }, ref listError);
        }

        private void AddningBook()
        {
            ICollection<ValidationFailure> listError = new List<ValidationFailure>();
           
            _bookDao.Add(new BookDto
            {
                Title = "The Door into Summer",
                Note = "test",
                Authors = new List<AuthorDto> { _authorDao.GetById(1) },
                City = "test",
                ISBN = "ISBN 9989-7777-1-X",
                PublishingHouse = "test",
                NumberOfPages = 300,
                ReliseDate = new DateTime(1975,1,1)
            }, ref listError);

            _bookDao.Add(new BookDto
            {
                Title = "Starman Jones",
                Note = "test",
                Authors = new List<AuthorDto> { _authorDao.GetById(1) },
                City = "test",
                ISBN = "ISBN 9989-7777-3-X",
                PublishingHouse = "test",
                NumberOfPages = 300,
                ReliseDate = new DateTime(1953, 1, 1)
            }, ref listError);

         
            _bookDao.Add(new BookDto
            {
                Title = "Peace and War",
                Note = "test",
                Authors = new List<AuthorDto> { _authorDao.GetById(2) },
                City = "test",
                ISBN = null,
                PublishingHouse = "test",
                NumberOfPages = 300,
                ReliseDate = new DateTime(1869, 1, 1)
            }, ref listError);

            _bookDao.Add(new BookDto
            {
                Title = "Murder on the Orient Express",
                Note = "test",
                Authors = new List<AuthorDto> { _authorDao.GetById(3) },
                City = "test",
                ISBN = "ISBN 9989-7777-2-X",
                PublishingHouse = "test",
                NumberOfPages = 300,
                ReliseDate = new DateTime(1934, 1, 1)
            }, ref listError);

            _bookDao.Add(new BookDto
            {
                Title = "OtherBook",
                Note = "test",
                Authors = new List<AuthorDto> 
                { 
                    _authorDao.GetById(2), 
                    _authorDao.GetById(3) 
                },
                City = "test",
                ISBN = "ISBN 9989-7777-9-X",
                PublishingHouse = "test",
                NumberOfPages = 300,
                ReliseDate = new DateTime(1934, 1, 1)
            }, ref listError);


        }

        private void AddingNewspaper()
        {
            ICollection<ValidationFailure> listError = new List<ValidationFailure>();

            _newspaperDao.Add(new NewspaperDto
            {
                Title = "The Daily News",
                Note = "",
                City = "",
                ISSN = "ISSN9789-1111",
                PublishingHouse = "Galveston County Daily News",
                NumberOfPages = 20,
                ReliseDate = new DateTime(2012,12,12)
            },ref listError);

            _newspaperDao.Add(new NewspaperDto
            {
                Title = "USA today",
                Note = "",
                City = "",
                ISSN = null,
                PublishingHouse = "Gannett Company",
                NumberOfPages = 20,
                ReliseDate = new DateTime(2010, 12, 12)
            }, ref listError);

            _newspaperDao.Add(new NewspaperDto
            {
                Title = "The Wall Street Journal",
                Note = "",
                City = "",
                ISSN = "ISSN9789-7777",
                PublishingHouse = "Dow Jones & Company",
                NumberOfPages = 20,
                ReliseDate = new DateTime(2011, 12, 12)
            }, ref listError);
        }

        private void AddingPatent()
        {
            ICollection<ValidationFailure> listError = new List<ValidationFailure>();

            _patentDao.Add(new PatentDto
            {
                Title = "First Car",
                Authors = new List<AuthorDto> { _authorDao.GetById(2) },
                Note = "",
                Country = "",
                RegistrationNumber = 3333,
                NumberOfPages = 20,
            }, ref listError);

            _patentDao.Add(new PatentDto
            {
                Title = "First Radio",
                Authors = new List<AuthorDto> { _authorDao.GetById(3) },
                Note = "",
                Country = "",
                RegistrationNumber = 4444,
                NumberOfPages = 20,
            }, ref listError);

            _patentDao.Add(new PatentDto
            {
                Title = "First Air",
                Authors = new List<AuthorDto> { _authorDao.GetById(3) },
                Note = "",
                Country = "",
                RegistrationNumber = 5555,
                NumberOfPages = 20,
            }, ref listError);
        }
    }
}
