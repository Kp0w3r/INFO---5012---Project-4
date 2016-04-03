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
                servHost = new ServiceHost(typeof(GOContracts.IDeck));

                servHost.Open();
                Console.WriteLine("Shoe service started. Press a key to quit.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadKey();
                if (servHost != null)
                    servHost.Close();
            }
        }
    }
}
