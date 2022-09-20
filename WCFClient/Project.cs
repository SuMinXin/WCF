using System;
using WCFClient.ServiceReference1;

namespace WCFClient
{
    class Project
    {
        static void Main()
        {
            ServiceClient client = new ServiceClient();

            // Use the 'client' variable to call operations on the service.
            string response = client.GetData(123);
            Console.Write(response);
            // Always close the client.
            client.Close();
        }
    }
}
