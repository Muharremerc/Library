using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;
using System.IO;
using System.Web;

namespace DAL
{
    public class Act
    {
        private static Act classDal;
        public static Act getClassEntity()
        {
            if (classDal == null)
                classDal = new Act();
            return classDal;

        }

        public Library Login(string lname, string password)
        {
            try
            {
                var db = DAL.Model.LibEntities1.getDBEntity();
                var l = db.Libraries.Where(x => x.Name == lname).Where(x => x.Password == password).FirstOrDefault();
                return l;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Book> getBookListbyLibId(int id)
        {
            try
            {
                var db = Model.LibEntities1.getDBEntity();
                var booklist = db.Books.Include("Author").Include("BookLocations").Include("RenterBooks.Renter").Where(x => x.LibraryId == id).ToList();
              
                return booklist;
            }
            catch (Exception)
            {

                throw;
            }

        }


        public Book AddNewBook(string bookName,string authorName, string authorSurname , string imageAlt ,int libraryId, string block, string floor)
        {
            try
            {
                var db = Model.LibEntities1.getDBEntity();
                var authorChect = AuthorCheck(authorName, authorSurname);
               
                if (authorChect == null)
                {
                    var author = AddNewAuthor(authorName, authorSurname);
                    Book b = new Book
                    {
                        AuthorId = author.Id,
                        Name = bookName,
                        ImageALT = imageAlt,
                        IsActive = true,
                        LibraryId = libraryId,
                    };
                    var btemp =db.Books.Add(b);
                    db.SaveChanges();
                    BookLocation bl = new BookLocation();
                    bl.BookId = btemp.Id;
                    bl.Floor = Int32.Parse(floor);
                    bl.Block = block;
                    db.BookLocations.Add(bl);
                    db.SaveChanges();
                    return b;
                }
                else
                {
                    Book b = new Book
                    {
                        AuthorId = authorChect.Id,
                        Name = bookName,
                        ImageALT = imageAlt,
                        IsActive = true,
                        LibraryId = libraryId,
                    };
                    var btemp = db.Books.Add(b);
                    db.SaveChanges();
                    BookLocation bl = new BookLocation();
                    bl.BookId = btemp.Id;
                    bl.Floor = Int32.Parse(floor);
                    bl.Block = block;
                    db.BookLocations.Add(bl);
                    db.SaveChanges();
                    return b;
                }    
                 

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public Author AuthorCheck(string name,string surname)
        {
            try
            {
                var db = Model.LibEntities1.getDBEntity();
                var author = db.Authors.Where(x => x.Name == name && x.Surname == surname).FirstOrDefault();
                return author;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public Author AddNewAuthor(string name,string surname)
        {

            try
            {
                var db = Model.LibEntities1.getDBEntity();
                Author a = (new Author
                { 
                    Name = name,
                    Surname = surname
                });
                db.Authors.Add(a);
                db.SaveChanges();
                return a;
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeletBook(int id)
        {
            try
            {
                var db = Model.LibEntities1.getDBEntity();
                var book = db.Books.Where(x => x.Id == id && x.IsActive==true).FirstOrDefault();
                
                if (book!=null &&  deleteBookLocation(id)==true)
                {
                    var t = deleteBookRenter(id);
                    var renterCheck = deleteBookRenter(id);
                    db.Books.Remove(book);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public bool deleteBookLocation(int id)
        {
            try
            {
                var db = Model.LibEntities1.getDBEntity();
                var bookLocation = db.BookLocations.Where(x => x.BookId == id).FirstOrDefault();
                db.BookLocations.Remove(bookLocation);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public bool deleteBookRenter(int id)
        {
            try
            {
                var db = Model.LibEntities1.getDBEntity();
                var renter = db.RenterBooks.Where(x => x.BookId == id).FirstOrDefault();
                if (renter == null)
                    return false;
                db.RenterBooks.Remove(renter);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public bool RentBook(string name,string surname,int bookId)
        {
            try
            {
                DateTime localDate = DateTime.Now;
                var db = Model.LibEntities1.getDBEntity();
                RenterBook rb = new RenterBook();
                rb.BookId = bookId;
                rb.Time = localDate;
                rb.RenterId = addRenter(name, surname).Id;
                rb.IsActive = true;
                var temp=db.RenterBooks.Add(rb);
                db.SaveChanges();
                changeIsActiveFromBook(bookId);
                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public Renter addRenter(string name,string surname)
        {
            try
            {
                var db = Model.LibEntities1.getDBEntity();
                var rentertemp = db.Renters.Where(x => x.Name == name && x.Surname == surname).FirstOrDefault();
                if (rentertemp != null)
                    return rentertemp;
                
                Renter r = new Renter();
                r.Name = name;
                r.Surname = surname;
                db.Renters.Add(r);
                db.SaveChanges();
                return r;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool changeIsActiveFromBook(int id)
        {
            try
            {
                var db = Model.LibEntities1.getDBEntity();
                var r = db.Books.Where(x => x.Id == id).FirstOrDefault();
                if (r.IsActive == false)
                {
                    r.IsActive = true;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    r.IsActive = false;
                    db.SaveChanges();
                    return true;
                }
               
               
            }
            catch (Exception)
            {

                throw;
            }

        }

        public Book getBook(int id)
        {
            try
            {
                var db = Model.LibEntities1.getDBEntity();
                var book = db.Books.Where(x => x.Id == id).FirstOrDefault();
                return book;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool changeIsActiveRenterBook(int id)
        {
            try
            {
                var db = Model.LibEntities1.getDBEntity();
                var renterBook = db.RenterBooks.Where(x => x.BookId == id && x.IsActive == true).FirstOrDefault();
                renterBook.IsActive = false;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }


        public bool givebackBook(int id)
        {
            try
            {
                var db = Model.LibEntities1.getDBEntity();
                var book = db.Books.Where(x => x.Id == id).FirstOrDefault();
                changeIsActiveFromBook(id);
                changeIsActiveRenterBook(book.Id);
                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }


        public string GetAuthorbyBookid(int id)
        {
            try
            {
                var db = Model.LibEntities1.getDBEntity();
                var a = db.Books.Include("Author").Where(x => x.Id == id).FirstOrDefault();

                return a.Author.Name+" "+a.Author.Surname;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public string GetBookLocationbyBookid(int id)
        {
            try
            {
                var db = Model.LibEntities1.getDBEntity();
                var l = db.BookLocations.Where(x => x.BookId == id).FirstOrDefault();

                return "Floor - " + l.Floor + " / Block - " + l.Block;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public string getBookRenterbyBookid(int id)
        {
            try
            {
                var db = Model.LibEntities1.getDBEntity();
                var i = db.RenterBooks.Include("Renter").FirstOrDefault(x => x.BookId == id && x.IsActive == true);
                if (i == null)
                    return null;
                return i.Renter.Name+" "+i.Renter.Surname;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public string getBookTimebyBookid(int id)
        {
            try
            {
                var db = Model.LibEntities1.getDBEntity();
                var i = db.RenterBooks.FirstOrDefault(x => x.BookId == id && x.IsActive == true);
                if (i == null)
                    return null;
                return i.Time.ToString();

            }
            catch (Exception)
            {

                throw;
            }

        }


    }
}
