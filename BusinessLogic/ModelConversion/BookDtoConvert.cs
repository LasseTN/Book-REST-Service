using DTOs;
using Model;

namespace BusinessLogic.ModelConversion {
    public class BookDtoConvert {

        public static Book? FromDtoToBook(BookInDto bookDto) {
            Book? convertedBook = null;
            if (bookDto != null) {
                convertedBook = new Book(
                    bookDto.Title,
                    bookDto.Author,
                    bookDto.Genre,
                    bookDto.NoOfPages,
                    bookDto.BookType,
                    bookDto.IsbnNo,
                    bookDto.Location,
                    bookDto.Status,
                    bookDto.BookImagesPath
                    );
            }
            return convertedBook;
        }

        public static BookOutDto? FromBookToDto(Book bookModel) {
            BookOutDto? bookOut = null;
            if (bookModel != null) {
                bookOut = new BookOutDto(
                    bookModel.BookId,
                    bookModel.Title ?? string.Empty,
                    bookModel.Author ?? string.Empty,
                    bookModel.Genre,
                    bookModel.NoOfPages,
                    bookModel.BookType ?? string.Empty,
                    bookModel.IsbnNo ?? string.Empty,
                    bookModel.Location,
                    bookModel.Status ?? string.Empty,
                    bookModel.BookImagesPath ?? new List<string>()
                );
            }
            return bookOut;
        }


        public static List<BookOutDto>? FromBookDtoToList(List<Book> bookModels) {
            List<BookOutDto>? aListOfDtos = null;
            if (bookModels != null) {
                aListOfDtos = new List<BookOutDto>();
                BookOutDto tempDto;
                foreach (Book book in bookModels) {
                    if (book != null) {
                        tempDto = FromBookToDto(book);
                        aListOfDtos.Add(tempDto);
                    }
                }
            }
            return aListOfDtos;
        }

    }
}
