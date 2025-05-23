﻿namespace UserManagementWebApp.API.Models.Domain
{
    public class Permission
    {
        public Guid PermissionId { get; set; }
        public string PermissionName { get; set; }
        public bool IsReadable { get; set; }
        public bool IsWritable { get; set; }
        public bool IsDeletable { get; set; }

        public Guid UserId { get; set; }         // Foreign key
        public User User { get; set; }           // Navigation property
    }
}
