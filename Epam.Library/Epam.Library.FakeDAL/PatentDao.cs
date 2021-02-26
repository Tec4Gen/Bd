using Epam.Library.DAL.Interface;
using Epam.Library.Entities;
using System.Collections.Generic;
using System;
using Epam.Library.Exceprions;
using System.Linq;
using FluentValidation.Results;

namespace Epam.Library.FakeDAL
{
    public class PatentDao : IPatentDao
    {
        public void Add(PatentDto patent, ref ICollection<ValidationFailure> errorList)
        {
            try
            {
                CheckingForUniqueness(patent);

                patent.Id = DataStore.LibraryStorage.Keys.LastOrDefault() + 1 ;
                DataStore.LibraryStorage.Add(patent.Id, patent);
            }
            catch (ArgumentException)
            {
                errorList.Add(new ValidationFailure(nameof(patent), "Unable to add {nameof(patent)}"));
            }
            catch(UniqueIdentifierException ex) 
            {
                errorList.Add(new ValidationFailure(nameof(patent), ex.Message));
            }
        }

        public IEnumerable<PatentDto> FindByAuthor(AuthorDto author)
        {
            foreach (var printProduct in DataStore.LibraryStorage.Values)
            {
                if (printProduct is PatentDto patent && patent.Authors.Any(p => p.Id == author.Id))
                {
                    yield return patent;
                }
            };
        }

        public PatentDto FindByTitle(string title)
        {
            return DataStore.LibraryStorage.Values.Where(p=>p is PatentDto patent && patent.Title == title) as PatentDto;
        }

        public IEnumerable<PatentDto> GetAll()
        {
            foreach (var printProduct in DataStore.LibraryStorage.Values)
            {
                if (printProduct is PatentDto patent) 
                {
                    yield return patent;
                }
            };
        }

        public PatentDto GetById(int id)
        {
            return DataStore.LibraryStorage.FirstOrDefault(p=>p.Key == id).Value as PatentDto;
        }

        public ILookup <int, PatentDto> GroupByYear()
        {
            return DataStore.LibraryStorage.Values
                .Where(p=>p is PatentDto)
                .Cast<PatentDto>()
                .ToLookup(p=>p.ReliseDate.Year);
        }

        public void Remove(int id, ref ICollection<ValidationFailure> errorList)
        {
            try
            {
                DataStore.LibraryStorage.Remove(id);
            }
            catch (ArgumentException)
            {
                errorList.Add(new ValidationFailure(nameof(id), "Cannot be found patent with such id"));
            };
        }

        public IEnumerable<PatentDto> SortedByYearForward()
        {
            return DataStore.LibraryStorage.Values
                .Where(p => p is PatentDto)
                .Cast<PatentDto>()
                .OrderBy(p=>p.ReliseDate.Year);
        }

        public IEnumerable<PatentDto> SortedByYearReverse()
        {
            return DataStore.LibraryStorage.Values
                .Where(p => p is PatentDto)
                .Cast<PatentDto>()
                .OrderByDescending(p => p.ReliseDate.Year);
        }

        public void Update(PatentDto patent, ref ICollection<ValidationFailure> errorList)
        {
            throw new NotImplementedException();
        }

        private void CheckingForUniqueness(PatentDto newspaper)
        {
            var tempData = DataStore.LibraryStorage.Values.Where(p => p is PatentDto).Cast<PatentDto>();

            if (tempData != null)
            {
                if (tempData.FirstOrDefault(p => p.RegistrationNumber == newspaper.RegistrationNumber && p.Country == newspaper.Country) != null)
                {
                    throw new UniqueIdentifierException($"A {nameof(newspaper)} with this registration number and country already registered");
                }            
            }

        }
    }
}
