
namespace Companies.Application.Base;

public record Result
{
    public bool IsSuccess => !HasNotifications;
    public bool HasNotifications => Notifications.Count != 0;
    public List<Notification> Notifications { get; set; } = [];

    public static Result Failure(Notification notification)
    {
        return new Result
        {
            Notifications = [notification]
        };
    }

    public static Result Success()
    {
        return new Result();
    }
}

public record Notification
{
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
