using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetEntityWithSpecification(ISpecifications<T> specifications);
        Task<IReadOnlyList<T>> ListEntityWithSpecificationAsync(ISpecifications<T> specifications);
        Task<int> CountAsync(ISpecifications<T> specifications);
    }
}