using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderToBitrix.Bitrix
{
   public class Bitrix
    {
        public static string BitrixName { get; set; }
        public static string BitrixPass { get; set; }
        public static string DepSale { get; set; }
        public static string DepFas { get; set; }
        public static string DepStone { get; set; }
        public static string StaffSale { get; set; }
        public static string StaffFas { get; set; }
        public static string StaffStone { get; set; }
        public static string Direct1 { get; set; }
        public static string Direct2 { get; set; }
        public static string Direct3 { get; set; }

        public static void Init()
        {
            INIManager BTX = new INIManager("c:\\config.ini");
            BitrixName = BTX.GetPrivateString("BTX", "logPD");
            BitrixPass = BTX.GetPrivateString("BTX", "pasPD");
            DepSale = BTX.GetPrivateString("BTX", "DepSale");
            DepFas = BTX.GetPrivateString("BTX", "DepFas");
            DepStone = BTX.GetPrivateString("BTX", "DepStone");
            StaffSale = BTX.GetPrivateString("BTX", "StaffSale");
            StaffFas = BTX.GetPrivateString("BTX", "StaffFas");
            StaffStone = BTX.GetPrivateString("BTX", "StaffStone");
            Direct1 = BTX.GetPrivateString("BTX", "Direct1");
            Direct2 = BTX.GetPrivateString("BTX", "Direct2");
            Direct3 = BTX.GetPrivateString("BTX", "Direct3");

        }

    }
}
