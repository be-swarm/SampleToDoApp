using BeSwarm.WebApi;

namespace BlazorWasmToDoApp
{
	public class SecureConfig : ISecureConfig
	{
		public string ServiceEntryPoint { get; set; } = "https://dev.beswarm.net";
		public string UserSwarm { get; set; } = "testdev";
		public string ApplicationId { get; set; } = "fc824c2d-5ce9-4f81-8199-4b76186d474d.2f5ad138-40c0-4e4e-9e58-d7703eed6c32.testdev";
		public string ClientSecret { get; set; } = "MySecret";
		public string CallBackUri { get; set; } = "http://localhost:7173/Login";
	}
	
}

