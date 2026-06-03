using Core.Models;
using Core.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Frontend.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private readonly IUserService _service;

        public ObservableCollection<User> Users { get; set; }
            = new ObservableCollection<User>();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICommand UpdateUserCommand { get; }

        private User selectedUser;

        public event PropertyChangedEventHandler? PropertyChanged;

        public User SelectedUser
        {
            get => selectedUser;
            set
            {
                selectedUser = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(SelectedUser)));
            }
        }

        public UserViewModel(IUserService service)
        {
            _service = service;
        }

        public async Task LoadUsers()
        {
            var Users = await _service.GetUsers();

            Users.Clear();
            foreach (var f in Users)
                Users.Add(f);
        }

        public async Task AddUser()
        {
            await _service.AddUser(new User
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = Password
            });

            await LoadUsers();
        }

        public async Task UpdateUser()
        {
            await _service.UpdateUser(SelectedUser);
            await LoadUsers();
        }

        public async Task LoadUser(long id)
        {
            SelectedUser = await _service.GetUserById(id);
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
