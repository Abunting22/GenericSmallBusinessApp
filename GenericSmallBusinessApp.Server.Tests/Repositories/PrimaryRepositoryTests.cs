//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using GenericSmallBusinessApp.Server.Interfaces;

//using Moq;

//namespace GenericSmallBusinessApp.Server.Tests.Repositories
//{
//    public class PrimaryRepositoryTests
//    {
//        [Fact]
//        public async Task Add_Returns_Bool_When_SaveDataCallSuccessfull()
//        {
//            //Arrange
//            var mockBaseRepository = new Mock<IBaseRepository>();
//            mockBaseRepository
//                .Setup(x => x.SaveData(It.IsAny<string>(), It.IsAny<Type>()))
//                .ReturnsAsync(Task<int>());
//            //Act
//            //Assert
//        }
//    }
//}
