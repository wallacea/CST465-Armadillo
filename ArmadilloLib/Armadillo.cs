using System;

namespace ArmadilloLib
{
    public class Armadillo
    {
        public int ID{get;set;}
        public string Name {get;set;}
        public string Description { get; set; }
        public int Age {get;set;}
        public int ShellHardness
        {
            get
            {
                return _ShellHardness;
            }
            set
            {
                if(value < 0)
                {
                    _ShellHardness = 0;
                }
                else if(value > 10)
                {
                    _ShellHardness = 10;
                }
                else
                {
                    _ShellHardness = value;
                }
            }
        }
        private int _ShellHardness;
        public bool IsPainted {get;set;}
        public string Homeland {get;set;}
    }
}
