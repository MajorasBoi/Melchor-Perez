using System;

namespace Models
{
    public enum TGenre
    {
        Soap_Opera,
        Science_Fiction,
        Tails,
        Romance,
        Fantasy,
        Horror,
        Comedy,
    }

    public enum TCondition
    {
        Excellent,
        Good,
        Bad,
    }

    public abstract class Documents
    {
        #region Properties

        public TCondition Condition { get; set; }
        public string Title { get; set; }
        public string Borrowed_to { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
        public DateTime TimeStamp { get; set; }

        #endregion
        #region Constructors

        public Documents(TCondition condition, string title, string borrowed_to, string author, int pages, DateTime timestamp)
        {
            Condition = condition;
            Title = title;
            Borrowed_to = borrowed_to;
            Author = author;
            Pages = pages;
            TimeStamp = timestamp;
        }

        public Documents(Documents doc)
        {
            Condition=doc.Condition;
            Title = doc.Title;
            Borrowed_to=doc.Borrowed_to;
            Author=doc.Author;
            Pages=doc.Pages;
            TimeStamp=doc.TimeStamp;
        }

        public Documents()
        {
            Condition = TCondition.Good;
            Title = "Unknown";
            Borrowed_to = "Nobody";
            Author = "Unknown";
            Pages = 0;
            TimeStamp = DateTime.UtcNow;
        }

        #endregion
        #region Methods
        #endregion
    }

    public class Book: Documents
    {
        #region Properties

        public string Editor { get; set; }
        public TGenre Genre { get; set; }
        public string ISBN { get; set; }

        #endregion
        #region Constructor

        public Book(TCondition condition, string title, string borrowed_to, string author, int pages, DateTime timestamp,
                    string editor, TGenre genre, string isbn): base(condition, title, borrowed_to, author, pages, timestamp)
        {
            Editor = editor;
            Genre = genre;
            ISBN = isbn;
        }

        public Book(Book book): base(book.Condition, book.Title, book.Borrowed_to, book.Author, book.Pages, book.TimeStamp)
        {
            Editor=book.Editor;
            Genre=book.Genre;
            ISBN=book.ISBN;
        }

        public Book()
        {
            Condition = TCondition.Good;
            Title = "Unknown";
            Borrowed_to = "Nobody";
            Author = "Unknown";
            Pages = 0;
            TimeStamp = DateTime.UtcNow;
            Editor = "Unknown";
            Genre = TGenre.Romance;
            ISBN = "Unknown";
        }

        #endregion
    }

    public class Magazine: Documents
    {
        #region Properties

        public int Volume { get; set; }
        public string DateOfRelease { get; set; }

        #endregion
        #region Constructor

        public Magazine(TCondition condition, string title, string borrowed_to, string author, int pages, DateTime timestamp,
                        int volume, string date_of_release): base(condition, title, borrowed_to, author, pages, timestamp)
        {
            Volume = volume;
            DateOfRelease = date_of_release;
        }

        public Magazine(Magazine magazine): base(magazine.Condition, magazine.Title, magazine.Borrowed_to, magazine.Author, magazine.Pages, magazine.TimeStamp)
        {
            Volume = magazine.Volume;
            DateOfRelease = magazine.DateOfRelease;
        }

        public Magazine()
        {
            Condition = TCondition.Good;
            Title = "Unknown";
            Borrowed_to = "Nobody";
            Author = "Unknown";
            Pages = 0;
            TimeStamp = DateTime.UtcNow;
            Volume = 0;
            DateOfRelease = null;
        }

        #endregion
    }
}