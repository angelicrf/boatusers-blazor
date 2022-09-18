using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using Plugin.CurrentActivity;
using Plugin.Permissions;
using System.Diagnostics;

namespace BoatUsersXMauiApp
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        /// <summary>
        /// Gets or sets the activity.
        /// </summary>
        /// <value>The activity.</value>
        public Android.App.Activity Activity { get; set; }

        /// <summary>
        /// Gets the current Application Context.
        /// </summary>
        /// <value>The activity.</value>
        public Context AppContext { get; }

        /// <summary>
        /// Fires when activity state events are fired
        /// </summary>
        public event EventHandler<ActivityEventArgs> ActivityStateChanged;
        public MainActivity() { }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            PermissionCheck.updateActivity(this);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation) == (int)Permission.Granted &&
                ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessCoarseLocation) == (int)Permission.Granted &&
                ContextCompat.CheckSelfPermission(this, Manifest.Permission.BluetoothScan) == (int)Permission.Granted &&
                ContextCompat.CheckSelfPermission(this, Manifest.Permission.BluetoothConnect) == (int)Permission.Granted &&
                ContextCompat.CheckSelfPermission(this, Manifest.Permission.Bluetooth) == (int)Permission.Granted &&
                ContextCompat.CheckSelfPermission(this, Manifest.Permission.BluetoothAdmin) == (int)Permission.Granted)
            {

                Toast.MakeText(this, "Granted", ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(this, "Not Granted", ToastLength.Short).Show();
                // AlertDialog.Builder dialog = new AlertDialog.Builder(this)
                //.SetTitle("Permission Entry")
                //.SetMessage("nOTGranted");
                ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.AccessCoarseLocation,Manifest.Permission.AccessFineLocation ,Manifest.Permission.BluetoothScan,
                Manifest.Permission.BluetoothAdmin, Manifest.Permission.Bluetooth, Manifest.Permission.BluetoothConnect}, 10);

            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public void Register()
        {
            CrossCurrentActivity.Current.ActivityStateChanged += Current_ActivityStateChanged;
        }

        private void Current_ActivityStateChanged(object sender, ActivityEventArgs e)
        {
            Toast.MakeText(this, $"Activity Changed: {e.Activity.LocalClassName} -  {e.Event}", ToastLength.Short).Show();
        }
    }
}