using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model {
    public class Log {

        public int LogId { get; set; }
        public int BookId { get; set; }
        public User UserId { get; set; }
        public int CurrentPage { get; set; }
        public Book NoOfPages { get; set; }

        public Log() { }

        public Log(int logId, int bookId, User userId, int currentPage, Book noOfPages) {
            LogId = logId;
            BookId = bookId;
            UserId = userId;
            CurrentPage = currentPage;
            NoOfPages = noOfPages;
        }

    }
}
