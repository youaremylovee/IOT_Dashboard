using Microsoft.EntityFrameworkCore;
using UI_IOT.Models;

namespace UI_IOT.Repository.ItemRepository
{
	public class ItemContext: DbContext
	{
		public ItemContext(DbContextOptions options) : base(options)
		{

		}
		public DbSet<Item> Items { get; set; }
		public DbSet<ItemInfo> ItemsInfo { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Item>().ToTable("Item");
			modelBuilder.Entity<ItemInfo>().ToTable("ItemInfo");
		}
	}
}
