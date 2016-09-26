using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using MEDSEEK.eHealth.ServiceLayer.Common.BusinessServices;
using MEDSEEK.eHealth.Utils.Common;

namespace AgathaTest
{
	/// <summary>
	/// Used to inspect/modify outbound WCF messages before transmission.
	/// </summary>
	internal class ClientMessageInspector : IClientMessageInspector, IEndpointBehavior
	{
		#region IClientMessageInspector Members

		/// <summary>
		/// Enables inspection or modification of a message after a reply message is received but prior to passing it back to the client application.
		/// </summary>
		/// <param name="reply">The message to be transformed into types and handed back to the client application.</param>
		/// <param name="correlationState">Correlation state data.</param>
		public void AfterReceiveReply(ref Message reply, object correlationState)
		{
		}

		/// <summary>
		/// Enables inspection or modification of a message before a request message is sent to a service.
		/// </summary>
		/// <param name="request">The message to be sent to the service.</param>
		/// <param name="channel">The WCF client object channel.</param>
		/// <returns>
		/// The object that is returned as the argument of the <see cref="M:System.ServiceModel.Dispatcher.IClientMessageInspector.AfterReceiveReply(System.ServiceModel.Channels.Message@,System.Object)"/> method.
		/// </returns>
		public object BeforeSendRequest(ref Message request, IClientChannel channel)
		{
			AddCustomHeaders(request);
			return null;
		}

		#endregion

		#region IEndpointBehavior Members

		/// <summary>
		/// Adds the binding parameters.
		/// </summary>
		/// <param name="serviceEndpoint">The service endpoint.</param>
		/// <param name="bindingParameters">The binding parameters.</param>
		public void AddBindingParameters(ServiceEndpoint serviceEndpoint, BindingParameterCollection bindingParameters)
		{
		}

		/// <summary>
		/// Applies the client behavior.
		/// </summary>
		/// <param name="serviceEndpoint">The service endpoint.</param>
		/// <param name="behavior">The behavior.</param>
		public void ApplyClientBehavior(ServiceEndpoint serviceEndpoint, ClientRuntime behavior)
		{
			behavior.MessageInspectors.Add(this);
		}

		/// <summary>
		/// Applies the dispatch behavior.
		/// </summary>
		/// <param name="serviceEndpoint">The service endpoint.</param>
		/// <param name="endpointDispatcher">The endpoint dispatcher.</param>
		public void ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint, EndpointDispatcher endpointDispatcher)
		{
		}

		/// <summary>
		/// Validates the specified service endpoint.
		/// </summary>
		/// <param name="serviceEndpoint">The service endpoint.</param>
		public void Validate(ServiceEndpoint serviceEndpoint)
		{
		}

		#endregion

		/// <summary>
		/// Adds the custom headers.
		/// </summary>
		/// <param name="request">The request.</param>
		private static void AddCustomHeaders(Message request)
		{
			var userContext = new UserContext()
				{
					UserName = "fulcrumadmin",
					SiteSubscriptionId = Guid.Empty
				};
			request.Headers.Add(CreateCustomHeader<UserContext>(BusinessServicesConstants.UserContextHeader, userContext));
		}

		/// <summary>
		/// Creates a custom WCF message header.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name">The name.</param>
		/// <param name="value">The value.</param>
		/// <returns>The created custom message header.</returns>
		private static MessageHeader CreateCustomHeader<T>(string name, T value)
		{
			MessageHeader<T> headerStr = new MessageHeader<T>(value, false, string.Empty, false);
			MessageHeader header = headerStr.GetUntypedHeader(name, string.Empty);
			return header;
		}
	}
}