 

namespace Shared.Exceptions;

public class BasketNotFoundException(string userName) : NotFoundException("ShoppingCart", userName)
{

}
