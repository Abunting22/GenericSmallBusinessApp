namespace GenericSmallBusinessApp.Server.AuthenticationAndAuthorization
{
    public class RoleIdentity
    {
        public static string RoleName(int id)
        {
            Dictionary<int, string> roleMap = new()
            {
                {1, "Admin" },
                {2, "Employee" },
                {3, "User" }
            };

            if (roleMap.TryGetValue(id, out string roleName))
            {
                return roleName;
            }
            else
                return "User";
        }
    }
}
