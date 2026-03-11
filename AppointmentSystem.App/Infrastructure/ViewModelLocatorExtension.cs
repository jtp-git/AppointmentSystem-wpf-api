using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace AppointmentSystem.App.Infrastructure
{
    public class ViewModelLocatorExtension : MarkupExtension
    {
        public Type? ViewModelType { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var locator = App.Services.GetRequiredService<ViewModelLocator>();
            return locator.GetRequiredViewModel(ViewModelType);
        }

    }
}
