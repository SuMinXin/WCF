using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Security;

namespace WCFServiceApp
{
    public class Service : IService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
        public string Login()
        {
            return string.Format("Success!");
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
            myServiceHost.AddServiceEndpoint(typeof(IService), binding, "");

            myServiceHost.Credentials.UserNameAuthentication.UserNamePasswordValidationMode = UserNamePasswordValidationMode.Custom;
            myServiceHost.Credentials.UserNameAuthentication.CustomUserNamePasswordValidator = new CustomValidator();

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
        }
    }
}
