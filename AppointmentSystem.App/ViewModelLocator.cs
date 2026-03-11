using Microsoft.Extensions.DependencyInjection;

namespace AppointmentSystem.App
{
    public class ViewModelLocator
    {
        private readonly IServiceProvider _serviceProvider;

        public ViewModelLocator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public object GetRequiredViewModel(Type type)
        {
            return _serviceProvider.GetRequiredService(type);
        }
    }
}
