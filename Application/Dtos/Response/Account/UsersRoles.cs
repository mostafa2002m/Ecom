namespace Application.Dtos.Response.Account
{
    public class UsersRoles
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public List<SelectedRolesDto> Roles { get; set; }
    }
}
