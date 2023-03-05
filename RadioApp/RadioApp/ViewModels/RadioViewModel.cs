using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using RadioApp.Annotations;
using RadioApp.Messages;
using RadioApp.Models;

namespace RadioApp.ViewModels;

public class RadioViewModel : INotifyPropertyChanged, IRecipient<AddFavouriteStationMessage>, IRecipient<RemoveFavouriteStationMessage>
{
    #region Props

    /// <summary>
    /// Prop to indicate the state of the radio
    /// </summary>
    private bool _isTurnedOn;

    public bool IsTurnedOn
    {
        get => _isTurnedOn;
        set
        {
            _isTurnedOn = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// List of <see cref="Station"/>s
    /// </summary>
    private ObservableCollection<Station> _stations = new();

    public ObservableCollection<Station> Stations
    {
        get => _stations;
        set
        {
            _stations = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// List of favourite <see cref="Station"/>s
    /// </summary>
    private ObservableCollection<Station> _favouritesStations = new();

    public ObservableCollection<Station> FavouriteStations
    {
        get => _favouritesStations;
        set
        {
            _favouritesStations = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Station that is being played
    /// </summary>
    private Station _activeStation = new();

    public Station ActiveStation
    {
        get => _activeStation;
        set
        {
            _activeStation = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Commands

    public ICommand SetActiveColor { get; set; }

    #endregion

    #region Functions

    public RadioViewModel()
    {
        Stations = GetSampleStations();

        SetActiveColor = new Command(ExecuteSetActiveColorCommand);

        //WeakReferenceMessenger.Default.Register<AddFavouriteStation>(this, ((recipient, message) =>
        //{
        //    MainThread.BeginInvokeOnMainThread(() =>
        //    {
        //        AddFavouriteStation(message.Value);
        //    });
        //}));

        WeakReferenceMessenger.Default.Register<AddFavouriteStationMessage>(this);

        WeakReferenceMessenger.Default.Register<RemoveFavouriteStationMessage>(this);
    }

    private ObservableCollection<Station> GetSampleStations()
    {
        return new ObservableCollection<Station>
        {
            new()
            {
                Frequency = 88.9,
                IsFavourite = false,
                BorderColor = Colors.LightGray
            },
            new()
            {
                Frequency = 90.5,
                IsFavourite = false,
                BorderColor = Colors.LightGray
            },
            new()
            {
                Frequency = 91.3,
                IsFavourite = false,
                BorderColor = Colors.LightGray
            },
            new()
            {
                Frequency = 97.3,
                IsFavourite = false,
                BorderColor = Colors.LightGray
            },
            new()
            {
                Frequency = 99.7,
                IsFavourite = false,
                BorderColor = Colors.LightGray
            },
            new()
            {
                Frequency = 100.5,
                IsFavourite = false,
                BorderColor = Colors.LightGray
            },
            new()
            {
                Frequency = 104.3,
                IsFavourite = false,
                BorderColor = Colors.LightGray
            },
        };
    }

    #endregion

    bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        if (Object.Equals(storage, value))
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

    public void Receive(AddFavouriteStationMessage message)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            FavouriteStations.Add(message.Value);

            OnPropertyChanged(nameof(FavouriteStations));
        });
    }

    public void Receive(RemoveFavouriteStationMessage message)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            FavouriteStations.Remove(message.Value);

            OnPropertyChanged(nameof(FavouriteStations));
        });
    }

    public void ExecuteSetActiveColorCommand()
    {
        foreach (var station in FavouriteStations)
        {
            if (station.Frequency == ActiveStation.Frequency)
            {
                station.BorderColor = Colors.LightBlue;
            }
            else
            {
                station.BorderColor = Colors.LightGray;
            }
        }

        OnPropertyChanged(nameof(FavouriteStations));
    }
}