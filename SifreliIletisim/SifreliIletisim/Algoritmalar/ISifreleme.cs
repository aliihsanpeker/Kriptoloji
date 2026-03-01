using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SifreliIletisim.Algoritmalar
{
    public interface ISifreleme
    {
        string Sifrele(string metin, string anahtar);
        string Coz(string sifreliMetin, string anahtar);
    }
}
