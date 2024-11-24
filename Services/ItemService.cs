using UI_IOT.Models;
using UI_IOT.Repository.ItemRepository;

namespace UI_IOT.Services
{
	public class ItemService
	{
		private readonly ItemRepository _repository;

		public ItemService(ItemRepository repository)
		{
			this._repository = repository;
		}
		public async Task<IEnumerable<Item>> Filter(ItemFilter filter)
		{
			return await _repository.Filter(filter);
		}
		public async Task<Item?> GetItem(int id)
		{
			return await _repository.Get(id);
		}
		public async Task<Item> AddItem(Item item)
		{
            return await _repository.Add(item);
        }
	}
}
