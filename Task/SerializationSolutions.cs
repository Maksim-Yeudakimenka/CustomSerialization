using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task.DB;
using Task.TestHelpers;
using System.Runtime.Serialization;
using System.Linq;
using System.Collections.Generic;

namespace Task
{
    [TestClass]
    public class SerializationSolutions
    {
        private Northwind _dbContext;

        [TestInitialize]
        public void Initialize()
        {
            _dbContext = new Northwind();
        }

        [TestMethod]
        public void SerializationCallbacks()
        {
            _dbContext.Configuration.ProxyCreationEnabled = false;

            var streamingContext = new StreamingContext(StreamingContextStates.All, _dbContext);
            var tester = new XmlDataContractSerializerTester<IEnumerable<Category>>(new NetDataContractSerializer(streamingContext), true);
            var categories = _dbContext.Categories.ToList();

            var c = categories.First();

            tester.SerializeAndDeserialize(categories);
        }

        [TestMethod]
        public void ISerializable()
        {
            _dbContext.Configuration.ProxyCreationEnabled = false;

            var streamingContext = new StreamingContext(StreamingContextStates.All, _dbContext);
            var tester = new XmlDataContractSerializerTester<IEnumerable<Product>>(new NetDataContractSerializer(streamingContext), true);
            var products = _dbContext.Products.ToList();

            tester.SerializeAndDeserialize(products);
        }


        [TestMethod]
        public void ISerializationSurrogate()
        {
            _dbContext.Configuration.ProxyCreationEnabled = false;

            var streamingContext = new StreamingContext(StreamingContextStates.All, _dbContext);
            var orderDetailsSurrogate = new OrderDetailsSerializationSurrogate();

            var surrogateSelector = new SurrogateSelector();
            surrogateSelector.AddSurrogate(typeof(Order_Detail), streamingContext, orderDetailsSurrogate);

            var serializer = new NetDataContractSerializer
            {
                SurrogateSelector = surrogateSelector,
                Context = streamingContext
            };

            var tester = new XmlDataContractSerializerTester<IEnumerable<Order_Detail>>(serializer, true);
            var orderDetails = _dbContext.Order_Details.ToList();

            tester.SerializeAndDeserialize(orderDetails);
        }

        [TestMethod]
        public void IDataContractSurrogate()
        {
            _dbContext.Configuration.ProxyCreationEnabled = true;
            _dbContext.Configuration.LazyLoadingEnabled = true;

            var serializerSettings = new DataContractSerializerSettings
            {
                DataContractSurrogate = new OrdersDataContractSurrogate()
            };

            var serializer = new DataContractSerializer(typeof(IEnumerable<Order>), serializerSettings);

            var tester = new XmlDataContractSerializerTester<IEnumerable<Order>>(serializer, true);
            var orders = _dbContext.Orders.ToList();

            tester.SerializeAndDeserialize(orders);
        }
    }
}