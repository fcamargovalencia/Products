﻿namespace Products.Models
{
    using GalaSoft.MvvmLight.Command;
    using Services;
    using System.Collections.Generic;
    using System.Windows.Input;
    using ViewModels;

    public class Category
    {
        #region Services
        NavigationService navigationService;
        #endregion

        #region Properties
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public List<Product> Products { get; set; }
        #endregion

        #region Constructors
        public Category()
        {
            navigationService = new NavigationService();
        }
        #endregion

        #region Commands
        public ICommand EditCommand
        {
            get
            {
                return new RelayCommand(Edit);
            }
        }

        async void Edit()
        {
            MainViewModel.GetInstance().EditCategory = new EditCategoryViewModel(this);
            await navigationService.Navigate("EditCategoryView");
        }

        public ICommand SelectCategoryCommand
        {
            get
            {
                return new RelayCommand(SelectCategory);
            }
        }

        async void SelectCategory()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Products = new ProductsViewModel(Products);
            await navigationService.Navigate("ProductsView");
        }
        #endregion

        #region Methods
        public override int GetHashCode()
        {
            return CategoryId;
        }
        #endregion
    }
}
