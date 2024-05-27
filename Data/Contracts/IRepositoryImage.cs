using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IRepositoryImage
    {
        Task<Image> CreateImage(Image image);
        Task<List<Image>> GetAllImages(int? categoryId);
        Task<bool> DeleteImage(int id);

    }
}
