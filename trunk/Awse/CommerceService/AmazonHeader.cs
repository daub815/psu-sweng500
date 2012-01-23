namespace Sweng500.Awse.CommerceService
{
    using System.ServiceModel.Channels;
    using System.Xml;

    /// <summary>
    /// Contains the required amazon header information for making requests
    /// </summary>
    public class AmazonHeader : MessageHeader
    {
        #region Statics

        /// <summary>
        /// Security namespace for amazon
        /// </summary>
        public const string AmazonSecurityNamespace = "http://security.amazonaws.com/doc/2007-01-01/";

        #endregion Statics

        #region Fields

        /// <summary>
        /// The backing field for the Name property
        /// </summary>
        private string mName = string.Empty;

        /// <summary>
        /// The backing value for the associated name
        /// </summary>
        private string mValue = string.Empty;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the AmazonHeader class
        /// </summary>
        /// <param name="name">The name of the message header</param>
        /// <param name="value">The value for the message header</param>
        public AmazonHeader(string name, string value)
        {
            this.mName = name;
            this.mValue = value;
        }

        #endregion Constructors

        #region MessageHeader

        /// <summary>
        /// Gets the name of the message header
        /// </summary>
        public override string Name
        {
            get
            {
                return this.mName;
            }
        }

        /// <summary>
        /// Gets the namespace of the message header
        /// </summary>
        public override string Namespace
        {
            get
            {
                return AmazonSecurityNamespace;
            }
        }

        /// <summary>
        /// Called when the header content is serialized using the specified XML writer
        /// </summary>
        /// <param name="writer">An System.Xml.XmlDictionaryWriter</param>
        /// <param name="messageVersion"> Contains information related to the version of SOAP associated with a message and its exchange</param>
        protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
        {
            writer.WriteString(this.mValue);
        }

        #endregion MessageHeader
    }
}
