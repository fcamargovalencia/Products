namespace Products.ViewModels
{
    using Products.Models;
    using System.Collections.ObjectModel;

    public class CategoriesViewModel
    {
        #region Properties
        public ObservableCollection<Category> Categories { get; set; }
        #endregion

        #region Constructors
        public CategoriesViewModel()
        {
            LoadCategories();
        }
        #endregion

        #region Methods
        private void LoadCategories()
        {

        }
        #endregion
    }
}
