using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace WCFServiceApp
{
    public class Service : IService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public string GetCallerIdentity()
        {
            // The client certificate is not mapped to a Windows identity by default.
            // ServiceSecurityContext.PrimaryIdentity is populated based on the information
            // in the certificate that the client used to authenticate itself to the service.
            return ServiceSecurityContext.Current.PrimaryIdentity.Name;
        }

        static void Main()
        {

            // Create the binding.
            WSHttpBinding binding = new WSHttpBinding(SecurityMode.Message);
            binding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;

            // Create the URI for the endpoint.
            Uri httpUri = new Uri("http://localhost:60366/Service");

            // Create the service host.
            ServiceHost myServiceHost =
                new ServiceHost(typeof(Service), httpUri);
            myServiceHost.AddServiceEndpoint(typeof(IService), binding, "Service");

            ServiceMetadataBehavior sm = new ServiceMetadataBehavior();
            sm.HttpGetEnabled = true;
            myServiceHost.Description.Behaviors.Add(sm);

            // Specify a certificate to authenticate the service.
            myServiceHost.Credentials.ServiceCertificate.
                SetCertificate(StoreLocation.LocalMachine,
                StoreName.My,
                X509FindType.FindBySubjectName,
                "localhost");

            myServiceHost.Open();
            Console.WriteLine("Listening...");
            Console.ReadLine();

            // Close the service.
            myServiceHost.Close();



            /*
            var userNameBinding = new WSHttpBinding();
            userNameBinding.Security.Mode = SecurityMode.Message;
            userNameBinding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;
            ServiceHost svcHost = new ServiceHost(typeof(IService));
            svcHost.AddServiceEndpoint(typeof(IService), userNameBinding, "");

            
            // WSHttpBinding binding = new WSHttpBinding();
            // binding.Name = "binding1";
            // binding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
            // binding.Security.Mode = SecurityMode.Message;
            // binding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;
            // binding.ReliableSession.Enabled = false;
            // binding.TransactionFlow = false;
            // //Specify a base address for the service endpoint.
            // //  http://localhost:60366/Service.svc
            // Uri baseAddress = new Uri(@"http://localhost:8000/Service");
            // Create a ServiceHost for the CalculatorService type
            // and provide it with a base address.
            // ServiceHost serviceHost = new ServiceHost(typeof(IService), baseAddress);
            // serviceHost.AddServiceEndpoint(typeof(IService), binding, "");
            
// 
            // Open the ServiceHostBase to create listeners
            // and start listening for messages.
            svcHost.Open();
            // The service can now be accessed.
            // Console.WriteLine("The service is ready.");
            // Console.WriteLine("Press <ENTER> to terminate service.");
            // Console.WriteLine(); Console.ReadLine();
            // // Close the ServiceHost to shutdown the service.
            // svcHost.Close();
            */
        }
    }
}
