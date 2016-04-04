using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using GOGameLogic;
using GOContracts;
using System.Text;
using System.Threading.Tasks;

namespace GOServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost servHost = null;

            //Code Taken From Cards example
            try
            {
                servHost = new ServiceHost(typeof(GOContracts.IGame));

                servHost.Open();
                Console.WriteLine("Game service started. Press a key to quit.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadKey();
                servHost?.Close();
            }
        }
    }
}
