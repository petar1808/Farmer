using Domain.Common;
using Domain.Enum;

namespace Domain.Models
{
    public class Treatment : Entity<int>, ITenant
    {
        public Treatment(DateTime date,
            ТreatmentType treatmentType,
            int articleId,
            decimal articleQuantity,
            int seedingId)
        {
            ValidateТreatmentType(treatmentType);
            Date = date;
            TreatmentType = treatmentType;
            ArticleId = articleId;
            ArticleQuantity = articleQuantity;
            SeedingId = seedingId;
        }

        public DateTime Date { get; private set; }

        public ТreatmentType TreatmentType { get; private set; }

        public int ArticleId { get; private set; }

        public Article Article { get; } = default!;

        public decimal ArticleQuantity { get; private set; }

        public int SeedingId { get; private set; }

        public Seeding Seeding { get; } = default!;

        public int TenantId { get; set; }

        #region UpdateMethods
        public Treatment UpdateТreatmentType(ТreatmentType treatmentType)
        {
            ValidateТreatmentType(treatmentType);
            this.TreatmentType = treatmentType;
            return this;
        }

        public Treatment UpdateDate(DateTime date)
        {
            this.Date = date;
            return this;
        }

        public Treatment UpdateArticle(int articleId)
        {
            this.ArticleId = articleId;
            return this;
        }

        public Treatment UpdateArticleQuantity(decimal articleQuantity)
        {
            this.ArticleQuantity = articleQuantity;
            return this;
        }
        #endregion

        private void ValidateТreatmentType(ТreatmentType type)
            => Guard.Guard.ForValidEnum<ТreatmentType>((int)type, nameof(ТreatmentType));
    }
}
