using Microsoft.Extensions.DependencyInjection;

using System.Windows.Markup;

namespace AppointmentSystem.App.Infrastructure
{
    public class ViewModelLocatorExtension : MarkupExtension
    {
        public Type ViewModelType { get; set; } = null!;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var locator = App.Services.GetRequiredService<ViewModelLocator>();
            return locator.GetRequiredViewModel(ViewModelType);
        }

    }
}
