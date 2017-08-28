namespace Bsharp.Repository.Test
{
	using System;
    using Bsharp.Repository.Domain;
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
    }
}
