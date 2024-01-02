
namespace DO;

///
///Enegeneer entity represent an engeneer with all its props
////
public record Engineer
(
    int Id,
    string? Name = null,
    string? Email = null,
    string? Experience = null,
    int Cost = 0
)
{
    public Engineer() : this(0) { }
    //*public Engineer(int id, string? name, string? email, string? experience, int cost)
    //{ 
     //Id = id;
    //Name = name;
    //Email = email;
    //Experience = experience;
    //Cost = cost;
    }
///}

