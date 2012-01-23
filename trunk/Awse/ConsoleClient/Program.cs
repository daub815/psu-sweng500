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
            //// TODO: Clean up after ourselves
            var client = new AWSECommerceServicePortTypeClient(Properties.Settings.Default.EndpointConfigName);
            client.ChannelFactory.Endpoint.Behaviors.Add(new AmazonSigningEndpointBehavior(Properties.Settings.Default.AwseAccessKey, Properties.Settings.Default.AwseSecretKey));

            // Create an item search for Books with the title of Software Engineering in it
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

                // Required
                AWSAccessKeyId = Properties.Settings.Default.AwseAccessKey,

                // Required
                AssociateTag = Properties.Settings.Default.AssociateTag
            };

            try
            {
                var response = client.ItemSearch(search);
                foreach (var item in response.Items)
                {
                    foreach (var i in item.Item)
                    {
                        Console.WriteLine(i.ItemAttributes.Title);
                    }
                }
            }
            catch (Exception e)
            {
                //// TODO: Need Better Error Handling
                Console.WriteLine(e);
            }

            // Wait for a key press
            Console.ReadLine();
        }
    }
}
