using System;
using log4net;
using NUnit.Framework;
using Moq;

namespace OpenLogger.Log4Net.Tests.Unit
{
	[TestFixture]
	public class Log4NetLoggerTests
	{
		private Mock<ILog> log;
		private Log4NetLogger logger;

		[SetUp]
		public void SetUp()
		{
			log = new Mock<ILog>();
			logger = new Log4NetLogger(log.Object);
		}

		[Test]
		public void WriteDebug()
		{
			const string message = "message";

			logger.WriteDebug(message);

			log.Verify(l => l.DebugFormat(message));
		}

		[Test]
		public void WriteError()
		{
			const string message = "message";

			logger.WriteError(message);

			log.Verify(l => l.ErrorFormat(message));
		}

		[Test]
		public void WriteInfo()
		{
			const string message = "message";

			logger.WriteInfo(message);

			log.Verify(l => l.InfoFormat(message));
		}

		[Test]
		public void WriteWarning()
		{
			const string message = "message";

			logger.WriteWarning(message);

			log.Verify(l => l.WarnFormat(message));
		}

		[Test]
		public void WriteException()
		{
			var exception = new Exception();

			logger.WriteException(exception);

			log.Verify(l => l.Error("Exception", exception));
		}

		[Test]
		public void Operation()
		{
			const string name = "name";

			using (logger.Operation(this, name))
			{
				log.Verify(l => l.DebugFormat("Entering {0}: {1}", GetType().Name, name));
			}

			log.Verify(l => l.DebugFormat("Exiting Log4NetLoggerTests"));
		}
	}
}