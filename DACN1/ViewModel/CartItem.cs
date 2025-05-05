namespace DACN1.ViewModel
{
	public class CartItem
	{
		public int CartItemId { get; set; }
		public int ProductId { get; set; }
		public string? Title { get; set; }
		public int? Quantity { get; set; }
		public decimal? Price { get; set; }
		public string? Image { get; set; }
		public double? PriceTotal => Quantity * (double?)Price;
	}
}
