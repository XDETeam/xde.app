namespace Xde.Software.Kafka
{
	/// <summary>
	/// Kafka broker
	/// </summary>
	/// 
	/// <remarks>
	/// TODO: https://kafka.apache.org/documentation/
	/// Some of these servers form the storage layer, called the brokers.
	/// 
	/// TODO: https://kafka.apache.org/documentation/#brokerconfigs
	/// </remarks>
	public class KafkaBroker
	{
		/// <summary>
		/// Broker ID
		/// </summary>
		/// 
		/// <remarks>
		/// TODO: https://kafka.apache.org/documentation/#brokerconfigs
		/// broker.id: The broker id for this server.If unset, a unique broker
		/// id will be generated.To avoid conflicts between zookeeper generated
		/// broker id's and user configured broker id's, generated broker ids
		/// start from reserved.broker.max.id + 1.
		/// </remarks>
		public int BrokerId { get; set; }

		public string LogDir { get; set;}

		public string ZookeeperConnect { get; set; }
	}
}
