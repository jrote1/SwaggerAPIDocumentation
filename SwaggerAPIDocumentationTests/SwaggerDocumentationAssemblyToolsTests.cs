﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using SwaggerAPIDocumentation;
using SwaggerAPIDocumentation.Implementations;
using SwaggerAPIDocumentation.Interfaces;

namespace SwaggerAPIDocumentationTests
{
	public class SwaggerDocumentationAssemblyToolsTests
	{
		private ISwaggerDocumentationAssemblyTools _swaggerDocumentationAssemblyTools;

		[SetUp]
		public void SetUp()
		{
			_swaggerDocumentationAssemblyTools = new SwaggerDocumentationAssemblyTools();
		}

		[Test]
		public void GetApiControllers_WhereClassInheritsFromBaseController_ReturnsFirstControllerAndSecondControllerAndThirdController()
		{
			var result = _swaggerDocumentationAssemblyTools.GetApiControllerTypes( typeof ( BaseController ) );

			Assert.AreEqual( 4, result.Count );
			Assert.IsTrue( result.Contains( typeof ( FirstController ) ) );
			Assert.IsTrue( result.Contains( typeof ( SecondController ) ) );
			Assert.IsTrue( result.Contains( typeof ( ThirdController ) ) );
			Assert.IsTrue( result.Contains( typeof ( FourthController ) ) );
		}

		[Test]
		public void TypesThatContainApiDocumentationAttribute_WhereClassContainsApiDocumentationAttribute_ReturnsFirstControllerAndThirdController()
		{
			var result = _swaggerDocumentationAssemblyTools.GetTypesThatAreDecoratedWithApiDocumentationAttribute( new List<Type>
			{
				typeof ( FirstController ),
				typeof ( SecondController ),
				typeof ( ThirdController ),
				typeof ( FourthController )
			} );

			Assert.AreEqual( 3, result.Count );
			Assert.IsTrue( result.Contains( typeof ( FirstController ) ) );
			Assert.IsTrue( result.Contains( typeof ( ThirdController ) ) );
			Assert.IsTrue( result.Contains( typeof ( FourthController ) ) );
		}
	}

	public class BaseController : System.Web.Mvc.Controller { }

	public class MyClass { }

	[ApiDocumentation("")]
	public class FirstController : BaseController { }

	public class SecondController : BaseController { }

	public class ThirdController : BaseController
	{
		[ApiDocumentation("")]
		public void MyMethod() { }
	}

	[ApiDocumentation("")]
	public class FourthController : BaseController
	{
		[ApiDocumentation("")]
		public void MyMethod() { }
	}
}