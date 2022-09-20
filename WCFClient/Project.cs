using System;
using WCFClient.ServiceReference1;

namespace WCFClient
{
    class Project
    {
        static void Main()
        {
            ServiceClient client = new ServiceClient();
            Console.WriteLine("Enter any number you want...");
            // Always close the client.
            int data = ReadKey();
            // Use the 'client' variable to call operations on the service.
            string response = client.GetData(data);
            Console.WriteLine(response);
            Console.Write("Press any key to stop...");
            // Always close the client.
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
