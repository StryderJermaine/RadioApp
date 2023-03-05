using CommunityToolkit.Mvvm.Messaging.Messages;
using RadioApp.Models;

namespace RadioApp.Messages;

public class AddFavouriteStationMessage : ValueChangedMessage<Station>
{
    public AddFavouriteStationMessage(Station value) : base(value)
    {
    }
}