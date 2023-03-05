using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using RadioApp.Annotations;
using RadioApp.Messages;

namespace RadioApp.Models;

/// <summary>
/// Model class for a radio station
/// </summary>
public class Station : INotifyPropertyChanged
{
    /// <summary>
    /// Frequency of the station
    /// </summary>
    public double Frequency { get; set; }

    /// <summary>
    /// Flag to indicate if a station is a favourite or not
    /// </summary>
    public bool IsFavourite { get; set; }

    public Color BorderColor { get; set; }

    public ICommand ToggleFavourite { get; set; }

    public Station()
    {
        ToggleFavourite = new Command(ExecuteToggleFavourite);
    }

    private void ExecuteToggleFavourite()
    {
        IsFavourite = !IsFavourite;

        OnPropertyChanged(nameof(IsFavourite));

        if (IsFavourite)
        {
            WeakReferenceMessenger.Default.Send(new AddFavouriteStationMessage(this));
        }
        else
        {
            WeakReferenceMessenger.Default.Send(new RemoveFavouriteStationMessage(this));
        }
    }

    bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        if (Equals(storage, value))
            return false;

        storage = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}