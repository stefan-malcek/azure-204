#r "Microsoft.Azure.DocumentDB.Core"
using System;
using System.Collections.Generic;
using Microsoft.Azure.Documents;

public static void Run(IReadOnlyList<Document> input, ILogger log)
{
    if (input != null && input.Count > 0)
    {
        log.LogInformation("Documents modified " + input.Count);
                foreach(var customer_obj in input )
                {
                       int customer_id=customer_obj.GetPropertyValue<int>("customerid") ;
                       string customer_name=customer_obj.GetPropertyValue<string>("customername") ;
                       string customer_city=customer_obj.GetPropertyValue<string>("city") ;

                       log.LogInformation($"The customer id is {customer_id}");
                       log.LogInformation($"The customer name is {customer_name}");
                       log.LogInformation($"The customer city is {customer_city}");
                }
    }
}
