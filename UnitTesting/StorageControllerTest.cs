using MSA3backend.Controllers;
using MSA3backend.Models;
using MSA3backend.Repositories;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTesting
{
    public class Tests
    {
        private StorageController _storageController;
        private Mock<IStorageRepository> _storageRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _storageRepositoryMock = new Mock<IStorageRepository>();
            _storageController = new StorageController(_storageRepositoryMock.Object);
        }

        private static List<Item> _mockStorage = new List<Item>()
        {
            new Item{Id = "id1", ItemName = "Item1", ItemDescription = "Item1Description", Price = 10, Quantity = 100 },
            new Item{Id = "id2", ItemName = "Item2", ItemDescription = "Item2Description", Price = 20, Quantity = 200 },
            new Item{Id = "id3", ItemName = "Item3", ItemDescription = "Item3Description", Price = 30, Quantity = 300 },
            new Item{Id = "id4", ItemName = "Item4", ItemDescription = "Item4Description", Price = 40, Quantity = 400 }
        };

        [Test]
        public async Task StorageControllerGetStorage()
        {
            // Arrange
            _storageRepositoryMock
                .Setup(x => x.GetStorageAsync())
                .ReturnsAsync(_mockStorage);

            // Act
            var actionResult = await _storageController.GetStorage();
            var firstItem = actionResult.Value[0];
            var secondItem = actionResult.Value[1];

            // Asset
            Assert.IsInstanceOf(typeof(List<Item>), actionResult.Value);
            Assert.AreEqual("id1", firstItem.Id);
            Assert.AreEqual("Item1", firstItem.ItemName);
            Assert.AreEqual("Item1Description", firstItem.ItemDescription);
            Assert.AreEqual(10, firstItem.Price);
            Assert.AreEqual(100, firstItem.Quantity);
            Assert.AreEqual("id2", secondItem.Id);
            Assert.AreEqual("Item2", secondItem.ItemName);
            Assert.AreEqual("Item2Description", secondItem.ItemDescription);
            Assert.AreEqual(20, secondItem.Price);
            Assert.AreEqual(200, secondItem.Quantity);
        }

        [Test]
        public async Task StorageControllerGetItemById()
        {
            // Arrange
            _storageRepositoryMock
                .Setup(x => x.GetItemByIdAsync("id2"))
                .ReturnsAsync(_mockStorage.Find(i => i.Id == "id2"));

            // Act
            var actionResult = await _storageController.GetItemById("id2");

            // Assert
            Assert.IsInstanceOf(typeof(Item), actionResult.Value);
            Assert.AreEqual("Item2", actionResult.Value.ItemName);
            Assert.AreEqual("Item2Description", actionResult.Value.ItemDescription);
            Assert.AreEqual(20, actionResult.Value.Price);
            Assert.AreEqual(200, actionResult.Value.Quantity);

        }

        [Test]
        public async Task StorageControllerCreateItem()
        {
            // Arrange
            var aItem = new Item { Id = "id5", ItemName = "Item5", ItemDescription = "Item5Description", Price = 50, Quantity = 500 };
            _storageRepositoryMock
                .Setup(x => x.CreateItemAsync(It.IsAny<Item>()))
                .ReturnsAsync(aItem);

            // Act
            var newItem = new CreateItem { ItemName = "Item5", ItemDescription = "Item5Description", Price = 50, Quantity = 500 };

            var actionResult = await _storageController.CreateItem(newItem);

            // Assert
            Assert.IsInstanceOf(typeof(Item), actionResult.Value);
            Assert.AreEqual("Item5", actionResult.Value.ItemName);
            Assert.AreEqual("Item5Description", actionResult.Value.ItemDescription);
            Assert.AreEqual(50, actionResult.Value.Price);
            Assert.AreEqual(500, actionResult.Value.Quantity);
        }

        [Test]
        public async Task StorageControllerUpdateItem()
        {
            // Arrange
            var aItem = new Item { Id = "id3", ItemName = "newItem3", ItemDescription = "newItem3Description", Price = 30, Quantity = 300 };
            _storageRepositoryMock
                .Setup(x => x.UpdateItemAsync(It.IsAny<UpdateItem>()))
                .ReturnsAsync(aItem);

            // Act
            var updateItem = new UpdateItem { Id = "id3", ItemName = "newItem3", ItemDescription = "newItem3Description" };
            var actionResult = await _storageController.UpdateItem(updateItem);

            // Assert
            Assert.IsInstanceOf(typeof(Item), actionResult.Value);
            Assert.AreEqual("newItem3", actionResult.Value.ItemName);
            Assert.AreEqual("newItem3Description", actionResult.Value.ItemDescription);
            Assert.AreEqual(30, actionResult.Value.Price);
            Assert.AreEqual(300, actionResult.Value.Quantity);
        }
    }
}