using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryDataAccess.Entities
{
    public class FakeData
    {
        public static IEnumerable<Province> Provinces =>
           new[]
           {
               new Province()
               {
                   Name = "Province 1.1",
                   Country = new Country()
                   {
                       Name = "County 1"
                   }
               },
               new Province()
               {
                   Name = "Province 1.2",
                   Country = new Country()
                   {
                       Name = "County 1"
                   }
               },
               new Province()
               {
                   Name = "Province 2.1",
                   Country = new Country()
                   {
                       Name = "County 2"
                   }
               },
               new Province()
               {
                   Name = "Province 2.2",
                   Country = new Country()
                   {
                       Name = "County 2"
                   }
               }
           };
    }
}
