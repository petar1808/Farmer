using Domain.Exceptions;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FarmerDomain.Test
{
    public class WorkingSeasonTets
    {
        [Fact]
        public void AddWorkingSeasonNameAndStartDateAndEndDateIsvalid()
        {
            var workingSeason = new WorkingSeason("2021/2022", DateTime.Today, DateTime.Today);

            Assert.Equal("2021/2022", workingSeason.Name);
            Assert.Equal(DateTime.Today, workingSeason.StartDate);
            Assert.Equal(DateTime.Today, workingSeason.EndDate);
        }

        [Fact]
        public void AddWorkingSeasonNameIsvalid()
        {
            var workingSeason = new WorkingSeason("2021/2022");

            Assert.Equal("2021/2022", workingSeason.Name);
            Assert.Equal(null!, workingSeason.StartDate);
            Assert.Equal(null!, workingSeason.EndDate);
        }

        [Fact]
        public void UpdateWorkingSeasonNameAndStartDateAndEndDateIsvalid()
        {
            var workingSeason = new WorkingSeason("2021/2022", DateTime.Today, DateTime.Today);

            workingSeason.UpdateName("2022/2023");
            workingSeason.UpdateSratDate(DateTime.Today.AddYears(1));
            workingSeason.UpdateEndDate(DateTime.Today.AddYears(2));

            Assert.Equal("2022/2023", workingSeason.Name);
            Assert.Equal(DateTime.Today.AddYears(1), workingSeason.StartDate);
            Assert.Equal(DateTime.Today.AddYears(2), workingSeason.EndDate);
        }

        [Fact]
        public void UpdateWorkingSeasonNameWithNull()
        {
            var workingSeason = new WorkingSeason("2021/2022");

            Assert.Throws<DomainException>(() => workingSeason.UpdateName(null!));
        }

        [Fact]
        public void UpdateWorkingSeasonNameUnderMinimumLenght()
        {
            var workingSeason = new WorkingSeason("2021/2022");

            Assert.Throws<DomainException>(() => workingSeason.UpdateName("2021"));
        }

        [Fact]
        public void UpdateWorkingSeasonNameOverMaximumLenght()
        {
            var workingSeason = new WorkingSeason("2021/2022");

            Assert.Throws<DomainException>(() => workingSeason.UpdateName("2021/2022222"));
        }
    }
}
