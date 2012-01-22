namespace Sweng500.Awse.ConsoleClient
{
    using System;
    using log4net;
    using Sweng500.Awse.CommerceService;

    /// <summary>
    /// The containing class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Class logger
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Main Entry Point
        /// </summary>
        /// <param name="args">Arguments from command line</param>
        protected static void Main(string[] args)
        {
            var client = new AWSECommerceServicePortTypeClient(Properties.Settings.Default.EndpointConfigName);

            var search = new ItemSearch
            {
                Request = new ItemSearchRequest[] 
                {
                    new ItemSearchRequest
                    {
                        SearchIndex = "Books",
                        Title = "Software Engineering",
                        ResponseGroup = new string[]
                        {
                            "Small"
                        }
                    }
                },
                AWSAccessKeyId = Properties.Settings.Default.AwseAccessKey,
                AssociateTag = string.Empty
            };

            var response = client.ItemSearch(search);
            foreach (var item in response.Items)
            {
                foreach (var i in item.Item)
                {
                    Console.WriteLine(i.ItemAttributes.Title);
                }
            }

            // Wait for a key press
            Console.ReadLine();
        }
    }
}
