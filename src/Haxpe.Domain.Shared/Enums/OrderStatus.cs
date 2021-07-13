namespace Haxpe.Orders
{
    public enum OrderStatus
    {
        Draft = 1,
        Canceled = 9,
        Created = 2,
        Reserved = 3,
        InProgress = 4,
        Paused = 10,
        Completed = 5,
        SystemError = 6,
        RejectByCustomer = 7,
        RejectByWorker = 8
    }
}