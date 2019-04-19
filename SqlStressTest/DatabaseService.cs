using System;
using System.IO;
using SQLite;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SqlStressTest
{
    public class DatabaseService
    {
        public SQLiteAsyncConnection Connection { get; private set; }

        private static DatabaseService _instance;
        private static readonly object padlock = new object();

        public static DatabaseService Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new DatabaseService();
                    }

                    return _instance;
                }
            }
        }

        private DatabaseService()
        {
        }

        public async Task Initialize()
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MedRep.db");

            Connection = new SQLiteAsyncConnection(databasePath);

            // Create tables for models
            //await Connection.CreateTableAsync<TModel>();
        }

        public async Task<List<TModel>> Get<TModel>() where TModel : BaseDBModel, new()
        {
            return await Connection.Table<TModel>().ToListAsync();
        }

        public async Task<int> Post<TModel>(TModel model) where TModel : BaseDBModel
        {
            return await Connection.InsertAsync(model);
        }

        public async Task<int> Put<TModel>(TModel model) where TModel : BaseDBModel
        {
            return await Connection.UpdateAsync(model);
        }

        public async Task<int> Delete<TModel>(TModel model) where TModel : BaseDBModel
        {
            return await Connection.DeleteAsync(model);
        }
    }
}
