using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

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
                var darabont = new Director { Name = "Frank", LastName = "Darabont", Age = 62 };
                var nolan = new Director { Name = "Christopher", LastName = "Nolan", Age = 52 };
                var fincher = new Director { Name = "David", LastName = "Fincher", Age = 59 };
                var jackson = new Director { Name = "Peter", LastName = "Jackson", Age = 60 };
                var wachowskis = new Director { Name = "Lana and Lilly", LastName = "Wachowski", Age = 57 };
                var scott = new Director { Name = "Ridley", LastName = "Scott", Age = 84 };
                var zemeckis = new Director { Name = "Robert", LastName = "Zemeckis", Age = 70 };
                var columbus = new Director { Name = "Chris", LastName = "Columbus", Age = 63 };
                var cameron = new Director { Name = "James", LastName = "Cameron", Age = 68 };
                var howard = new Director { Name = "Ron", LastName = "Howard", Age = 68 };
                var besson = new Director { Name = "Luc", LastName = "Besson", Age = 63 };
                var nolan2 = new Director { Name = "Christopher", LastName = "Nolan", Age = 52 };
                var fincher2 = new Director { Name = "David", LastName = "Fincher", Age = 59 };
                var jackson2 = new Director { Name = "Peter", LastName = "Jackson", Age = 60 };
                var adamson = new Director { Name = "Andrew", LastName = "Adamson", Age = 55 };
                var jenson = new Director { Name = "Vicky", LastName = "Jenson", Age = 59 };
                Directors?.AddRange(scott, zemeckis, columbus, cameron, howard, besson, nolan2, fincher2, jackson2, adamson, jenson);

                var gladiator = new Movie { Title = "Gladiator", Director = scott, Year = 2000, Poster = "https://upload.wikimedia.org/wikipedia/ru/thumb/0/0d/Gladiator_ver1.jpg/220px-Gladiator_ver1.jpg", Description = "Эпическая история о генерале, превращенном в раба, который восстает против императора Рима." };
                var backToTheFuture = new Movie { Title = "Back to the Future", Director = zemeckis, Year = 1985, Poster = "https://upload.wikimedia.org/wikipedia/ru/thumb/0/0b/Back_to_the_Future.jpg/220px-Back_to_the_Future.jpg", Description = "Приключенческая комедия о молодом изобретателе, путешествующем во времени на машине времени." };
                var harryPotter = new Movie { Title = "Harry Potter and the Philosopher's Stone", Director = columbus, Year = 2001, Poster = "https://upload.wikimedia.org/wikipedia/ru/thumb/9/9c/Harry_Potter_and_the_Philosopher%27s_Stone.jpg/220px-Harry_Potter_and_the_Philosopher%27s_Stone.jpg", Description = "Приключенческий фильм о мальчике-волшебнике, отправляющемся в Школу чародейства и волшебства Хогвартс." };
                var avatar = new Movie { Title = "Avatar", Director = cameron, Year = 2009, Poster = "https://upload.wikimedia.org/wikipedia/ru/thumb/f/f2/Avatar_official.jpg/220px-Avatar_official.jpg", Description = "Научно-фантастическая сага об исследовании далекой планеты Пандоры и борьбе за ее ресурсы." };
                var aBeautifulMind = new Movie { Title = "A Beautiful Mind", Director = howard, Year = 2001, Poster = "https://upload.wikimedia.org/wikipedia/ru/thumb/5/58/A_beautiful_mind.jpg/220px-A_beautiful_mind.jpg", Description = "Драма о жизни математика Джона Нэша, страдающего от шизофрении, и его борьбе с болезнью." };
                var leon = new Movie { Title = "Leon: The Professional", Director = besson, Year = 1994, Poster = "https://upload.wikimedia.org/wikipedia/ru/thumb/c/cd/Leon_poster.jpg/220px-Leon_poster.jpg", Description = "Триллер о профессиональном убийце, который становится опекуном 12-летней девочки." };
                var darkKnight = new Movie { Title = "The Dark Knight", Director = nolan2, Year = 2008, Poster = "https://upload.wikimedia.org/wikipedia/ru/thumb/8/8a/Dark_Knight.jpg/220px-Dark_Knight.jpg", Description = "Боевик о борьбе Бэтмена с преступным гениями, включая Жокера." };
                var theGame = new Movie { Title = "The Game", Director = fincher2, Year = 1997, Poster = "https://upload.wikimedia.org/wikipedia/ru/thumb/6/61/The_Game_1997.jpg/220px-The_Game_1997.jpg", Description = "Триллер о миллионере, втянутом в загадочную игру, которая становится все более опасной." };
                var lordOfTheRings2 = new Movie { Title = "The Lord of the Rings: The Return of the King", Director = jackson2, Year = 2003, Poster = "https://upload.wikimedia.org/wikipedia/ru/thumb/7/7a/The_Lord_of_the_Rings_The_Return_of_the_King.jpg/220px-The_Lord_of_the_Rings_The_Return_of_the_King.jpg", Description = "Финальная часть эпической трилогии о борьбе за Средиземье." };
                var shrek = new Movie { Title = "Shrek", Director = adamson, Year = 2001, Poster = "https://upload.wikimedia.org/wikipedia/ru/thumb/8/83/Shrek.jpg/220px-Shrek.jpg", Description = "Комедийный мультфильм о зеленом огре по имени Шрэк, который отправляется спасать принцессу из замка." };

               

                Directors?.AddRange(darabont, nolan, fincher, jackson, wachowskis);
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
                var shawshankRedemption = new Movie { Title = "The Shawshank Redemption", Director = darabont, Year = 1994, Poster = "https://upload.wikimedia.org/wikipedia/ru/d/de/Movie_poster_the_shawshank_redemption.jpg", Description = "Драма о дружбе, надежде и выживании в тюрьме Шоушенк." };
                var interstellar = new Movie { Title = "Interstellar", Director = nolan, Year = 2014, Poster = "https://upload.wikimedia.org/wikipedia/ru/c/c3/Interstellar_2014.jpg", Description = "Научно-фантастический фильм о поиске нового дома для человечества в глубинах космоса." };
                var fightClub = new Movie { Title = "Fight Club", Director = fincher, Year = 1999, Poster = "https://upload.wikimedia.org/wikipedia/ru/8/8a/Fight_club.jpg", Description = "Психологический триллер о человеке, страдающем от бессонницы, который встречает харизматичного неразумного продавца мыла и основывает с ним тайный бойцовский клуб." };
                var lordOfTheRings = new Movie { Title = "The Lord of the Rings: The Fellowship of the Ring", Director = jackson, Year = 2001, Poster = "https://upload.wikimedia.org/wikipedia/en/f/fb/Lord_Rings_Fellowship_Ring.jpg", Description = "Эпическая фэнтези-сага о кольце всевластия и путешествии, направленном на его уничтожение." };
                var matrix = new Movie { Title = "The Matrix", Director = wachowskis, Year = 1999, Poster = "https://upload.wikimedia.org/wikipedia/ru/9/9d/Matrix-DVD.jpg", Description = "Научно-фантастический боевик о программисте, который узнает, что мир, в котором он живет, на самом деле является компьютерной симуляцией, созданной искусственным интеллектом." };

                gladiator.Genres = new List<Genre> { action, adventure, drama };
                backToTheFuture.Genres = new List<Genre> { adventure, comedy, scifi };
                harryPotter.Genres = new List<Genre> { adventure, family, fantasy };
                avatar.Genres = new List<Genre> { action, adventure, fantasy, scifi };
                aBeautifulMind.Genres = new List<Genre> { biography, drama };
                leon.Genres = new List<Genre> { action, crime, drama };
                darkKnight.Genres = new List<Genre> { action, crime, drama };
                theGame.Genres = new List<Genre> { drama, thriller };
                lordOfTheRings2.Genres = new List<Genre> { adventure, drama, fantasy };
                shrek.Genres = new List<Genre> {  adventure, comedy, family, fantasy };
                Movies?.AddRange(gladiator, backToTheFuture, harryPotter, avatar, aBeautifulMind, leon, darkKnight, theGame, lordOfTheRings2, shrek);
                shawshankRedemption.Genres = new List<Genre> { drama };
                interstellar.Genres = new List<Genre> { adventure, drama, scifi };
                fightClub.Genres = new List<Genre> { drama };
                lordOfTheRings.Genres = new List<Genre> { adventure, drama, fantasy };
                matrix.Genres = new List<Genre> { action, scifi };
                Movies?.AddRange(shawshankRedemption, interstellar, fightClub, lordOfTheRings, matrix);


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
                var rating10 = new Rating { User = user1, Movie = gladiator, Grade = 4 };
                var rating11 = new Rating { User = user2, Movie = gladiator, Grade = 5 };
                var rating12 = new Rating { User = user3, Movie = gladiator, Grade = 4 };

                var rating13 = new Rating { User = user1, Movie = backToTheFuture, Grade = 5 };
                var rating14 = new Rating { User = user2, Movie = backToTheFuture, Grade = 4 };
                var rating15 = new Rating { User = user3, Movie = backToTheFuture, Grade = 5 };

                var rating16 = new Rating { User = user1, Movie = harryPotter, Grade = 4 };
                var rating17 = new Rating { User = user2, Movie = harryPotter, Grade = 4 };
                var rating18 = new Rating { User = user3, Movie = harryPotter, Grade = 3 };

                var rating19 = new Rating { User = user1, Movie = avatar, Grade = 5 };
                var rating20 = new Rating { User = user2, Movie = avatar, Grade = 5 };
                var rating21 = new Rating { User = user3, Movie = avatar, Grade = 4 };

                var rating22 = new Rating { User = user1, Movie = aBeautifulMind, Grade = 5 };
                var rating23 = new Rating { User = user2, Movie = aBeautifulMind, Grade = 4 };
                var rating24 = new Rating { User = user3, Movie = aBeautifulMind, Grade = 3 };

                var rating25 = new Rating { User = user1, Movie = leon, Grade = 4 };
                var rating26 = new Rating { User = user2, Movie = leon, Grade = 5 };
                var rating27 = new Rating { User = user3, Movie = leon, Grade = 4 };

                var rating28 = new Rating { User = user1, Movie = darkKnight, Grade = 5 };
                var rating29 = new Rating { User = user2, Movie = darkKnight, Grade = 5 };
                var rating30 = new Rating { User = user3, Movie = darkKnight, Grade = 4 };

                var rating31 = new Rating { User = user1, Movie = theGame, Grade = 4 };
                var rating32 = new Rating { User = user2, Movie = theGame, Grade = 3 };
                var rating33 = new Rating { User = user3, Movie = theGame, Grade = 5 };

                var rating34 = new Rating { User = user1, Movie = lordOfTheRings2, Grade = 5 };
                var rating35 = new Rating { User = user2, Movie = lordOfTheRings2, Grade = 5 };
                var rating36 = new Rating { User = user3, Movie = lordOfTheRings2, Grade = 4 };

                var rating37 = new Rating { User = user1, Movie = shrek, Grade = 4 };
                var rating38 = new Rating { User = user2, Movie = shrek, Grade = 5 };
                var rating39 = new Rating { User = user3, Movie = shrek, Grade = 4 };

                var rating40 = new Rating { User = user1, Movie = shawshankRedemption, Grade = 5 };
                var rating41 = new Rating { User = user2, Movie = shawshankRedemption, Grade = 5 };
                var rating42 = new Rating { User = user3, Movie = shawshankRedemption, Grade = 4 };

                var rating43 = new Rating { User = user1, Movie = interstellar, Grade = 5 };
                var rating44 = new Rating { User = user2, Movie = interstellar, Grade = 4 };
                var rating45 = new Rating { User = user3, Movie = interstellar, Grade = 4 };

                var rating46 = new Rating { User = user1, Movie = fightClub, Grade = 4 };
                var rating47 = new Rating { User = user2, Movie = fightClub, Grade = 5 };
                var rating48 = new Rating { User = user3, Movie = fightClub, Grade = 4 };

                var rating49 = new Rating { User = user1, Movie = lordOfTheRings, Grade = 5 };
                var rating50 = new Rating { User = user2, Movie = lordOfTheRings, Grade = 5 };
                var rating51 = new Rating { User = user3, Movie = lordOfTheRings, Grade = 4 };

                var rating52 = new Rating { User = user1, Movie = matrix, Grade = 5 };
                var rating53 = new Rating { User = user2, Movie = matrix, Grade = 5 };
                var rating54 = new Rating { User = user3, Movie = matrix, Grade = 4 };

                Ratings?.AddRange(rating40, rating41, rating42, rating43, rating44, rating45, rating46, rating47, rating48,
                                  rating49, rating50, rating51, rating52, rating53, rating54);


                Ratings?.AddRange(rating10, rating11, rating12, rating13, rating14, rating15, rating16, rating17, rating18,
                                  rating19, rating20, rating21, rating22, rating23, rating24, rating25, rating26, rating27,
                                  rating28, rating29, rating30, rating31, rating32, rating33, rating34, rating35, rating36,
                                  rating37, rating38, rating39);

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
