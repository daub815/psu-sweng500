namespace Sweng500.Awse.CommerceService
{
using System;
using System.Collections.Generic;
using System.Linq;

    /// <summary>
    /// class is used to do an Item search using the Amazon webservice for Product Advertising
    /// </summary>
    public class AWSECommerceService : Sweng500.Awse.CommerceService.IAWSECommerceService
    {
        /// <summary>
        /// perform an item search
        /// </summary>
        /// <param name="searchIndex"> a string reprsenting the domain to search</param>
        /// <param name="title"> words in the title</param>
        /// <param name="keywords"> keywords to search for</param>
        /// <returns> a list of ItemResponses</returns>
        public IList<ItemResponse> ItemSearch(string searchIndex, string title, string keywords)
        {
            IList<ItemResponse> responses = new List<ItemResponse>();
            var client = new AWSECommerceServicePortTypeClient(Properties.Settings.Default.EndpointConfigName);
            client.ChannelFactory.Endpoint.Behaviors.Add(new AmazonSigningEndpointBehavior(Properties.Settings.Default.AwseAccessKey, Properties.Settings.Default.AwseSecretKey));
            var search = new ItemSearch
            {
                Request = new ItemSearchRequest[] 
                {
                    new ItemSearchRequest
                    {
                        SearchIndex = searchIndex,
                        Title = title,
                        ResponseGroup = new string[]
                        {
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
                    responses.Add(this.BuildItemResponse(items[i]));
                }
            }
            catch (Exception e)
            {
                //// TODO: Need Better Error Handling
                Console.WriteLine(e);
            }

            return responses;
        }

        /// <summary>
        /// convert the datestring into a DateTime
        /// if the input is null, a default time is returned.
        /// </summary>
        /// <param name="datestring"> the string </param>
        /// <returns> a DateTime</returns>
        public DateTime GetDateTime(string datestring)
        {
            DateTime convertedDate = new DateTime(1970, 1, 1);
            if (null != datestring)
            {
                convertedDate = DateTime.Parse(datestring);
            }

            return convertedDate;
        }

        /// <summary>
        /// builds a response for the item supplied
        /// </summary>
        /// <param name="suppliedItem"> the Item returned by the search</param>
        /// <returns> a response object where item attributes of interest have be selected</returns>
        private ItemResponse BuildItemResponse(Item suppliedItem)
        {
            ItemResponse itemresponse = new ItemResponse();
            itemresponse.Description = this.GetDescription(suppliedItem);
            itemresponse.Eisbn = suppliedItem.ItemAttributes.EISBN;
            itemresponse.Formats = suppliedItem.ItemAttributes.Format;
            itemresponse.Genre = suppliedItem.ItemAttributes.Genre;
            itemresponse.Isbn = suppliedItem.ItemAttributes.ISBN;
            itemresponse.Productgroup = suppliedItem.ItemAttributes.ProductGroup;
            itemresponse.Publicationdate = this.GetDateTime(suppliedItem.ItemAttributes.PublicationDate);
            itemresponse.Publisher = suppliedItem.ItemAttributes.Publisher;
            itemresponse.Releasedate = this.GetDateTime(suppliedItem.ItemAttributes.ReleaseDate);
            itemresponse.Title = suppliedItem.ItemAttributes.Title;
            itemresponse.Upc = suppliedItem.ItemAttributes.UPC;
            if (suppliedItem.LargeImage != null)
            {
                itemresponse.Imageurl = suppliedItem.LargeImage.URL;
            }

            itemresponse.Authorsname = this.GetAuthors(suppliedItem);
            itemresponse.Directorsname = this.GetDirectors(suppliedItem);
            itemresponse.Producersname = this.GetProducers(suppliedItem);
           
            return itemresponse;
        }

        /// <summary>
        /// gets the names of the authors.
        /// Empty array if no authors found
        /// </summary>
        /// <param name="suppliedItem"> the item returned by the search</param>
        /// <returns> an array of item names which may be empty</returns>
        private ItemName[] GetAuthors(Item suppliedItem)
        {
            IList<ItemName> authors = new List<ItemName>();
            string[] itemauthors = suppliedItem.ItemAttributes.Author;
            if (itemauthors != null
                && itemauthors.Length > 0)
            {
                foreach (string anauthor in itemauthors)
                {
                    ItemName aname = new ItemName(anauthor);

                    authors.Add(aname);
                }
            }

            return authors.ToArray<ItemName>();
        }

        /// <summary>
        /// gets the names of the directors.
        /// Empty array if no directors found
        /// </summary>
        /// <param name="suppliedItem"> the item returned by the search</param>
        /// <returns> an array of item names which may be empty</returns>
        private ItemName[] GetDirectors(Item suppliedItem)
        {
            IList<ItemName> directors = new List<ItemName>();
            string[] itemdirectors = suppliedItem.ItemAttributes.Director;
            if (itemdirectors != null
                && itemdirectors.Length > 0)
            {
                foreach (string adirector in itemdirectors)
                {
                    ItemName aname = new ItemName(adirector);
                    directors.Add(aname);
                }
            }

            return directors.ToArray<ItemName>();
        }

        /// <summary>
        /// gets the names of the producers.
        /// Empty array if no producers found
        /// </summary>
        /// <param name="suppliedItem"> the item returned by the search</param>
        /// <returns> an array of item names which may be empty</returns>
        private ItemName[] GetProducers(Item suppliedItem)
        {
            IList<ItemName> producers = new List<ItemName>();
            ItemAttributesCreator[] creators = suppliedItem.ItemAttributes.Creator;
            if (creators != null
                && creators.Length > 0)
            {
                foreach (ItemAttributesCreator creator in creators)
                {
                   if (creator.Role != null 
                        && "Producer".Equals(creator.Role))
                    {
                        ItemName aname = new ItemName(creator.Value);
                        producers.Add(aname); 
                    }
                }
            }

            return producers.ToArray<ItemName>();
        }

        /// <summary>
        /// gets the description of the item from an Editorial Review item.
        /// Empty if no description found
        /// </summary>
        /// <param name="suppliedItem"> the item returned by the search</param>
        /// <returns> a description of the item which may be empty</returns>
        private string GetDescription(Item suppliedItem)
        {
            string description = string.Empty;
            EditorialReview[] reviews = suppliedItem.EditorialReviews;
            if (reviews != null
                && reviews.Length > 0)
            {
                foreach (EditorialReview review in reviews)
                {
                    if ("Product Description".Equals(review.Source, StringComparison.OrdinalIgnoreCase))
                    {
                        description = review.Content;
                    }
                }
            }

            return description;
        }
    }
}
