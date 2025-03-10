using Xunit;
using Xunit.Sdk;

namespace CarWorkshop.Domain.Entities.Tests
{
    public class CarWorkshopTests
    {
        [Fact()]
        public void EncodeName_SetEncodedName()
        {
            CarWorkshop carWorkshop = new CarWorkshop();
            carWorkshop.Name = "Test Workshop";

            carWorkshop.EncodeName();

            Xunit.Assert.Equal( "test-workshop", carWorkshop.EncodedName);
        }
        [Fact()]
        public void EncodeName_NameIsNull_ThorwException()
        {
            CarWorkshop carWorkshop = new CarWorkshop();

            Action action = () => carWorkshop.EncodeName(); 

            Xunit.Assert.Throws<NullReferenceException>( action);
        }
    }
}