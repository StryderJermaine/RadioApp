using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RadioApp.Annotations;
using RadioApp.Models;

namespace RadioApp.ViewModels;

public class RadioViewModel : INotifyPropertyChanged
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
    /// List of stations
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



    #endregion

    #region Functions

    public RadioViewModel()
    {

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
}