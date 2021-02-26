using Epam.Library.BLL;
using Epam.Library.BLL.Interface;
using Epam.Library.DAL.Interface;
using Epam.Library.Entities;
using Epam.Library.FakeDAL;
using FluentValidation;
using Ninject.Modules;
using Epam.Library.BLL.Validations;
namespace Epam.Library.Ioc
{
    public class NinjectBinds : NinjectModule
    {
        public override void Load()
        {
            Bind<IAuthorDao>().To<AuthorDao>();
            Bind<IBookDao>().To<BookDao>();
            Bind<INewspaperDao>().To<NewspaperDao>();
            Bind<IPatentDao>().To<PatentDao>();

            Bind<IValidator<AuthorDto>>().To<ValidationAuthor>();
            Bind<IValidator<BookDto>>().To<ValidationBook>();
            Bind<IValidator<NewspaperDto>>().To<ValidationNewspaper>();
            Bind<IValidator<PatentDto>>().To<ValidationPatent>();


            Bind<IAuthorLogic>().To<AuthorLogic>();
            Bind<IBookLogic>().To<BookLogic>();
            Bind<INewspaperLogic>().To<NewspaperLogic>();
            Bind<IPatentLogic>().To<PatentLogic>();

        }
    }
}
