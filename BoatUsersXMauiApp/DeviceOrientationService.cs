namespace BoatUsersXMauiApp
{
    public enum DeviceOrientation
    {
        Undefined,
        Landscape,
        Portrait
    }
    public partial class DeviceOrientationService
    {
        public partial DeviceOrientation GetOrientation();
        //{ return DeviceOrientation.Undefined; }
    }
}
