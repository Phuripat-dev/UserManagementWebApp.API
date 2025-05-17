namespace UserManagementWebApp.API.Models.DTO
{
    public class DeleteUserResponseDto
    {
        public StatusDto Status { get; set; } = new StatusDto();
        public DeleteUserDataDto Data { get; set; } = new DeleteUserDataDto();
    }

    public class DeleteUserDataDto
    {
        public bool Result { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
