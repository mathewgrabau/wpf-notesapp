using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.ViewModels
{
    public class DatabaseHelper
    {
        /// <summary>
        /// Contains the path where we store it.
        /// </summary>
        static string _dbFile = Path.Combine(Environment.CurrentDirectory, "notesDatabase.db3");

        public static bool Insert<T>(T item)
        {
            var result = false;

            using (SQLiteConnection connection = new SQLiteConnection(_dbFile))
            {
                // Ensure the table exists, and then insert it.
                connection.CreateTable<T>();
                var modifiedRows = connection.Insert(item);
                result = modifiedRows > 0;
            }

            return result;
        }

        public static bool Update<T>(T item)
        {
            var result = false;

            using (SQLiteConnection connection = new SQLiteConnection(_dbFile))
            {
                // Ensure the table exists, and then insert it.
                connection.CreateTable<T>();
                int modifiedRows = connection.Update(item);
                result = modifiedRows > 0;
            }

            return result;
        }

        public static bool Delete<T>(T item)
        {
            var result = false;

            using (SQLiteConnection connection = new SQLiteConnection(_dbFile))
            {
                // Ensure the table exists, and then insert it.
                connection.CreateTable<T>();
                int modifiedRows = connection.Delete(item);
                result = modifiedRows > 0;
            }

            return result;
        }
    }
}