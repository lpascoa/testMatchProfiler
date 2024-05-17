//using Bogus;
//using System;
//using TestMatchProfile.Domain.Entities;


//namespace TestMatchProfile.Infrastructure.Shared.Mock
//{
//    public class LegalContractsInsertBogusConfig : Faker<LegalContract>
//    {
//        public LegalContractsInsertBogusConfig()
//        {
//            RuleFor(o => o.IdProcess, f => f.Database.Random.Int());
//            RuleFor(o => o.DescribeLegalEntity, f => f.Lorem.Slug(50));
//            RuleFor(o => o.CreatedProcess, f => f.Date.Past(1));
//            RuleFor(o => o.UpdatedProcess, f => f.Date.Recent(1));
//        }
//    }
//}
