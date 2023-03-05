using CommunityToolkit.Mvvm.Messaging.Messages;
using RadioApp.Models;

namespace RadioApp.Messages;

public class RemoveFavouriteStationMessage : ValueChangedMessage<Station>
{
    public RemoveFavouriteStationMessage(Station value) : base(value)
    {
    }
}