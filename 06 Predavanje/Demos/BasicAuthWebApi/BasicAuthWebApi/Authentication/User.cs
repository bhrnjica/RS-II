﻿using System;

namespace BasicAuthWebApi.Authentication
{
    public class User
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
