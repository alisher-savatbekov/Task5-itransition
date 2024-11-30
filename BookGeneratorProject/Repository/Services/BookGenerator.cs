using Bogus;
using BookGeneratorProject.Model;
using BookGeneratorProject.Repository.IRepository;
using System;
using System.Collections.Generic;

namespace BookGeneratorProject.Repository.Services
{
    public class BookGenerator
    {
        private readonly Faker<Book> _bookFaker;
        private readonly Faker _faker;

        public BookGenerator(int seed)
        {
            _faker = new Faker();
            _bookFaker = new Faker<Book>()
                .RuleFor(b => b.ISBN, f => $"{f.Random.Number(100, 999)}-{f.Random.Number(1000, 9999)}-{f.Random.Number(100, 999)}-{f.Random.Number(10, 99)}")
                .RuleFor(b => b.Title, (f, b) => GenerateTitle(f, b))
                .RuleFor(b => b.Authors, (f, b) => GenerateAuthors(f, b))
                .RuleFor(b => b.Publisher, (f, b) => GeneratePublisher(f, b))
                .RuleFor(b => b.Likes, f => GenerateLikes(f))
                .RuleFor(b => b.Reviews, f => GenerateReviews(f));
        }

        public List<Book> GenerateBooks(Region region, int bookCount, double averageLikes, double averageReviews)
        {
            var books = new List<Book>(bookCount);
            for (int i = 1; i <= bookCount; i++)
            {
                var book = _bookFaker.Generate();
                book.Index = i;
                book.Title = GenerateTitleByRegion(region); 
                book.Authors = GenerateAuthorsForRegion(region); 
                book.Publisher = GeneratePublisherForRegion(region); 
                book.Likes = GenerateCount(averageLikes);   
                book.Reviews = GenerateCount(averageReviews);  
                books.Add(book);


            }
            return books;
        }

        private string GenerateTitle(Faker f, Book b)
        {
            return f.Lorem.Sentence(); 
        }

        private string GenerateTitleByRegion(Region region)
        {
            var faker = GetFakerForRegion(region);
            return faker.Lorem.Sentence(); 
        }

        private List<string> GenerateAuthors(Faker f, Book b)
        {
            return f.Make(3, () => f.Name.FullName()).ToList(); 
        }

        private List<string> GenerateAuthorsForRegion(Region region)
        {
            var faker = GetFakerForRegion(region);
            return faker.Make(3, () => faker.Name.FullName()).ToList(); 
        }

        private string GeneratePublisher(Faker f, Book b)
        {
            return f.Company.CompanyName(); 
        }

        private string GeneratePublisherForRegion(Region region)
        {
            var faker = GetFakerForRegion(region);
            return faker.Company.CompanyName(); 
        }

        private int GenerateLikes(Faker f)
        {
            return f.Random.Int(0, 10); 
        }

        private int GenerateReviews(Faker f)
        {
            return f.Random.Int(0, 500); 
        }

        private int GenerateCount(double average)
        {
            if (average < 1)
            {
          
                return _faker.Random.Double() < average ? 1 : 0;
            }
            int baseCount = (int)average; 
            double fractionalPart = average - baseCount; 

            return _faker.Random.Double() < fractionalPart ? baseCount + 1 : baseCount;
        }


        private Faker GetFakerForRegion(Region region)
        {

            switch (region)
            {
                case Region.USA:
                    return new Faker("en_US"); 
                case Region.South_Korea:
                    return new Faker("ko"); 
                case Region.UAE:
                    return new Faker("ar"); 
                default:
                    return new Faker("en_US"); 
            }
        }
    }

}
