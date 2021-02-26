using Epam.Library.BLL.Interface;
using Epam.Library.BLL.Validations;
using Epam.Library.DAL.Interface;
using Epam.Library.Entities;
using Epam.Library.Exceprions;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Library.BLL
{
    public class NewspaperLogic : INewspaperLogic
    {
        private INewspaperDao _newspaperDao;
        private IValidator<NewspaperDto> _validationNewspaper;

        public NewspaperLogic(INewspaperDao newspaperDao, IValidator<NewspaperDto> validationNewspaper)
        {
            _newspaperDao = newspaperDao;
            _validationNewspaper = validationNewspaper;
        }

        public void Add(NewspaperDto newspaper, out ICollection<ValidationFailure> errorList)
        {
            if (!_validationNewspaper.Validate(newspaper).IsValid)
            {
                errorList = _validationNewspaper.Validate(newspaper).Errors;
                return;
            }

            errorList = new List<ValidationFailure>();
            _newspaperDao.Add(newspaper, ref errorList);
        }

        public NewspaperDto FindByTitle(string title)
        {
            return _newspaperDao.FindByTitle(title);
        }

        public IEnumerable<NewspaperDto> GetAll()
        {
            foreach (var newspaper in _newspaperDao.GetAll())
            {
                yield return newspaper;
            };
        }

        public NewspaperDto GetById(int id)
        {
            return _newspaperDao.GetById(id);
        }

        public ILookup<int, NewspaperDto> GroupByYear()
        {
            return _newspaperDao.GroupByYear();
        }

        public void Remove(int id, out ICollection<ValidationFailure> errorList)
        {
            errorList = new List<ValidationFailure>();

            _newspaperDao.Remove(id, ref errorList);  
        }

        public IEnumerable<NewspaperDto> SortedByYearForward()
        {
            foreach (var newspaper in _newspaperDao.SortedByYearForward())
            {
                yield return newspaper;
            }
        }

        public IEnumerable<NewspaperDto> SortedByYearReverse()
        {
            foreach (var newspaper in _newspaperDao.SortedByYearForward())
            {
                yield return newspaper;
            }
        }

        public void Update(NewspaperDto book, out ICollection<ValidationFailure> errorList)
        {
            throw new NotImplementedException();
        }

    }
}
