using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EF_Core_Introduction
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //With custom repository

            using (var libraryContext = new LibraryContext())
            {

            //    libraryContext.Add(new Author("Ghita", 44));
            //    libraryContext.Add(new Author("Petrica", 30));
            //    libraryContext.Add(new Author("Vasile", 62));
            //    libraryContext.Add(new Author("Valerica", 44));
            //    libraryContext.Add(new Author("Ioane", 81));
            //    libraryContext.Add(new Author("Tolica", 37));

            //    libraryContext.SaveChanges();

            //    var c = libraryContext.Authors.ToList();

            //    foreach (var v in c)
            //    {
            //        Console.WriteLine(v);
            //    }

            //    var d = libraryContext.Find<Author>(4);

            //    Console.WriteLine(d);

            //    var e = libraryContext.Authors.Where(a => a.Name == "Vasea");

            //    foreach (var v in e)
            //    {
            //        Console.WriteLine(v);
            //    }

            //    var f = libraryContext.Find<Author>(3);
            //    f.Name = "Alexei";
            //    libraryContext.Update(f);

            //    libraryContext.SaveChanges();

            //    Console.WriteLine(f);

            //    libraryContext.Remove<Author>(d);

            //    libraryContext.SaveChanges();

            //    var h = libraryContext.Authors.ToList();

            //    foreach (var v in h)
            //    {
            //        Console.WriteLine(v);
            //    }
            }

            ////READ (SELECT ALL)
            //using (var lc = new LibraryContext(options))
            //{
            //    var list = lc.Authors.ToList();
            //    foreach (var a in list)
            //    {
            //        Console.WriteLine(a);
            //    }
            //}

            ////READ BY ID (SELECT BY ID)
            //using (var lc = new LibraryContext(options))
            //{
            //    var a = lc.Find<Author>(1);
            //    Console.WriteLine(a);
            //}

            ////UPDATE
            //using (var lc = new LibraryContext(options))
            //{
            //    var a = lc.Find<Author>(7);

            //    a.Name = "Igor";

            //    lc.Update(a);
            //    lc.SaveChanges();

            //    var b = lc.Find<Author>(7);
            //    Console.WriteLine(b);
            //}

            ////DELETE
            //using (var lc = new LibraryContext(options))
            //{
            //    var a = lc.Find<Author>(6);

            //    lc.Remove(a);
            //    lc.SaveChanges();

            //    var b = lc.Find<Author>(6);
            //    var list = lc.Authors.ToList();
            //    foreach (var l in list)
            //    {
            //        Console.WriteLine(l);
            //    }
            //}
        }
    }
}
