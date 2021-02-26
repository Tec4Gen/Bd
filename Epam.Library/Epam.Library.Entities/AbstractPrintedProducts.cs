using System;

namespace Epam.Library.Entities
{
    public abstract class AbstractPrintedProducts
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public int NumberOfPages { get; set; }
        public DateTime ReliseDate { get; set; }
    }
}
