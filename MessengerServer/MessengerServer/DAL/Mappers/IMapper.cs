using System.Data;

namespace MessengerServer.DAL.Mappers
{
    public interface IMapper<out T>
    {
        T ReadItem(IDataReader reader);
    }
}