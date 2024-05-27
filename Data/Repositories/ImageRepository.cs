using Data.ConexionDB;
using Data.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Shared.Services.Service;

namespace Data.Repositories
{
    public class ImageRepository : IRepositoryImage
    {

        private readonly Ms_configurationContext _context;
        private readonly ILogger _logger;

        public ImageRepository(Ms_configurationContext context, ILogger<ImageRepository> logger )
        {
            _context = context;
            _logger = logger;
        }


        public async Task<Image> CreateImage(Image image)
        {
            try
            {
                _context.Image.Add(image);
                await _context.SaveChangesAsync();
                return image;
            } catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new CustomeException("Ha ocurrido un error al guardar la imagen");
            }
        }

        public async Task<List<Image>> GetAllImages(int? categoryId)
        {
            try
            {
                if(categoryId is null)
                {
                    return await _context.Image.ToListAsync();
                }
                return await _context.Image.Where(img=> img.Id.Equals(categoryId)).ToListAsync();
            } catch (Exception ex)
            {
                _logger?.LogError(ex, ex.Message);
                throw new CustomeException("Ha ocurrido un error al obtener las imagenes");
            }
        }


        public async Task<bool> DeleteImage (int id)
        {
            try
            {
                var img = await _context.Image.FindAsync(id);

                if(img is null)
                {
                    return false;
                }
                _context.Image.Remove(img);
                await _context.SaveChangesAsync();
                return true;

            } catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new CustomeException("Ha ocurrido un error al eliminar la imagen");
            }
        }  
    }
}
