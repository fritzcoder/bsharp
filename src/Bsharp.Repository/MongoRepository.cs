namespace Bsharp.Repository
{
	using System;
	using System.Collections.Generic;
	using Bsharp.Repository.Domain;
    using MongoDB.Driver;

    public class MongoRepository : IBSharpRepository
    {
        private string _connection;
        private MongoClient _client;
		private IMongoDatabase _database;


		public MongoRepository(string connection)
        {
            _connection = connection;
			_client = new MongoClient(_connection);

			_database = _client.GetDatabase("BSharp");

			CreateUserIndexes();
        }
        public Arena Arena(string title)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Arena> Arenas()
        {
            throw new NotImplementedException();
        }

        public void CreateArena(string title, IEnumerable<Song> songs)
        {
            throw new NotImplementedException();
        }

        public void CreateBattle(DateTime time, Song song1, Song song2)
        {
            throw new NotImplementedException();
        }

        public void CreateSong(Guid userId, Song song)
        {
            throw new NotImplementedException();
        }

        public void CreateUser(User user)
        {
			var collection = _database.GetCollection<User>("users");

			user.Id = Guid.NewGuid();

			collection.InsertOne(user);
        }

        public void DeleteArena(string title)
        {
            throw new NotImplementedException();
        }

        public void DeleteSong(Song song)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(string email)
        {
			var collection = _database.GetCollection<User>("users");
			collection.DeleteOne(x => x.Email == email);
        }

        public void DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public Song Song()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Song> Songs()
        {
            throw new NotImplementedException();
        }

        public void SubmitForNextBattle(Song song)
        {
            throw new NotImplementedException();
        }

        public void UpdateSong(Song song)
        {
            throw new NotImplementedException();
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

		private void CreateUserIndexes()
		{
			var collection =
				_database.GetCollection<User>("users");

			var options = new CreateIndexOptions() { Unique = true, Sparse = true };

			var key = new StringFieldDefinition<User>("Email");
			
			var indexDefinition = new IndexKeysDefinitionBuilder<User>()
				.Ascending(key);

			collection.Indexes.CreateOne(indexDefinition, options);
		}

		/*private void CreateRoleIndexes()
		{
			var collection =
				database.GetCollection<Role>("roles");

			var options = new CreateIndexOptions() { Unique = true, Sparse = true };

			var key1 = new StringFieldDefinition<Role>("Name");
			var key2 = new StringFieldDefinition<Role>("DoaminId");
			var indexDefinition = new IndexKeysDefinitionBuilder<Role>()
				.Ascending(key1).Ascending(key2);

			collection.Indexes.CreateOne(indexDefinition, options);
		}

		private void CreateManagerIndexes()
		{
			var collection =
				database.GetCollection<Manager>("managers");

			var options = new CreateIndexOptions() { Unique = true, Sparse = true };

			var key1 = new StringFieldDefinition<Manager>("UserName");

			var indexDefinition = new IndexKeysDefinitionBuilder<Manager>()
				.Ascending(key1);

			collection.Indexes.CreateOne(indexDefinition, options);
		}

		private void CreateAdminIndexes()
		{
			var collection =
				database.GetCollection<Administrator>("administrators");

			var options = new CreateIndexOptions() { Unique = true, Sparse = true };

			var key1 = new StringFieldDefinition<Administrator>("DomainId");
			var key2 = new StringFieldDefinition<Administrator>("ManagerId");
			var indexDefinition = new IndexKeysDefinitionBuilder<Administrator>()
				.Ascending(key1).Ascending(key2);

			collection.Indexes.CreateOne(indexDefinition, options);
		}*/
	}
}
