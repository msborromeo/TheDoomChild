namespace DChild.Gameplay.UI.Alerts
{
    public abstract class UIAlertRecorder<T>
    {
        public abstract void RecordNewNotification(T data, bool hasNewInfo = true);

        public abstract bool HasNewNotification(T data);

        public abstract bool HasAnyNewNotification();
    }
}