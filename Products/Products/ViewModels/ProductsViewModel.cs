namespace Products.ViewModels
{
    using System.Collections.Generic;
    using Products.Models;
    using System.ComponentModel;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class ProductsViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Attributes
        ObservableCollection<Product> _products;
        private List<Product> productsList;
        #endregion

        #region Properties
        public ObservableCollection<Product> ProductsList
        {
            get
            {
                return _products;
            }
            set
            {
                if (_products != value)
                {
                    _products = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ProductsList)));
                }
            }
        }
        #endregion



        #region Constructors
        public ProductsViewModel(List<Product> products)
        {
            this.productsList = products;
            ProductsList = new ObservableCollection<Product>(products.OrderBy(p => p.Description));
        }
        #endregion

    }
}
