using System.ComponentModel;
using System.Windows;
using Lab1.Models;

namespace Lab1.ViewModels;

public class UserViewModel : BaseBindable
{
    private User _user;

    public UserViewModel()
    {
        User = new User();
    }

    public User User
    {
        get => _user;
        set
        {
            if (UpdateProperty(ref _user, value, nameof(User)))
            {
                User.PropertyChanged += OnUserPropertyChanged;
            }
        }
    }
    
    private void OnBirthdayUpdate() {
        if (User.GetPropertyErrors(nameof(User.BirthDate)) is var errors && errors != null)
        {
            MessageBox.Show(errors.ElementAt(0), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    protected override void PrePropertyUpdated(string propertyName)
    {
        if (propertyName == nameof(Models.User))
        {
            User.PropertyChanged -= OnUserPropertyChanged;
        }
    }

    private void OnUserPropertyChanged(object? sender, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == nameof(User.BirthDate))
            OnBirthdayUpdate();
    }
}
