using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BoatRazorLibrary.Models;

public class MapFunctions
{
    public ElementReference mapElement;
    public IJSObjectReference? mapModule;
    public IJSObjectReference? mapInstance;
    public string UserAddress { get; set; }
    public MapPlacesModel mapPlaces = new();
    public async Task MarkerPlace()
    {
        if (mapModule is not null && mapInstance is not null)
        {
            await mapModule.InvokeVoidAsync("setMapMarker", mapInstance).AsTask();
        }
    }
    public async Task ShowCurrentLocation()
    {
        if (mapModule is not null && mapInstance is not null)
        {
            mapInstance = await mapModule.InvokeAsync<IJSObjectReference>(
            "currentLocationAddMap", mapElement);
        }
    }

    public async Task FindPlace()
    {
        UserAddress = mapPlaces.Name;
        if (mapModule is not null && mapInstance is not null)
        {

            await mapModule.InvokeVoidAsync("findPlace", mapInstance, UserAddress).AsTask();
        }
    }
    public async Task AllMarkers()
    {

        if (mapModule is not null && mapInstance is not null)
        {

            await mapModule.InvokeVoidAsync("showStoredMarkers", mapInstance).AsTask();
        }
    }
}
