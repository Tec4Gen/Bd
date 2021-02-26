using Epam.Library.BLL;
using Epam.Library.BLL.Validations;
using Epam.Library.DAL.Interface;
using Epam.Library.Entities;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Library.Tests.BLLUnitTests
{
    public class BookLogicTests
    {
        delegate void MyActionForAdd(BookDto author, ref ICollection<ValidationFailure> errorList);

        delegate void MyActionForRemove(int id, ref ICollection<ValidationFailure> errorList);
        [Test]
        public void Add_AddingBook_CorrectlyData()
        {
            var mockDao = new Mock<IBookDao>();

            var mockValidator = new Mock<IValidator<BookDto>>();

            var a = mockValidator.Setup(x => x.Validate(It.IsAny<BookDto>()))
                              .Returns(new ValidationResult());

            mockDao.Setup(c => c.Add(It.IsAny<BookDto>(), ref It.Ref<ICollection<ValidationFailure>>.IsAny))
            .Callback(new MyActionForAdd((BookDto book, ref ICollection<ValidationFailure> error) =>
            { }));


            var bookLogic = new BookLogic(mockDao.Object, mockValidator.Object);

            bookLogic.Add(new BookDto(), out ICollection<ValidationFailure> error);

            Assert.False(error.Any());
        }
        [Test]
        public void Add_AddingBook_NotCorrectlyData()
        {
            var mockDao = new Mock<IBookDao>();

            var mockValidator = new Mock<IValidator<BookDto>>();

            var a = mockValidator.Setup(x => x.Validate(It.IsAny<BookDto>()))
                              .Returns(new ValidationResult(new List<ValidationFailure>
                              {
                                    new ValidationFailure("TestField","Test Message")
                              }));

            mockDao.Setup(c => c.Add(It.IsAny<BookDto>(), ref It.Ref<ICollection<ValidationFailure>>.IsAny))
            .Callback(new MyActionForAdd((BookDto book, ref ICollection<ValidationFailure> error) =>
            { }));

            var bookLogic = new BookLogic(mockDao.Object, mockValidator.Object);

            bookLogic.Add(new BookDto(), out ICollection<ValidationFailure> error);

            Assert.True(error.Any());
        }
        [Test]
        public void Add_AddingBook_ErrorInDao()
        {
            var mockDao = new Mock<IBookDao>();

            var mockValidator = new Mock<IValidator<BookDto>>();

            var a = mockValidator.Setup(x => x.Validate(It.IsAny<BookDto>()))
                              .Returns(new ValidationResult());

            mockDao.Setup(c => c.Add(It.IsAny<BookDto>(), ref It.Ref<ICollection<ValidationFailure>>.IsAny))
            .Callback(new MyActionForAdd((BookDto book, ref ICollection<ValidationFailure> error) =>
            {
                 error.Add(new ValidationFailure("TestField", "Test Message"));
            }));

            var bookLogic = new BookLogic(mockDao.Object, mockValidator.Object);

            bookLogic.Add(new BookDto(), out ICollection<ValidationFailure> error);

            Assert.True(error.Any());
        }
        [Test]
        public void Remove_RemovalBook_Correctly()
        {
            var mockDao = new Mock<IBookDao>();

            mockDao.Setup(c => c.Remove(It.IsAny<int>(), ref It.Ref<ICollection<ValidationFailure>>.IsAny))
            .Callback(new MyActionForRemove((int id, ref ICollection<ValidationFailure> error) =>
            {

            }));

            var bookLogic = new BookLogic(mockDao.Object, new ValidationBook());

            bookLogic.Remove(1, out ICollection<ValidationFailure> error);

            Assert.False(error.Any());
        }
        [Test]
        public void Remove_RemovalBook_ErrorInDao()
        {
            var mockDao = new Mock<IBookDao>();

            mockDao.Setup(c => c.Remove(It.IsAny<int>(), ref It.Ref<ICollection<ValidationFailure>>.IsAny))
            .Callback(new MyActionForRemove((int id, ref ICollection<ValidationFailure> error) =>
            {
                error.Add(new ValidationFailure("TestField", "Test Message"));
            }));

            var bookLogic = new BookLogic(mockDao.Object, new ValidationBook());

            bookLogic.Remove(1, out ICollection<ValidationFailure> error);

            Assert.True(error.Any());
        }
        [Test]
        public void GetById_ReceivingBook_NotNull()
        {
            var mockDao = new Mock<IBookDao>();

            mockDao.Setup(c => c.GetById(It.IsAny<int>())).Returns((int id) => new BookDto { Id = 1 });


            var bookLogic = new BookLogic(mockDao.Object, new ValidationBook());

            Assert.NotNull(bookLogic.GetById(1));
        }
        [Test]
        public void GetById_ReceivingBook_Null()
        {
            var mockDao = new Mock<IBookDao>();

            mockDao.Setup(c => c.GetById(It.IsAny<int>())).Returns((int id) => null);

            var bookLogic = new BookLogic(mockDao.Object, new ValidationBook());

            Assert.IsNull(bookLogic.GetById(1));
        }
        [Test]
        public void GetAll_ReceivingBooks_NotEmptyList()
        {
            var mockDao = new Mock<IBookDao>();

            mockDao.Setup(c => c.GetAll()).Returns(() => new List<BookDto> { new BookDto() });

            var bookLogic = new BookLogic(mockDao.Object, new ValidationBook());

            Assert.IsNotEmpty(bookLogic.GetAll());
        }
        [Test]
        public void GetAll_ReceivingBooks_EmptyList()
        {
            var mockDao = new Mock<IBookDao>();

            mockDao.Setup(c => c.GetAll()).Returns(() => new List<BookDto> {});

            var bookLogic = new BookLogic(mockDao.Object, new ValidationBook());

            Assert.IsEmpty(bookLogic.GetAll());
        }
        [Test]
        public void FindByTitle_RecervingBook_NotNull() 
        {
            var mockDao = new Mock<IBookDao>();

            mockDao.Setup(c => c.FindByTitle(It.IsAny<string>())).Returns((string title) => new BookDto());

            var bookLogic = new BookLogic(mockDao.Object, new ValidationBook());

            Assert.IsNotNull(bookLogic.FindByTitle("test"));
        }
        [Test]
        public void FindByTitle_RecervingBook_Null()
        {
            var mockDao = new Mock<IBookDao>();

            mockDao.Setup(c => c.FindByTitle(It.IsAny<string>())).Returns((string title) => null);

            var bookLogic = new BookLogic(mockDao.Object, new ValidationBook());

            Assert.IsNull(bookLogic.FindByTitle("test"));
        }
        [Test]
        public void SortedByYearForward_RecervingBookSorted_NotEmpty()
        {
            var mockDao = new Mock<IBookDao>();

            mockDao.Setup(c => c.SortedByYearForward()).Returns(() => new List<BookDto>() 
            { 
                new BookDto(), 
                new BookDto()}
            );

            var bookLogic = new BookLogic(mockDao.Object, new ValidationBook());

            Assert.IsNotEmpty(bookLogic.SortedByYearForward());
        }
        [Test]
        public void SortedByYearForward_RecervingBookSorted_Empty()
        {
            var mockDao = new Mock<IBookDao>();

            mockDao.Setup(c => c.SortedByYearForward()).Returns(() => new List<BookDto>());

            var bookLogic = new BookLogic(mockDao.Object, new ValidationBook());

            Assert.IsEmpty(bookLogic.SortedByYearForward());
        }
        [Test]
        public void SortedByYearReverse_RecervingBookSorted_NotEmpty()
        {
            var mockDao = new Mock<IBookDao>();

            mockDao.Setup(c => c.SortedByYearReverse()).Returns(() => new List<BookDto>() 
            {
                new BookDto(),
                new BookDto()
            });

            var bookLogic = new BookLogic(mockDao.Object, new ValidationBook());

            Assert.IsNotEmpty(bookLogic.SortedByYearReverse());
        }
        [Test]
        public void SortedByYearReverse_RecervingBookSorted_Empty()
        {
            var mockDao = new Mock<IBookDao>();

            mockDao.Setup(c => c.SortedByYearReverse()).Returns(() => new List<BookDto>());

            var bookLogic = new BookLogic(mockDao.Object, new ValidationBook());

            Assert.IsEmpty(bookLogic.SortedByYearReverse());
        }
        [Test]
        public void FindByAuthor_RecervingBooksByAuthor_NotEmpty()
        {
            var mockDao = new Mock<IBookDao>();

            mockDao.Setup(c => c.FindByAuthor(It.IsAny<AuthorDto>())).Returns(() => new List<BookDto>
            {
                new BookDto(),
                new BookDto()
            });

            var bookLogic = new BookLogic(mockDao.Object, new ValidationBook());

            Assert.IsNotEmpty(bookLogic.FindByAuthor(new AuthorDto()));
        }
        [Test]
        public void FindByAuthor_RecervingBooksByAuthor_Empty()
        {
            var mockDao = new Mock<IBookDao>();

            mockDao.Setup(c => c.FindByAuthor(It.IsAny<AuthorDto>())).Returns(() => new List<BookDto>());

            var bookLogic = new BookLogic(mockDao.Object, new ValidationBook());

            Assert.IsEmpty(bookLogic.FindByAuthor(new AuthorDto()));
        }
        [Test]
        public void GetGroupsBooksStartingWithSetOfCharacters_RecervingBooks_NotEmpty()
        {
            var mockDao = new Mock<IBookDao>();

            mockDao.Setup(c => c.GetGroupsBooksStartingWithSetOfCharacters(It.IsAny<string>())).Returns((string title) => new List<BookDto>() 
            {
                new BookDto { PublishingHouse = "Abcd"},
                new BookDto { PublishingHouse = "Abcde"},
                new BookDto { PublishingHouse = "Asfsd"},
            }.Where(p => p is BookDto book && book.PublishingHouse.StartsWith(title))
            .Cast<BookDto>()
            .ToLookup(p => p.PublishingHouse));

            var bookLogic = new BookLogic(mockDao.Object, new ValidationBook());

            Assert.True(bookLogic.GetGroupsBooksStartingWithSetOfCharacters("Abc").Any());
        }
        [Test]
        public void GetGroupsBooksStartingWithSetOfCharacters_RecervingBooks_Empty()
        {
            var mockDao = new Mock<IBookDao>();

            mockDao.Setup(c => c.GetGroupsBooksStartingWithSetOfCharacters(It.IsAny<string>())).Returns((string title) => new List<BookDto>()
            {
                new BookDto { PublishingHouse = "Abcd"},
                new BookDto { PublishingHouse = "Abcde"},
                new BookDto { PublishingHouse = "Asfsd"},
            }.Where(p => p is BookDto book && book.PublishingHouse.StartsWith(title))
            .Cast<BookDto>()
            .ToLookup(p => p.PublishingHouse));

            var bookLogic = new BookLogic(mockDao.Object, new ValidationBook());

            Assert.False(bookLogic.GetGroupsBooksStartingWithSetOfCharacters("sFdfsd").Any());
        }
        [Test]
        public void GroupByYear_RecervingBooks_NotEmpty()
        {
            var mockDao = new Mock<IBookDao>();

            mockDao.Setup(c => c.GroupByYear()).Returns(() => new List<BookDto>()
            {
                new BookDto { ReliseDate = new System.DateTime(2012,12,12)},
                new BookDto { ReliseDate = new System.DateTime(2013,12,12)},
                 new BookDto { ReliseDate = new System.DateTime(2014,12,12)},
            }.Where(p => p is BookDto)
            .Cast<BookDto>()
            .ToLookup(p => p.ReliseDate.Year));

            var bookLogic = new BookLogic(mockDao.Object, new ValidationBook());

            Assert.True(bookLogic.GroupByYear().Any());
        }
        [Test]
        public void GroupByYear_RecervingBooks_Empty()
        {
            var mockDao = new Mock<IBookDao>();

            mockDao.Setup(c => c.GroupByYear()).Returns(() => new List<BookDto>()
            .Where(p => p is BookDto)
            .Cast<BookDto>()
            .ToLookup(p => p.ReliseDate.Year));

            var bookLogic = new BookLogic(mockDao.Object, new ValidationBook());

            Assert.False(bookLogic.GroupByYear().Any());
        }
    }
}
