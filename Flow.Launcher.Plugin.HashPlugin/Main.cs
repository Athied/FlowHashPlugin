using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Windows;
using System.Text;
using Flow.Launcher.Plugin;
using System.Runtime.Intrinsics.Arm;

namespace Flow.Launcher.Plugin.HashPlugin
{
#pragma warning disable CS1591
    public class HashPlugin : IPlugin
    {
        private PluginInitContext _context;

        public void Init(PluginInitContext context)
        {
            _context = context;
        }

        public List<Result> Query(Query query)
        {
            string input = query.Search;

            string iconPath = "Images/logo.png";

            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] inputBytesUTF8 = Encoding.UTF8.GetBytes(input);

            return new List<Result>()
            {
                MakeResult("md5", HashString(inputBytes, MD5.Create()), iconPath),
                MakeResult("sha1", HashString(inputBytes, SHA1.Create()), iconPath),
                MakeResult("sha256", HashString(inputBytes, SHA256.Create()), iconPath),
                MakeResult("sha384", HashString(inputBytes, SHA384.Create()), iconPath),
                MakeResult("sha512", HashString(inputBytes, SHA512.Create()), iconPath),
                MakeResult("base64", Convert.ToBase64String(inputBytesUTF8), iconPath),
                MakeResult("uppercase", input.ToUpper(), iconPath),
                MakeResult("lowercase", input.ToLower(), iconPath),
            };
        }

        static Result MakeResult(string title, string output, string iconPath)
        {
            return new Result
            {
                Title = title,
                SubTitle = output,
                IcoPath = iconPath,
                Action = (ActionContext context) =>
                {
                    Clipboard.SetText(output);
                    return Clipboard.GetText() == output;
                }
            };
        }

        static string HashString(byte[] buffer, HashAlgorithm hashAlgorithm)
        {
            byte[] hashBytes = hashAlgorithm.ComputeHash(buffer);

            hashAlgorithm.Dispose();

            return Convert.ToHexString(hashBytes).ToLower();
        }
    }
}