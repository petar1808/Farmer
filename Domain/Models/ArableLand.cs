using Domain.Common;
using Domain.Exceptions;
using static Domain.ModelConstraint.CommonConstraints;

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

        private void ValidateName(string name)
        {
            if (name.Length > MaxNameLenght)
            {
                throw new DomainException(nameof(ArableLand));
            }
        }
    } 
}
