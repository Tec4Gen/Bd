using Epam.Library.DAL.Interface;
using Epam.Library.Entities;
using Epam.Library.Exceprions;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Library.FakeDAL
{
    public class NewspaperDao : INewspaperDao
    {
        public void Add(NewspaperDto newspaper, ref ICollection<ValidationFailure> errorList)
        {
            try
            {
                CheckingForUniqueness(newspaper);

                newspaper.Id = DataStore.LibraryStorage.Keys.LastOrDefault() + 1;
                DataStore.LibraryStorage.Add(newspaper.Id, newspaper);
            }
            catch (ArgumentException)
            {
                errorList.Add(new ValidationFailure(nameof(newspaper), $"Unable to add {nameof(newspaper)}"));
            }
            catch (UniqueIdentifierException ex)
            {
                errorList.Add(new ValidationFailure(nameof(newspaper), ex.Message));
            }
        }

        public NewspaperDto FindByTitle(string title)
        {
            return DataStore.LibraryStorage.Values.FirstOrDefault(p => p is NewspaperDto newspaper && newspaper.Title == title) as NewspaperDto;
        }

        public IEnumerable<NewspaperDto> GetAll()
        {
            foreach (var printedProducts in DataStore.LibraryStorage.Values)
            {
                if (printedProducts is NewspaperDto newspapaero)
                {
                    yield return newspapaero;
                }
            }
        }

        public NewspaperDto GetById(int id)
        {
            return DataStore.LibraryStorage.FirstOrDefault(p=>p.Key == id).Value as NewspaperDto;
        }

        public ILookup<int, NewspaperDto> GroupByYear()
        {
            return DataStore.LibraryStorage.Values
                 .Where(p => p is NewspaperDto)
                 .Cast<NewspaperDto>()
                 .ToLookup(p => p.ReliseDate.Year);
        }

        public void Remove(int id, ref ICollection<ValidationFailure> errorList)
        {
            try
            {
                DataStore.LibraryStorage.Remove(id);
            }
            catch (ArgumentException)
            {
                errorList.Add(new ValidationFailure(nameof(id), "Cannot be found newspaper with such id"));
            }
        }

        public IEnumerable<NewspaperDto> SortedByYearForward()
        {
            return DataStore.LibraryStorage.Values
                .Where(p => p is NewspaperDto).Cast<NewspaperDto>()
                .OrderBy(p=>p.ReliseDate);
        }

        public IEnumerable<NewspaperDto> SortedByYearReverse()
        {
            return DataStore.LibraryStorage.Values
               .Where(p => p is NewspaperDto)
               .Cast<NewspaperDto>()
               .OrderByDescending(p => p.ReliseDate);
        }

        public void Update(NewspaperDto book, ref ICollection<ValidationFailure> errorList)
        {
            throw new NotImplementedException();
        }

        private void CheckingForUniqueness(NewspaperDto newspaper)
        {
            var tempData = DataStore.LibraryStorage.Values.Where(p => p is NewspaperDto).Cast<NewspaperDto>();

            if (tempData != null)
            {
                if (newspaper.ISSN != null && tempData.FirstOrDefault(p => p.ISSN == newspaper.ISSN) != null)
                {
                    throw new UniqueIdentifierException($"A {nameof(newspaper)} with this ISSN already exists");
                }
                else
                {
                    var findNewspapaer = tempData.FirstOrDefault(p=>p.Title == newspaper.Title && p.PublishingHouse == newspaper.PublishingHouse && p.ReliseDate == newspaper.ReliseDate);

                    if (findNewspapaer != null)
                    {
                        throw new UniqueIdentifierException($"A {nameof(newspaper)} with this title, publishing house and date publication already exists");
                    }
                }
            }

        }
    }
}
