using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp
{
    public class OrderDetails
    {
        public List<OrderDetail> Details;
        public OrderDetails()
        {
            this.Details = new List<OrderDetail>();
        }
        public OrderDetails(List<OrderDetail> details)
        {
            this.Details = details;
        }
        public override string ToString()
        {
            string res = "";
            for(int i=0;i<Details.Count; i++)
            {
                res += Details[i].ToString();
                if(i<Details.Count-1) res += ",";
            }
            return res;
        }
    }
}
