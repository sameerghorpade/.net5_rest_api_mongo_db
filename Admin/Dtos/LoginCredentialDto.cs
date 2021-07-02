namespace Admin.Dtos
{
    public record LoginCredentialDto
    {
        public string UserName { get; init; }
        public string Password { get; init; }
    }
}