using Microsoft.EntityFrameworkCore;
using TP1.Context;
using TP1.Models;
using TP1.ReponseExceptions;

namespace TP1.Services
{
    public interface IGenericService<T> where T : BaseEntity
    {
        public IEnumerable<T> FindAll(int? page, int? offset);
        public T? FindById(int id);
        public T? Create(T creationDto);
        public T? Update(T updateDto);
        public T? Delete(int id);

    }
    public class GenericService<T>: IGenericService<T> where T : BaseEntity
    {
        protected readonly MovieDbContext _context;
        protected readonly DbSet<T> _repository;

        public GenericService(MovieDbContext context)
        {
            _context = context;
            _repository = _context.Set<T>();
        }

        public IEnumerable<T> FindAll(int? page, int? offset)
        {
            var allElements = _repository.ToList();
            if(page != null)
            {
                if(offset == null)
                {
                    throw new ArgumentNullException(nameof(offset));
                }
                allElements = allElements.Skip((page-1) * offset ?? 0).Take(offset ?? 10).ToList();
            }
            return allElements;

        }


        public T? FindById(int id)
        {
            return _repository.Find(id);
        }

        public T? Create(T creationDto)
        {
            var created = _repository.Add(creationDto);
            _context.SaveChanges();
            return _repository.FirstOrDefault(el => el.Id == creationDto.Id);
        }

        public T? Update(T updateDto)
        {
            var updated = _repository.Update(updateDto);
            _context.SaveChanges();
            return _repository.FirstOrDefault(el => el.Id == updateDto.Id);
        }

        public T? Delete(int id)
        {
            var entity = _repository.Find(id);
            if(entity == null)
            {
                throw new NotFoundException($"{typeof(T)} Not Found");
            }
            else
            {
                _repository.Remove(entity);
            }
            return entity;
        }
    }
}
