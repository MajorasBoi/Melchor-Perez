using System;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core.Flux.Domain;
using InfluxDB.Client.Writes;

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
        public long BorrowerID { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
        public DateTime TimeStamp { get; set; }

        #endregion
        #region Constructors

        public Documents(TCondition condition, string title, long borrower, string author, int pages, DateTime timestamp)
        {
            Condition = condition;
            Title = title;
            BorrowerID = borrower;
            Author = author;
            Pages = pages;
            TimeStamp = timestamp;
        }

        public Documents(Documents doc)
        {
            Condition=doc.Condition;
            Title = doc.Title;
            BorrowerID=doc.BorrowerID;
            Author=doc.Author;
            Pages=doc.Pages;
            TimeStamp=doc.TimeStamp;
        }

        public Documents()
        {
            Condition = TCondition.Good;
            Title = "Unknown";
            BorrowerID = 0;
            Author = "Unknown";
            Pages = 0;
            TimeStamp = DateTime.UtcNow;
        }

        #endregion
        #region Methods

        public abstract PointData ConvertToPointData(WritePrecision precision);

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

        public Book(TCondition condition, string title, long borrowed_to, string author, int pages, DateTime timestamp,
                    string editor, TGenre genre, string isbn): base(condition, title, borrowed_to, author, pages, timestamp)
        {
            Editor = editor;
            Genre = genre;
            ISBN = isbn;
        }

        public Book(Book book): base(book.Condition, book.Title, book.BorrowerID, book.Author, book.Pages, book.TimeStamp)
        {
            Editor=book.Editor;
            Genre=book.Genre;
            ISBN=book.ISBN;
        }

        public Book()
        {
            Condition = TCondition.Good;
            Title = "Unknown";
            BorrowerID = 0;
            Author = "Unknown";
            Pages = 0;
            TimeStamp = DateTime.UtcNow;
            Editor = "Unknown";
            Genre = TGenre.Romance;
            ISBN = "Unknown";
        }

        #endregion
        #region Methods

        public override PointData ConvertToPointData(WritePrecision precision)
        {
            var point = PointData
                .Measurement("book")
                .Tag("title", Title)
                .Tag("author", Author)
                .Tag("isbn", ISBN)
                .Field("pages", Pages)
                .Field("borrower", BorrowerID)
                .Tag("condition", Condition.ToString())
                .Tag("editor", Editor)
                .Tag("genre", Genre.ToString())
                .Timestamp(TimeStamp, precision);

            return point;
        }

        #endregion
    }

    public class Magazine: Documents
    {
        #region Properties

        public int Volume { get; set; }
        public DateTime DateOfRelease { get; set; }

        #endregion
        #region Constructor

        public Magazine(TCondition condition, string title, long borrowed_to, string author, int pages, DateTime timestamp,
                        int volume, DateTime date_of_release): base(condition, title, borrowed_to, author, pages, timestamp)
        {
            Volume = volume;
            DateOfRelease = date_of_release;
        }

        public Magazine(Magazine magazine): base(magazine.Condition, magazine.Title, magazine.BorrowerID, magazine.Author, magazine.Pages, magazine.TimeStamp)
        {
            Volume = magazine.Volume;
            DateOfRelease = magazine.DateOfRelease;
        }

        public Magazine()
        {
            Condition = TCondition.Good;
            Title = "Unknown";
            BorrowerID = 0;
            Author = "Unknown";
            Pages = 0;
            TimeStamp = DateTime.UtcNow;
            Volume = 0;
            DateOfRelease = DateTime.UtcNow;
        }

        #endregion
        #region Methods

        public override PointData ConvertToPointData(WritePrecision precision)
        {
            var point = PointData
                   .Measurement("magazine")
                   .Tag("title", Title)
                   .Tag("author", Author)
                   .Tag("volume", Volume.ToString())
                   .Field("pages", Pages)
                   .Field("borrower", BorrowerID)
                   .Tag("condition", Condition.ToString())
                   //.Field("date_of_release", DateOfRelease)
                   .Timestamp (TimeStamp, precision);

            return point;
        }

        #endregion
    }
}