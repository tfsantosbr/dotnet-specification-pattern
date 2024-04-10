namespace Companies.Application.Base;

public class Result
{
    public bool IsSucess => !HasNotifications;
    public bool HasNotifications => Notifications.Count != 0;
    public List<Notification> Notifications { get; set; } = [];
}

public class Notification
{
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
