using Epam.Library.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Epam.Library.BLL.Interface;
using Epam.Library.Entities;
using FluentValidation.Results;

namespace Epam.Library.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = DependenciesResolver.Kernel.Get<IAuthorLogic>();

            a.Add(new AuthorDto
            {
                FirstName = "Robert",
                LastName = "Hainlain"
            }, out ICollection<ValidationFailure> errorList);
            a.Add(new AuthorDto
            {
                FirstName = "Lev",
                LastName = "Tolstoi"
            }, out ICollection<ValidationFailure> errorList1);
            a.Add(new AuthorDto
            {
                FirstName = "Alexandr",
                LastName = "Pushkin"
            }, out ICollection<ValidationFailure> errorList2);
           
            Console.WriteLine();
        }
    }
}
