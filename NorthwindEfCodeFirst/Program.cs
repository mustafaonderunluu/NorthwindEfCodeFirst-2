using NorthwindEfCodeFirst.Contexts;
using NorthwindEfCodeFirst.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace NorthwindEfCodeFirst
{
    public class Program
    {
        static void Main(string[] args)
        {
            // One();
            //  Two();
            // Three();
            // Four();
            //  Five();
            // Listele();
            //BirdenFazlaListeleme();
            //  Gruplama();
            //GruplamaBırdenFazla();
            //OrderBy();
            // Join();
            // LeftRightJoin();
            // SingleLineQuery();


            using (var nortwindContext = new NortwindConstext()) //Lazy Loading 
            {


                var result = nortwindContext.Customers.Include("Orders"); //Include("Orders")eklenmeseydi.
                { 
                
            foreach (var customer in result)
                    {

                        Console.WriteLine("{0},{1}", customer.ContactName,customer.Orders.Count);

                    }
                
                
                }


                Console.ReadLine();

            }















        }

        private static void SingleLineQuery()
        {
            using (var nortwindContext = new NortwindConstext())  //SingleLineQuery 

            {

                var result = nortwindContext.Customers.Where(c => c.City == "London" && c.Country == "Uk")
                    .OrderBy(c => c.ContactName).Select(cus => new { cus.CustomerId, cus.ContactName });




                foreach (var customer in result)
                {



                    Console.WriteLine("{0},{1}", customer.CustomerId, customer.ContactName);

                }


            }
        }

        private static void LeftRightJoin()
        {
            using (var nortwindContext = new NortwindConstext())          // Bir müşterinin siparişi yoksa gene gelir buna left/right join deriz.(hangi müşteriler bizden sipariş almamış ? )
            {
                var result = from c in nortwindContext.Customers
                             join o in nortwindContext.Orders
                             on c.CustomerId equals o.CustomerID into temp
                             from co in temp.DefaultIfEmpty()
                             where temp.Count() == 0 // sadece siparişi olmayanlar gözüküyor 
                             select new
                             {

                                 c.CustomerId,
                                 c.ContactName,
                                 c.CompanyName

                             };

                foreach (var customer in result)
                {



                    Console.WriteLine("{0},{1},{2}", customer.CustomerId, customer.ContactName, customer.CompanyName);

                }
                Console.WriteLine(" {0} Adet kayıt vardır.", result.Count());
            }
        }

        private static void Join()
        {
            using (var nortwindContext = new NortwindConstext())
            {
                var result = from c in nortwindContext.Customers
                             join o in nortwindContext.Orders
                             on new { CustomerId = c.CustomerId, Sehir = c.City } equals

                           new { CustomerId = o.CustomerID, Sehir = o.ShipCity }
                             orderby c.CustomerId
                             select new
                             {
                                 c.CustomerId,
                                 c.ContactName,
                                 o.OrderDate,
                                 o.ShipCity
                             };


                foreach (var item in result)
                {

                    Console.WriteLine("{0} , {1},{2},{3}", item.CustomerId, item.ContactName, item.OrderDate, item.ShipCity);

                }
                Console.WriteLine("{0} adet sipariş vardır. ", result.Count());
            }
        }

        private static void OrderBy()
        {
            using (var nortwindContext = new NortwindConstext())
            {

                List<Customer> result = (from c in nortwindContext.Customers
                                         orderby c.Country, c.ContactName  //descending ascending kullanılabillir 
                                         select c).ToList();

                foreach (var customer in result)
                {

                    Console.WriteLine("{0},{1}", customer.Country, customer.ContactName);

                }



            }
        }

        private static void GruplamaBırdenFazla()
        {
            using (var nortwindContext = new NortwindConstext())
            {

                var result = from c in nortwindContext.Customers
                             group c by new { c.Country, c.City }
                              into g
                             select new
                             {
                                 Sehir = g.Key.City,
                                 Ulke = g.Key.Country,
                                 Adet = g.Count(),

                             };


                foreach (var group in result)
                {

                    Console.WriteLine("Ülke : {0},Sehir : {1},Adet : {2} ", group.Ulke, group.Sehir, group.Ulke);

                }



            }
        }

        private static void Gruplama()
        {
            using (var nortwindContext = new NortwindConstext())
            {

                var result = from c in nortwindContext.Customers
                             group c by c.Country
                              into g
                             select g;

                foreach (var group in result)
                {

                    Console.WriteLine(group.Key);

                }



            }
        }

        private static void BirdenFazlaListeleme()
        {
            using (var nortwindContext = new NortwindConstext())
            {

                var result = from c in nortwindContext.Customers
                             select new { c.City, c.CompanyName, c.ContactName };

                foreach (var customer in result)
                {

                    Console.WriteLine(customer);

                }



            }
        }

        private static void Listele()
        {
            using (var nortwindContext = new NortwindConstext())
            {

                IQueryable<Customer> result = from c in nortwindContext.Customers
                                              where c.City =="London" && c.Country =="UK"
                                              select c;

                foreach (var customer in result)
                {

                    Console.WriteLine(customer.ContactName);

                }



            }
        }

        private static void Five()
        {
            using (var nortwindContext = new NortwindConstext())  //Güncelleme 
            {
                Customer customer = nortwindContext.Customers.Find("Önder");
                customer.Country = "Turkey";
                nortwindContext.SaveChanges();
            }
        }

        private static void Four()
        {
            using (var nortwindContext = new NortwindConstext())
            {
                Order order = nortwindContext.Orders.Find(1);
                nortwindContext.Orders.Remove(order);

                nortwindContext.SaveChanges();

            }
        }

        private static void Two()
        {
            using (var nortwindContext = new NortwindConstext())
            {
                Customer customer = nortwindContext.Customers.Find("Önder");
                customer.Orders.Add(new Order { OrderID = 1, OrderDate = DateTime.Now, ShipCity = "Antalya", ShipCountry = "Türkiye" });

                nortwindContext.SaveChanges();
            }
        }

        private static void One()
        {
            using (var nortwindContext = new NortwindConstext())
            {
                foreach (var customer in nortwindContext.Customers)
                {
                    Console.WriteLine("Customer Name: {0} ", customer.ContactName);
                }

              
               
            }
        }

        private static void Three()
        {
            using (var nortwindContext = new NortwindConstext())
            {
                Customer newCustomer = new Customer
                {
                    CustomerId = "Önder",
                    City = "İstanbul",
                    CompanyName = "YazılımEvi.Com",
                    ContactName = "Önder Ünlü",
                    Country = "Türkiye"
                };

                nortwindContext.Customers.Add(newCustomer);
                nortwindContext.SaveChanges();
            }
        }
    }  

}

