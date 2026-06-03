using Core.Models;

namespace Core.Services
{
    public interface ICollectionCenterService
    {
        Task AddCollectionCenter(CollectionCenter collectionCenter);
        Task<CollectionCenter> GetCollectionCenterById(long id);
        Task<List<CollectionCenter>> GetCollectionCenters();
        Task UpdateCollectionCenter(CollectionCenter collectionCenter);
    }
}
