using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Windows;
using Flow.Launcher.Plugin;

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
            string md5Output = string.Empty;
            string sha1Output = string.Empty;
            string sha256Output = string.Empty;

            string iconPath = "Images/logo.png";

            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                md5Output = Convert.ToHexString(hashBytes).ToLower();
            }

            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = sha1.ComputeHash(inputBytes);

                sha1Output = Convert.ToHexString(hashBytes).ToLower();
            }

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                sha256Output = Convert.ToHexString(hashBytes).ToLower();
            }

            Result md5Result = new Result
            {
                Title = "md5",
                SubTitle = md5Output,
                IcoPath = iconPath,
                Action = (ActionContext context) =>
                {
                    Clipboard.SetText(md5Output);
                    return Clipboard.GetText() == md5Output;
                }
            };

            Result sha1Result = new Result
            {
                Title = "sha1",
                SubTitle = sha1Output,
                IcoPath = iconPath,
                Action = (ActionContext context) =>
                {
                    Clipboard.SetText(sha1Output);
                    return Clipboard.GetText() == sha1Output;
                }
            };

            Result sha256Result = new Result
            {
                Title = "sha256",
                SubTitle = sha256Output,
                IcoPath = iconPath,
                Action = (ActionContext context) =>
                {
                    Clipboard.SetText(sha256Output);
                    return Clipboard.GetText() == sha256Output;
                }
            };

            return new List<Result>()
            {
                md5Result,
                sha1Result,
                sha256Result
            };
        }
    }
}