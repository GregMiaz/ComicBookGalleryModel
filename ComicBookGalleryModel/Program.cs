using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;

namespace ComicBookGalleryModel
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new Context())
            {
                context.Database.Log = (message) => Debug.WriteLine(message);

                var comicBookId = 1;

                var comicBook1 = context.ComicBooks.Find(comicBookId);
                var comicBook2 = context.ComicBooks.Find(comicBookId);

                var comicBook = context.ComicBooks
                    .Include(cb => cb.Series)
                    .Include(cb => cb.Artists.Select(a => a.Artist))
                    .Include(cb => cb.Artists.Select(a => a.Role))
                    .SingleOrDefault(cb=>cb.Id==comicBookId); //FirstOrDefault return first found, there are also Single and First

                Console.WriteLine(comicBook.DisplayText);


                //var comicBooks = context.ComicBooks
                //    //.Include(cb => cb.Series)
                //    //.Include(cb => cb.Artists.Select(a => a.Artist))
                //    //.Include(cb => cb.Artists.Select(a => a.Role))
                //    .ToList();

                //foreach (var comicBook in comicBooks)
                //{
                //    if (comicBook.Series == null)
                //    {
                //        context.Entry(comicBook)
                //            .Reference(cb => cb.Series)
                //            .Load();
                //    }

                //    var artistRoleNames = comicBook.Artists.Select(a => $"{a.Artist.Name} - {a.Role.Name}").ToList();
                //    var artistsRoleDisplayText = string.Join(", ", artistRoleNames);

                //    Console.WriteLine(comicBook.DisplayText);
                //    Console.WriteLine(artistsRoleDisplayText);
                //}

                Console.ReadLine();
            }
        }
    }
}
