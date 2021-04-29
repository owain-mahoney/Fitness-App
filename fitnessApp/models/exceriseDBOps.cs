using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Essentials;
using System.Threading.Tasks;
namespace fitnessApp.models
 
{
    class exceriseDBOps
    {
        private SQLiteAsyncConnection _conncetion;
        private Databasemodel _dbModel;
        public exceriseDBOps()
        {
            _dbModel = new Databasemodel();
            _conncetion = _dbModel.GetConnection();
            _conncetion.CreateTableAsync<Tables.Point>();
            _conncetion.CreateTableAsync<Tables.Person>();
           
        }
        public async Task<Tables.Person> GetPersonAsync()
        {
            return await _conncetion.FindAsync<Tables.Person>(1);
        }
        public async Task AddPoint(Tables.Point point)
        {
            await _conncetion.InsertAsync(point);
        }
        public async Task<Tables.Point>GetLastPoint(int id )
        {
            return await _conncetion.FindAsync<Tables.Point>(id);
        }

        public async Task UpdatePerson(Tables.Person person)
        {
            await _conncetion.UpdateAsync(person);
        }
        public async Task InsertPerson(Tables.Person person)
        {
            await _conncetion.InsertAsync(person);
        }
        
    }
}
