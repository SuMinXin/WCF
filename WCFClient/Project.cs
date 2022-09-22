using System;
using System.ServiceModel;
using WCFClient.ServiceReference;

namespace WCFClient
{
    class Project
    {
        static void Main()
        {
            // Create the binding.
            WSHttpBinding myBinding = new WSHttpBinding();
            myBinding.Security.Mode = SecurityMode.Message;
            myBinding.Security.Message.ClientCredentialType =
                MessageCredentialType.UserName;

            // Create the client.
            ServiceClient client = new ServiceClient();

            // Set the user name and password. The code to
            // return the user name and password is not shown here. Use
            // an interface to query the user for the information.
            client.ClientCredentials.UserName.UserName = ReadData("account");
            client.ClientCredentials.UserName.Password = ReadData("password");

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
        static string ReadData(string value)
        {
            Console.WriteLine($"Please enter your {value}:");
            string data = Console.ReadLine();
            if (string.IsNullOrEmpty(data))
            {
                Console.WriteLine($"{value} Required.");
                return ReadData(value);
            }
            return data;
        }
    }
}
