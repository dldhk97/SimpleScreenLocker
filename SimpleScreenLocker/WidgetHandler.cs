using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.Widget;

namespace SimpleScreenLocker
{
    [BroadcastReceiver(Label = "심플 잠금 위젯")]
    [IntentFilter(new string[] { "android.appwidget.action.APPWIDGET_UPDATE" })]
    [MetaData("android.appwidget.provider", Resource = "@xml/appwidgetprovider")]
    public class WidgetHandler : AppWidgetProvider
    {
        public override void OnUpdate(Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds)
        {
            var me = new ComponentName(context, Java.Lang.Class.FromType(typeof(WidgetHandler)).Name);
            appWidgetManager.UpdateAppWidget(me, BuildRemoteViews(context, appWidgetIds));
        }

        private RemoteViews BuildRemoteViews(Context context, int[] appWidgetIds)
        {
            var widgetView = new RemoteViews(context.PackageName, Resource.Layout.widget_btn);

            RegisterClicks(context, appWidgetIds, widgetView);

            return widgetView;
        }

        private static string _WidgetClickedTag = "WidgetClickedTag";

        private void RegisterClicks(Context context, int[] appWidgetIds, RemoteViews widgetView)
        {
            widgetView.SetOnClickPendingIntent(Resource.Id.widget_btnImage, GetPendingSelfIntent(context, _WidgetClickedTag));
        }

        private PendingIntent GetPendingSelfIntent(Context context, string action)
        {
            var intent = new Intent(context, typeof(WidgetHandler));
            intent.SetAction(action);
            return PendingIntent.GetBroadcast(context, 0, intent, 0);
        }

        public override void OnReceive(Context context, Intent intent)
        {
            base.OnReceive(context, intent);

            // Check if the click is from the "Announcement" button
            if (_WidgetClickedTag.Equals(intent.Action))
            {
                LockHandler lockHandler = new LockHandler();
                lockHandler.Lock(context);
            }
        }
    }
}