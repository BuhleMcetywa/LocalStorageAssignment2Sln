using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalStorageAssignment2.Models
{
    internal class Profile
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string Bio { get; set; }
        public string PicturePath { get; internal set; }

    }
}
