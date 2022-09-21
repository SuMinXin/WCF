using System;
using WCFClient.ServiceReference1;

namespace WCFClient
{
    class Project
    {
        static void Main()
        {
            ServiceClient client = new ServiceClient();

            // Configure client with valid computer or domain account (username,password).
            client.ClientCredentials.UserName.UserName = "WCF-DEMO";
            client.ClientCredentials.UserName.Password = "WCF-DEMO".ToString();
            
            Console.WriteLine(client.GetCallerIdentity());

            Console.WriteLine("Enter any number you want...");
            int data = ReadKey();
            string response = client.GetData(data);
            Console.WriteLine(response);
            Console.Write("Press any key to stop...");
            // close the client.
            Console.ReadKey();
            client.Close();
        }
        static int ReadKey()
        {
            string data = Console.ReadLine();
            if (string.IsNullOrEmpty(data))
            {
                Console.WriteLine("Data Required");
                return ReadKey();
            }
            int number;
            bool success = int.TryParse(data, out number);
            if (!success)
            {
                Console.WriteLine($"[N.U.M.B.E.R]");
                return ReadKey();
            }
            return number;
        }
    }
}
