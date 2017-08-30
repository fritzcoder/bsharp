namespace Bsharp.Repository.Test
{
	using System;
    using System.Collections.Generic;
    using System.Linq;
    using Bsharp.Repository;
    using Bsharp.Domain;
    using Xunit;

    public class RepositoryTest
    {
        [Fact]
        public void CreateGetUpdateDeleteUser()
        {
            var repo = new MongoRepository("mongodb://localhost");
            var user = new User();
            user.Email = "test@bsharp.io";
            user.Handle = "bsharptest";
            repo.DeleteUser(user.Email);
            repo.CreateUser(user);
            user = repo.User(user.Email);
            Assert.NotNull(user);
            user.Email = "test1@bsharp.io";
            repo.UpdateUser(user);
            user = repo.User(user.Email);
            Assert.NotNull(user);
            repo.DeleteUser(user.Email);

            try
            {
                user = repo.User(user.Email);
                Assert.True(false);
            }
            catch(Exception ex)
            {
                Assert.True(true, ex.Message);
            }
        }

        [Fact]
        public void CreateGetDeleteSong()
        {
			var repo = new MongoRepository("mongodb://localhost");
            var artist = new Artist("test Artist", 
                                    new [] {"thisguy", "thatguy"},DateTime.Now);
			var song = new Song("test@bsharp.io", artist,"testsong","testalbum",
                                60,DateTime.Now);
            
            repo.DeleteSong(song.Id);
            
            repo.CreateSong(song);
            song = repo.Song(song.Name,song.Artist.Name,song.Album);
			Assert.NotNull(song);
			
            repo.DeleteSong(song.Id);

			try
			{
				song = repo.Song(song.Name, song.Artist.Name, song.Album);
				Assert.True(false);
			}
			catch (Exception ex)
			{
				Assert.True(true, ex.Message);
			}
        }

        [Fact]
        public void CreateGetDeleteArena()
        {
            var count = 4;
            var repo = new MongoRepository("mongodb://localhost");
            var songs = new List<Song>();

            for (int i = 0; i < 32; i++)
            {
                var artist = new Artist(string.Format("Artist{0}", i),
                                        new string[] { "Test1", "test2" },
                                        DateTime.Now);
                songs.Add(
                    new Song(string.Format("test{0}@bsharp.io",i),
                             artist,
                             string.Format("Name{0}", i),
                             string.Format("Album{0}", i),
                             i, DateTime.Now));
            }

            var arena = new Arena("testarena1", songs);
            var tierCount = arena.Tiers.ToList().Count();

            Assert.Equal(count, tierCount);
            repo.CreateArena(arena);
            var currentTier = arena.GetCurrentTier();

            foreach(var battle in currentTier.Battles.ToList())
            {
                battle.Song1.VoteCount++;
            }

            repo.UpdateArena(arena);
            arena.CreateNextTier();
			currentTier = arena.GetCurrentTier();

			foreach (var battle in currentTier.Battles.ToList())
			{
				battle.Song1.VoteCount++;
			}

            repo.UpdateArena(arena);
			arena.CreateNextTier();
			currentTier = arena.GetCurrentTier();

			foreach (var battle in currentTier.Battles.ToList())
			{
				battle.Song1.VoteCount++;
			}

            repo.UpdateArena(arena);
			arena.CreateNextTier();
			currentTier = arena.GetCurrentTier();

			foreach (var battle in currentTier.Battles.ToList())
			{
			    battle.Song1.VoteCount++;
			}

            repo.UpdateArena(arena);
            arena.CreateNextTier();

			repo.UpdateArena(arena);
            Assert.NotNull(arena.Winner);
        }
    }
}
