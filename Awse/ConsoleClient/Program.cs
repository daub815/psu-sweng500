namespace Sweng500.Awse.ConsoleClient
{
    using System;
    using System.Collections.Generic;
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
                        Author = "Erich Gamma",
////                        SearchIndex = "DVD",
////                        Title = "Inglourious Basterds",

                        Keywords = string.Empty,
                        ResponseGroup = new string[]
                        {
                        // "ItemAttributes"
                        "Medium"
                        }
                    }
                },

                // Required
                AWSAccessKeyId = Properties.Settings.Default.AwseAccessKey,

                // Required to be valid or string.Empty
                AssociateTag = string.Empty
            };

            try
            {
                var response = client.ItemSearch(search);

                Items info = response.Items[0];
                Item[] items = info.Item;

                for (int i = 0; i < items.Length; i++)
                {
                    Item item = items[i];
                    string imageurl = string.Empty;
                    if (item.LargeImage != null)
                    {
                        imageurl = item.LargeImage.URL;
                    }

                    string title = item.ItemAttributes.Title;
                    string pub = item.ItemAttributes.Publisher;
                   string isbn  = string.Empty;
                    if (item.ItemAttributes.ISBN == null)
                    {
                        if (null != item.ItemAttributes.EISBN)
                        {
                            isbn = item.ItemAttributes.EISBN[0];
                        } 
                        else
                        {
                            if (null != item.ItemAttributes.EAN)
                            {
                                isbn = item.ItemAttributes.EAN;
                            }
                        }
                    }
                    else
                    {
                        isbn = item.ItemAttributes.ISBN;
                    }
                    
                    string pubdate = item.ItemAttributes.PublicationDate;
                    string upc = item.ItemAttributes.UPC;
                    string genre = item.ItemAttributes.Genre;
                    string productgroup = item.ItemAttributes.ProductGroup;
                    string releasedate = item.ItemAttributes.ReleaseDate;
                    ItemAttributesCreator[] creators = item.ItemAttributes.Creator;
                    if (creators != null 
                        && creators.Length > 0)
                    {
                    foreach (ItemAttributesCreator creator in creators)
                    {
                        string creatorrole = string.Empty;
                        string creatorvalue = string.Empty;
                        if (creator.Role != null)
                        {
                            creatorrole = creator.Role;
                        }

                        if (creator.Value != null)
                        {
                            creatorvalue = creator.Value;
                        }
                       
                        Console.WriteLine("creatorrole: {0}   creatorvalue: {1}", creatorrole, creatorvalue);
                        creator.ToString();
                    }
                    }

                    string[] authors = item.ItemAttributes.Author;
                    string[] directors = item.ItemAttributes.Director;
                    string[] formats = item.ItemAttributes.Format;
                             
                        Console.WriteLine("Authors: {0}", authors);
                        Console.WriteLine("Title: {0}", title);
                        Console.WriteLine("ISBN: {0}", isbn);
                        Console.WriteLine("Pub Date: {0}", pubdate);
                        Console.WriteLine("Release Date: {0}", releasedate);
                        Console.WriteLine("Publisher: {0}", pub);
                        Console.WriteLine("Director: {0}", directors);
                        Console.WriteLine("Genre: {0}", genre);
                        Console.WriteLine("Formats: {0}", formats);
                        Console.WriteLine("ProductGroup: {0}", productgroup);
                        Console.WriteLine("ImageUrl: {0}", imageurl);
                }

                if (response.Items.Length > 1)
                {
                    Items infoimage = response.Items[1];
                    Item[] itemsimage = infoimage.Item;
                    for (int i = 0; i < items.Length; i++)
                    {
                        Item item = items[i];
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
