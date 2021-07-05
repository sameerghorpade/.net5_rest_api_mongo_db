using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Entities;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Linq;

namespace Admin.Repository
{
    public class UserRepository : IUserRepository
    {
        //collection name
        private const string _collectionName = "users";
        //database name
        private readonly string _dbName = "User";
        private  IMongoCollection<User> _userCollection;
        private readonly FilterDefinitionBuilder<User> _filterDefBuilder = Builders<User>.Filter; 
        public UserRepository(IMongoClient mongoClient)
        {
             var database = mongoClient.GetDatabase(_dbName);
             _userCollection = database.GetCollection<User>(_collectionName);
              
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userCollection.Find(new BsonDocument()).ToListAsync(); 
        }

        public async Task<User> GetUserByCredentialsAsync(string userName, string password)
        {
            var filterDef = _filterDefBuilder.Eq(x=>x.UserName,userName) & _filterDefBuilder.Eq(x=>x.Password,password);
            return await _userCollection.Find(filterDef).SingleOrDefaultAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var filterDef = _filterDefBuilder.Eq(x=>x.Email,email);
            return await _userCollection.Find(filterDef).SingleOrDefaultAsync();
        }

        public async Task<User> GetUserByIdAsync(Guid Id)
        {
            var filterDef = _filterDefBuilder.Eq(x=>x.Id,Id);
            return await _userCollection.Find(filterDef).SingleOrDefaultAsync();
            
        }
 
        public async Task CreateUserAsync(User newUser)
        {
           await _userCollection.InsertOneAsync(newUser);
            
        }

        public async Task<bool> DeleteUserAsync(Guid Id)
        {
            var filterDef = _filterDefBuilder.Eq(x=>x.Id,Id);
             return await Task.FromResult(_userCollection.DeleteOne(filterDef).IsAcknowledged);
        }
        
        public async Task UpdateUserAsync(User user)
        {
            var filterDef = _filterDefBuilder.Eq(x=>x.Id,user.Id);
            await _userCollection.ReplaceOneAsync(filterDef,user);
        } 

    }
}