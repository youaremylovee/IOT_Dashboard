using Microsoft.EntityFrameworkCore;
using UI_IOT.Models;

namespace UI_IOT.Repository.ItemRepository
{
	public class ItemRepository
	{
		private readonly ItemContext _context;
		public ItemRepository(ItemContext context)
		{
			_context = context;
		}
		public async Task<Item?> Get(int id)
		{
			var item = await _context.Items.FindAsync(id);
			if(item != null)
			{
				item.ItemInfo = _context.ItemsInfo.Find(item.ID);
			}
            return item;
		}
		public async Task<Item> Add(Item item)
		{
			item.ID = Guid.NewGuid().ToString();
			item.ItemInfo ??= new ItemInfo();
            item.ItemInfo.ItemID = item.ID;
            _context.Items.Add(item);
			_context.ItemsInfo.Add(item.ItemInfo);
            await _context.SaveChangesAsync();
			return item;
        }
		public async Task<IEnumerable<Item>> Filter(ItemFilter filter)
		{
			var query = _context.Items.AsQueryable();
			if (!string.IsNullOrEmpty(filter.Status) && !filter.Status.Equals("all", StringComparison.OrdinalIgnoreCase))
			{
				query = query.Where(x => x.Status.Contains(filter.Status));
			}
			if (filter.From.HasValue)
			{
				query = query.Where(x => x.Time >= filter.From);
			}
			if (filter.To.HasValue)
			{
				query = query.Where(x => x.Time <= filter.To);
			}
			var result = await query.ToListAsync();
			foreach(var item in result)
			{
                item.ItemInfo = _context.ItemsInfo.Find(item.ID);
            }
			return result;
		}
	}
}
