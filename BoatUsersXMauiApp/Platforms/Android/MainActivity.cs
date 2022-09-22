using Android;
using Android.App;
using Android.Bluetooth;
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

        public event EventHandler<ActivityEventArgs> ActivityStateChanged;
        private CreateScanCallBack createcl { get; set; } = new CreateScanCallBack();

        PermissionCheck receiver;

        IntentFilter filter;
        public MainActivity()
        {

        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            PermissionCheck.bluetoothManager = (BluetoothManager)ApplicationContext.GetSystemService(BluetoothService);
            // CreateScanCallBack.thisAdapter = PermissionCheck.bluetoothManager.Adapter;

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
                receiver = new PermissionCheck();
                filter = new IntentFilter(BluetoothDevice.ActionFound);
                filter.AddAction(BluetoothDevice.ActionFound);
                filter.AddAction(BluetoothDevice.ActionBondStateChanged);
                filter.AddAction(BluetoothAdapter.ActionDiscoveryFinished);
                filter.AddAction(BluetoothDevice.ActionUuid);
                filter.Priority = (int)IntentFilterPriority.HighPriority;

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
        protected override void OnResume()
        {
            base.OnResume();
            receiver = new PermissionCheck();
            RegisterReceiver(receiver, filter);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected void OnDestry()
        {
            UnregisterReceiver(receiver);
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