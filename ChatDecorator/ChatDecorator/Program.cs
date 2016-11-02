using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatDecorator
{
    interface IChat
    {
        string send(string user);
        void receive(string msg);
    }

    class ChatComponent : IChat
    {
        private readonly string _user;
    
        public ChatComponent(string user)
        {
            _user = user;
        }

        public string send(string msg)
        {
            return _user + ":" + msg;
        }

        public void receive(string msg)
        {
            Console.WriteLine(msg);
        }
    }

    class ChatUserDecorator : IChat
    {
        private readonly IChat _chatComponent;

        public ChatUserDecorator(IChat chatComponent)
        {
            _chatComponent = chatComponent;
        }

        public string send(string msg)
        {
            string[] result = _chatComponent.send(msg).Split(':');
            var s = result[0].GetHashCode().ToString("X") + ":" + result[1];
            Console.WriteLine(s + " => ");
            return s;
        }

        public void receive(string msg)
        {
            _chatComponent.receive(" <= " + msg);
        }
    }

    class ChatMessageDecorator : IChat
    {
        private readonly IChat _chatComponent;

        public ChatMessageDecorator(IChat chatComponent)
        {
            _chatComponent = chatComponent;
        }

        private string code(string msg)
        {            
            return"<coded>" + msg + "</coded>";
        }

        private string decode(string msg)
        {
            return msg.Replace("<coded>", "").Replace("</coded>", "");
            
        }

        public string send(string msg)
        {
            string[] result = _chatComponent.send(msg).Split(':');
            var s = result[0] + ":" + code(result[1]);
            Console.WriteLine(s + " =>");
            return s;
        }

        public void receive(string msg)
        {
            _chatComponent.receive(" <= " + decode(msg));
        }
    }

    class Program
    {        
        static void Main(string[] args)
        {
            ChatComponent component = new ChatComponent("Vasia");
            ChatUserDecorator userDecorator = new ChatUserDecorator(component);
            ChatMessageDecorator msgDecorator = new ChatMessageDecorator(component);

            userDecorator.receive(userDecorator.send("test"));
            msgDecorator.receive(msgDecorator.send("test"));

            Console.ReadKey();
        }
    }
}
