﻿using IdentityModel;
using System.Security.Cryptography;
using System.Text;

namespace Sha256Helper
{
    public class Sha256Helper : ISha256Helper
    {
        public string ComputeHash(string codeVerifier)
        {
            using var sha256 = SHA256.Create();
            var challengeBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier));
            var codeChallenge = Base64Url.Encode(challengeBytes);
            return codeChallenge;
        }
    }
}
