/*Task1: 
 * Create a plugin that triggers on the "Create" event of a custom entity 
 * and sets a default value for a specific attribute.
 PPS=Post-Operation 
 */

using System;
using Microsoft.Xrm.Sdk;

namespace PlugginPractice
{
     class Task1 : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            try
            {
                ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
                tracingService.Trace("1st"); // debugging

                IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

                IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));

                IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

                tracingService.Trace("2nd");

                // Call the custom method for the logic
                PerformCustomLogic(service, context, tracingService);

                tracingService.Trace("3rd");
            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException("An error occurred: " + ex.Message);
            }
        }

        // Custom method to encapsulate logic
        private void PerformCustomLogic(IOrganizationService service, IPluginExecutionContext context, ITracingService tracingService)
        {
            Entity contact = new Entity("contact");
            contact["jobtitle"] = "sales manager";
            contact.Id = context.PrimaryEntityId;
            service.Update(contact);

            // Additional logic can be added here
        }
    }
}
