using System.Linq;
using AE.Crud.WebApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AE.Crud.WebApi.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class FoodDescriptionsControllerTests
    {
        private FoodDescriptionsController _target;

        [TestInitialize]
        public void Setup()
        {
            _target = new FoodDescriptionsController();
        }

        [TestCleanup]
        public void Teardown()
        {
            _target.Dispose();
            _target = null;
        }

        [TestMethod]
        public void GivenODataWhenTopTenRequestedThenShouldReturnTopTenItems()
        {
            var results = _target.GetFoodDescriptions().Take(10);
            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count(), 10);
        }
    }
}
