using Core.Models;
using Core.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace Frontend.ViewModels
{

    public class CollectionCenterViewModel : INotifyPropertyChanged
    {
        private readonly ICollectionCenterService _service;

        public ObservableCollection<CollectionCenter> CollectionCenters { get; set; }
            = new ObservableCollection<CollectionCenter>();

        public CollectionCenter FormModel { get; set; } = new CollectionCenter();

        private bool isEditMode;
        public bool IsEditMode
        {
            get => isEditMode;
            set
            {
                isEditMode = value;
                OnPropertyChanged(nameof(IsEditMode));
            }
        }

        private bool isPaneOpen;
        public bool IsPaneOpen
        {
            get => isPaneOpen;
            set
            {
                isPaneOpen = value;
                OnPropertyChanged(nameof(IsPaneOpen));
            }
        }

        public ICommand OpenAddCommand { get; }
        public ICommand OpenEditCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        public CollectionCenterViewModel(ICollectionCenterService service)
        {
            _service = service;

            OpenAddCommand = new RelayCommand<CollectionCenter>(async c => OpenAdd());
            OpenEditCommand = new RelayCommand<CollectionCenter>(async c => OpenEdit(c));
            SaveCommand = new RelayCommand<CollectionCenter>(async c => await Save());
            DeleteCommand = new RelayCommand<CollectionCenter>(async c => await Delete(c));
        }

        private void OpenAdd()
        {
            FormModel = new CollectionCenter();
            IsEditMode = false;
            IsPaneOpen = true;
            OnPropertyChanged(nameof(FormModel));
        }

        private void OpenEdit(CollectionCenter center)
        {
            FormModel = center;
            IsEditMode = true;
            IsPaneOpen = true;
            OnPropertyChanged(nameof(FormModel));
        }

        private async Task Save()
        {

            if (string.IsNullOrEmpty(FormModel.Name))
            {
                await ShowMessageDialogAsync("Validation Error", "Please input the collection center name!");
                return;
            } else if (string.IsNullOrEmpty(FormModel.ManagerName))
            {
                await ShowMessageDialogAsync("Validation Error", "Please input manager");
                return;
            }

            if (IsEditMode)
            {
                await _service.UpdateCollectionCenter(FormModel);
            }
            else
            {
                await _service.AddCollectionCenter(FormModel);
            }

            await Load();
            IsPaneOpen = false;
        }

        private async Task Delete(CollectionCenter center)
        {
            await _service.Delete(center.Id);
            CollectionCenters.Remove(center);
        }

        public async Task Load()
        {
            var data = await _service.GetCollectionCenters();

            CollectionCenters.Clear();
            foreach (var item in data)
                CollectionCenters.Add(item);
        }

        public async Task LoadById(long id)
        {
            var center = await _service.GetCollectionCenterById(id);
            if (center != null)
            {
                FormModel = center;
                OnPropertyChanged(nameof(FormModel));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private async Task ShowMessageDialogAsync(string title, string content)
        {
            ContentDialog errorDialog = new ContentDialog
            {
                Title = title,
                Content = content,
                CloseButtonText = "OK",
                DefaultButton = ContentDialogButton.Close
            };

            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.Xaml.Controls.ContentDialog"))
            {
                await errorDialog.ShowAsync();
            }
        }
    }
}