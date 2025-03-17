namespace HotelBookingKata.Common
{
    public interface UseCase<TRequest, TResponse>
    {
        TResponse Execute(TRequest request);
    }

    public interface UseCase<TRequest>
    {
        void Execute(TRequest request);
    }
}
