public interface IRoleRepository
{
    Task<ICollection<Role>> GetAllRoles();
    Task<Role> GetRoleById(long roleId);
    Task<Role> CreateRole(Role role);
    Task<Role> UpdateRole(Role role, long roleId);
    Task<bool> DeleteRole(long roleId);
}
