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
        /// <summary>
        /// Initialize Service Host
        /// Implemented From Cards example
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ServiceHost servHost = null;

            
            try
            {
                servHost = new ServiceHost(typeof(GOGameLogic.GoFish));
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
