using System;
using System.Collections.Generic;
using ApiSportsBoilerPlate.Data.Entity;

namespace ApiSportsBoilerPlate.Configuration
{
    public class SportsDataConfiguration
    {
        public List<Person> People { get; set; } = new List<Person>
        {
            new Person
            {
                FirstName = "علی",
                LastName = "احمدی",
                DateOfBirth = new DateTime(2010,10,20)
            },
            new Person
            {
                FirstName = "Hasan",
                LastName = "Hoseyni",
                DateOfBirth = new DateTime(2011,10,20)
            },
            new Person
            {
                FirstName = "Zahra",
                LastName = "Ghasemi",
                DateOfBirth = new DateTime(2005,10,20)
            }
        };
        public List<Club> Clubs { get; set; } = new List<Club>
        {
            new Club
            {
                Name = "جوانان",
                Description = "سال تاسیس 1355",
                Image = "D:\\Img1.png"
            },
            new Club
            {
                Name = "باشگاه فرهنگی ورزشی سلیمانی",
                Description = "باشگاه فرهنگی ورزشی سلیمانی",
                Image = "D:\\Img2.png"
            }
        };
    }
}
