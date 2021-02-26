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
    public class AuthorLogicTests
    {
        delegate void MyActionForAdd(AuthorDto author , ref ICollection<ValidationFailure> errorList);
        delegate void MyActionForRemove(int id, ref ICollection<ValidationFailure> errorList);

        [Test]
        public void Add_AddingAuthor_CorrectlyData() 
        {     
            var mockDao = new Mock<IAuthorDao>();

            var mockValidator = new Mock<IValidator<AuthorDto>>();

            var a = mockValidator.Setup(x => x.Validate(It.IsAny<AuthorDto>()))
                              .Returns(new ValidationResult());

            mockDao.Setup(c => c.Add(It.IsAny<AuthorDto>(), ref It.Ref<ICollection<ValidationFailure>>.IsAny))
            .Callback(new MyActionForAdd((AuthorDto author, ref ICollection<ValidationFailure> error) =>
            {}));


            var authorLogic = new AuthorLogic(mockDao.Object, mockValidator.Object);

            authorLogic.Add(new AuthorDto(), out ICollection<ValidationFailure> error);

            Assert.True(!error.Any());
        }
        [Test]
        public void Add_NotAddingAuthor_NotCorrectlyData()
        {
            var mockDao = new Mock<IAuthorDao>();

            var mockValidator = new Mock<IValidator<AuthorDto>>();

            var a = mockValidator.Setup(x => x.Validate(It.IsAny<AuthorDto>()))
                              .Returns(new ValidationResult( new List<ValidationFailure>()
                               {
                                    new ValidationFailure("TestField","Test Message")
                               }));

            mockDao.Setup(c => c.Add(It.IsAny<AuthorDto>(), ref It.Ref<ICollection<ValidationFailure>>.IsAny))
            .Callback(new MyActionForAdd((AuthorDto author, ref ICollection<ValidationFailure> error) =>
            { }));

            var authorLogic = new AuthorLogic(mockDao.Object, mockValidator.Object);

            authorLogic.Add(new AuthorDto(), out ICollection<ValidationFailure> error);

            Assert.True(error.Any());
        }
        [Test]
        public void Add_NotAddingAuthor_ErrorInDao()
        {
            var mockDao = new Mock<IAuthorDao>();

            var mockValidator = new Mock<IValidator<AuthorDto>>();

            var a = mockValidator.Setup(x => x.Validate(It.IsAny<AuthorDto>()))
                              .Returns(new ValidationResult());

            mockDao.Setup(c => c.Add(It.IsAny<AuthorDto>(), ref It.Ref<ICollection<ValidationFailure>>.IsAny))
            .Callback(new MyActionForAdd((AuthorDto author, ref ICollection<ValidationFailure> error) =>
            {
                error.Add(new ValidationFailure("TestField", "Test Message"));
            }));

            var authorLogic = new AuthorLogic(mockDao.Object, mockValidator.Object);

            authorLogic.Add(new AuthorDto(), out ICollection<ValidationFailure> error);

            Assert.AreEqual(1, error.Count());
        }
        [Test]
        public void Remove_RemovalAuthor_Correctly() 
        {
            var mockDao = new Mock<IAuthorDao>();

            mockDao.Setup(c => c.Remove(It.IsAny<int>(), ref It.Ref<ICollection<ValidationFailure>>.IsAny))
            .Callback(new MyActionForRemove((int id, ref ICollection<ValidationFailure> error) =>
            {
            }));

            var authorLogic = new AuthorLogic(mockDao.Object, new ValidationAuthor());

            authorLogic.Remove(1, out ICollection<ValidationFailure> error);

            Assert.False(error.Any());
        }
        [Test]
        public void Remove_RemovalAuthor_ErrorInDao()
        {
            var mockDao = new Mock<IAuthorDao>();

            mockDao.Setup(c => c.Remove(It.IsAny<int>(), ref It.Ref<ICollection<ValidationFailure>>.IsAny))
            .Callback(new MyActionForRemove((int id, ref ICollection<ValidationFailure> error) =>
            {
                error.Add(new ValidationFailure("TestField", "Test Message"));
            }));

            var authorLogic = new AuthorLogic(mockDao.Object, new ValidationAuthor());

            authorLogic.Remove(1, out ICollection<ValidationFailure> error);

            Assert.True(error.Any());
        }
        [Test]
        public void GetById_ReceivingAuthor_NotNull()
        {
            var mockDao = new Mock<IAuthorDao>();

            mockDao.Setup(c => c.GetById(It.IsAny<int>())).Returns((int id ) => new AuthorDto {Id = 1});


            var authorLogic = new AuthorLogic(mockDao.Object, new ValidationAuthor());

            Assert.NotNull(authorLogic.GetById(1));
        }
        [Test]
        public void GetById_ReceivingAuthor_Null()
        {
            var mockDao = new Mock<IAuthorDao>();

            mockDao.Setup(c => c.GetById(It.IsAny<int>())).Returns((int id) => null);

            var authorLogic = new AuthorLogic(mockDao.Object, new ValidationAuthor());

            Assert.IsNull(authorLogic.GetById(1));
        }
        [Test]
        public void GetAll_ReceivingAuthors_NotEmptyList()
        {
            var mockDao = new Mock<IAuthorDao>();

            mockDao.Setup(c => c.GetAll()).Returns(() => new List<AuthorDto> { new AuthorDto() });

            var authorLogic = new AuthorLogic(mockDao.Object, new ValidationAuthor());

            Assert.IsNotEmpty(authorLogic.GetAll());
        }
        [Test]
        public void GetAll_ReceivingAuthors_EmptyList()
        {
            var mockDao = new Mock<IAuthorDao>();

            mockDao.Setup(c => c.GetAll()).Returns(() => new List<AuthorDto> {});

            var authorLogic = new AuthorLogic(mockDao.Object, new ValidationAuthor());

            Assert.IsEmpty(authorLogic.GetAll());
        }
        [Test]
        public void FindAllPatentsAndBooks_Receiving_NotEmptyList()
        {
            var mockDao = new Mock<IAuthorDao>();

            mockDao.Setup(c => c.FindAllPatentsAndBooks(It.IsAny<AuthorDto>())).Returns(() => new List<AbstractPrintedProducts> { new BookDto(), new PatentDto()});

            var authorLogic = new AuthorLogic(mockDao.Object, new ValidationAuthor());

            Assert.IsNotEmpty(authorLogic.FindAllPatentsAndBooks(new AuthorDto()));
        }
        [Test]
        public void FindAllPatentsAndBooks_Receiving_EmptyList()
        {
            var mockDao = new Mock<IAuthorDao>();

            mockDao.Setup(c => c.FindAllPatentsAndBooks(It.IsAny<AuthorDto>())).Returns(() => new List<AbstractPrintedProducts>());

            var authorLogic = new AuthorLogic(mockDao.Object, new ValidationAuthor());

            Assert.IsEmpty(authorLogic.FindAllPatentsAndBooks(new AuthorDto()));
        }
    }
}
