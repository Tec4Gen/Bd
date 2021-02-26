using Epam.Library.BLL;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Epam.Library.Ioc;
using Ninject;
using Epam.Library.BLL.Interface;
using Epam.Library.Entities;
using FluentValidation.Results;
using System.Linq;

namespace Epam.Library.Tests.IntegrationsTests
{
    public class LogicAuthorIntegrationTests
    {
        IAuthorLogic _authorLogic;
        [SetUp]
        public void SetUp() 
        {
            _authorLogic = DependenciesResolver.Kernel.Get<IAuthorLogic>();
        }

        [Test]
        public void Add_AddingOneAuhtorWithCorrectlyData_Corretly() 
        {
            _authorLogic.Add(new AuthorDto 
            {
                FirstName = "Alexandr",
                LastName = "Pushkin"
            }, out ICollection<ValidationFailure> errorList);

            Assert.False(errorList.Any());
        }

        [Test]
        public void Add_AddingTwoAuhtorWithCorrectlyData_Corretly()
        {
            _authorLogic.Add(new AuthorDto
            {
                FirstName = "Alexandr",
                LastName = "Pushkin"
            }, out ICollection<ValidationFailure> errorList);

            Assert.False(errorList.Any());

            _authorLogic.Add(new AuthorDto
            {
                FirstName = "Jack",
                LastName = "London"
            },out errorList);

            Assert.False(errorList.Any());
        }
        [Test]
        public void Add_AddingOneAuhtorWithNotValidData_NotCorretly()
        {
            _authorLogic.Add(new AuthorDto
            {
                FirstName = "AlexandAr",
                LastName = "Pushkin-Sadsasad"
            }, out ICollection<ValidationFailure> errorList);

            Assert.True(errorList.Any());
        }

        [Test]
        public void Add_AddingTwoAuhtorWithNotValidData_NotCorretly()
        {
            _authorLogic.Add(new AuthorDto
            {
                FirstName = "Alexandr",
                LastName = "Push'Kin Al'Pacho"
            }, out ICollection<ValidationFailure> errorList);

            Assert.True(errorList.Any());

            _authorLogic.Add(new AuthorDto
            {
                FirstName = "Lev",
                LastName = "Tols-toi"
            }, out errorList);

            Assert.True(errorList.Any());
        }

        [Test]
        public void Remove_RemovalAuhtorIncludedInDataBase_Corretly()
        {
            _authorLogic.Remove(5, out ICollection<ValidationFailure> errorList);

            Assert.False(errorList.Any());
        }
        [Test]
        public void Remove_RemovalAuhtorNotIncludedInDataBase_NotCorretly()
        {
            _authorLogic.Remove(100, out ICollection<ValidationFailure> errorList);

            Assert.True(errorList.Any());
        }
        [Test]
        public void GetById_ReceivingAuhtorById_NotNull()
        {       
            Assert.NotNull(_authorLogic.GetById(2));
        }
        [Test]
        public void GetById_ReceivingAuhtorById_Null()
        {
            Assert.Null(_authorLogic.GetById(100));
        }
        [Test]
        public void GetAll_ReceivingAuhtors_NotEmpty()
        {
            Assert.IsNotEmpty(_authorLogic.GetAll());
        }
        [Test]
        public void GetAll_ReceivingAllOfTheAuthorsBooks_ResultOneBook()
        {
            Assert.AreEqual(2,_authorLogic.FindAllPatentsAndBooks(new AuthorDto 
            { 
                Id = 1
            }).Count());
        }
        [Test]
        public void GetAll_ReceivingAllOfTheAuthorsBooksAndPatent_ResultOneBook()
        {
            Assert.AreEqual(2, _authorLogic.FindAllPatentsAndBooks(new AuthorDto 
            { 
                Id = 1
            }).Count());
        }
    }
}
