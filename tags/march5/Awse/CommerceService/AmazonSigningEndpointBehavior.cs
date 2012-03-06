namespace Sweng500.Awse.CommerceService
{
    using System.ServiceModel.Channels;
    using System.ServiceModel.Description;
    using System.ServiceModel.Dispatcher;

    /// <summary>
    /// A simple implementation that adds the AmazonSigningMessageInspector 
    /// to the endpoints list of behaviors.
    /// </summary>
    //// TODO: Implement BehaviorExtensionElement to config this with a file
    //// TODO: Implement with SecretString
    public class AmazonSigningEndpointBehavior : IEndpointBehavior
    {
        #region Fields

        /// <summary>
        /// The access key to access Amazon
        /// </summary>
        private string mAccessKeyId = string.Empty;

        /// <summary>
        /// The secret key to access Amazon
        /// </summary>
        private string mSecretKey = string.Empty;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the AmazonSigningEndpointBehavior class
        /// </summary>
        /// <param name="accessKeyId">The Amazon access key id</param>
        /// <param name="secretKey">The Amazon secret key</param>
        public AmazonSigningEndpointBehavior(string accessKeyId, string secretKey)
        {
            this.mAccessKeyId = accessKeyId;
            this.mSecretKey = secretKey;
        }

        #endregion Constructors

        #region IEndpointBehavior

        /// <summary>
        /// Implement to pass data at runtime to bindings to support custom behavior
        /// </summary>
        /// <param name="endpoint">The endpoint to modify</param>
        /// <param name="bindingParameters">The objects that binding elements require to support the behavior</param>
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            // Nothing to do
        }

        /// <summary>
        /// Implements a modification or extension of the client across an endpoint.
        /// In this case, we will add the AmazonSigningMessageInspector to insert our required header
        /// </summary>
        /// <param name="endpoint">The endpoint that is to be customized</param>
        /// <param name="clientRuntime">The client runtime to be customized</param>
        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new AmazonSigningMessageInspector(this.mAccessKeyId, this.mSecretKey));
        }

        /// <summary>
        /// Implements a modification or extension of the service across an endpoint
        /// </summary>
        /// <param name="endpoint">The endpoint that exposes the contract</param>
        /// <param name="endpointDispatcher">The endpoint dispatcher to be modified or extended</param>
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            // Nothing to do
        }

        /// <summary>
        /// Implement to confirm that the endpoint meets some intended criteria
        /// </summary>
        /// <param name="endpoint">The endpoint to validate</param>
        public void Validate(ServiceEndpoint endpoint)
        {
            // Nothing to do
        }

        #endregion IEndpointBehavior
    }
}
