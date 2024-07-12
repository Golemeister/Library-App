using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication4
{
    class Korisnik
    {
        
        public String username;
        public String password;
        public String ime;
        public String prezime;
        public String tip;//admin ili obican korisnik
        

        public Korisnik(string u, string p, string a)
        {
            username = u;
            password = p;
            tip = a;
            

        }
        
    }
}
