﻿using NotesApp.Models;
using NotesApp.ViewModels.Commands;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.ViewModels
{
    public class LoginViewModel
    {
        private User _user;

        public User User
        {
            get { return _user; }
            set { _user = value; }
        }

        public RegisterCommand RegisterCommand { get; set; }

        public LoginCommand LoginCommand { get; set; }

        public event EventHandler HasLoggedIn;

        public LoginViewModel()
        {
            User = new User();

            RegisterCommand = new RegisterCommand(this);
            LoginCommand = new LoginCommand(this);
        }

        public void Login()
        {
            using (SQLiteConnection connection = new SQLiteConnection(DatabaseHelper.DatabaseFile))
            {
                connection.CreateTable<User>();
                var user = connection.Table<User>().Where(u => u.Username == User.Username).FirstOrDefault();

                // TODO 
                if (user.Password == User.Password)
                {
                    App.UserId = user.Id.ToString();
                    HasLoggedIn(this, EventArgs.Empty);
                }
            }
        }

        public void Register()
        {
            using (SQLiteConnection connection = new SQLiteConnection(DatabaseHelper.DatabaseFile))
            {
                connection.CreateTable<User>();
                var result = DatabaseHelper.Insert(User);
                if (result)
                {
                    App.UserId = User.Id.ToString();
                    HasLoggedIn(this, EventArgs.Empty);
                }
            }
        }
    }
}
