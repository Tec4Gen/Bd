using System;
using System.Collections.Generic;

namespace Epam.Library.Entities
{
    public class BookDto: AbstractPrintedProducts
    {
        public IEnumerable<AuthorDto> Authors { get; set; }
        public string City { get; set; }
        public string ISBN { get; set; }
        public string PublishingHouse { get; set; }
    }

}
