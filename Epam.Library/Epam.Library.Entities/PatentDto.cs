using System;
using System.Collections.Generic;

namespace Epam.Library.Entities
{
    public class PatentDto : AbstractPrintedProducts
    {
        public IEnumerable<AuthorDto> Authors { get; set; }
        public string Country { get; set; }
        public int RegistrationNumber { get; set; }
        public DateTime? SubmissionDate { get; set; }
    }
}
