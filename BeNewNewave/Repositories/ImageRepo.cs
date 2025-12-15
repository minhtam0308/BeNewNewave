
using BeNewNewave.Data;
using BeNewNewave.Entities;
using BeNewNewave.Interface.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BeNewNewave.Repositories
{
    public class ImageRepo : BaseRepositoryImage<BookImage>, IImageRepository
    {
        public ImageRepo(ImageDBContext imageContext): base(imageContext) { }


    }
}
