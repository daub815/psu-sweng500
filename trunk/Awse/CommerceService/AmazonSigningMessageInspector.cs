namespace Sweng500.Awse.CommerceService
{
    using System.Security.Cryptography;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Dispatcher;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// A message inspector that inserts the required SOAP Header for Amazon to
    /// accept our requests
    /// </summary>
    //// TODO: Replace string with SecretString
    public class AmazonSigningMessageInspector : IClientMessageInspector
    {
        #region Statics

        /// <summary>
        /// Regex to determine the action occuring in the wsdl
        /// </summary>
        /// <example>
        /// http://soap.amazon.com/ItemSearch will result in ItemSearch
        /// </example>
        private static Regex sHeaderRegex = new Regex("[^/]+$", RegexOptions.Compiled);

        #endregion Statics

        #region Fields

        /// <summary>
        /// The access key to access Amazon
        /// </summary>
        private string mAccessKeyId = string.Empty;

        /// <summary>
        /// The secret key to access Amazon
        /// </summary>
        private string mSecretKey = string.Empty;

        /// <summary>
        /// The hashing algorithm
        /// </summary>
        private HMACSHA256 mHashAlgorithm;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the AmazonSigningMessageInspector class
        /// </summary>
        /// <param name="accessKeyId">The Amazon access key id</param>
        /// <param name="secretKey">The Amazon secret key</param>
        public AmazonSigningMessageInspector(string accessKeyId, string secretKey)
        {
            this.mAccessKeyId = accessKeyId;
            this.mSecretKey = secretKey;
            this.mHashAlgorithm = new HMACSHA256(Encoding.UTF8.GetBytes(this.mSecretKey));
        }

        #endregion Constructors

        /// <summary>
        /// Enables inspection or modification of a message after a reply message is
        /// received but prior to passing it back to the client application
        /// </summary>
        /// <param name="reply">The message to be transformed into types and handed back to the client application</param>
        /// <param name="correlationState">Correlation state data</param>
        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            // No need to do anything
        }

        /// <summary>
        /// Enables inspection or modification of a message before a request message is sent to a service.
        /// In this case, we are signing the message, so that Amazon will accept the request.
        /// </summary>
        /// <param name="request">The message to be sent to the service</param>
        /// <param name="channel">The client object channel</param>
        /// <returns>Null is always returned.  See the interface comment for why</returns>
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            // This is used to calulate the signature
            string operation = sHeaderRegex.Match(request.Headers.Action).ToString();

            // Timestamp must be UTC and fresh (less than 15 minutes old)
            string timestamp = Amazon.Util.AWSSDKUtils.FormattedCurrentTimestampISO8601;

            // Create the signature, which is HMAC signed combination of the operation and timestamp
            string signature = Amazon.Util.AWSSDKUtils.HMACSign(operation + timestamp, this.mSecretKey, this.mHashAlgorithm);

            // Add the required headers
            request.Headers.Add(new AmazonHeader("AWSAccessKeyId", this.mAccessKeyId));
            request.Headers.Add(new AmazonHeader("Timestamp", timestamp));
            request.Headers.Add(new AmazonHeader("Signature", signature));

            return null;
        }
    }
}
