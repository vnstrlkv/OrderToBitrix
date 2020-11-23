using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderToBitrix.Bitrix
{
    class Bitrix
    {
        public static async void Start(Order currentOrder)
        {

            string TaskID = await AddTaskToBitrix();
            Console.WriteLine(DateTime.Now + ": " + "Готово");
        }

        private static async Task<string> AddTaskToBitrix()
        {
            return null;
        }

        private static async Task<string> GetIdTaskFromBitrix()
        {
            return null;
        }
        private static async Task<string> GetIdStaffFromBitrix()
        {
            return null;
        }
        private static async Task<string> AddCheklistToBitrixTask()
        {
            return null;
        }
        private static async Task<string> GetCheklistFromBitrixTask()
        {
            return null;
        }





    }
}
