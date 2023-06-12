using System.ComponentModel.DataAnnotations;

namespace ProductCQRS.Api.ViewModels
{
	public class RegisterViewModel
	{
		[Required]
		public string? Username { get; set; }

		[EmailAddress]
		[Required]
		public string? Email { get; set; }

		[Required]
		public string? Password { get; set; }
	}
}
