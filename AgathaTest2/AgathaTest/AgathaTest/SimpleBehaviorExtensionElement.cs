using System;
using System.ServiceModel.Configuration;

namespace AgathaTest
{
	public class SimpleBehaviorExtensionElement : BehaviorExtensionElement
	{
		public override Type BehaviorType
		{
			get { return typeof(ClientMessageInspector); }
		}

		protected override object CreateBehavior()
		{
			return new ClientMessageInspector();
		}
	}
}