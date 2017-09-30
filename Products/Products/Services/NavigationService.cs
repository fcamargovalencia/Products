﻿namespace Products.Services
{
    using Views;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class NavigationService
    {
        public async Task Navigate(string pageName)
        {
            switch (pageName)
            {
                case "CategoriesView":
                    await Application.Current.MainPage.Navigation.PushAsync(new CategoriesView());
                    break;
                case "ProductsView":
                    await Application.Current.MainPage.Navigation.PushAsync(new CategoriesView());
                    break;
            }

        }
    }
}
