using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using GameContext;
using GameLib;
using System.Collections;

namespace CodeFirstManyToMany
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (GameContextDB db = new GameContextDB())
                {
                    //первая миграция
                    List<Game> games = db.Games.ToList();

                    if (games == null || games.Count == 0 )
                    {
                        Genre genre1 = new Genre { Title = "RTS" };
                        Genre genre2 = new Genre { Title = "Action" };
                        Genre genre3 = new Genre { Title = "RPG" };
                        db.Genres.Add(genre1);
                        db.Genres.Add(genre2);
                        db.Genres.Add(genre3);

                        Studio studio1 = new Studio { Title = "SEK" };
                        Studio studio2 = new Studio { Title = "NCSoft" };
                        db.Studios.Add(studio1);
                        db.Studios.Add(studio2);

                        Game game1 = new Game { Title = "Paraworld", Studio=studio1,Genres = new List<Genre> { genre1 } , ReleaseDate = new DateTime(2006,9,15) };
                        Game game2 = new Game { Title = "Aion", Studio=studio2,Genres = new List<Genre> { genre2,genre3 } , ReleaseDate = new DateTime(2008,11,25) };
                        Game game3 = new Game { Title = "Counter Strike", Studio = studio1, Genres = new List<Genre> { genre2 }, ReleaseDate = new DateTime (2000,11,8) };
                        db.Games.Add(game1);
                        db.Games.Add(game2);
                        db.Games.Add(game3);
                    }
                    db.SaveChanges();

                    games = db.Games.ToList();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Первый вывод\n");
                    Console.ResetColor();
                    foreach (var g in games)
                    {
                        Console.WriteLine("Название: "+g.Title);
                        Console.WriteLine("Студия: " + g.Studio);
                        Console.Write("Жанр: ");
                        foreach (var s in g.Genres)
                        {
                            Console.Write(" " + s.Title);
                        }
                        Console.WriteLine("\nДата релиза: "+ g.ReleaseDate);
                        Console.WriteLine();
                    }


                    //вторая миграция

                    games = db.Games.ToList();

                    if (games.Count < 4)
                    {
                        foreach (var g in games)
                        {
                            g.Mode = GameMode.MultiPlayer;
                        }
                        Studio studio3 = new Studio { Title = "Valve Corporation" };
                        db.Add(studio3);

                        Genre genre4 = new Genre { Title = "Puzzle" };
                        db.Genres.Add(genre4);

                        Game game4 = new Game { Title = "Portal", Studio = studio3, Genres = new List<Genre> { genre4 }, Mode = GameMode.SinglePlayer, ReleaseDate = new DateTime(2007, 10, 10) };
                        db.Games.Add(game4);
                        db.SaveChanges();

                    }

                    games = db.Games.ToList();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Второй вывод\n");
                    Console.ResetColor();
                    foreach (var g in games)
                    {
                        Console.WriteLine("Название: " + g.Title);
                        Console.WriteLine("Студия: " + g.Studio);
                        Console.Write("Жанр: ");
                        foreach (var s in g.Genres)
                        {
                            Console.Write(" " + s.Title);
                        }
                        Console.WriteLine("\nДата релиза: " + g.ReleaseDate);
                        Console.WriteLine("Режим игры: " + g.Mode);
                        Console.WriteLine();
                    }




                    //третья миграция

                    games = db.Games.ToList();

                    if (games.Count < 5)
                    {
                        Random random = new Random();
                        foreach (var g in games)
                        {
                            g.SaledCopies = random.Next(1000,10001);
                        }
                        Studio studio4 = new Studio { Title = "PUBG Corporation" };
                        db.Add(studio4);

                        Genre genre5 = new Genre { Title = "Battleground" };
                        db.Genres.Add(genre5);

                        Game game5 = new Game { Title = "PUBG", Studio = studio4, Genres = new List<Genre> { genre5 }, Mode = GameMode.MultiPlayer, ReleaseDate = new DateTime(2017, 11, 19), SaledCopies=10000};
                        db.Games.Add(game5);
                        db.SaveChanges();

                    }

                    games = db.Games.ToList();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Третий вывод\n");
                    Console.ResetColor();
                    foreach (var g in games)
                    {
                        Console.WriteLine("Название: " + g.Title);
                        Console.WriteLine("Студия: " + g.Studio);
                        Console.Write("Жанр: ");
                        foreach (var s in g.Genres)
                        {
                            Console.Write(" " + s.Title);
                        }
                        Console.WriteLine("\nДата релиза: " + g.ReleaseDate);
                        Console.WriteLine("Режим игры: " + g.Mode);
                        Console.WriteLine("Количество проданных копий: " + g.SaledCopies);
                        Console.WriteLine();
                    }

                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}