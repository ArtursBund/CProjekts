using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CProjekts
{
    public class DataStorage
    {
        public double[] X_Data_G { get; set; } // X ass dati
        public double[] X_Diff_G { get; set; } // Pirmā signāla Y ass dati
        public double[] Y_Diff_G { get; set; } // Otrā signāla Y ass dati
        public double[] Abs_X_Data { get; set; } // X ass dati pēc apstrādes
        public double[] Abs_Diff { get; set; } // Apkopotais signāls
        public double[] Abs_Diff_Fit { get; set; } // Analītiskā funkcija
        public string[] Data_Raw { get; set; } // Ielasītais fails kāds ir
    }
}
