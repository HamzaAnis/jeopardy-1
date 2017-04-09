﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DatabaseConnection
{
    /// <summary>
    /// A base type for objects that will interact with the database
    /// </summary>
    public abstract class DBTable
    {
        /// <summary>
        /// singleton mysql connection
        /// </summary>
        protected static MySqlConnection connection;

        /// <summary>
        /// This variable will contain additional information on the last error to occur
        /// </summary>
        public string errorMessage { get; set; }

        #region connection info
        /// <summary>
        /// MySQL server. this can be an IP or URL
        /// </summary>
        private const string SERVER = "mysql.cs690.mattsmithcs.com";
        /// <summary>
        /// Name of the database on to use on the server
        /// </summary>
        private const string DATABASE = "jeopardy_data";
        /// <summary>
        /// Username to use for logging into the database.
        /// </summary>
        private const string UID = "jeopardy";
        /// <summary>
        /// Password for logging into the database.
        /// </summary>
        private const string PASSWORD = "Po-Tay-Toes!";
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public DBTable()
        {
            if(connection==null)
            {
                string connectionString = "SERVER=" + SERVER + ";" + "DATABASE=" + DATABASE + ";" + "UID=" + UID + ";" + "PASSWORD=" + PASSWORD + ";";
                connection = new MySqlConnection(connectionString);
            }
            errorMessage = "";
        }

        /// <summary>
        /// Opens the MySQL Connection
        /// </summary>
        /// <returns>true if the operation succeeds and false if it fails</returns>
        protected bool openConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Closes the MySQL Connection
        /// </summary>
        /// <returns>true if the operation succeeds and false if it fails</returns>
        protected bool closeConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Creates a table in the database for the type if one does not already exist
        /// </summary>
        /// <returns>true if the operation succeeds and false if it fails</returns>
        public abstract bool createTable();

        /// <summary>
        /// Reads all records in a table
        /// </summary>
        /// <returns>A list of all records of the table or null if the read fails</returns>
        public abstract List<object> readAll();

        /// <summary>
        /// Reads one record from the able in the database.
        /// </summary>
        /// <param name="primaryKey">primary key of the record to read</param>
        /// <returns>The record from the database or null if it does not exist</returns>
        public abstract object readById(int primaryKey);

        /// <summary>
        /// Inserts one record into the table in the database. The key of the record will
        /// be ignored and a new one will be assigned.
        /// </summary>
        /// <param name="record">record to be inserted</param>
        /// <returns>the object that was inserted into the database including primary key</returns>
        public abstract bool insertRecord(object record);

        /// <summary>
        /// Updates the record in the database that matches the primary key in the passed record
        /// </summary>
        /// <param name="record">Record with updated information</param>
        /// <returns>true if the operation succeeds and false if it fails</returns>
        public abstract bool updateRecord(object record);

        /// <summary>
        /// Deletes the record with the provide key from the database
        /// </summary>
        /// <param name="primaryKey">Key of the record to delete</param>
        /// <returns>true if the operation succeeds and false if it fails</returns>
        public abstract bool deleteRecord(int primaryKey);

        
    }
}

