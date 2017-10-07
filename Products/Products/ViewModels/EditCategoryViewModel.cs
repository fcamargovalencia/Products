namespace Products.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Services;
    using System.ComponentModel;
    using System.Windows.Input;

    public class EditCategoryViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        DialogService dialogService;
        ApiService apiService;
        NavigationService navigationService;
        #endregion

        #region Attributes
        bool _isRunning;
        bool _isEnabled;
        Category category;
        #endregion

        #region Properties
        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsRunning)));
                }
            }
        }

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsEnabled)));
                }
            }
        }

        public string Description
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public EditCategoryViewModel(Category category)
        {
            this.category = category;
            dialogService = new DialogService();
            apiService = new ApiService();
            navigationService = new NavigationService();
            Description = category.Description;
            IsEnabled = true;
        }
        #endregion

        #region Commands
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        async void Save()
        {
            if (string.IsNullOrEmpty(Description))
            {
                await dialogService.ShowMessage(
                    "Error",
                    "You must enter a category description");
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            category.Description = Description;

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.Put(
                "http://productsapiapplication.azurewebsites.net",
                "api",
                "Categories",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                category);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            var categoryViewModel = CategoriesViewModel.GetInstance();
            categoryViewModel.UpdateCategory(category);

            await navigationService.Back();

            IsRunning = false;
            IsEnabled = true;
        }
        #endregion
    }
}
