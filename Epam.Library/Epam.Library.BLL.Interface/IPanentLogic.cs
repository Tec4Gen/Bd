using Epam.Library.Entities;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Library.BLL.Interface
{
    public interface IPatentLogic
    {
        void Add(PatentDto patent, out ICollection<ValidationFailure> errorList);
        void Remove(int id, out ICollection<ValidationFailure> errorList);
        void Update(PatentDto patent, out ICollection<ValidationFailure> errorList);
        PatentDto GetById(int id);
        IEnumerable<PatentDto> GetAll();
        PatentDto FindByTitle(string title);
        IEnumerable<PatentDto> SortedByYearForward();
        IEnumerable<PatentDto> SortedByYearReverse();
        IEnumerable<PatentDto> FindByAuthor(AuthorDto author);
        ILookup<int, PatentDto> GroupByYear();
    }
}
