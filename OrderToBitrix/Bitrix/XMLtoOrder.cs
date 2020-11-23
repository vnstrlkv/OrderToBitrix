using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace OrderToBitrix.Bitrix
{
  public  class XMLtoOrder
    {
        private Order order;
        public Order CurrentOrder
        {
            get { return order; }
            private set { order = value; }
        }
        
        public Order GetOrder(string path)
        {
            CurrentOrder = null;
            XmlSerializer serializer = new XmlSerializer(typeof(Order));

            StreamReader reader = new StreamReader(path);
            CurrentOrder = (Order)serializer.Deserialize(reader);
            reader.Close();

            return CurrentOrder;
        }


    }
}
