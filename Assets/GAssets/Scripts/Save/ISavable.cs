namespace Core.Save
{
    public interface ISavable
    {
        public string Save();
        public void Load(string jsonData);
    }
}
