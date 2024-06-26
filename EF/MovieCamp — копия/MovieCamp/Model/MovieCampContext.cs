﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCamp.Model
{
    internal class MovieCampContext : DbContext
    {
        public DbSet<Movie> Movies { get; set;}
        public DbSet<Director> Directors { get; set;}
        public DbSet<Genre> Genres { get; set;}
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<User> Users { get; set; }

        static DbContextOptions<MovieCampContext> _options;

        static MovieCampContext()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection")!;

            var optionsBuilder = new DbContextOptionsBuilder<MovieCampContext>();
            _options = optionsBuilder.UseLazyLoadingProxies().UseSqlServer(connectionString).Options;
        }
        public MovieCampContext() : base(_options)
        {
            //Database.EnsureDeleted();
            if (Database.EnsureCreated())
            {
                var kubrick = new Director { Name = "Stanley", LastName = "Kubrick", Age = 59 };
                var tarantino = new Director { Name = "Quentin", LastName = "Tarantino", Age = 57 };
                Directors?.AddRange(kubrick, tarantino);

                var action = new Genre { Title = "Action" };
                var comedy = new Genre { Title = "Comedy" };
                var horror = new Genre { Title = "Horror" };
                var crime = new Genre { Title = "Crime" };
                var drama = new Genre { Title = "Drama" };
                var melodrama = new Genre { Title = "Melodrama" };
                var adventure = new Genre { Title = "Adventure" };
                var fantasy = new Genre { Title = "Fantasy" };
                var thriller = new Genre { Title = "Thriller" };
                var romance = new Genre { Title = "Romance" };
                var family = new Genre { Title = "Family" };
                var war = new Genre { Title = "War" };
                var documentary = new Genre { Title = "Documentary" };
                var musical = new Genre { Title = "Musical" };
                var biography = new Genre { Title = "Biography" };
                var scifi = new Genre { Title = "Sci-fi" };
                var western = new Genre { Title = "Western" };
                var postapocalyptic = new Genre { Title = "Post-apocalyptic" };
                Genres?.AddRange(action, comedy, horror, crime,drama,melodrama, adventure ,fantasy, thriller, romance, family, war, documentary, musical, biography, scifi, western, postapocalyptic);
                 
                var shining = new Movie { Title = "The Shining", Director = kubrick, Year = 1980, Poster = "https://upload.wikimedia.org/wikipedia/ru/thumb/e/ef/The_Shining.jpg/220px-The_Shining.jpg", Description= "Главный герой — Джек Торренс — приехал в элегантный уединенный отель, чтобы поработать смотрителем во время мертвого сезона вместе со своей женой и сыном. Торренс здесь раньше никогда не бывал. Или это не совсем так? Ответ лежит во мраке, сотканном из преступного кошмара." };
                var pulpFiction = new Movie { Title = "Pulp Fiction", Director = tarantino, Year = 1994, Poster = "https://upload.wikimedia.org/wikipedia/ru/9/93/Pulp_Fiction.jpg", Description = "Двое бандитов Винсент Вега и Джулс Винфилд проводят время в философских беседах в перерыве между разборками и «решением проблем» с должниками своего криминального босса Марселласа Уоллеса. Параллельно разворачивается три истории." };
                var reservoirDogs = new Movie { Title = "Reservoir Dogs", Director = tarantino, Year = 1992, Poster = "https://upload.wikimedia.org/wikipedia/ru/8/8a/%D0%A4%D0%B8%D0%BB%D1%8C%D0%BC_%D0%B0%D0%BC%D0%B5%D1%80%D0%B8%D0%BA%D0%B0%D0%BD%D1%81%D0%BA%D0%B8%D0%B9.jpg", Description = "Это должно было стать идеальным преступлением. Задумав ограбить ювелирный магазин, криминальный босс Джо Кэбот собрал вместе шестерых опытных и совершенно незнакомых друг с другом преступников. Но с самого начала все пошло не так, и обычный грабеж превратился в кровавую бойню." };

                shining.Genres = new List<Genre> { action, horror };
                pulpFiction.Genres = new List<Genre> { action, comedy };
                reservoirDogs.Genres = new List<Genre> { action, crime };
                Movies?.AddRange(shining, pulpFiction, reservoirDogs);
                 
                var user1 = new User { Login = "user1", Password = "password1" };
                var user2 = new User { Login = "user2", Password = "password2" };
                var user3 = new User { Login = "user3", Password = "password3" };
                var user4 = new User { Login = "admin", Password = "adminadmin" };
                Users?.AddRange(user1, user2, user3,user4);
                 
                var rating1 = new Rating { User = user1, Movie = shining, Grade = 5 };
                var rating2 = new Rating { User = user2, Movie = shining, Grade = 4 };
                var rating3 = new Rating { User = user3, Movie = shining, Grade = 3 };
                var rating4 = new Rating { User = user1, Movie = pulpFiction, Grade = 5 };
                var rating5 = new Rating { User = user2, Movie = pulpFiction, Grade = 4 };
                var rating6 = new Rating { User = user3, Movie = pulpFiction, Grade = 3 };
                var rating7 = new Rating { User = user1, Movie = reservoirDogs, Grade = 5 };
                var rating8 = new Rating { User = user2, Movie = reservoirDogs, Grade = 4 };
                var rating9 = new Rating { User = user3, Movie = reservoirDogs, Grade = 3 };
                Ratings?.AddRange(rating1, rating2, rating3, rating4, rating5, rating6, rating7, rating8, rating9);
            }
            SaveChanges();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Director>(entity =>
            {
                entity.Property(d => d.Name).HasMaxLength(30);
                entity.Property(d => d.LastName).HasMaxLength(30);
                entity.Property(u => u.Age).HasDefaultValue(18);
                entity.ToTable(t => t.HasCheckConstraint("Age", "Age >0 AND Age <120"));

            });
            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(d => d.Title).HasMaxLength(30);
                entity.HasIndex(i => new { i.Title }, "gnr_unq").IsUnique();

            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(d => d.Login).HasMaxLength(30);
                entity.Property(d => d.Password).HasMaxLength(20);
                entity.ToTable(t => t.HasCheckConstraint("Password", "LEN(Password) >7 AND LEN(Password) <21"));
            });
            modelBuilder.Entity<Movie>(entity =>
            {
                var minYear = 1900;
                entity.ToTable(t=>t.HasCheckConstraint($"CK_Movie_Year", $"Year >= {minYear}")) ;
            });
        }
    }
}
