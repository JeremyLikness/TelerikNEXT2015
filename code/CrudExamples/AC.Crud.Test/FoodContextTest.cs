using System.Linq;
using AB.Crud.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AC.Crud.Test
{
    [TestClass]
    public class FoodContextTest
    {
        [TestMethod]
        public void GivenContextWhenDescriptionsAccessedThenDoesNotThrowErrors()
        {
            using (var ctx = new FoodContext())
            {
                var listOfFoodDescriptions = ctx.FoodDescriptions.ToList();
                Assert.IsNotNull(listOfFoodDescriptions);
            }
        }
    }
}
