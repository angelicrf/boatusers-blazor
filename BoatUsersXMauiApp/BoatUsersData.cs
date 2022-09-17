namespace BoatUsersXMauiApp
{
    public enum DeviceOrientation
    {
        Undefined,
        Landscape,
        Portrait
    }
    public partial class BoatUsersData
    {
        public partial DeviceOrientation GetOrientation();
        //{ return DeviceOrientation.Undefined; }
    }
}
