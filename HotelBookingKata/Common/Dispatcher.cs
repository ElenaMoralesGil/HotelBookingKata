namespace HotelBookingKata.Common
{
    public interface Dispatcher
    {
        TResponse Dispatch<TRequest, TResponse>(TRequest request);
        void Dispatch<TRequest>(TRequest request);
    }
}
