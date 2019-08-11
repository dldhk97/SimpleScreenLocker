using Android.Content;
using Android.App.Admin;
using Android.Widget;
using Android.App;
using Android.Support.Design.Widget;

namespace SimpleScreenLocker
{
    public class LockHandler
    {
        public void SetAsDeviceAdmin(Context context)
        {
            DevicePolicyManager devicePolicyManager = (DevicePolicyManager)context.GetSystemService(Context.DevicePolicyService);
            ComponentName deviceAdmin = new ComponentName(context, Java.Lang.Class.FromType(typeof(DeviceAdmin)));
            Intent intent = new Intent(DevicePolicyManager.ActionAddDeviceAdmin);
            intent.PutExtra(DevicePolicyManager.ExtraDeviceAdmin, deviceAdmin);
            intent.PutExtra(DevicePolicyManager.ExtraAddExplanation, "화면 잠금을 위해 기기 관리자 설정을 필요로 합니다.\n삭제할 때는 기기관리자를 해제해야 삭제가 가능합니다.");
            context.StartActivity(intent);
        }

        public void Lock(Context context)
        {
            DevicePolicyManager devicePolicyManager = (DevicePolicyManager)context.GetSystemService(Context.DevicePolicyService);
            ComponentName deviceAdmin = new ComponentName(context, Java.Lang.Class.FromType(typeof(DeviceAdmin)));
            if (!devicePolicyManager.IsAdminActive(deviceAdmin))
            {
                Intent intent = new Intent(context, typeof(DialogWidgetActivity));
                context.StartActivity(intent);
            }
            else
            {
                devicePolicyManager.LockNow();
            }
        }
    }

    [BroadcastReceiver(Permission = "android.permission.BIND_DEVICE_ADMIN")]
    [MetaData("android.app.device_admin", Resource = "@xml/device_admin_sample")]
    [IntentFilter(new[] { "android.app.action.DEVICE_ADMIN_ENABLED", Intent.ActionMain })]
    public class DeviceAdmin : DeviceAdminReceiver
    {
        public override void OnEnabled(Context context, Intent intent)
        {
            base.OnEnabled(context, intent);
            Toast.MakeText(context, "기기 관리자 권한이 설정되었습니다.", ToastLength.Short).Show();
            Toast.MakeText(context, "삭제시 반드시 기기 관리자를 해제한 후 삭제해주세요.", ToastLength.Long).Show();
        }
        public override void OnDisabled(Context context, Intent intent)
        {
            base.OnDisabled(context, intent);
            Toast.MakeText(context, "기기 관리자 관리자 권한이 해제되었습니다.", ToastLength.Short).Show();
        }
    }
}