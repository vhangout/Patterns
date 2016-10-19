using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailBuilder.Builders
{
    class MailStateBuilder
    {
        public interface IRecepientMailBuilder
        {
            IMailBuilder AddBody(string body);
        }

        public interface IMailBuilder
        {
            void AddRecepients(List<string> recepients);
            void AddRecepient(string recepient);
            void RemoveRecepients(List<string> recepients);
            void RemoveRecepient(string recepient);
            void SetBody(string body);
            void AddToBody(string text);
            void SetSubject(string subject);
            void AddCopies(List<string> copies);
            void AddCopy(string copy);
            void RemoveCopies(List<string> copies);
            void RemoveCopy(string copy);
            string Build();
        }

        public static IRecepientMailBuilder AddRecepients(List<string> recepients)
        {
            if (recepients.Count == 0)            
                throw new Exception("Recepients can't be empty.");

            return new RecepientMailBuilder(recepients);
        }

        public static IRecepientMailBuilder AddRecepient(string recepient)
        {
            return AddRecepients(String.IsNullOrWhiteSpace(recepient) ? new List<string>() : recepient.Replace(" ", "").Split(',').ToList());
        }

        private class RecepientMailBuilder : IRecepientMailBuilder
        {
            private readonly List<string> _recepients;

            public RecepientMailBuilder(List<string> recepients)
            {
                _recepients = recepients;
            }

            public IMailBuilder AddBody(string body)
            {
                if (String.IsNullOrWhiteSpace(body))
                    throw new Exception("Body isn't empty.");
                return new MailBuilder(_recepients, body);
            }
        }

        private class MailBuilder : IMailBuilder
        {
            private HashSet<string> _recepients = new HashSet<string>();
            private StringBuilder _bodyBuilder = new StringBuilder();
            private HashSet<string> _copies = new HashSet<string>();

            public string Subject { get; private set; }

            public MailBuilder(List<string> recepients, string body)
            {
                _recepients.UnionWith(recepients);
                _bodyBuilder.AppendLine(body);
                Subject = "";
            }

            public void AddRecepients(List<string> recepients)
            {
                if (recepients.Count > 0)
                    _recepients.UnionWith(recepients);
            }

            public void AddRecepient(string recepient)
            {
                if (recepient != null)
                {
                    this.AddRecepients(recepient.Replace(" ", "").Split(',').ToList());
                }
            }

            public void RemoveRecepients(List<string> recepients)
            {
                if (recepients.Count > 0)
                {
                    var tmp = (from r in _recepients
                              where !recepients.Contains(r)
                              select r).ToList();
                    if (tmp.Count > 0)
                    {
                        _recepients.Clear();
                        this.AddRecepients(tmp);
                    }
                    else
                        throw new Exception("Recepients can't be empty.");

                }                   
            }

            public void RemoveRecepient(string recepient)
            {
                this.RemoveRecepients(recepient.Replace(" ", "").Split(',').ToList());
            }

            public void SetBody(string body)
            {
                if (!string.IsNullOrWhiteSpace(body))
                {
                    _bodyBuilder.Clear();
                    _bodyBuilder.Append(body);
                }
                else
                    throw new Exception("Body isn't empty.");
            }

            public void AddToBody(string text)
            {
                if (!string.IsNullOrWhiteSpace(text))
                {
                    _bodyBuilder.AppendLine(text);
                }
            }

            public void SetSubject(string subject)
            {
                Subject = subject != null ? subject : "";
            }

            public void AddCopies(List<string> copies)
            {
                if (copies.Count > 0)
                    _copies.UnionWith(copies);
            }

            public void AddCopy(string copy)
            {
                if (copy != null)
                {
                    this.AddCopies(copy.Replace(" ", "").Split(',').ToList());
                }
            }

            public void RemoveCopies(List<string> copies)
            {
                if (copies.Count > 0)
                {
                    var tmp = (from c in _copies
                               where !copies.Contains(c)
                               select c).ToList();
                    _copies.Clear();
                    _copies.UnionWith(tmp);
                }
            }

            public void RemoveCopy(string copy)
            {
                this.RemoveCopies(copy.Replace(" ", "").Split(',').ToList());
            }

            public string Build()
            {
                var result = new StringBuilder(" _____\n|\\   /|\n| \\_/ |\n|     |\n -----\n");
                result.Append("To: ");
                result.AppendLine(string.Join(", ", _recepients.OrderBy(r => r).ToList()));
                result.Append("CC: ");
                result.AppendLine(string.Join(", ", _copies.OrderBy(r => r).ToList()));
                result.AppendLine("Subject: " + Subject);
                result.AppendLine("Body:");
                result.AppendLine(_bodyBuilder.ToString());
                return result.ToString();
            }

        }
        
    }
}
