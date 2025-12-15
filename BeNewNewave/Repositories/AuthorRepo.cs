using BeNewNewave.Data;
using BeNewNewave.Entities;
using BeNewNewave.Interface.IRepositories;

namespace BeNewNewave.Repositories
{
    public class AuthorRepo : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepo(AppDbContext context) : base(context) { }


    }
}
