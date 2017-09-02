namespace Bsharp.Repository
{
	using System;
	using System.Collections.Generic;
    using Bsharp.Domain;
    using MongoDB.Driver;

    public class MongoRepository : IBSharpRepository
    {
        private string _connection;
        private MongoClient _client;
		private IMongoDatabase _database;
        private object _lock;

        public MongoRepository(string connection = "mongodb://localhost")
        {
            _connection = connection;
			_client = new MongoClient(_connection);

			_database = _client.GetDatabase("BSharp");

			CreateUserIndexes();
            CreateArenaIndexes();
            CreateVoteIndexes();
        }

        public Arena Arena(string title)
        {
			var collection = _database.GetCollection<Arena>("arenas");
            var arena = collection.Find(x => x.Title == title)
								 .FirstOrDefault();
            return arena;
        }

        public IEnumerable<Arena> Arenas()
        {
            var collection = _database.GetCollection<Arena>("arenas");
            return collection.Find(x => true).ToList();
        }

        public void CreateArena(Arena arena)
        {
			var collection = _database.GetCollection<Arena>("arenas");

			collection.InsertOne(arena);
        }

        public void CreateSong(Song song)
        {
			var collection = _database.GetCollection<Song>("songs");

			collection.InsertOne(song);
        }

        public void CreateUser(User user)
        {
			var collection = _database.GetCollection<User>("users");

			user.Id = Guid.NewGuid();

			collection.InsertOne(user);
        }

		public IEnumerable<User> Users()
		{
			var collection = _database.GetCollection<User>("Users");
			return collection.Find(x => true).ToList();
		}

        public void DeleteArena(string title)
        {
			var collection = _database.GetCollection<Arena>("arenas");
            collection.DeleteOne(x => x.Title == title);
        }

        public void DeleteSong(string id)
        {
			var collection = _database.GetCollection<Song>("songs");
            collection.DeleteOne(x => x.Id == id);
        }

        public void DeleteUser(string email)
        {
			var collection = _database.GetCollection<User>("users");
			collection.DeleteOne(x => x.Email == email);
        }

        public Song Song(string name, string artistName, string albumName)
        {
			var collection = _database.GetCollection<Song>("songs");
            var song = collection.Find(x => x.Name == name && 
                                       x.Album == albumName && 
                                       x.Artist.Name == artistName)
								 .FirstOrDefault();
			return song;
        }

        public IEnumerable<Song> Songs()
        {
			var collection = _database.GetCollection<Song>("songs");
			return collection.Find(x => true).ToList();
        }

        public void UpdateUser(User user)
        {
			var users = _database.GetCollection<User>("users");
            var filter = Builders<User>.Filter.Eq(m => m.Id, user.Id);
			var update = Builders<User>
					.Update
                    .Set(x => x.Email, user.Email)
                    .Set(x => x.Handle, user.Handle);

			users.UpdateOne(filter, update);
        }

        public User User(string email)
        {
			var collection = _database.GetCollection<User>("users");
            var user = collection.Find(x => x.Email == email)
                                 .FirstOrDefault();

            if(user == null)
            {
                throw new Exception("User not found");
            }
			
			return user;
        }

		public void UpdateArena(Arena arena)
		{
			var a = _database.GetCollection<Arena>("arenas");
			var filter = Builders<Arena>.Filter.Eq(m => m.Title, arena.Title);
			var update = Builders<Arena>
					.Update
					.Set(x => x.Tiers[arena.CurrentTier], arena.Tiers[arena.CurrentTier])
					.Set(x => x.Winner, arena.Winner)
					.Set(x => x.CurrentTier, arena.CurrentTier);

			a.UpdateOne(filter, update);
		}

        public void Vote(Vote vote)
        {
			var collection = _database.GetCollection<Vote>("votes");

			vote.Id = Guid.NewGuid();

			collection.InsertOne(vote);
        }

		private void CreateUserIndexes()
		{
			var collection =
				_database.GetCollection<User>("users");

			var options = new CreateIndexOptions() { Unique = true, 
                Sparse = true };

			var key = new StringFieldDefinition<User>("Email");
			
			var indexDefinition = new IndexKeysDefinitionBuilder<User>()
				.Ascending(key);

			collection.Indexes.CreateOne(indexDefinition, options);
		}

		private void CreateArenaIndexes()
		{
			var collection =
				_database.GetCollection<Arena>("arenas");

			var options = new CreateIndexOptions()
			{
				Unique = true,
				Sparse = true
			};

			var key = new StringFieldDefinition<Arena>("Title");

			var indexDefinition = new IndexKeysDefinitionBuilder<Arena>()
				.Ascending(key);

			collection.Indexes.CreateOne(indexDefinition, options);
		}

		private void CreateVoteIndexes()
		{
			var collection =
				_database.GetCollection<Vote>("votes");

			var options = new CreateIndexOptions()
			{
				Unique = true,
				Sparse = true
			};

			var key1 = new StringFieldDefinition<Vote>("BattleId");
            var key2 = new StringFieldDefinition<Vote>("Email");

			var indexDefinition = new IndexKeysDefinitionBuilder<Vote>()
                .Ascending(key1).Ascending(key2);

			collection.Indexes.CreateOne(indexDefinition, options);
		}

        public void SubmitForNextBattle(Song song)
        {
            throw new NotImplementedException();
        }
    }
}
