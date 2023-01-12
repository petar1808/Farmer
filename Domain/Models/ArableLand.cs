using Domain.Common;
using Domain.Exceptions;
using static Domain.ModelConstraint.CommonConstraints;

namespace Domain.Models
{
    public class ArableLand : Entity<int>, ITenant
    {
        public ArableLand(string name, int sizeInDecar)
        {
            Validate(name, sizeInDecar);
            Name = name;
            SizeInDecar = sizeInDecar;
        }

        public string Name { get; private set; }

        public int SizeInDecar { get; private set; }

        public List<Seeding> Seedings { get; } = default!;
        public int TenantId { get; set; }

        public ArableLand UpdateName(string name)
        {
            ValidateName(name);
            this.Name = name;
            return this;
        }

        public ArableLand UpdateSizeInDecar(int size)
        {
            ValidateArableLand(size);
            this.SizeInDecar = size;
            return this;
        }   

        private void ValidateName(string name)
            => Guard.Guard.ForStringMaxLength(
                name,
                MaxNameLenght,
                nameof(this.Name));

        private void ValidateArableLand(int size)
            => Guard.Guard.ForPositiveNumber(size, nameof(SizeInDecar));

        private void Validate(string name, int size)
        {
            ValidateName(name);
            ValidateArableLand(size);
        }
    } 
}
