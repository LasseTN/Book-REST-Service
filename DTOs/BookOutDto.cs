﻿using Model;

namespace DTOs {
    public class BookOutDto {

        public int? BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public Genre? Genre { get; set; }
        public int NoOfPages { get; set; }
        public string BookType { get; set; }
        public string IsbnNo { get; set; }
        public Location? Location { get; set; }
        public string Status { get; set; }
        public List<string> BookImagesPath { get; set; }

        public BookOutDto() { }

        public BookOutDto(int? bookId, string title, string author, Genre? genre, int noOfPages, string bookType, string isbnNo, Location? location, string status, List<string> bookImagesPath) {
            BookId = bookId;
            Title = title;
            Author = author;
            Genre = genre;
            NoOfPages = noOfPages;
            BookType = bookType;
            IsbnNo = isbnNo;
            Location = location;
            Status = status;
            BookImagesPath = bookImagesPath;
        }
    }
}