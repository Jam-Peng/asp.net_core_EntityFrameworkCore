namespace WalksUI.Models
{
	public class CreateRegionViewModel
	{
		public required string Code { get; set; }

		public required string Name { get; set; }

		public string? RegionImageUrl { get; set; }
	}
}
