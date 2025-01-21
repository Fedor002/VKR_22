namespace VKR_Visik.Classes
{
    public class TreeMessage
    {
        public MessageHistory ParentMessage { get; set; }
        public List<MessageHistory> Replies { get; set; }
    }

}
