using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.FakeDAL
{
    public class AuthorsCompaper : EqualityComparer<AuthorDto>
    {

        public override bool Equals(AuthorDto firstAuthor, AuthorDto secondAuthor)
        {
            if (firstAuthor.Id == secondAuthor.Id)
                return true;
            else
                return false;
        }

        public override int GetHashCode(AuthorDto obj)
        {
            return obj.Id * obj.Id.GetHashCode();
        }
    }
}
