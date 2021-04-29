using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using SQLite;
using System.IO;

namespace fitnessApp.models
{
    class Databasemodel
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var libFolder = FileSystem.AppDataDirectory;
            var dbname = "database.db3";
            var dbPath = Path.Combine(libFolder, dbname);
            return new SQLiteAsyncConnection(dbPath);
        }
    }
}
