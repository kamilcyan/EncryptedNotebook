using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptedNotebook
{
    public class Notes
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
        public string Body { get; set; }
    }
}
