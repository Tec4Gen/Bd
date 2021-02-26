using Epam.Library.BLL;
using Epam.Library.BLL.Validations;
using Epam.Library.DAL.Interface;
using Epam.Library.Entities;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Library.Tests.BLLUnitTests
{
    class NewspaperLogicTests
    {
        delegate void MyActionForAdd(NewspaperDto author, ref ICollection<ValidationFailure> errorList);

        delegate void MyActionForRemove(int id, ref ICollection<ValidationFailure> errorList);

        public void Add_AddingNewspaperWithCorrectlyData_Correctly()
        {
            var mockDao = new Mock<INewspaperDao>();

            var mockValidator = new Mock<IValidator<NewspaperDto>>();

            var a = mockValidator.Setup(x => x.Validate(It.IsAny<NewspaperDto>()))
                              .Returns(new ValidationResult());

            mockDao.Setup(c => c.Add(It.IsAny<NewspaperDto>(), ref It.Ref<ICollection<ValidationFailure>>.IsAny))
            .Callback(new MyActionForAdd((NewspaperDto newspaper, ref ICollection<ValidationFailure> error) => { }));


            var newspaperLogic = new NewspaperLogic(mockDao.Object, mockValidator.Object);

            newspaperLogic.Add(new NewspaperDto(), out ICollection<ValidationFailure> error);

            Assert.False(error.Any());
        }
        [Test]
        public void Add_AddingNewspaperNotWithCorrectlyData_NotCorrectly()
        {
            var mockDao = new Mock<INewspaperDao>();

            var mockValidator = new Mock<IValidator<NewspaperDto>>();

            var a = mockValidator.Setup(x => x.Validate(It.IsAny<NewspaperDto>()))
                               .Returns(new ValidationResult(new List<ValidationFailure>
                              {
                                    new ValidationFailure("TestField","Test Message")
                              }));

            mockDao.Setup(c => c.Add(It.IsAny<NewspaperDto>(), ref It.Ref<ICollection<ValidationFailure>>.IsAny))
           .Callback(new MyActionForAdd((NewspaperDto newspaper, ref ICollection<ValidationFailure> error) => { }));

            var newspaperLogic = new NewspaperLogic(mockDao.Object, mockValidator.Object);

            newspaperLogic.Add(new NewspaperDto(), out ICollection<ValidationFailure> error);

            Assert.True(error.Any());
        }
        [Test]
        public void Add_AddingNewspaper_ErrorInDao()
        {
            var mockDao = new Mock<INewspaperDao>();

            var mockValidator = new Mock<IValidator<NewspaperDto>>();

            var a = mockValidator.Setup(x => x.Validate(It.IsAny<NewspaperDto>()))
                              .Returns(new ValidationResult());

            mockDao.Setup(c => c.Add(It.IsAny<NewspaperDto>(), ref It.Ref<ICollection<ValidationFailure>>.IsAny))
            .Callback(new MyActionForAdd((NewspaperDto newspaper, ref ICollection<ValidationFailure> error) =>
            {
                error.Add(new ValidationFailure("TestField", "Test Message"));
            }));

            var newspaperLogic = new NewspaperLogic(mockDao.Object, mockValidator.Object);

            newspaperLogic.Add(new NewspaperDto(), out ICollection<ValidationFailure> error);

            Assert.True(error.Any());
        }
        [Test]
        public void Remove_RemovalNewspaper_Correctly()
        {
            var mockDao = new Mock<INewspaperDao>();

            mockDao.Setup(c => c.Remove(It.IsAny<int>(), ref It.Ref<ICollection<ValidationFailure>>.IsAny))
            .Callback(new MyActionForRemove((int id, ref ICollection<ValidationFailure> error) =>
            {}));

            var newspaperLogic = new NewspaperLogic(mockDao.Object, new ValidationNewspaper());

            newspaperLogic.Remove(1, out ICollection<ValidationFailure> error);

            Assert.False(error.Any());
        }
        [Test]
        public void Remove_RemovalNewspaper_ErrorInDao()
        {
            var mockDao = new Mock<INewspaperDao>();

            mockDao.Setup(c => c.Remove(It.IsAny<int>(), ref It.Ref<ICollection<ValidationFailure>>.IsAny))
            .Callback(new MyActionForRemove((int id, ref ICollection<ValidationFailure> error) =>
            {
                error.Add(new ValidationFailure("TestField", "Test Message"));
            }));

            var newspaperLogic = new NewspaperLogic(mockDao.Object, new ValidationNewspaper());

            newspaperLogic.Remove(1, out ICollection<ValidationFailure> error);

            Assert.True(error.Any());
        }
        [Test]
        public void GetById_ReceivingNewspaper_NotNull()
        {
            var mockDao = new Mock<INewspaperDao>();

            mockDao.Setup(c => c.GetById(It.IsAny<int>())).Returns((int id) => new NewspaperDto { Id = 1 });

            var newspaperLogic = new NewspaperLogic(mockDao.Object, new ValidationNewspaper());

            Assert.NotNull(newspaperLogic.GetById(1));
        }
        [Test]
        public void GetById_ReceivingNewspaper_Null()
        {
            var mockDao = new Mock<INewspaperDao>();

            mockDao.Setup(c => c.GetById(It.IsAny<int>())).Returns((int id) => null);

            var newspaperLogic = new NewspaperLogic(mockDao.Object, new ValidationNewspaper());

            Assert.IsNull(newspaperLogic.GetById(1));
        }
        [Test]
        public void GetAll_ReceivingNewspapers_NotEmptyList()
        {
            var mockDao = new Mock<INewspaperDao>();

            mockDao.Setup(c => c.GetAll()).Returns(() => new List<NewspaperDto> { new NewspaperDto()});

            var newspaperLogic = new NewspaperLogic(mockDao.Object, new ValidationNewspaper());

            Assert.IsNotEmpty(newspaperLogic.GetAll());
        }
        [Test]
        public void GetAll_ReceivingNewspapers_EmptyList()
        {
            var mockDao = new Mock<INewspaperDao>();

            mockDao.Setup(c => c.GetAll()).Returns(() => new List<NewspaperDto> {});

            var newspaperLogic = new NewspaperLogic(mockDao.Object, new ValidationNewspaper());

            Assert.IsEmpty(newspaperLogic.GetAll());
        }
        [Test]
        public void FindByTitle_RecervingNewspaper_NotNull()
        {
            var mockDao = new Mock<INewspaperDao>();

            mockDao.Setup(c => c.FindByTitle(It.IsAny<string>())).Returns((string title) => new NewspaperDto());

            var newspaperLogic = new NewspaperLogic(mockDao.Object, new ValidationNewspaper());

            Assert.IsNotNull(newspaperLogic.FindByTitle("test"));
        }
        [Test]
        public void FindByTitle_RecervingNewspaper_Null()
        {
            var mockDao = new Mock<INewspaperDao>();

            mockDao.Setup(c => c.FindByTitle(It.IsAny<string>())).Returns((string title) => null);

            var newspaperLogic = new NewspaperLogic(mockDao.Object, new ValidationNewspaper());

            Assert.IsNull(newspaperLogic.FindByTitle("test"));
        }
        [Test]
        public void SortedByYearForward_RecervingNewspaperSorted_NotEmpty()
        {
            var mockDao = new Mock<INewspaperDao>();

            mockDao.Setup(c => c.SortedByYearForward()).Returns(() => new List<NewspaperDto>()
            {
                new NewspaperDto(),
                new NewspaperDto()
            });

            var newspaperLogic = new NewspaperLogic(mockDao.Object, new ValidationNewspaper());

            Assert.IsNotEmpty(newspaperLogic.SortedByYearForward());
        }
        [Test]
        public void SortedByYearForward_RecervingNewspaperSorted_Empty()
        {
            var mockDao = new Mock<INewspaperDao>();

            mockDao.Setup(c => c.SortedByYearForward()).Returns(() => new List<NewspaperDto>());

            var newspaperLogic = new NewspaperLogic(mockDao.Object, new ValidationNewspaper());

            Assert.IsEmpty(newspaperLogic.SortedByYearForward());
        }
        [Test]
        public void SortedByYearReverse_RecervingNewspaperSorted_NotEmpty()
        {
            var mockDao = new Mock<INewspaperDao>();

            mockDao.Setup(c => c.SortedByYearForward()).Returns(() => new List<NewspaperDto>() 
            {
                new NewspaperDto(),
                new NewspaperDto(),
            });

            var newspaperLogic = new NewspaperLogic(mockDao.Object, new ValidationNewspaper());

            Assert.IsNotEmpty(newspaperLogic.SortedByYearForward());
        }
        [Test]
        public void SortedByYearReverse_RecervingNewspaperSorted_Empty()
        {
            var mockDao = new Mock<INewspaperDao>();

            mockDao.Setup(c => c.SortedByYearReverse()).Returns(() => new List<NewspaperDto>());

            var newspaperLogic = new NewspaperLogic(mockDao.Object, new ValidationNewspaper());

            Assert.IsEmpty(newspaperLogic.SortedByYearReverse());
        }
        [Test]
        public void GroupByYear_RecervingNewspapers_NotEmpty()
        {
            var mockDao = new Mock<INewspaperDao>();

            mockDao.Setup(c => c.GroupByYear()).Returns(() => new List<NewspaperDto>()
            {
                new NewspaperDto { ReliseDate = new DateTime(2012,12,12)},
                new NewspaperDto { ReliseDate = new DateTime(2013,12,12)},
                new NewspaperDto { ReliseDate = new DateTime(2014,12,12)},

            }.Where(p => p is NewspaperDto)
            .Cast<NewspaperDto>()
            .ToLookup(p => p.ReliseDate.Year));

            var newspaperLogic = new NewspaperLogic(mockDao.Object, new ValidationNewspaper());

            Assert.True(newspaperLogic.GroupByYear().Any());
        }
        [Test]
        public void GroupByYear_RecervingNewspapers_Empty()
        {
            var mockDao = new Mock<INewspaperDao>();

            mockDao.Setup(c => c.GroupByYear()).Returns(() => new List<NewspaperDto>()
            .Where(p => p is NewspaperDto)
            .Cast<NewspaperDto>()
            .ToLookup(p => p.ReliseDate.Year));

            var newspaperLogic = new NewspaperLogic(mockDao.Object, new ValidationNewspaper());

            Assert.False(newspaperLogic.GroupByYear().Any());
        }
    }
}
