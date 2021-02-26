using Epam.Library.Entities;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Library.DAL.Interface
{
    public interface INewspaperDao
    {
        void Add(NewspaperDto newspaper, ref ICollection<ValidationFailure> errorList);
        void Remove(int id, ref ICollection<ValidationFailure> errorList);
        void Update(NewspaperDto newspaper, ref ICollection<ValidationFailure> errorList);
        NewspaperDto GetById(int id);
        IEnumerable<NewspaperDto> GetAll();
        NewspaperDto FindByTitle(string title);
        IEnumerable<NewspaperDto> SortedByYearForward();
        IEnumerable<NewspaperDto> SortedByYearReverse();
        ILookup<int, NewspaperDto> GroupByYear();
    }
}
