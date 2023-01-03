namespace SIGED_API.Models
{
    public class UserRequest
    {
                  public string Domain { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public static class UserRequestValidate
        {
            public static string Validate(this UserRequest user)
            {
                //if (string.IsNullOrEmpty(user.Domain))
                //{
                //    return "Domain can't be null or empty";
                //}
                //else 
                if (string.IsNullOrEmpty(user.UserName))
                {
                    return "Domain can't be null or empty";
                }
                else if (string.IsNullOrEmpty(user.Password))
                {
                    return "Domain can't be null or empty";
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }

