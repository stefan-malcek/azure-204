using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace blobproperties
{
    class Program
    {
        static string connstring = "";
        static string containerName = "data";
        static string filename = "sample.txt";
        static void Main(string[] args)
        {
            GetProperties();
            SetMetadata();
            GetMetadata();
            
            Console.WriteLine("Operation complete");
            Console.ReadKey();
        }

        static void GetProperties()
        {
            
            BlobServiceClient blobServiceClient = new BlobServiceClient(connstring);
            
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            
            BlobClient blob = containerClient.GetBlobClient(filename);

            
            BlobProperties properties=blob.GetProperties();
            Console.WriteLine("The Access tier of the blob is {0}",properties.AccessTier);
            Console.WriteLine("The Content Length of the blob is {0}", properties.ContentLength);

            
        }

        static void GetMetadata()
        {
            
            BlobServiceClient blobServiceClient = new BlobServiceClient(connstring);
              
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            
            BlobClient blob = containerClient.GetBlobClient(filename);
                        
            BlobProperties properties = blob.GetProperties();

                        foreach(var metadata in properties.Metadata)
            {
                Console.WriteLine(metadata.Key.ToString());
                Console.WriteLine(metadata.Value.ToString());
            }
        }

        static void SetMetadata()
        {
            string p_key = "ApplicationType";
            string p_value = "Ecommerce";

            BlobServiceClient blobServiceClient = new BlobServiceClient(connstring);
            
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            
            BlobClient blob = containerClient.GetBlobClient(filename);
               
            IDictionary<string, string> obj = new Dictionary<string, string>();
            obj.Add(p_key, p_value);
            blob.SetMetadata(obj);
        }

    }
}
