using Android.App;
using Android.OS;
using Android.Support.V7.App;

namespace SimpleScreenLocker
{
    [Activity(Label = "DialogWidgetActivity", Theme = "@style/AppCompatDialog")]
    public class DialogWidgetActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
            builder.SetTitle("기기 관리자로 설정하시겠습니까?");
            builder.SetMessage("화면 잠금을 위해 기기 관리자로 설정해야 합니다.\n삭제하실 때는 기기 관리자를 해제하셔야합니다.");
            builder.SetPositiveButton("예", (senderAlert, args) =>
            {
                LockHandler lh = new LockHandler();
                lh.SetAsDeviceAdmin(this);
                Finish();
            });
            builder.SetNegativeButton("아니오", (senderAlert, args) =>
            {
                Finish();
            });
            Dialog dialog = builder.Create();
            dialog.Show();
        }
    }

}