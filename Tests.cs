using System.Linq;
using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace Interview
{
    [TestFixture]
    public class Tests
    {        

        private Repository<SingleStoreable> repository;

        private IEnumerable<SingleStoreable> allItems;

        [Test]
        public void RepositoryReturnsIEnumerable()
        {
            //Arrange

            repository = new Repository<SingleStoreable>();

            //Act
            allItems= repository.All();
            
            //Assert
            Assert.IsInstanceOf<IEnumerable<SingleStoreable>>(allItems);
        }

        [Test]
        public void RepositorySavesNewItem_OfTypeSingleStoreable()
        {
            //Arrange

            repository = new Repository<SingleStoreable>();

            SingleStoreable newItem = new SingleStoreable { Id = 1 };

            //Act
            repository.Save(newItem);

            allItems = repository.All();

            //Assert
            
            Assert.IsTrue(((IEnumerable<SingleStoreable>)allItems).Contains(newItem)); 
        }

        [Test]
        public void RepositorySavesNewItem_NoDuplicates()
        {
            //Arrange

            repository = new Repository<SingleStoreable>();

            SingleStoreable newItem = new SingleStoreable { Id = 1 };

            //Act
            repository.Save(newItem);

            SingleStoreable anotherItem = new SingleStoreable { Id = 1 };

            repository.Save(newItem);

            allItems = repository.All();

            //Assert

            Assert.AreEqual(allItems.Count(), 1);
        }

        [Test]
        public void ThrowsExceptionWhenNewItem_IsOfNullType()
        {
            //Arrange

            repository = new Repository<SingleStoreable>();

            SingleStoreable newItem = null;

            //Act //Assert

            var exception = Assert.Throws<ArgumentNullException>(() => repository.Save(newItem));

            Assert.AreEqual("item", exception.ParamName);

        }

        [Test]
        public void ThrowsExceptionWhenItemIdIsNull()
        {
            //Arrange

            repository = new Repository<SingleStoreable>();

            SingleStoreable newItem = new SingleStoreable { Id = null };

            //Act //Assert

            var exception = Assert.Throws<ArgumentException>(() => repository.FindById(newItem.Id));

            Assert.AreEqual("Id must not be null", exception.Message);

        }

        [Test]
        public void RepositoryReturnsItem_UsingFindById()
        {
            //Arrange

            repository = new Repository<SingleStoreable>();

            SingleStoreable actual = new SingleStoreable { Id = 5 };

            //Act
            repository.Save(actual);

            SingleStoreable result = repository.FindById(5);


            //Assert
            Assert.AreEqual(actual, result);
        }

        [Test]
        public void RepositoryDeletesItem()
        {
            //Arrange

            repository = new Repository<SingleStoreable>();

            SingleStoreable newItem = new SingleStoreable { Id = 4};

            repository.Save(newItem);

            //Act

            repository.Delete(4);

            allItems = repository.All();


            //Assert

            Assert.IsFalse(((IEnumerable<SingleStoreable>)allItems).Contains(newItem));

        }

    }
        
}