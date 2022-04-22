using Domain.Models;
using Domain.Exceptions;
using Xunit;

namespace FarmerDomain.Test
{
    public class ArableLandTest
    {
        [Fact]
        public void AddArableLandNameAndSizeInDecarIsValid()
        {
            var arableLand = new ArableLand("???????", 50);

            Assert.Equal("???????", arableLand.Name);
            Assert.Equal(50, arableLand.SizeInDecar);
        }

        [Fact]
        public void UpdateArableLandNameAndSizeInDecarIsValid()
        {
            var arableLand = new ArableLand("???????", 50);

            arableLand.UpdateName("????????");
            arableLand.UpdateSizeInDecar(60);

            Assert.Equal("????????", arableLand.Name);
            Assert.Equal(60, arableLand.SizeInDecar);
        }

        [Fact]
        public void UpdateArableLandNameAndSizeInDecarIsInvalid()
        {
            var arableLand = new ArableLand("???????", 50);

            Assert.Throws<DomainException>(() => arableLand.UpdateName(null!));
            Assert.Throws<DomainException>(() => arableLand.UpdateSizeInDecar(-5));
        }

        [Fact]
        public void UpdateArableLandNameIsInvalid()
        {
            var arableLand = new ArableLand("???????", 50);

            Assert.Throws<DomainException>(() => arableLand.UpdateName("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"));
        }
    }
}