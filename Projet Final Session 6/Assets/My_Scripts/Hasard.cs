using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class Hasard : Random
    {
        private static Hasard instance = null;
        private Hasard() : base()
        { }

        public static Hasard Get_Instance()
        {
            if (instance == null)
            {
                instance = new Hasard();
            }
            return instance;

        }
    }
}
