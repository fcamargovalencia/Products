namespace Products.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Input;

    public class CategoriesViewModel : INotifyPropertyChanged
    {

        #region Services
        ApiService apiService;
        DialogService dialogService;
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Attributes
        ObservableCollection<Category> _categories;
        List<Category> categories;
        bool _isRefreshing;
        #endregion

        #region Properties
        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }
            set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsRefreshing)));
                }
            }
        }

        public ObservableCollection<Category> CategoriesList
        {
            get
            {
                return _categories;
            }
            set
            {
                if (_categories != value)
                {
                    _categories = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(CategoriesList)));
                }
            }
        }
        #endregion

        #region Constructors
        public CategoriesViewModel()
        {
            instance = this;
            apiService = new ApiService();
            dialogService = new DialogService();
            LoadCategories();
        }
        #endregion

        #region Singleton
        static CategoriesViewModel instance;

        public static CategoriesViewModel GetInstance()
        {
            if (instance == null)
            {
                return new CategoriesViewModel();
            }
            return instance;
        }
        #endregion

        #region Methods
        async void LoadCategories()
        {
            IsRefreshing = true;
            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.GetList<Category>(
                "http://productsapiapplication.azurewebsites.net",
                "api",
                "categories",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken);

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            categories = (List<Category>)response.Result;
            CategoriesList = new ObservableCollection<Category>(categories.OrderBy(c => c.Description));
            IsRefreshing = false;
        }

        public void AddCategory(Category category)
        {
            IsRefreshing = true;
            categories.Add(category);
            CategoriesList = new ObservableCollection<Category>(categories.OrderBy(c => c.Description));
            IsRefreshing = false;
        }

        public void UpdateCategory(Category category)
        {
            IsRefreshing = true;
            var oldCategory = categories.Where(c => c.CategoryId == category.CategoryId).FirstOrDefault();
            oldCategory = category;
            CategoriesList = new ObservableCollection<Category>(categories.OrderBy(c => c.Description));
            IsRefreshing = false;
        }

        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadCategories);
            }
        }
        #endregion
    }
}
