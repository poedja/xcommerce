using System;
using System.Collections.Generic;
using System.Text;

namespace XCommerce.Bot
{
    public class MessageListItem
    {
        public string Text { get; set; }
        public string Sender { get; set; }
        public MessageListItem(string text, string sender)
        {
            Text = text;
            Sender = sender;
        }


    }
}
