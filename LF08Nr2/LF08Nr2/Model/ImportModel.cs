using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF08Nr2.Model
{
    public class ImportModel
    {
        public string schoolClass { get; set; }
        public string Name { get; set; }

        public string Lastname { get; set; }

        public bool isInM1Monday { get; set; } = false;

        public bool isInM1Tuesday { get;set; } = false;

        public bool isInD1Monday { get;set; } = false;

        public bool isInD1Thursday { get;set; } = false;

    }
}
