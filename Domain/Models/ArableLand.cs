using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ArableLand : Entity<int>
    {
        public ArableLand(string name, int sizeInDecar)
        {
            Name = name;
            SizeInDecar = sizeInDecar;
        }

        public string Name { get; private set; }

        public int SizeInDecar { get; private set; }

        public ArableLand UpdateName(string name)
        {
            this.Name = name;
            return this;
        }

        public ArableLand UpdateSizeInDecar(int size)
        {
            this.SizeInDecar = size;
            return this;
        }

        
    } 
}
