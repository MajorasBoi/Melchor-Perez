using System.Text;
using System.Threading.Tasks;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core.Flux.Domain;
using InfluxDB.Client.Writes;
using Models;

namespace Management
{
    public static class BookMapping
    {
        /// <summary>
        /// Define Custom Domain Object Converter
        /// </summary>
        private class DomainEntityConverter : IDomainObjectMapper
        {
            /// <summary>
            /// Convert to DomainObject.
            /// </summary>
            public object ConvertToEntity(FluxRecord fluxRecord, Type type)
            {
                if (type != typeof(Book))
                {
                    throw new NotSupportedException($"This converter doesn't supports: {type}");
                }

                var customEntity = new Book
                {
                    Condition = (TCondition)Convert.ToInt16(fluxRecord.GetValueByKey("condition")),
                    Title = Convert.ToString(fluxRecord.GetValueByKey("title")),
                    Borrowed_to = Convert.ToString(fluxRecord.GetValueByKey("borrower")),
                    Author = Convert.ToString(fluxRecord.GetValueByKey("author")),
                    Pages = Convert.ToInt32(fluxRecord.GetValueByKey("pages")),
                    Editor = Convert.ToString(fluxRecord.GetValueByKey("editor")),
                    Genre = (TGenre)Convert.ToInt16(fluxRecord.GetValueByKey("genre")),
                    ISBN = Convert.ToString(fluxRecord.GetValueByKey("isbn")),
                    TimeStamp = fluxRecord.GetTime().GetValueOrDefault().ToDateTimeUtc(),
                };

                return Convert.ChangeType(customEntity, type);
            }

            /// <summary>
            /// Convert to DomainObject.
            /// </summary>
            public T ConvertToEntity<T>(FluxRecord fluxRecord)
            {
                return (T)ConvertToEntity(fluxRecord, typeof(T));
            }

            /// <summary>
            /// Convert to Point
            /// </summary>
            public PointData ConvertToPointData<T>(T entity, WritePrecision precision)
            {
                if (!(entity is Book book))
                {
                    throw new NotSupportedException($"This converter doesn't supports: {entity}");
                }

                var point = PointData
                    .Measurement("book")
                    .Tag("title", book.Title)
                    .Tag("author", book.Author)
                    .Tag("isbn", book.ISBN)
                    .Field("pages", book.Pages)
                    .Field("borrower", book.Borrowed_to)
                    .Field("condition", book.Condition)
                    .Field("editor", book.Editor)
                    .Field("genre", book.Genre)
                    .Timestamp(book.TimeStamp, precision);

                return point;
            }
        }

    }

    public static class MagazineMapping
    {
        /// <summary>
        /// Define Custom Domain Object Converter
        /// </summary>
        private class DomainEntityConverter : IDomainObjectMapper
        {
            /// <summary>
            /// Convert to DomainObject.
            /// </summary>
            public object ConvertToEntity(FluxRecord fluxRecord, Type type)
            {
                if (type != typeof(Magazine))
                {
                    throw new NotSupportedException($"This converter doesn't supports: {type}");
                }

                var customEntity = new Magazine
                {
                    Condition = (TCondition)Convert.ToInt16(fluxRecord.GetValueByKey("condition")),
                    Title = Convert.ToString(fluxRecord.GetValueByKey("title")),
                    Borrowed_to = Convert.ToString(fluxRecord.GetValueByKey("borrower")),
                    Author = Convert.ToString(fluxRecord.GetValueByKey("author")),
                    Pages = Convert.ToInt32(fluxRecord.GetValueByKey("pages")),
                    Volume = Convert.ToInt32(fluxRecord.GetValueByKey("volume")),
                    DateOfRelease = Convert.ToString(fluxRecord.GetValueByKey("date_of_release")),
                    TimeStamp = fluxRecord.GetTime().GetValueOrDefault().ToDateTimeUtc(),
                };

                return Convert.ChangeType(customEntity, type);
            }

            /// <summary>
            /// Convert to DomainObject.
            /// </summary>
            public T ConvertToEntity<T>(FluxRecord fluxRecord)
            {
                return (T)ConvertToEntity(fluxRecord, typeof(T));
            }

            /// <summary>
            /// Convert to Point
            /// </summary>
            public PointData ConvertToPointData<T>(T entity, WritePrecision precision)
            {
                if (!(entity is Magazine magazine))
                {
                    throw new NotSupportedException($"This converter doesn't supports: {entity}");
                }

                var point = PointData
                    .Measurement("book")
                    .Tag("title", magazine.Title)
                    .Tag("author", magazine.Author)
                    .Tag("volume", magazine.Volume.ToString())
                    .Field("pages", magazine.Pages)
                    .Field("borrower", magazine.Borrowed_to)
                    .Field("condition", magazine.Condition)
                    .Field("date_of_release", magazine.DateOfRelease)
                    .Timestamp(magazine.TimeStamp, precision);

                return point;
            }
        }

    }
}
