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
    public class PatentLogicTests
    {
        delegate void MyActionForAdd(PatentDto author, ref ICollection<ValidationFailure> errorList);

        delegate void MyActionForRemove(int id, ref ICollection<ValidationFailure> errorList);

        public void Add_AddingPatentWithCorrectlyData_Correctly()
        {
            var mockDao = new Mock<IPatentDao>();

            var mockValidator = new Mock<IValidator<PatentDto>>();

            var a = mockValidator.Setup(x => x.Validate(It.IsAny<PatentDto>()))
                              .Returns(new ValidationResult());

            mockDao.Setup(c => c.Add(It.IsAny<PatentDto>(), ref It.Ref<ICollection<ValidationFailure>>.IsAny))
            .Callback(new MyActionForAdd((PatentDto patent, ref ICollection<ValidationFailure> error) => { }));


            var patentLogic = new PatentLogic(mockDao.Object, mockValidator.Object);

            patentLogic.Add(new PatentDto(), out ICollection<ValidationFailure> error);

            Assert.False(error.Any());
        }
        [Test]
        public void Add_AddingPatentNotWithCorrectlyData_NotCorrectly()
        {
            var mockDao = new Mock<IPatentDao>();

            var mockValidator = new Mock<IValidator<PatentDto>>();

            var a = mockValidator.Setup(x => x.Validate(It.IsAny<PatentDto>()))
                               .Returns(new ValidationResult(new List<ValidationFailure>
                              {
                                    new ValidationFailure("TestField","Test Message")
                              }));

            mockDao.Setup(c => c.Add(It.IsAny<PatentDto>(), ref It.Ref<ICollection<ValidationFailure>>.IsAny))
           .Callback(new MyActionForAdd((PatentDto patent, ref ICollection<ValidationFailure> error) => { }));

            var patentLogic = new PatentLogic(mockDao.Object, mockValidator.Object);

            patentLogic.Add(new PatentDto(), out ICollection<ValidationFailure> error);

            Assert.True(error.Any());
        }
        [Test]
        public void Add_AddingPatent_ErrorInDao()
        {
            var mockDao = new Mock<IPatentDao>();

            var mockValidator = new Mock<IValidator<PatentDto>>();

            var a = mockValidator.Setup(x => x.Validate(It.IsAny<PatentDto>()))
                              .Returns(new ValidationResult());

            mockDao.Setup(c => c.Add(It.IsAny<PatentDto>(), ref It.Ref<ICollection<ValidationFailure>>.IsAny))
            .Callback(new MyActionForAdd((PatentDto patent, ref ICollection<ValidationFailure> error) =>
            {
                error.Add(new ValidationFailure("TestField", "Test Message"));
            }));

            var patentLogic = new PatentLogic(mockDao.Object, mockValidator.Object);

            patentLogic.Add(new PatentDto(), out ICollection<ValidationFailure> error);

            Assert.True(error.Any());
        }
        [Test]
        public void Remove_RemovalPatent_Correctly()
        {
            var mockDao = new Mock<IPatentDao>();

            mockDao.Setup(c => c.Remove(It.IsAny<int>(), ref It.Ref<ICollection<ValidationFailure>>.IsAny))
            .Callback(new MyActionForRemove((int id, ref ICollection<ValidationFailure> error) =>
            { }));

            var patentLogic = new PatentLogic(mockDao.Object, new ValidationPatent());

            patentLogic.Remove(1, out ICollection<ValidationFailure> error);

            Assert.False(error.Any());
        }
        [Test]
        public void Remove_RemovalPatent_ErrorInDao()
        {
            var mockDao = new Mock<IPatentDao>();

            mockDao.Setup(c => c.Remove(It.IsAny<int>(), ref It.Ref<ICollection<ValidationFailure>>.IsAny))
            .Callback(new MyActionForRemove((int id, ref ICollection<ValidationFailure> error) =>
            {
                error.Add(new ValidationFailure("TestField", "Test Message"));
            }));

            var patentLogic = new PatentLogic(mockDao.Object, new ValidationPatent());

            patentLogic.Remove(1, out ICollection<ValidationFailure> error);

            Assert.True(error.Any());
        }
        [Test]
        public void GetById_ReceivingPatent_NotNull()
        {
            var mockDao = new Mock<IPatentDao>();

            mockDao.Setup(c => c.GetById(It.IsAny<int>())).Returns((int id) => new PatentDto { Id = 1 });

            var patentLogic = new PatentLogic(mockDao.Object, new ValidationPatent());

            Assert.NotNull(patentLogic.GetById(1));
        }
        [Test]
        public void GetById_ReceivingPatent_Null()
        {
            var mockDao = new Mock<IPatentDao>();

            mockDao.Setup(c => c.GetById(It.IsAny<int>())).Returns((int id) => null);

            var patentLogic = new PatentLogic(mockDao.Object, new ValidationPatent());

            Assert.IsNull(patentLogic.GetById(1));
        }
        [Test]
        public void GetAll_ReceivingPatents_NotEmptyList()
        {
            var mockDao = new Mock<IPatentDao>();

            mockDao.Setup(c => c.GetAll()).Returns(() => new List<PatentDto> { new PatentDto() });

            var patentLogic = new PatentLogic(mockDao.Object, new ValidationPatent());

            Assert.IsNotEmpty(patentLogic.GetAll());
        }
        [Test]
        public void GetAll_ReceivingPatents_EmptyList()
        {
            var mockDao = new Mock<IPatentDao>();

            mockDao.Setup(c => c.GetAll()).Returns(() => new List<PatentDto> { });

            var patentLogic = new PatentLogic(mockDao.Object, new ValidationPatent());

            Assert.IsEmpty(patentLogic.GetAll());
        }
        [Test]
        public void FindByTitle_RecervingPatent_NotNull()
        {
            var mockDao = new Mock<IPatentDao>();

            mockDao.Setup(c => c.FindByTitle(It.IsAny<string>())).Returns((string title) => new PatentDto());

            var patentLogic = new PatentLogic(mockDao.Object, new ValidationPatent());

            Assert.IsNotNull(patentLogic.FindByTitle("test"));
        }
        [Test]
        public void FindByTitle_RecervingPatent_Null()
        {
            var mockDao = new Mock<IPatentDao>();

            mockDao.Setup(c => c.FindByTitle(It.IsAny<string>())).Returns((string title) => null);

            var patentLogic = new PatentLogic(mockDao.Object, new ValidationPatent());

            Assert.IsNull(patentLogic.FindByTitle("test"));
        }
        [Test]
        public void SortedByYearForward_RecervingPatentSorted_NotEmpty()
        {
            var mockDao = new Mock<IPatentDao>();

            mockDao.Setup(c => c.SortedByYearForward()).Returns(() => new List<PatentDto>()
            {
                new PatentDto(),
                new PatentDto()
            });

            var patentLogic = new PatentLogic(mockDao.Object, new ValidationPatent());

            Assert.IsNotEmpty(patentLogic.SortedByYearForward());
        }
        [Test]
        public void SortedByYearForward_RecervingPatentSorted_Empty()
        {
            var mockDao = new Mock<IPatentDao>();

            mockDao.Setup(c => c.SortedByYearForward()).Returns(() => new List<PatentDto>());

            var patentLogic = new PatentLogic(mockDao.Object, new ValidationPatent());

            Assert.IsEmpty(patentLogic.SortedByYearForward());
        }
        [Test]
        public void SortedByYearReverse_RecervingPatentSorted_NotEmpty()
        {
            var mockDao = new Mock<IPatentDao>();

            mockDao.Setup(c => c.SortedByYearForward()).Returns(() => new List<PatentDto>()
            {
                new PatentDto(),
                new PatentDto(),
            });

            var patentLogic = new PatentLogic(mockDao.Object, new ValidationPatent());

            Assert.IsNotEmpty(patentLogic.SortedByYearForward());
        }
        [Test]
        public void SortedByYearReverse_RecervingPatentSorted_Empty()
        {
            var mockDao = new Mock<IPatentDao>();

            mockDao.Setup(c => c.SortedByYearReverse()).Returns(() => new List<PatentDto>());

            var patentLogic = new PatentLogic(mockDao.Object, new ValidationPatent());

            Assert.IsEmpty(patentLogic.SortedByYearReverse());
        }
        [Test]
        public void GroupByYear_RecervingPatents_NotEmpty()
        {
            var mockDao = new Mock<IPatentDao>();

            mockDao.Setup(c => c.GroupByYear()).Returns(() => new List<PatentDto>()
            {
                new PatentDto { ReliseDate = new DateTime(2012,12,12)},
                new PatentDto { ReliseDate = new DateTime(2013,12,12)},
                new PatentDto { ReliseDate = new DateTime(2014,12,12)},
            }.Where(p => p is PatentDto)
            .Cast<PatentDto>()
            .ToLookup(p => p.ReliseDate.Year));

            var patentLogic = new PatentLogic(mockDao.Object, new ValidationPatent());

            Assert.True(patentLogic.GroupByYear().Any());
        }
        [Test]
        public void GroupByYear_RecervingPatents_Empty()
        {
            var mockDao = new Mock<IPatentDao>();

            mockDao.Setup(c => c.GroupByYear()).Returns(() => new List<PatentDto>()
            .Where(p => p is PatentDto)
            .Cast<PatentDto>()
            .ToLookup(p => p.ReliseDate.Year));

            var patentLogic = new PatentLogic(mockDao.Object, new ValidationPatent());

            Assert.False(patentLogic.GroupByYear().Any());
        }
    }
}
