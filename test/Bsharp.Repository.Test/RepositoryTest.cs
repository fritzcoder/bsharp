namespace Bsharp.Repository.Test
{
	using System;
    using System.Collections.Generic;
    using System.Linq;
    using Bsharp.Repository;
    using Bsharp.Domain;
    using Xunit;
    using MongoDB.Driver;

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
            var artist = "test Artist";
			var song = new Song("test@bsharp.io", artist,"testsong","testalbum",
                                60,DateTime.Now);
            
            repo.DeleteSong(song.Id);
            
            repo.CreateSong(song);
            song = repo.Song(song.Name,song.Artist,song.Album);
			Assert.NotNull(song);
			
            repo.DeleteSong(song.Id);

			try
			{
				song = repo.Song(song.Name, song.Artist, song.Album);
				Assert.True(false);
			}
			catch (Exception ex)
			{
				Assert.True(true, ex.Message);
			}
        }

		[Fact]
		public void CreateGetDeleteSongById()
		{
			var repo = new MongoRepository("mongodb://localhost");
            var artist = "test Artist";

			var song = new Song("test@bsharp.io", artist, "testsong", "testalbum",
								60, DateTime.Now);

			repo.DeleteSong(song.Id);

			repo.CreateSong(song);
            song = repo.Song(song.Name, song.Artist, song.Album);
			Assert.NotNull(song);

			repo.DeleteSong(song.Id);

			try
			{
				song = repo.Song(song.Id);
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
                var artist = string.Format("Artist{0}", i);
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

        [Fact]
        public void TestVote()
        {
            var repo = new MongoRepository("mongodb://localhost");
            var arenaId = Guid.NewGuid();
            var battleId = Guid.NewGuid();
            var vote = new Vote(arenaId, battleId, "test@bsharp.io", 
                               0,"testsong");
            repo.Vote(vote);
            vote.Email = "test1@bsharp.io";
            repo.Vote(vote);
            try
            {
                repo.Vote(vote);
                Assert.True(false);
            }
            catch(MongoWriteException ex)
            {
                Assert.True(true,ex.Message);
            }
        }
    }
}
