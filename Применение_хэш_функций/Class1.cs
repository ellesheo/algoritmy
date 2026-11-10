using System;

namespace ConsoleApp5
{
    public class UserCredentials
    {
        public byte[] Salt { get; set; }
        public byte[] PasswordHash { get; set; }

        public UserCredentials(byte[] sol, byte[] hesh)
        {
            Salt = sol;
            PasswordHash = hesh;
        }
    }

    public class OshibkaAuth : Exception
    {
        public OshibkaAuth(string soobshenie) : base(soobshenie)
        {
        }
    }
}