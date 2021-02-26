using System;
using System.Collections.Generic;

namespace Epam.Library.Entities
{
    public class NewspaperDto: AbstractPrintedProducts { 
    
        public string City { get; set; }
        public string ISSN { get; set; }
        public string PublishingHouse { get; set; }
        public IEnumerable<InstanceNewpaperDto> ListOfEditions { get; set; }

    }
}
