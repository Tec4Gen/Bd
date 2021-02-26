using Epam.Library.Entities;
using System.Collections.Generic;

namespace Epam.Library.FakeDAL
{
    public class DataStore
    {
        public static IDictionary<int, AbstractPrintedProducts> LibraryStorage;

        public static IDictionary<int, AuthorDto> AuthorStorage;
        static DataStore()
        {
            LibraryStorage = new Dictionary<int, AbstractPrintedProducts>();
            AuthorStorage = new Dictionary<int, AuthorDto>();
        }
    }
}
