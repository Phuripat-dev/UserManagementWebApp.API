namespace UserManagementWebApp.API.Models.DTO
{
    public class PermissionDto
    {
        public Guid? PermissionId { get; set; }
        public string? PermissionName { get; set; } // optional
        public bool IsReadable { get; set; }
        public bool IsWritable { get; set; }
        public bool IsDeletable { get; set; }
    }
}
