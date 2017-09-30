namespace Products.Models
{
    using System;

    public class Product
    {
        public int ProductId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastPurchase { get; set; }
        public string Image { get; set; }
        public double Stock { get; set; }
        public string Remarks { get; set; }
        public string ImageFullPath
        {
            get
            {
                String image = "Content/Images/notImage.png";
                if (Image != null)
                {
                    image = Image.Substring(1);
                }
                return string.Format(
                    "http://productsbackendapp.azurewebsites.net/{0}",
                    image);
            }
        }
    }
}
