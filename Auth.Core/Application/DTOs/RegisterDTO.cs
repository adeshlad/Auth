﻿namespace Auth.Core.Application.DTOs
{
	public class RegisterDTO
	{
		public string Email { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public string ConfirmPassword { get; set; } = string.Empty;
	}
}
