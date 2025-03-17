using HotelBookingKata.Exceptions;
namespace HotelBookingKata.Common
{
    public class UseCaseDispatcher : Dispatcher
    {
        private IServiceProvider serviceProvider;

        public UseCaseDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public TResponse Dispatch<TRequest, TResponse>(TRequest request)
        {
            var useCase = serviceProvider.GetService(typeof(UseCase<TRequest, TResponse>));
            if (useCase == null)
            {
                throw new UseCaseNotFoundException();
            }
            return ((UseCase<TRequest, TResponse>)useCase).Execute(request);
        }

        public void Dispatch<TRequest>(TRequest request)
        {
            var useCase = serviceProvider.GetService(typeof(UseCase<TRequest>));
            if (useCase == null)
            {
                throw new UseCaseNotFoundException();
            }
            ((UseCase<TRequest>)useCase).Execute(request);
        }
    }
}
