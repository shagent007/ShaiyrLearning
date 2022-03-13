namespace Eventator.DataContext.Xml.Factories
{
    public interface IFactory<Entity>
    {
        public Entity Create(string filePath);
    }
}
