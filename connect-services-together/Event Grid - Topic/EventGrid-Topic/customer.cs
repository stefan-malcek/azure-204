using System;
using System.Collections.Generic;
using System.Text;

namespace EventGrid_Topic
{
    public class customer
    {
        public int customerid { get; set; }
        public string customername { get; set; }

        public customer(int p_id,string p_name)
        {
            customerid = p_id;
            customername = p_name;
        }
    }
}
