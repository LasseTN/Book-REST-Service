using Dapper;
using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Model;
using System.Data.SqlClient;

namespace DataAccess {
    public class BookAccess : IBookAccess {

        private readonly string? _connectionString;
        private readonly ILogger<IBookAccess>? _logger;

        public BookAccess(IConfiguration configuration, ILogger<IBookAccess>? logger = null) {
            _connectionString = configuration.GetConnectionString("DbAccessConnection");
            _logger = logger;
        }

        // For Test
        public BookAccess(string connectionStringForTest) {
            _connectionString = connectionStringForTest;
        }
        public async Task<int> Create(Book entity) {
            var insertedId = -1;
            using (SqlConnection conn = new SqlConnection(_connectionString)) {
                conn.Open();
                using (var transaction = conn.BeginTransaction()) {
                    try {
                        // Insert to Book table
                        var sqlBook = @"INSERT INTO Book
                                (genreId,
                                locationId,
                                isbnNo,
                                title,
                                author,
                                noOfPages,
                                bookType,
                                imageURL,
                                status)
                            OUTPUT INSERTED.BookId
                            VALUES
                                (@genreId,
                                 @locationId,
                                 @isbnNo,
                                 @title,
                                 @author,
                                 @noOfPages,
                                 @bookType,
                                 @imageURL,
                                 @status)";


                        insertedId = await conn.ExecuteScalarAsync<int>(sqlBook, new {
                            entity.Genre.GenreId,
                            entity.Location.LocationId,
                            entity.IsbnNo,
                            entity.Title,
                            entity.Author,
                            entity.NoOfPages,
                            entity.BookType,
                            entity.ImageURL,
                            entity.Status
                        }, transaction);

                        transaction.Commit();
                    } catch (Exception ex) {
                        _logger?.LogError(ex.Message);
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            return insertedId;
        }


        public async Task<Book>? Get(int id) {
            using (SqlConnection conn = new SqlConnection(_connectionString)) {
                await conn.OpenAsync();

                var bookQuery = @"SELECT
                b.bookId, 
                b.isbnNo,
                b.title,
                b.author,
                b.noOfPages,
                b.bookType,
                b.imageURL,
                b.status,              
                b.genreId,
                g.genreName,
                b.locationId,               
                l.locationName
            FROM Book b
            INNER JOIN Genre g ON b.genreId = g.genreId
            LEFT JOIN Location l ON b.locationId = l.locationId
            WHERE b.BookId = @bookId";

                var result = await conn.QueryAsync<Book, Genre, Location, Book>(
                    bookQuery,
                    (book, genre, location) => {
                        book.Genre = genre;
                        book.Location = location;
                        return book;
                    },
                    new { bookId = id },  
                    splitOn: "Genreid, LocationName"  // Splitting columns for mapping
                );

                return result.FirstOrDefault();  // Return the first (or default) book from the result
            }
        }


        public async Task<List<Book>> GetAll() {
            using (SqlConnection conn = new SqlConnection(_connectionString)) {
                await conn.OpenAsync();

                using (var transaction = conn.BeginTransaction()) {
                    var books = new List<Book>();
                    string bookQuery = @"SELECT
                b.bookId, 
                b.isbnNo,
                b.title,
                b.author,
                b.noOfPages,
                b.bookType,
                b.imageURL,
                b.status,              
                b.genreId,
                g.genreName,
                b.locationId,               
                l.locationName
                FROM Book b
                INNER JOIN Genre g ON b.genreId = g.genreId
                LEFT JOIN Location l ON b.locationId = l.locationId";

                    books = (await conn.QueryAsync<Book, Genre, Location, Book>(bookQuery,
                        (book, genre, location) => {
                            book.Genre = genre;
                            book.Location = location;
                            return book;
                        },

                        transaction: transaction,
                        splitOn: "GenreId,LocationId"))
                    .ToList();

                    transaction.Commit();
                    return books; // Return list of books
                }
            }
        }

        public async Task<bool> Update(int id, Book entity) {
            int rowsAffected = -1;
            entity.BookId = id;

            using (SqlConnection conn = new SqlConnection(_connectionString)) {
                conn.Open();
                var updateSql = @"
                    UPDATE Book 
                    SET 
                        isbnNo = @isbnNo, 
                        title = @title, 
						author = @author,
						noOfPages = @noOfPages,
						bookType = @bookType,
                        imageURL = @imageURL,
                        status = @status, 
						genreId = @genreId,
                        locationId = @locationId 
                       
                    WHERE 
                        bookId = @bookId";
                try {
                    rowsAffected = await conn.ExecuteAsync(updateSql, new {
                        isbnNo = entity.IsbnNo,
                        title = entity.Title,
                        author = entity.Author,
                        noOfPages = entity.NoOfPages,
                        bookType = entity.BookType,
                        imageURL = entity.ImageURL,
                        status = entity.Status,
                        genreId = entity.Genre?.GenreId ?? 0,
                        locationId = entity.Location?.LocationId ?? 0,
                        bookId = entity.BookId,
                    });


                } catch (Exception ex) {
                    _logger?.LogError(ex.Message);
                }
            }
            return rowsAffected > 0;
        }
    }
}
