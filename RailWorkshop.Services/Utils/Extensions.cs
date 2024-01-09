﻿using System.Security.Cryptography;
using System.Text;

namespace RailWorkshop.Services.Utils
{
    public static class Extensions
    {
        public static string ToSha256Hash(this string value)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            byte[] hash = SHA256.HashData(bytes);
            return Convert.ToHexString(hash).ToLower();
        }
    }
}