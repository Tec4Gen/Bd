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
    public class PatentLogic : IPatentLogic
    {
        private IPatentDao _patentDao;
        private IValidator<PatentDto> _validationPatent;
        public PatentLogic(IPatentDao patentDao, IValidator<PatentDto> validationPatent)
        {
            _patentDao = patentDao;
            _validationPatent = validationPatent;
        }
        public void Add(PatentDto patent, out ICollection<ValidationFailure> errorList)
        {

            if (!_validationPatent.Validate(patent).IsValid)
            {
                errorList = _validationPatent.Validate(patent).Errors;
                return;
            }

            errorList = new List<ValidationFailure>();
            _patentDao.Add(patent, ref errorList);      
        }

        public IEnumerable<PatentDto> FindByAuthor(AuthorDto author)
        {
            foreach (var patent in _patentDao.FindByAuthor(author))
            {
                yield return patent;
            }
        }

        public PatentDto FindByTitle(string title)
        {
            return _patentDao.FindByTitle(title);
        }

        public IEnumerable<PatentDto> GetAll()
        {
            foreach (var patent in _patentDao.GetAll())
            {
                yield return patent;
            }
        }

        public PatentDto GetById(int id)
        {
            return _patentDao.GetById(id); ;
        }

        public ILookup<int, PatentDto> GroupByYear()
        {
            return _patentDao.GroupByYear();
        }

        public void Remove(int id, out ICollection<ValidationFailure> errorList)
        {
            errorList = new List<ValidationFailure>();

            _patentDao.Remove(id, ref errorList);
        }

        public IEnumerable<PatentDto> SortedByYearForward()
        {
            foreach (var patent in _patentDao.SortedByYearForward())
            {
                yield return patent;
            }
        }

        public IEnumerable<PatentDto> SortedByYearReverse()
        {
            foreach (var patent in _patentDao.SortedByYearReverse())
            {
                yield return patent;
            }
        }

        public void Update(PatentDto book, out ICollection<ValidationFailure> errorList)
        {
            throw new NotImplementedException();
        }
    }
}
