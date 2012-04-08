namespace Sweng500.Awse.CommerceService
{
using System;
using System.Collections.Generic;
using System.Linq;
using log4net;

    /// <summary>
    /// class is used to do an Item search using the Amazon webservice for Product Advertising
    /// </summary>
    public class AWSECommerceService : Sweng500.Awse.CommerceService.IAWSECommerceService
    {
        /// <summary>
        /// Class logger
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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

                bool goodresponse = this.CheckResponse(response);

                if (goodresponse)
                {
                    for (int i = 0; i < response.Items.Length; i++)
                    {
                        Items info = response.Items[i];
                        string totalpages = info.TotalPages;
                        string totalresults = info.TotalResults;
                        log.InfoFormat("ItemSearch processes response.Items for searchindex: {0}  title: {1}, item: {2} total pages: {3} total results: {4}", searchIndex, title, i, totalpages, totalresults);
                        Item[] items = info.Item;
                        for (int itemindex = 0; itemindex < items.Length; itemindex++)
                        {
                            Item item = items[itemindex];
                            responses.Add(this.BuildItemResponse(items[itemindex]));
                        }
                    }
                }
                else
                {
                    log.WarnFormat("ItemSearch did not get a good response.  Nothing returned");
                }
            }
            catch (Exception e)
            {
                log.ErrorFormat("unable to perform ItemSearch.  received exception: {0} ", e);
                throw;
            }

            return responses;
        }

        /// <summary>
        /// perform an item search
        /// </summary>
        /// <param name="author"> name of the author to search on</param>
        /// <param name="keywords"> keywords to search for</param>
        /// <returns> a list of ItemResponses</returns>
        public IList<ItemResponse> AuthorSearch(string author, string keywords)
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
                        SearchIndex = "Books",
                        Author = author,
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

                bool goodresponse = this.CheckResponse(response);

                if (goodresponse)
                {
                    for (int i = 0; i < response.Items.Length; i++)
                    {
                        Items info = response.Items[i];
                        string totalpages = info.TotalPages;
                        string totalresults = info.TotalResults;
                        log.InfoFormat("AuthorSearch processes response.Items for author: {1}, item: {2} total pages: {3} total results: {4}", author, i, totalpages, totalresults);
                        Item[] items = info.Item;
                        for (int itemindex = 0; itemindex < items.Length; itemindex++)
                        {
                            Item item = items[itemindex];
                            responses.Add(this.BuildItemResponse(items[itemindex]));
                        }
                    }
                }
                else
                {
                    log.WarnFormat("AuthorSearch did not get a good response.  Nothing returned");
                }
            }
            catch (Exception e)
            {
                log.ErrorFormat("unable to perform AuthorSearch.  received exception: {0} ", e);
                throw;
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
            DateTime convertedDate = DateTime.MinValue;

            if (null != datestring)
            {
                try
                {
                    string[] splitchars = { "-", " " };
                    string[] datepieces = datestring.Split(splitchars, StringSplitOptions.RemoveEmptyEntries);
                    if (1 == datepieces.Length
                        && 4 == datepieces[0].Length)
                    {
                        DateTime yearonly = new DateTime(Convert.ToInt32(datepieces[0]), 1, 1);
                        return yearonly;
                    }
                }
                catch (Exception e)
                {
                    log.WarnFormat("GetDateTime Failed due to exception: {0}  Input String is {1}", e, datestring);
                }
            }

            if (false == DateTime.TryParse(datestring, out convertedDate))
            {
                log.WarnFormat("GetDateTime Failed:  Input String is {0}", datestring);
            }
            
            return convertedDate;
        }

        /// <summary>
        /// checks the response by looking for IsValud == "True".
        /// When True, verify the errors object is null or empty and the number of
        /// TotalResults is greater than 0
        /// </summary>
        /// <param name="response"> the response from the webservice</param>
        /// <returns>triue if IsValid  equals "True"</returns>
        private bool CheckResponse(ItemSearchResponse response)
        {
            bool goodresponse = false;
            if (null != response
                    && response.Items.Length > 0)
            {
                string isvalid = response.Items[0].Request.IsValid;
                if (null != isvalid &&
                    "True".Equals(isvalid, StringComparison.OrdinalIgnoreCase))
                {
                    if ((null == response.Items[0].Request.Errors
                        || 0 == response.Items[0].Request.Errors.Length)
                        && 0 < Convert.ToInt32(response.Items[0].TotalResults))
                    {
                        goodresponse = true;
                    }
                }
            }

            return goodresponse;
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
