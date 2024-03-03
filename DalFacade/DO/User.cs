
namespace DO;

public record User
( 
    int Password,
    int? UserId = null
)
{
    public User() : this(0,null) { }

}

   


