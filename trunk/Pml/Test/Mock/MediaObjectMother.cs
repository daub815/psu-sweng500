namespace Sweng500.Pml.Test.Mock
{
    using System.Collections.Generic;
    using Sweng500.Pml.DataAccessLayer;

    /// <summary>
    /// The mother of all book objects for the unit tests
    /// </summary>
    public static class MediaObjectMother
    {
        /// <summary>
        /// Returns a list of created books without an id
        /// </summary>
        /// <returns>A list of new books</returns>
        public static IEnumerable<Book> CreateNewBooks()
        {
            IList<Book> books = new List<Book>();
            Book book1 = new Book();
            //// book.BookID = bookID;
            //// book.MediaID = mediaID;
            book1.Acquired = new System.DateTime(2004, 6, 5);
            book1.Comment = "A sample thesaurus";
            book1.ISBN = "0679780092";
            book1.NumOfStars = 2;
            book1.IsBorrowed = false;
            book1.LibraryLocation = string.Empty;
            book1.MediaDescription = "Authororitative and comprehensive, yet easy to use and portable";
            book1.Price = new decimal(11.95);
            book1.Published = new System.DateTime(1995, 1, 1);
            book1.Publisher = "Random House Inc";
            book1.Title = "School & Office Thesaurus";

            Book book2 = new Book();

            //// book.BookID = bookID;
            //// book.MediaID = mediaID;
            book2.Acquired = new System.DateTime(2008, 7, 13);
            book2.Comment = "To help me stretch";
            book2.ISBN = "9780470067413";
            book2.NumOfStars = 2;
            book2.IsBorrowed = false;
            book2.LibraryLocation = "Home";
            book2.MediaDescription = "The fun and easy way to increase flexibility, improve athletic performance, and decrease stress";
            book2.Price = new decimal(16.95);
            book2.Published = new System.DateTime(1995, 1, 1);
            book2.Publisher = "Random House Inc";
            book2.Title = "Streching for Dummies";

            books.Add(book1);
            books.Add(book2);

            return books;
        }

        /// <summary>
        /// Returns a list of created videos without an id
        /// </summary>
        /// <returns>A list C:\Users\Bob\Documents\Visual Studio 2010\Projects\psu-sweng500\trunk\Pml\Test\Unit\of new books</returns>
        public static IEnumerable<Video> CreateNewVideos()
        {
            IList<Video> videos = new List<Video>();
            Video video1 = new Video();
            //// book.BookID = bookID;
            //// book.MediaID = mediaID;
            video1.Acquired = new System.DateTime(2010, 12, 25);
            video1.BoardRatingID = 3;
            video1.Comment = "Great Series";
            video1.NumOfStars = 4;
            video1.IsBorrowed = false;
            video1.MediaDescription = "The Complete First Season";
            video1.Price = new decimal(9.99);
            video1.Published = new System.DateTime(2006, 1, 1);
            video1.Publisher = "Metro Goldwyn Meyers Studio";
            video1.Title = "Stargate SG.1 Season 1";
            videos.Add(video1);
            return videos;
        }
    }
}
